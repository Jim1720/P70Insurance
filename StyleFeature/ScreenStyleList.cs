﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace A70Insurance.StyleFeature
{

    public interface IScreenStyleList
    {

        public void add(ScreenStyleObject screenStyleObject);
        public void replace(ScreenStyleObject screenStyleObject); 
        public ScreenStyleObject Find(String screenName);
        public List<ScreenStyleObject> getScreenStyleList();
        public void setScreenStyleList(List<ScreenStyleObject> list);

        public Boolean isEmpty();

        public void dump();

    }
    public  class ScreenStyleList : IScreenStyleList
    {

        private  List<ScreenStyleObject> screenStyleList;

       
        public ScreenStyleList()
        {

            screenStyleList = new List<ScreenStyleObject>();

        } 
        public  void add(ScreenStyleObject screenStyleObject)
        {
            screenStyleList.Add(screenStyleObject);
        }
        public  void replace(ScreenStyleObject screenStyleObject)
        {
    
            var index = screenStyleList.FindIndex(s => s.screen == screenStyleObject.screen);
            screenStyleList.RemoveAt(index);
            screenStyleList.Insert(index, screenStyleObject); 

        }

        public  ScreenStyleObject Find(String screenName)
        {
            return screenStyleList.Find(s => s.screen == screenName);
        }

        /* persistent operations - main controller responsible */
 
        public List<ScreenStyleObject> getScreenStyleList()
        {
            return screenStyleList;
        }

        public void setScreenStyleList(List<ScreenStyleObject> list)
        {
            this.screenStyleList = list;
        }

        public bool isEmpty()
        {
            return this.screenStyleList.Count == 0;
        }

        public void dump()
        {
            int i = 0;
            foreach(var sso in screenStyleList)
            {
                i = i + 1;
                Console.WriteLine("");
                Console.WriteLine(i.ToString());
                Console.WriteLine("------------------------------");
                var ic = sso.internalClass;
                var ec = sso.externalClass;
                var uc = sso.userColor;
                Console.WriteLine(ic);
                Console.WriteLine(ec);
                Console.WriteLine(uc);
            }
        }
      
     
    }
}
