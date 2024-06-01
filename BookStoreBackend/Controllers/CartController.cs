using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Cart;
using ModelLayer.Response;
using RepositoryLayer.Entity;
using System.Security.Claims;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly CartInterfaceBL _cart;
        public CartController(CartInterfaceBL cart)
        {
            _cart = cart;
        }
        [HttpPost]
        public IActionResult addCart(CartRequest request)
        {
            int uId = int.Parse(User.FindFirstValue("userId"));
            request.userId = uId;
            var data = _cart.addCart(request);
            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "cart Item deleted Successfully",
                Data = data

            };
            return Ok(response);
        }

        [HttpPut("{cartId}/{quantity}")]
        public IActionResult updateCartquantity(int cartId, int quantity)
        {
            var data = _cart.updateCartquantity(cartId, quantity);
            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "cart quantity updated Successfully",
                Data = data

            };
            return Ok(response);
        }

        [HttpPut("/Order")]
        public IActionResult updateCartOrder(int cartId, bool isOrdered)
        {
            var data = _cart.updateCartOrder(cartId, isOrdered);
            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "cart Item Updated Successfully",
                Data = data

            };
            return Ok(response);
        }
        [HttpPatch]
        public IActionResult uncart(int cartId)
        {
            int userId = int.Parse(User.FindFirstValue("userId"));
            var data = _cart.unCart(cartId, userId);
            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "cart Item uncarted Successfully",
                Data = data

            };
            return Ok(response);
        }

        [HttpGet]
        public IActionResult getCartByUserId()
        {
            int id = int.Parse(User.FindFirstValue("userId"));
            List<CartResponse> data = _cart.getByUserId(id);
            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "cart items :",
                Data = data

            };
            return Ok(response);
        }
        [HttpDelete]
        public IActionResult deleteCart(int cartId)
        {
            try
            {
                int userId = int.Parse(User.FindFirstValue("userId"));
                var deleteditem = _cart.DeleteCart(userId, cartId);
                var response = new ResponseModel<bool>
                {
                    Success = true,
                    Message = "cart Item deleted Successfully",
                    Data = deleteditem

                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                var response = new ResponseModel<bool>
                {
                    Success = false,
                    Message = "error while deleting the cart item",
                    Data = ex.Message
                };
                return BadRequest(response);
            }
        }
    }
}
