using BussinessLayer.Interface;
using ModelLayer.Address;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Service
{
    public class AddressServiceBL : IAddressBL
    {
        private readonly IAddress _address;
        public AddressServiceBL(IAddress address)
        {
            _address = address;
        }

        public string AddAddress(AddressRequest address, int userId)
        {
            return _address.AddAddress(address, userId);
        }
    }
}
