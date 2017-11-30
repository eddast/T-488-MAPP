using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using System;
using IOSWeek1;
using UIKit;
using System.Collections.Generic;
using IOSWeek1.MovieDownload;
using System.Threading;

namespace IOSWeek1.iOS.Controllers
{
    public class TabBarController : UITabBarController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.TabBar.BackgroundColor = UIColor.LightGray;
            this.SelectedIndex = 0;
            this.TabBar.TintColor = UIColor.Green;

            this.ViewControllerSelected += (sender, e) =>
            {
                if (this.SelectedIndex == 1) {
                    this.TabBar.TintColor = UIColor.Orange;
                } else { this.TabBar.TintColor = UIColor.Green;  }
            };
        }
    }
}
