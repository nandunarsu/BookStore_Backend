using BussinessLayer.Interface;
using ModelLayer.Cart;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class CartServiceBL:CartInterfaceBL
    {
        private readonly CartInterface _cart;
        public CartServiceBL(CartInterface cart)
        {
            _cart = cart;
        }
        private Cart MapToEntity(CartRequest request)
        {
            return new Cart
            {
                bookId = request.bookId,
                quantity = request.quantity,
                userId = request.userId
            };
        }

        public int addCart(CartRequest request)
        {
            bool flag = true;
            List<CartResponse> li = _cart.getByUserId(request.userId);
            if (li == null || !li.Any())
            {
                return _cart.addCart(MapToEntity(request));
            }

            foreach (var item in li)
            {


                if (item.BookId == request.bookId)
                {
                    if (item.IsOrdered)
                    {
                        flag = true;
                        break;
                    }
                    else if (item.isUnCarted)
                    {
                        flag = true;
                        break;
                    }
                    else
                        flag = false;

                }


            }

            if (flag)
                return _cart.addCart(MapToEntity(request));
            else
                return 0;

        }
        public List<CartResponse> getByUserId(int id)
        {
            return _cart.getByUserId(id);
        }

        public bool updateCartOrder(int cartId, bool isOrdered)
        {
            return _cart.updateCartOrder(cartId, isOrdered);
        }

        public bool updateCartquantity(int cartId, int quantity)
        {
            return _cart.updateCartquantity(cartId, quantity);
        }

        public bool unCart(int cartId, int userId)
        {
            return _cart.unCart(cartId, userId);
        }
        public bool DeleteCart(int userId, int cartId)
        {
            return _cart.DeleteCart(userId, cartId);
        }

    }
}
