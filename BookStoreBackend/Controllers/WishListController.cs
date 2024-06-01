using BussinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Response;
using ModelLayer.WishList;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookStoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishListController : ControllerBase
    {
        private readonly WishListInterfaceBL _wishList;

        public WishListController(WishListInterfaceBL wishList)
        {
            _wishList = wishList;
        }
        [HttpPost]
        public IActionResult addWishList(WishListRequest request)
        {
            try
            {

                int uId = int.Parse(User.FindFirstValue("userId"));

                var data = _wishList.addWishList(request, uId);
                var response = new ResponseModel<object>
                {
                    Success = true,
                    Message = "Book Added to Wishlist Successfully",
                    Data = data

                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                var response = new ResponseModel<object>
                {
                    Success = true,
                    Message = "Error occured while adding book to wishlist",
                    Data = ex.Message

                };
                return Ok(response);
            }
        }
        [HttpGet]
        public IActionResult getWishList()
        {
            int uId = int.Parse(User.FindFirstValue("userId"));
            var data = _wishList.getWishList(uId);
            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "books wishlist ",
                Data = data

            };
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult deleteWishList(int wishListId)
        {
            int uId = int.Parse(User.FindFirstValue("userId"));
            var data =_wishList.deleteWishList(uId, wishListId);
            var response = new ResponseModel<object>
            {
                Success = true,
                Message = "book  deleted from wishlist Successfully",
                Data = data

            };
            return Ok(response);
        }
    }
}
