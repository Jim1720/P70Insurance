using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A70Insurance.Models
{
    public class StampData
    {
        public string AdjustedClaimId { get; set; }
        public string AdjustingClaimId { get; set; }
        public DateTime DateAdjusted { get; set; } 
        public string AppAdjusting { get; set; }

        public StampData()
        {

        }

        public StampData(string pAdjustedId, string pAdjustingId, DateTime pDateAdjusted,
               string pAppAdjusting)
        {
            this.AdjustedClaimId = pAdjustedId;
            this.AdjustingClaimId = pAdjustingId;
            this.DateAdjusted = pDateAdjusted;
            this.AppAdjusting = pAppAdjusting;

        }
    }
}
