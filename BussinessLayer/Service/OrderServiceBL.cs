using BussinessLayer.Interface;
using ModelLayer.Order;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class OrderServiceBL : IOrderBL
    {
        private readonly IOrder _order;
        public OrderServiceBL(IOrder order)
        {
            _order = order;
        }
        public List<Object> GetOrder(int userId)
        {
            return _order.GetOrder(userId);
        }
        public int AddOrder(OrderRequest request, int userId)
        {
            return _order.AddOrder(request, userId);
        }
    }
}
