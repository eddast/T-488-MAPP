using Android.App;
using Android.Content;
using Android.OS;
using AMDb.Services;
using Newtonsoft.Json;

namespace AMDb.Droid
{
    [Activity(Label = "AMDb", Theme = "@style/LightTheme.Splash", MainLauncher = true, Icon = "@drawable/icon")]
    public class InitializerActivity : Activity
    {
        // Initializes service model of the external API
        // Field-injects it into main activity (the toolbar setup)
        // Then launches application and terminates itself
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); // call base initially
            ToolbarFragmentActivity.server = new MovieDBService();
            var intent = new Intent(this, typeof(ToolbarFragmentActivity));
            this.StartActivity(intent); this.Finish();
        }
    }
}
