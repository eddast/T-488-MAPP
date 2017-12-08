using Android.App;
using Android.Content;
using Android.OS;
using IOSWeek1.Services;
using Newtonsoft.Json;

namespace IOSWeek1.Droid
{
    [Activity(Label = "AMDB", Theme = "@style/LightTheme.Splash", MainLauncher = true, Icon = "@drawable/icon")]
    public class InitializerActivity : Activity
    {
        // Initializes service model of the external API
        // Bundles it and passes it into main activity (the toolbar setup)
        // Then launches application and terminates itself
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); // call base initially

            // Field injection of dependency (movieDB server) into main activiy
            MainActivity.server = new MovieDBService();
            var intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent); this.Finish();
        }
    }
}
