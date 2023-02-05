using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Newtonsoft.Json;
using System.Net.Http;
using A70Insurance.Models;
using System.Net.Security;

namespace A70Insurance.ClaimHelpers
{
    public class ServiceHelpers
    {

        private HttpClient http;
        private string url;
        private bool areServicesLoaded = false;
        private List<ServiceEntry> serviceEntries;

        public ServiceHelpers(HttpClient Http, string url)
        {
            this.http = Http;
            this.url = url;
            serviceEntries = new List<ServiceEntry>();
        }

        public decimal GetCostForService(string ClaimType, string ServiceName)
        {
            // used in calc to find cost of service
            // depricated.
            decimal cost = 0;

            foreach(var s in serviceEntries)
            {
                if (s.ClaimType == ClaimType && s.ServiceName.Trim() == ServiceName.Trim())
                    cost = s.Cost;
            }
            return cost;

        }

        public async Task<List<ServiceEntry>> GetCompleteList()
        {

            var r = await ReadServices(); 
            serviceEntries = r.list; 
            return serviceEntries; 

        }
 
        public async Task<List<string>> GetServiceNamesForClaimType(string ClaimType)
        {
            // used to fill dropdown by claim type
            // depricated..
            if(!areServicesLoaded)
            { 
                var r =  await ReadServices(); 
                if(r.OK)
                { 
                    areServicesLoaded = true;
                    serviceEntries = r.list;
                }

            }
            

           // if (serviceEntries.Count == 0) { Console.WriteLine("no service entries"); }

           

            // serviceEntires has full list
            // serviceNames has subset by type.

            List<string> serviceNames = new List<string>();
            foreach(var s in this.serviceEntries)
            { 
                if (s.ClaimType == ClaimType)
                { 
                    serviceNames.Add(s.ServiceName.ToString().Trim());
                }
            } 
            //Console.WriteLine("returning..sh bye bye");
            return serviceNames;
        }


        public async Task<(List<ServiceEntry> list, bool OK, string Message)> ReadServices()
        {
            // load plan data into list

            var send = Env.url;
            var suffix = "/api/readServices";
            var url = send + suffix;

            string message = "";

            List<ServiceEntry> services = new List<ServiceEntry>();

            HttpResponseMessage m = await ReadServicesApi(url);

            var goodResult = m.IsSuccessStatusCode;

            if (goodResult == false)
            {
                message = "could not complete service read call -  unexpected result.";
                message += "status code:" + m.StatusCode.ToString() + " was returned. ";

                var status = m.StatusCode.ToString();
                if (status == "NotFound") { message = "Customer not found."; }
                if (status == "ServerError") { message = "Server error."; }


                return (services, false, message);
            }


            HttpContent content = m.Content;
            var input = await content.ReadAsStringAsync(); 

            services = JsonConvert.DeserializeObject<List<ServiceEntry>>(input);

            return (services, true, "Good Services Call"); 
        }


        private async Task<HttpResponseMessage> ReadServicesApi(string url)
        {
            //string message = "";

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
                Console.WriteLine("Read Services: Exception on call: " + ex.Message.ToString());

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
