using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Blazored.SessionStorage;

namespace A70Insurance
{
    public interface IActionOperations
    {
        public void setAction(string action, string claimId, ISyncSessionStorageService ss);

        public (string literal, string claimId) getActionInfo(int i, ISyncSessionStorageService ss);

    }


    public class ActionOperations : IActionOperations
    {
         

        List<ActionObject> actionList;

        public ActionOperations()
        {
        }
         

        public ActionOperations(ISyncSessionStorageService ss)
        { 

            actionList = new List<ActionObject>(); 
        }

        public void setAction(string action, string claimId, ISyncSessionStorageService sessionStorage)
        {
             

            var actionObject = new ActionObject(action, claimId);
            // keep the last two addtions only - two buttons on the screen.

            actionList = sessionStorage.GetItem<List<ActionObject>>("ActionList") as List<ActionObject>;

            if(actionList == null)
            {
                actionList = new List<ActionObject>();
            }
 
            var count = actionList.Count();
            switch(count)
            { 
                case 0: actionList.Add(actionObject); break;
                case 1: actionList.Add(actionObject); break;
                case 2: actionList.RemoveAt(0);
                        actionList.Add(actionObject);
                        break;
                default: break;

            }

            sessionStorage.SetItem("ActionList", actionList);
        }

        public (string literal, string claimId) getActionInfo(int i , ISyncSessionStorageService sessionStorage)
        {

            actionList = sessionStorage.GetItem<List<ActionObject>>("ActionList") as List<ActionObject>;
            if(actionList == null)
            {
                return ("", "");
            }
            int count = actionList.Count();
            if(i >= count)
            {
                return ("", "");
            }

            ActionObject a = actionList[i]; 

            string claimId = a.claimId; 
            var action = a.action;
             
            var dash = "-";
            var suffix = claimId.Substring(claimId.Length - 2);
            suffix = suffix.Replace(":", "");
            string literal = action.Substring(0, 3) + dash + suffix;
            return (literal, claimId); 
        } 
    }

    class ActionObject
    {
        public string action { get;  }
        public string claimId { get;  }

        public ActionObject(string action, string claimId)
        {

            this.action = action;
            this.claimId = claimId;
                
        }

    }
}
