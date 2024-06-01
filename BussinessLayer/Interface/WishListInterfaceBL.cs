using ModelLayer.WishList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface WishListInterfaceBL
    {
        bool addWishList(WishListRequest request, int Uid);
        bool deleteWishList(int uId, int cartId);
        List<Object> getWishList(int uId);
    }
}
