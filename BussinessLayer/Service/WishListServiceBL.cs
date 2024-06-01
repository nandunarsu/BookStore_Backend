using BussinessLayer.Interface;
using ModelLayer.WishList;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class WishListServiceBL : WishListInterfaceBL
    {
        private readonly WishListInteface _wishlist;

        public WishListServiceBL(WishListInteface wishlist)
        {
            _wishlist = wishlist;
        }
        private WishList mapToEntity(WishListRequest request, int Uid)
        {
            return new WishList
            {
                BookId = request.BookId,
                UserId = Uid
            };
        }

        public bool addWishList(WishListRequest request, int Uid)
        {
            return _wishlist.addWishList(mapToEntity(request, Uid));
        }



        public bool deleteWishList(int uId, int wishListId)
        {
            return _wishlist.deleteWishList(uId, wishListId);
        }

        public List<object> getWishList(int uId)
        {
            return _wishlist.getWishList(uId);
        }
    }
}
