using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using IOSWeek1.Services;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;

namespace IOSWeek1.Droid
{
    [Activity (Label = "Movie Search", Theme = "@style/LightTheme")]
    public class MovieSearchFragment : Fragment
	{
        private MovieDBService _server;
        public InputMethodManager manager;
        public TextView movieField;

        public MovieSearchFragment(){}
        public MovieSearchFragment(MovieDBService server)
        {
            this._server = server;
            manager = null;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.MovieSearch, container, false);

            // Optimize and keep track of view's objects 
            movieField = rootView.FindViewById<TextView>(Resource.Id.movieField);
            var heading = rootView.FindViewById<TextView>(Resource.Id.heading);
            heading.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
            var getMovieButton = rootView.FindViewById<Button>(Resource.Id.getMovieButton);
            var spinner = rootView.FindViewById<ProgressBar>(Resource.Id.spinner);
            spinner.Visibility = ViewStates.Invisible;
            manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);

            // Listens for clicks on "get movies" button in view
            OnClickMovieButton(spinner, movieField, getMovieButton);


            return rootView;
		}

        private void OnClickMovieButton (ProgressBar spinner, TextView movieField, Button getMovieButton)
        {
            getMovieButton.Click += async (object sender, EventArgs e) => {

                if(movieField.Text == "" || movieField.Text == null) {

                    // Error message provided if no results were found
                    Toast.MakeText(Context, "Please Provide A Query String", ToastLength.Long).Show();
                    return;
                }

                // Initiate spinner to indicate background activity
                // Set button to disabled while processing request
                spinner.Visibility = ViewStates.Visible;
                getMovieButton.Enabled = false;
                manager.HideSoftInputFromWindow(movieField.WindowToken, 0);

                // Get list of movies by query string user provided
                List<MovieModel> movies = new List<MovieModel>();
                movies = await _server.GetMovieListByTitleAsync(movieField.Text);

                // On click, start the movie result list activity
                // Pass movie list found via json bundle
                var intent = new Intent(this.Context, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(movies));
                intent.PutExtra("movieDBservice", JsonConvert.SerializeObject(_server));
                this.StartActivity(intent);

                // Dismiss spinner and re-enable button
                spinner.Visibility = ViewStates.Invisible;
                getMovieButton.Enabled = true; movieField.Text = "";
            };
        }
	}
}


