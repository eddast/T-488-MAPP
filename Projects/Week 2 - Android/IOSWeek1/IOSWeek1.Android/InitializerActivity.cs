using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;


namespace IOSWeek1.Droid
{
    [Activity(Label = "Movie Inspector", Theme = "@style/LightTheme.Splash", MainLauncher = true, Icon = "@drawable/icon")]
    public class InitializerActivity : Activity
    {
            protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                this.StartActivity(typeof(MainActivity));
                this.Finish();
            }

    }
}
