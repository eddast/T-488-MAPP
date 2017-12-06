using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using IOSWeek1.Services;
using Newtonsoft.Json;

namespace IOSWeek1.Droid
{
    [Activity(Label = "Movie Inspector", Theme = "@style/LightTheme.Splash", MainLauncher = true, Icon = "@drawable/icon")]
    public class InitializerActivity : Activity
    {
        // Initializes service model of the external API
        // Bundles it and passes it into main activity (the toolbar setup)
        // Then launches application and terminates itself
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); // call base initially

            var intent = new Intent(this, typeof(MainActivity));
            MovieDBService server = new MovieDBService();
            intent.PutExtra("movieDBservice", JsonConvert.SerializeObject(server));

            this.StartActivity(intent); this.Finish();
        }
    }
}
