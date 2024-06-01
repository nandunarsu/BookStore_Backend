using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Address;
using ModelLayer.Response;
using RepositoryLayer.Entity;
using System.Security.Claims;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL _address;
        public AddressController( IAddressBL address)
        {
            _address = address;
        }
        
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
        [HttpGet("GetAddress")]
        public ResponseStructure<List<Object>> GetAddress()
        {
            int userId = int.Parse(User.FindFirstValue("userId"));
            var cartBooks = _address.GetAddress(userId);
            return new ResponseStructure<List<Object>>(true, "Retrieved Address successfully", cartBooks);
        }
        [HttpPut("UpdateAddress")]
        public ResponseStructure<Address> UpdateAddress([FromBody] Address addressRequest)
        {
            int userId = int.Parse(User.FindFirstValue("userId"));
            var updatedCartRequest = _address.UpdateAddress(userId, addressRequest);
            return new ResponseStructure<Address>(true, "Updateded address successfully", updatedCartRequest);
        }
        [HttpDelete("DeleteAddress")]
        public ResponseStructure<bool> DeleteAddress(int addressId)
        {
            int userId = int.Parse(User.FindFirstValue("userId"));
            var isDeleted = _address.DeleteAddress(addressId);
            return new ResponseStructure<bool>(true, "Deleteded from Address successfully", true);
        }
    }
}
