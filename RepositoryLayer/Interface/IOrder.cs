using ModelLayer.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IOrder
    {
       public List<Object> GetOrder(int userId);
       public int AddOrder(OrderRequest request, int userId);
    }
}
