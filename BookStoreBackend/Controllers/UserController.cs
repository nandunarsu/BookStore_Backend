using BussinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Response;
using ModelLayer.UserRegistration;
using RepositoryLayer.Interface;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserInterfaceBL _user;

        public UserController(UserInterfaceBL user)
        {
            _user = user;
        }
        [HttpPost]
        public async Task<IActionResult> UserRegistration(UserRegistrationModel user)
        {
            try
            {
                await _user.RegisterUser(user);
               
                var response = new ResponseModel<string>
                {
                    Success = true,
                    Message = "User Registered Successfully"

                };
                return Ok(response);
            }
            catch (Exception ex)
            {
               
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
                return Ok(response);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel userLogin)
        {
            try
            {

                // Authenticate the user and generate JWT token
                var Token = await _user.UserLogin(userLogin);
                //Console.WriteLine(Token);
                // return Ok(Token);
                var response = new ResponseModel<string>
                {
                    Success = true,
                    Message = "User Login Successfully",
                    Data = Token

                };
                return Ok(response);

            }
            catch (Exception ex)
            {
               
                var response = new ResponseModel<string>
                {
                    Success = false,
                    Message = ex.Message,

                };

                return BadRequest(ex);
            }
        }
    }
}
