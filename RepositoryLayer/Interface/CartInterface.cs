using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Cart;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface CartInterface
    {
        int addCart(Cart cart);
        List<CartResponse> getByUserId(int id);
        bool unCart(int cartId, int userId);
        bool updateCartOrder(int cartId, bool isOrdered);
        bool updateCartquantity(int cartId, int quantity);

        public bool DeleteCart(int userId, int cartId);
    }
}
