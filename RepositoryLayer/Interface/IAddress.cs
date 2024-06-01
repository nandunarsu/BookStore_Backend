﻿using ModelLayer.Address;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddress
    {
        public string AddAddress(AddressRequest addresrequest, int userId);
        public Address UpdateAddress(int userId, Address request);
        public bool DeleteAddress(int AddressId);
        public List<Object> GetAddress(int userId);
    }
}
