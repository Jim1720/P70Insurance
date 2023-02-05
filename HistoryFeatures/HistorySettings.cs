using Blazored.SessionStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A70Insurance
{

    public interface IHistorySettings
    {

        public bool isStayOn(ISyncSessionStorageService ss);

        public void setStay(bool value, ISyncSessionStorageService ss);

        public bool isFocusOn(ISyncSessionStorageService ss);

        public void setFocus(bool value, ISyncSessionStorageService ss);
    }

    public class HistorySettings : IHistorySettings
    { 
         

        public  HistorySettings()
        {

        }

       

        public  bool isStayOn(ISyncSessionStorageService sessionStorage) 
        { 

            return sessionStorage.GetItem<bool>("stay");
        }

        public  void setStay(bool value, ISyncSessionStorageService sessionStorage)
        {
            
            sessionStorage.SetItem("stay", value);
        }

        public  bool isFocusOn(ISyncSessionStorageService sessionStorage)
        { 
            bool value =  sessionStorage.GetItem<bool>("focus");
            return value;
        } 

        public  void setFocus(bool value, ISyncSessionStorageService sessionStorage)
        { 
            sessionStorage.SetItem("focus", value);
        }



    }
}
