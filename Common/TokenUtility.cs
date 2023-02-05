using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace A70Insurance.Common
{
    public static class TokenUtility
    { 
        static public string GetToken(HttpResponseMessage m)
        {
            // save anti forgery token for future api calls...


            string token = "";
            foreach (var header in m.Headers)
            {
                if (header.Key == "Set-Cookie")
                {
                    token = "";
                    foreach (var s in header.Value)
                    {
                        token += s;
                    }
                };
            };

            return token;

        }

    }
}