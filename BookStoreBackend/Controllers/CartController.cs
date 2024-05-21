using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Cart;
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
            return Ok(_cart.addCart(request));
        }

        [HttpPut("{cartId}/{quantity}")]
        public IActionResult updateCartquantity(int cartId, int quantity)
        {
            return Ok(_cart.updateCartquantity(cartId, quantity));
        }

        [HttpPut("/Order")]
        public IActionResult updateCartOrder(int cartId, bool isOrdered)
        {
            return Ok(_cart.updateCartOrder(cartId, isOrdered));
        }
        [HttpPatch]
        public IActionResult uncart(int cartId)
        {
            int userId = int.Parse(User.FindFirstValue("userId"));
            return Ok(_cart.unCart(cartId, userId));
        }

        [HttpGet]
        public IActionResult getCartByUserId()
        {
            int id = int.Parse(User.FindFirstValue("userId"));
            List<CartResponse> c = _cart.getByUserId(id);
            return Ok(c);
        }
    }
}
