using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using System.Net.Http; 
using Newtonsoft.Json;
using A70Insurance.Models;
using System.Text.Encodings.Web;

namespace A70Insurance.ClaimHelpers
{

    public class ClaimReadHelper
    {
        private HttpClient http;
        private string url; 

        public ClaimReadHelper(HttpClient Http, string url)
        {
            this.http = Http;
            this.url = url; 

        }

        public async Task<(Claim claim, bool OK, string Message)> ReadClaim(string ClaimId)
        {   

            string message = ""; 

            HttpResponseMessage m = await ReadClaimApi(ClaimId);

            var goodResult = m.IsSuccessStatusCode;

            if (goodResult == false)
            {
                message = "could not complete claim read call -  unexpected result.";
                message += "status code:" + m.StatusCode.ToString() + " was returned. ";

                var status = m.StatusCode.ToString();
                if (status == "NotFound") { message = "Customer not found."; }
                if (status == "ServerError") { message = "Server error."; }
                Claim claim0 = new Claim();

                return (claim0, false, message);
            }


            HttpContent content = m.Content;
            var input = await content.ReadAsStringAsync(); 

            Claim claim = JsonConvert.DeserializeObject<Claim>(input);

            return (claim, true, "Good Claim Call"); 

        }


        private async Task<HttpResponseMessage> ReadClaimApi(String ClaimId)
        {
            var send = Env.url;
            var suffix = "/api/claim/"  ;
              
             
            var url = send + suffix + ClaimId; 


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
                Console.WriteLine("Read Claim: Exception on call: " + ex.Message.ToString());

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
