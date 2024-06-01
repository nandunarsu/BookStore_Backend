using Dapper;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class WishListService : WishListInteface
    {
        private readonly DapperContext _context;
        public WishListService(DapperContext context)
        {
            _context = context;
        }
        public bool addWishList(WishList wishList)
        {
            using (var connection = _context.CreateConnection())
            {
               
                var checkQuery = "SELECT COUNT(*) FROM WishList WHERE UserId = @UserId AND BookId = @BookId;";
                var count = connection.ExecuteScalar<int>(checkQuery, new { wishList.UserId, wishList.BookId });

                if (count > 0)
                {
                    
                    throw new InvalidOperationException("The book is already in the wishlist.");
                }
                else
                {
                   
                    var insertQuery = "INSERT INTO WishList (UserId, BookId) VALUES (@UserId, @BookId);";
                    var result = connection.Execute(insertQuery, new { wishList.UserId, wishList.BookId });
                    return result > 0;
                }
            }
        }


        public bool deleteWishList(int uId, int wishListId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "DELETE FROM WishList WHERE UserId = @UserId AND WishListId = @WishListId;";
                var result = connection.Execute(query, new { UserId = uId, WishListId = wishListId });
                return result > 0;
            }
        }

        public List<Object> getWishList(int uId)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT w.WishListId,w.bookId,w.userId,b.* FROM WishList w inner join Books b on w.bookId = b.bookId where w.UserId=@UserId;";
                var wishLists = connection.Query<Object>(query, new { UserId = uId }).ToList();
                return wishLists;
            }
        }
    }
}
