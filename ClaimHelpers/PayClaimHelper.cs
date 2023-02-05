using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A70Insurance.Models; 
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Json; 
using Blazored.SessionStorage;

namespace A70Insurance.ClaimHelpers
{

    public class PayClaimHelper
    {
        private HttpClient http;
        private string url;
        private ISyncSessionStorageService sessionStorage;

        public PayClaimHelper(HttpClient Http, string url, ISyncSessionStorageService ss)
        {
            this.http = Http;
            this.url = url;
            this.sessionStorage = ss;
        }


        public async Task<(bool OK, string Message)> PayClaim(string ClaimId, string Amount, DateTime date) 
        {
            var message = "";

            decimal _amount = decimal.Parse(Amount);
            PayData payData = new PayData(ClaimId, _amount, date);  

            HttpResponseMessage m = await PayClaimApi(payData);

            var goodResult = m.IsSuccessStatusCode;

            if (goodResult == false)
            {
                message = "could not complete pay claim call -  unexpected result.";
                message += "status code:" + m.StatusCode.ToString() + " was returned. ";

                var status = m.StatusCode.ToString();
                if (status == "NotFound") { message = "Claim not found."; }
                if (status == "ServerError") { message = "Server error."; }


                return (false, message);
            }

            return (true, "Good Pay Claim Call");


        }


        private async Task<HttpResponseMessage> PayClaimApi(PayData payData)
        {
            var send = Env.url;
            var uri = send + "/api/PayClaim/";

            HttpContent item = JsonContent.Create<PayData>(payData);

            HttpRequestMessage req = new HttpRequestMessage()
            {

                RequestUri = new Uri(uri),
                Method = new HttpMethod("Put"),
                Content = item

            };


            var token = sessionStorage.GetItem<string>("A65TOKEN");
            req.Headers.Add("A65TOKEN", token);

            // catch server down exception ....

            HttpResponseMessage m = null;

            try
            {
                m = await http.SendAsync(req);

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Pay Claim: exception on call " + ex.Message.ToString());

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
