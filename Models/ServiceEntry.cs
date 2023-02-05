using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A70Insurance.Models
{
    public class ServiceEntry
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ClaimType { get; set; }
        public string ClaimTypeLiteral { get; set; }
        public decimal Cost { get; set; } 
    }
}


 