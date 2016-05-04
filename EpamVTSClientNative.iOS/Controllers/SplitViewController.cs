using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class SplitViewContoller : UISplitViewController
    {
        LoginPageViewController masterView;

        public SplitViewContoller() : base()
        {
            PreferredDisplayMode = UISplitViewControllerDisplayMode.AllVisible;
            // create our master and detail views
            masterView = new LoginPageViewController();

            // create an array of controllers from them and then assign it to the 
            // controllers property
            ViewControllers = new UIViewController[]
                { masterView, masterView}; // order is important: master first, detail second



            
        }
    }
}
