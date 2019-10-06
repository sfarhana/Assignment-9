using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApplication.Model
{
    public class Customer
    {
        public int CustId{get; set;}
        public string Code{get; set;}
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public int DId { get; set; }
        public string District { get; set; }

    }
}
