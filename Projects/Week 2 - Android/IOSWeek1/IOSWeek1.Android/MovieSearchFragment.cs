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
using Fragment = Android.Support.V4.App.Fragment;

namespace IOSWeek1.Droid
{
    [Activity (Label = "Movie Search", Theme = "@style/LightTheme")]
    public class MovieSearchFragment : Fragment
	{
        private MovieDBService _server;

        public MovieSearchFragment(MovieDBService server) { this._server = server; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle bundle)
        {
            var rootView = inflater.Inflate(Resource.Layout.MovieSearch, container, false);

            // Keep track of view's objects 
            var movieField = rootView.FindViewById<TextView>(Resource.Id.movieField);
            var getMovieButton = rootView.FindViewById<Button>(Resource.Id.getMovieButton);
            var spinner = rootView.FindViewById<ProgressBar>(Resource.Id.spinner);
            spinner.Visibility = ViewStates.Invisible;

            // Function for button functionality
            OnClickMovieButton(spinner, movieField, getMovieButton);

            return rootView;
		}

        private void OnClickMovieButton (ProgressBar spinner, TextView movieField, Button getMovieButton) {
            
            getMovieButton.Click += async (object sender, EventArgs e) => {

                // Initiate spinner to indicate background activity
                // Set button to disabled while processing request
                // Hide device keyboard
                spinner.Visibility = ViewStates.Visible;
                getMovieButton.Enabled = false;
                var manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);
                manager.HideSoftInputFromWindow(movieField.WindowToken, 0);

                // Use MovieDBService to display 
                List<MovieModel> movies = new List<MovieModel>();
                if (movieField.Text != "" && movieField.Text != null) {
                    movies = await _server.GetMovieListByTitleAsync(movieField.Text);
                } 
                var intent = new Intent(this.Context, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(movies));
                intent.PutExtra("movieDBservice", JsonConvert.SerializeObject(_server));
                this.StartActivity(intent);

                // Dismiss spinner and re-enable button
                spinner.Visibility = ViewStates.Invisible;
                getMovieButton.Enabled = true;
                movieField.Text = "";
            };
        }
	}
}


