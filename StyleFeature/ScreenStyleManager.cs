using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A70Insurance.Models;

namespace A70Insurance.StyleFeature
{

    public interface IScreenStyleManager
    {
        public bool AuthorizeStyles(String ifscreen);
        public bool AreStylesActive(String ifscreen); 
    }

    public  class ScreenStyleManager : IScreenStyleManager
    {

        List<String> activeScreenStyleList; 

        public ScreenStyleManager()
        {
            activeScreenStyleList = new List<String>
            {
                 "update",
                 "claim"
            };
        }

        public  bool AuthorizeStyles(String mScreen)
        {

            String usingStyles = Env.usingStyles.ToUpper();
            if(usingStyles != "Y")
            {
                return false;
            } 
            return activeScreenStyleList.Contains(mScreen);

        }

        public  bool AreStylesActive(String mScreen)
        {
             
            if (!Env.AreStylesUsed())
            {
                return false;
            }
            return activeScreenStyleList.Contains(mScreen);

        }
    }
}
