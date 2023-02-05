using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Net.Http;
using A70Insurance.Models;
using System.Net.Http.Json; 
using Blazored.SessionStorage;

namespace A70Insurance.ClaimHelpers
{
    public class StampAdjustementHelper
    {
        private HttpClient http;
        private string url;
        private ISyncSessionStorageService sessionStorage;

        public StampAdjustementHelper(HttpClient Http, string url, ISyncSessionStorageService ss )
        {
            this.http = Http;
            this.url = url;
            this.sessionStorage = ss;
        }
         

        public async Task<(bool OK, string Message)> StampAdjustedClaim(StampData stampData)
        {
            var message = ""; 

            HttpResponseMessage m = await StampAdjustedClaimApi(stampData);

            var goodResult = m.IsSuccessStatusCode;

            if (goodResult == false)
            {
                message = "could not complete stamp adjustment call -  unexpected result.";
                message += "status code:" + m.StatusCode.ToString() + " was returned. ";

                var status = m.StatusCode.ToString();
                if (status == "NotFound") { message = "Customer not found."; }
                if (status == "ServerError") { message = "Server error."; }


                return (false, message);
            } 

            return (true, "Good Adjusted Claim Stamp  Call");


        }


        private async Task<HttpResponseMessage> StampAdjustedClaimApi(StampData stampData)
        {
            var send = Env.url;
            var suffix = "/api/StampAdjustedClaim/";
            var url = send + suffix;


            HttpContent item = JsonContent.Create<StampData>(stampData);

            HttpRequestMessage req = new HttpRequestMessage()
            {

                RequestUri = new Uri(url),
                Method = new HttpMethod("Put"),
                Content = item

            };

            var token = sessionStorage.GetItem<string>("A65TOKEN");
            req.Headers.Add("A65TOKEN", token);

            //string json = JsonConvert.SerializeObject(stampData);
            //var content = new StringContent(json,
            //    System.Text.Encoding.UTF8,
            //    "application/json");

            // catch server down exception ....

            HttpResponseMessage m = null;

            try
            {
                m = await http.SendAsync(req);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Stamp Adjusted Claim: exception on call " + ex.Message.ToString());

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
