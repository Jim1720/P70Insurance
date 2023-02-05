using A70Insurance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace A70Insurance.ClaimHelpers
{
    static class CostHelperClass
    {
         

        public async static Task<(decimal cost, decimal covered, decimal balance)>

               CalculateCostValues(string plan, string service, string claimType, HttpClient http, string url,
               ServiceHelpers sh,
               PlanHelpers ph)
        {

            // find percent from current customer plan.
            // (the plan list may be held in core after first call.  
            decimal percent = await ph.GetPlanPercentCovered(plan);


            // from service loaded before claim displayed 
            // find the service and cost of that service.
            // (this may be held in core after first call)  
            decimal cost =  sh.GetCostForService(claimType, service);

            decimal covered = cost * percent / 100;
            decimal balance = cost - covered;
            return (cost, covered, balance);

        }
    }
}
