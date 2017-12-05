using System;

using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.InputMethods;
using IOSWeek1.Services;
using Android.Content;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IOSWeek1.Droid
{
    [Activity (Label = "Droid Week 2", MainLauncher = true, Theme = "@style/LightTheme", Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Keep track of view's objects 
            var movieField = this.FindViewById<TextView>(Resource.Id.movieField);
            var getMovieButton = this.FindViewById<Button>(Resource.Id.getMovieButton);
            var spinner = this.FindViewById<ProgressBar>(Resource.Id.spinner);
            spinner.Visibility = ViewStates.Invisible;

            // Function for button functionality
            OnClickMovieButton(spinner, movieField, getMovieButton);
		}

        private void OnClickMovieButton (ProgressBar spinner, TextView movieField, Button getMovieButton) {
            
            getMovieButton.Click += async (object sender, EventArgs e) => {

                // Initiate spinner to indicate background activity
                // Set button to disabled while processing request
                // Hide device keyboard
                spinner.Visibility = ViewStates.Visible;
                getMovieButton.Enabled = false;
                var manager = (InputMethodManager)this.GetSystemService(InputMethodService);
                manager.HideSoftInputFromWindow(movieField.WindowToken, 0);

                // Use MovieDBService to display 
                MovieDBService server = new MovieDBService();
                List<MovieModel> movies = new List<MovieModel>();
                if (movieField.Text != "" && movieField.Text != null) {
                    movies = await server.GetMovieListByTitleAsync(movieField.Text);
                } 
                var intent = new Intent(this, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(movies));
                this.StartActivity(intent);

                // Dismiss spinner and re-enable button
                spinner.Visibility = ViewStates.Invisible;
                getMovieButton.Enabled = true;
                movieField.Text = "";
            };
        }
	}
}


