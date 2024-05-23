using ModelLayer.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddress
    {
        string AddAddress(AddressRequest addresrequest, int userId);
    }
}
