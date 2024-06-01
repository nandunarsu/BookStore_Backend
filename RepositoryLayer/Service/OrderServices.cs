using Dapper;
using ModelLayer.Order;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class OrderServices : IOrder
    {
        private readonly DapperContext _context;
        public OrderServices(DapperContext context)
        {
            _context = context;
        }
        public int AddOrder(OrderRequest request, int userId)
        {
            Console.WriteLine(request.bookId + " " + request.orderDate);
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@addressId", request.addressId);
                parameters.Add("@orderDate", request.orderDate.ToString("yyyy-MM-dd"));
                parameters.Add("@bookId", request.bookId);
                parameters.Add("@userId", userId);
                Console.WriteLine(request.addressId + " " + request.bookId + " " + FormatDate(request.orderDate));
                using (var connection = _context.CreateConnection())
                {
                   int data =  connection.Execute("spAddOrder", parameters, commandType: CommandType.StoredProcedure);

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        static string FormatDate(DateTime input)
        {
            return input.ToString("MMMM d");
        }
        public List<object> GetOrder(int userId)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var query = @"
                SELECT b.BookImage, b.BookName, b.Quantity, b.AuthorName, b.Price, o.OrderId, o.OrderDate
                FROM Books b
                INNER JOIN Orders o ON  o.UserId = @UserId";
                

                    var orders = connection.Query<object>(query, new { UserId = userId });
                    return orders.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }


    }
}
