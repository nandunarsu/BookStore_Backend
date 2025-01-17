﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Address
    {
        public int addressId { get; set; }
        public string name { get; set; }
        public long mobileNumber { get; set; }
        public String address { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public string type { get; set; }
        public int userId { get; set; }
    }
}
