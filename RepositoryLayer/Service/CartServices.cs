using Dapper;
using ModelLayer.Cart;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CartServices: CartInterface
    {      private readonly DapperContext context;
        public CartServices(DapperContext _context) {
            context = _context;
        }
        public int addCart(Cart cart)
        {
            IDbConnection con = context.CreateConnection();
            string insertQuery = @"INSERT INTO Cart (Quantity, UserId, BookId, isOrdered,isUnCarted) 
                                       VALUES (@Quantity, @UserId, @BookId, @isOrdered,@isUnCarted);select SCOPE_IDENTITY()";

            int nora = con.QueryFirst<int>(insertQuery, cart);
            return nora;
        }
        public List<CartResponse> getByUserId(int id)
        {
            IDbConnection con = context.CreateConnection();
            string query = @"select c.* ,b.BookName,b.BookImage,b.Description,b.AuthorName,b.Quantity as QuantityAvailable,b.Price
                            from Books as b Inner Join Cart as c
                            on b.bookid=c.bookid
                            where c.userid= @UserId";
            return con.Query<CartResponse>(query, new { UserId = id }).ToList();
        }

        public bool unCart(int cartId, int userId)
        {
            IDbConnection con = context.CreateConnection();
            string query = @"UPDATE Cart SET isUnCarted = @isUnCarted WHERE cartId = @CartId";
            int rowsAffected = con.Execute(query, new { CartId = cartId, isUnCarted = true });
            return rowsAffected > 0;
        }

        public bool updateCartOrder(int cartId, bool isOrdered)
        {
            IDbConnection con = context.CreateConnection();
            string query = @"UPDATE Cart SET isOrdered = @IsOrdered WHERE cartId = @CartId";
            int rowsAffected = con.Execute(query, new { CartId = cartId, IsOrdered = isOrdered });
            return rowsAffected > 0;
        }

        public bool updateCartquantity(int cartId, int quantity)
        {
            IDbConnection con = context.CreateConnection();
            string query = @"UPDATE Cart SET quantity = @Quantity WHERE cartId = @CartId";
            int rowsAffected = con.Execute(query, new { CartId = cartId, Quantity = quantity });
            return rowsAffected > 0;
        }
        public bool DeleteCart(int userId, int cartId)
        {
            try
            {
                using (var connection = context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@UserId", userId);
                    parameters.Add("@CartId", cartId);
                    if (IsCartItemExists(userId, cartId))
                    {
                        connection.Execute("spDeleteCartItem", parameters, commandType: CommandType.StoredProcedure);

                    }
                    return true;
                }
            }catch(SqlException ex)
            {
                throw ex;
            }
        }
        private bool IsCartItemExists(int userId, int cartId)
        {
            string query = "SELECT COUNT(*) FROM Cart WHERE UserId = @UserId AND CartId = @CartId";
            using (var connection = context.CreateConnection())
            {
                var count = connection.ExecuteScalar<int>(query, new { UserId = userId, CartId = cartId });
                return count > 0;
            }
        }
    }
}
