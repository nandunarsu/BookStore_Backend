using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Order;
using ModelLayer.Response;
using System.Security.Claims;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL _order;
        public OrderController(IOrderBL order)
        {
            _order = order;
        }
        [HttpPost]
        public ResponseStructure<Object> AddOrder([FromBody] OrderRequest requestDto)
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue("userId"));
                Console.WriteLine("User id is:",userId);
                var result = _order.AddOrder(requestDto, userId);
                if (result != null)
                    return new ResponseStructure<Object>(true, "Order added successfully", result);
                else
                    return new ResponseStructure<Object>(false, "Failed to add Order.");
            }
            catch (Exception ex)
            {
                return new ResponseStructure<Object>(false, $"Error: {ex.Message}");
            }
        }
        [HttpGet("GetOrder")]
        public ResponseStructure<List<Object>> GetOrder()
        {
            int userId = int.Parse(User.FindFirstValue("userId"));
            var cartBooks = _order.GetOrder(userId);
            return new ResponseStructure<List<Object>>(true, "Retrieved order successfully", cartBooks);
        }
    }
}
