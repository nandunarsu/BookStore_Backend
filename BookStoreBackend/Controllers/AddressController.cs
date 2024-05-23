using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Address;
using ModelLayer.Response;
using System.Security.Claims;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL _address;
        public AddressController( IAddressBL address)
        {
            _address = address;
        }
        [Authorize]
        [HttpPost]
        public ResponseStructure<Object> AddAddress([FromBody] AddressRequest requestDto)
        {
            try
            {

                int userId = int.Parse(User.FindFirstValue("userId"));
                var result = _address.AddAddress(requestDto, userId);
                Console.WriteLine(result);
                if (result != null)
                    return new ResponseStructure<Object>(true, "Address added successfully");
                else
                    return new ResponseStructure<Object>(false, "Failed to add Address.");
            }
            catch (Exception ex)
            {
                return new ResponseStructure<object>(false, $"Error: {ex.Message}");
            }
        }
    }
}
