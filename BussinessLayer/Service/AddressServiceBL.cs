using BussinessLayer.Interface;
using ModelLayer.Address;
using RepositoryLayer.Entity;
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
        public Address UpdateAddress(int userId, Address request)
        {
            return _address.UpdateAddress(userId, request);
        }
        public bool DeleteAddress(int AddressId)
        {
            return _address.DeleteAddress(AddressId);
        }
        public List<Object> GetAddress(int userId)
        {
            return _address.GetAddress(userId);
        }
    }
}
