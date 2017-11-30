using System;
using UIKit;

namespace IOSWeek1.iOS.Controllers
{
    public class TabBarController : UITabBarController
    {
        public TabBarController() {
            
            public override void ViewDidLoad()
        {
                base.ViewDidLoad();
            this.TabBar.BackgroundColor = UIColor.LightGray;
            this.TabBar.TintColor = UIColor.Red;
            this.SelectedIndex = 0;
        }
           
        }
    }
}
