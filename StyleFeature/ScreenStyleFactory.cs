using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace A70Insurance.StyleFeature
{ 
    public interface IScreenStyleFactory
    {
          
         public void getNextStyle(String ifscreenName);
         public void getNextColor(String ifscreenName); 
         public ScreenStyleObject getCurrentStyleForScreen(String ifscreenName); 
    }

    public class ScreenStyleFactory : IScreenStyleFactory
    {
        // Dependency Injection Chain - add the ScreenStyleMananager and the ScreenStyleList
         
        public readonly IScreenStyleList _screenStyleList;

        public ScreenStyleFactory(IScreenStyleList screenStyleList) 
        { 

            _screenStyleList = screenStyleList; 
        } 
         

        List<String> userColors = new List<String>
        {
            "white", "red" , "pink","blue","aqua","yellow","green","lawngreen","gold","goldenrod"
        };

        List<String> labelColors = new List<String>
        {
             "black" , "white","red","white","blue","black","white","black","brown","brown"
        };

        List<String> headerColors = new List<String>
        {
            "black" , "white", "red","white","blue","black","white","black","brown","brown"
        };

        List<String> messageColors = new List<String>
        { 
            "black" , "white", "red","white","blue","black","white","black","brown","brown"
        };

        List<String> internalClasses = new List<String>
        {
            "Style", "Solid", "Picture",  "Outline" 
        };

        List<String> externalClasses = new List<String>
        {
            "Style","bg-solid", "bg-image",  "bg-outline" 
        };

        String defaultHeaderColor = "burleywood";
        String defaultLabelColor = "dodgerblue";
        String defaultMessageColor = "burleywood";
        String defaultLabelColorForPictureStyle = "white";
        String defaultPictureMessageColor = "white";
        String defaultPictureHeaderColor = "white";

        private void AddNewStyleObject(String screenName)
        {
            ScreenStyleObject screenStyleObject = new ScreenStyleObject()
            {

                // add cycle #2 = picture.

                screen = screenName,
                internalClass = "Solid",
                externalClass = "bg-solid",
                userColor = "white",
                labelColor = "black",
                headerColor = "black",
                messageColor = "black" 
            };

            _screenStyleList.add(screenStyleObject);

        }

     

        public void getNextStyle(String screenName)
        { 
            var screenStyleObject = _screenStyleList.Find(screenName);
            var situation = screenStyleObject == null ? "New" : "Existing";
            Console.Write("get next style - screen name: " + screenName);
            if(situation == "New")
            {
                AddNewStyleObject(screenName);
                Console.Write("adding to list:" + screenName);
                // _screenStyleList.dump();
                return;
            }
            var max = this.internalClasses.Count;
            var currentInternalClass = screenStyleObject.internalClass;
            var match = false;
            for(var i = 0; !match && i < max; i++)
            {
                if (currentInternalClass == this.internalClasses[i])
                {
                    var endOfList = i == max - 1;
                    var next = endOfList ? 0 : i + 1;
                    screenStyleObject.internalClass = this.internalClasses[next]; 
                    screenStyleObject.externalClass = this.externalClasses[next]; 
                    // assign initial colors
                    var first = 0;
                    screenStyleObject.userColor = userColors[first];
                    if (screenStyleObject.internalClass == "Solid")
                    {
                        screenStyleObject.headerColor = headerColors[first];
                        screenStyleObject.labelColor = labelColors[first];
                        screenStyleObject.messageColor = messageColors[first];
                    }
                    else if(screenStyleObject.internalClass == "Picture")
                    {
                        screenStyleObject.headerColor = defaultPictureHeaderColor;
                        screenStyleObject.labelColor = defaultLabelColorForPictureStyle;
                        screenStyleObject.messageColor = defaultPictureMessageColor;
                    }
                    else if (screenStyleObject.internalClass == "Outline")
                    { 
                        screenStyleObject.headerColor = "goldenrod";
                        screenStyleObject.labelColor = "dodgerblue";
                        screenStyleObject.messageColor = "goldenrod"; ;
                    }
                    else
                    { 
                        screenStyleObject.headerColor = defaultHeaderColor;
                        screenStyleObject.labelColor = defaultLabelColor;
                        screenStyleObject.messageColor = defaultMessageColor;
                    } 

                    match = true;
                }
            } 

            _screenStyleList.replace(screenStyleObject);

           

        }

        public void getNextColor(string screenName)
        {

            var screenStyleObject = _screenStyleList.Find(screenName);
            var max = userColors.Count;
            var match = false;
            for(var i = 0; match == false && i < max; i++)
            {

               if(screenStyleObject.userColor == userColors[i])
               {

                    match = true;
                    var endOfList = i == max - 1;
                    var next = endOfList ? 0 : i + 1;
                    screenStyleObject.userColor = userColors[next];
                    if(screenStyleObject.internalClass == "Solid")
                    {
                        screenStyleObject.headerColor = headerColors[next];
                        screenStyleObject.labelColor = labelColors[next];
                        screenStyleObject.messageColor = messageColors[next];
                    }
                    else
                    {
                        screenStyleObject.headerColor = defaultHeaderColor;
                        screenStyleObject.labelColor = defaultLabelColor; 
                        screenStyleObject.messageColor = defaultMessageColor;
                    }

                    _screenStyleList.replace(screenStyleObject);

                    Console.Write("updating list:" + screenName);
                    // _screenStyleList.dump();

                } 
            }
           
        }

        public ScreenStyleObject getCurrentStyleForScreen(String screenName)
        {
             
            var screenStyleObject = _screenStyleList.Find(screenName);
            return screenStyleObject;


        }

      
    }
}
