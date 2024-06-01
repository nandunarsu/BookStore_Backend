using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface WishListInteface
    {
        bool addWishList(WishList wishList);
        bool deleteWishList(int uId, int cartId);
        List<Object> getWishList(int uId);
    }
}
