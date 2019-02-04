using UIKit;

namespace AMDb.iOS.Controllers
{
    public class TabBarController : UITabBarController
    {
        public override void ViewDidLoad()
        {
            // Intialize screen tab view
            base.ViewDidLoad();
            this.TabBar.BackgroundColor = UIColor.LightGray;
            this.SelectedIndex = 0;
            this.TabBar.TintColor = UIColor.Green;

            // Function to switch tab icon colors between views on click
            this.ViewControllerSelected += (sender, e) => {
                
                UIColor SearchColor = UIColor.Green;
                UIColor TopRatedColor = UIColor.Orange;

                if (this.SelectedIndex == 0) this.TabBar.TintColor = SearchColor;
                else if (this.SelectedIndex == 1) this.TabBar.TintColor = TopRatedColor;
            };
        }
    }
}
