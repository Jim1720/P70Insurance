using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.SessionStorage;
 


namespace A70Insurance.Models
{
    public static class Env
    {
        const string yes = "Y";

        // set up url for each screen  
        public static string url { get; set; }

        public static string ePattern { get; set; }

        public static string usingStyles { get; set; }

        public static bool usingStay { get; set; }

        public static bool usingFocus { get; set; }

        public static bool usingNav { get; set; }

        public static bool  usingActions { get; set; }

        public static Boolean AreStylesUsed()
        {
            var positive = usingStyles.ToUpper() == "Y";
            return positive;
        }
         
    }
}
