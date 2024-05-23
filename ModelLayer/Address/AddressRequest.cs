using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Address
{
    public class AddressRequest
    {
        public string name { get; set; }
        public long mobileNumber { get; set; }
        public String address { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public string type { get; set; }
    }
}
