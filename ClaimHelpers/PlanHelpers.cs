using A70Insurance.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace A70Insurance.ClaimHelpers
{
    public class PlanHelpers
    {
        private HttpClient http;
        private string url;
        // if load fails - list empty and cost returns 0.
        private List<PlanEntry> planList;
        private bool plansLoaded = false;

        public PlanHelpers(HttpClient Http, string url)
        {
            this.http = Http;
            this.url = url;
            planList = new List<PlanEntry>();
        }

        public async Task<decimal> GetPlanPercentCovered(string PlanName)
        {
            decimal value = 0;
            decimal amountCovered = 0; 

            // build list first time.
            if(plansLoaded == false)
            {
                plansLoaded = true;
                var r = await ReadPlans();
                planList = r.PlanList;
            }

            // find cost in list of plans.
            foreach(var p in planList)
            {
                if (p.PlanName.Trim() == PlanName)
                {
                    if( decimal.TryParse(p.Percent, out value))
                    {
                        amountCovered = value;
                    } 
                }
            }
            return amountCovered;

        }

        public async Task<(List<PlanEntry> PlanList, bool OK, string Message)> ReadPlans()
        {

            string message = "";

            List<PlanEntry> plans = new List<PlanEntry>(); 

            HttpResponseMessage m = await ReadPlansApi();

            var goodResult = m.IsSuccessStatusCode;

            if (goodResult == false)
            {
                message = "could not complete plan read call -  unexpected result.";
                message += "status code:" + m.StatusCode.ToString() + " was returned. ";

                var status = m.StatusCode.ToString();
                if (status == "NotFound") { message = "Customer not found."; }
                if (status == "ServerError") { message = "Server error."; }


                return (plans, false, message);
            }


            HttpContent content = m.Content;
            var input = await content.ReadAsStringAsync();
            //Console.WriteLine(input);

            plans = JsonConvert.DeserializeObject<List<PlanEntry>>(input);

            return (plans, true, "Good Plan Call"); 

        }


        private async Task<HttpResponseMessage> ReadPlansApi()
        {

            // load plan data into list

            var send = Env.url;
            var suffix = "/api/readPlans";
            var url = send + suffix;

            HttpRequestMessage req = new HttpRequestMessage()
            {

                RequestUri = new Uri(url),
                Method = new HttpMethod("Get")

            };

            // catch server down exception ....

            HttpResponseMessage m = null;

            try
            {
                m = await http.SendAsync(req);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Read Plans: Exception on call: " + ex.Message.ToString());

                //StateHasChanged();

               /* message = "could not complete plan read unexpected result.";
                message += "status code:" + m.StatusCode.ToString() + " was returned. ";

                var status = m.StatusCode.ToString();
                if (status == "NotFound") { message = "Plans not found."; }
                if (status == "ServerError") { message = "Server error."; } */ 

            }

            return m;


        }

    }
}
