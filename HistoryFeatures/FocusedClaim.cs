using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.SessionStorage;

 
namespace A70Insurance
{
    public interface IFocusedClaim
    { 

        public void setFocusedClaim(string claimId, ISyncSessionStorageService ss);
        public string getFocusedClaim(ISyncSessionStorageService ss);  

    }
    public class FocusedClaim : IFocusedClaim
    {  

        public FocusedClaim()
        {

        }
         
         
        public void setFocusedClaim(string claimId, ISyncSessionStorageService sessionStorage)
        { 
            sessionStorage.SetItem("focusedClaimId", claimId);
        }

        public string getFocusedClaim(ISyncSessionStorageService sessionStorage)
        {

            String focusedId =  sessionStorage.GetItem<String>("focusedClaimId") as string;
            if(focusedId == null)
            {
                focusedId = ""; 
            }
            return focusedId;
        }


    }
}
