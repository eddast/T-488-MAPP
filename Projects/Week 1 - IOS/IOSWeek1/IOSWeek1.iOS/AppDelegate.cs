using Foundation;
using UIKit;
using IOSWeek1.iOS.Controllers;

namespace IOSWeek1.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.

    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);

            // Create a search controller for movie search view
            // And its own navigation controller stack
            var searchController = new IOSMainViewController();
            var searchNavigationController = new UINavigationController(searchController);

            // Create another controller for top rated movies view
            // And its own navigation controller stack
            var topMoviesController = new TopMoviesController();
            var topMoviesNavigationController = new UINavigationController(topMoviesController);

            // A tab bar is created, then set as the root controller
            // containing the two controllers
            var tabBarController = new TabBarController() {
                
                ViewControllers = new UIViewController[] { searchNavigationController, topMoviesNavigationController }
            };

            this.Window.RootViewController = tabBarController;
            this.Window.MakeKeyAndVisible();


            return true;
        }

        // AppDelegate standard functions unused in this project
        public override void OnResignActivation(UIApplication application) { }
        public override void DidEnterBackground(UIApplication application) { }
        public override void WillEnterForeground(UIApplication application) { }
        public override void OnActivated(UIApplication application) { }
        public override void WillTerminate(UIApplication application) { }
    }
}



