using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;

namespace HelloWorld.Droid
{
	[Activity (Label = "HelloWorld", Theme = "@style/MyTheme", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		//int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it

            var nameText = this.FindViewById<EditText>(Resource.Id.nameText);
            var button = this.FindViewById<Button>(Resource.Id.greetingButton);
            var greetingText = this.FindViewById<TextView>(Resource.Id.greetingText);

            button.Click += (object sender, EventArgs e) => {
                var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
                manager.HideSoftInputFromWindow(nameText.WindowToken, 0);
                greetingText.Text = "Hello, " + nameText.Text;
            };
		}
	}
}


