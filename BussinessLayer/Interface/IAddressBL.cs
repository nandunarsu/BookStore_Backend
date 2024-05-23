using ModelLayer.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
    public interface IAddressBL
    {
        string AddAddress(AddressRequest addresrequest, int userId);
    }
}
