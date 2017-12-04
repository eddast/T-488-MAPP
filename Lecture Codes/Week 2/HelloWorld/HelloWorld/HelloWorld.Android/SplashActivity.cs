using System.Text;
using Android.Graphics.Drawables;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.Content.Res.Resources;

namespace HelloWorld.Droid
{
    [Activity(Label = "HelloWorld", Theme="@style/MyTheme.Splash", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashActivity : Activity {
        
        protected override void OnCreate(Bundle savedInstanceState) {
            
            base.OnCreate(savedInstanceState);
            this.StartActivity(typeof(MainActivity));
            this.Finish();
        }
    }
}
