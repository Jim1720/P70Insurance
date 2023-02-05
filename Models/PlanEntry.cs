using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A70Insurance.Models
{
    
 
    public partial class PlanEntry
    {
        public int Id { get; set; }
        public string PlanName { get; set; }
        public string PlanLiteral { get; set; }
        public string Percent { get; set; }
    }
}