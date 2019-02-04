using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AMDb.Services;
using Newtonsoft.Json;
using Fragment = Android.Support.V4.App.Fragment;

namespace AMDb.Droid
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
            var infoButton = rootView.FindViewById<ImageButton>(Resource.Id.infoButton);
            var spinner = rootView.FindViewById<ProgressBar>(Resource.Id.spinner);
            spinner.Visibility = ViewStates.Invisible;
            manager = (InputMethodManager)this.Context.GetSystemService(Context.InputMethodService);

            // Listens for clicks on buttons in view
            OnClickMovieButton(spinner, movieField, getMovieButton);
            OnClickInfoButton(infoButton);


            return rootView;
		}

        // Listenes for clicks on "get movie" button and displays results in another activity
        private void OnClickMovieButton (ProgressBar spinner, TextView queryString, Button getMovieButton)
        {
            getMovieButton.Click += async (object sender, EventArgs e) => {

                if(queryString.Text == "" || queryString.Text == null) {

                    // Error message provided if no results were found
                    Toast.MakeText(Context, "Please Provide A Query String", ToastLength.Long).Show();
                    return;
                }

                // Initiate spinner to indicate background activity
                // Set button to disabled while processing request
                spinner.Visibility = ViewStates.Visible;
                getMovieButton.Enabled = false;
                manager.HideSoftInputFromWindow(queryString.WindowToken, 0);

                // Get list of movies by query string user provided
                List<MovieModel> movies = new List<MovieModel>();
                movies = await _server.GetMovieListByTitleAsync(queryString.Text);

                // On click, start the movie result list activity
                // Pass movie list found via json bundle
                var intent = new Intent(this.Context, typeof(MovieListActivity));
                intent.PutExtra("movieList", JsonConvert.SerializeObject(movies));
                intent.PutExtra("movieDBservice", JsonConvert.SerializeObject(_server));
                this.StartActivity(intent);

                // Dismiss spinner and re-enable button
                spinner.Visibility = ViewStates.Invisible;
                getMovieButton.Enabled = true; queryString.Text = "";
            };
        }

        // Listenes for click on the information button and if clicked, displays information dialog
        private void OnClickInfoButton(ImageButton infoButton)
        {
            infoButton.Click += (object sender, EventArgs e) => {
                
                AlertDialog.Builder info = new AlertDialog.Builder(this.Context);
                info.SetTitle("Welcome to AMDb!");

                info.SetMessage("This is how you use your favorite on-the-go movie database the App Movie DataBase!" +
                                "\n\n" +
                                "Browsing for your favorite movies:\n" +
                                "Simply click on the input box and input a substring of some movie title and click the button below to search for it. " +
                                "You will then be directed to another page displaying the search results after results have been retrieved." +
                                "\n\n" +
                                "Discovering new amazing movies:\n" +
                                "To explore top rated movies navigate to the next tab by sliding right or clicking on the tab on the right in above tool bar. " +
                                "Each time you navigate to the top rated tab the top rated movie list is reloaded from the database to keep you instantly updated with the best movies! " +
                                "Once the top movie list has been retrieved they will be displayed in a list in which each movie can be clicked on for details.");
                
                var OKbutton = info.SetPositiveButton("OK, got it!", (senderAlert, args) => { });
                Dialog dialog = info.Create();
                dialog.Show();
            };
        }
	}
}


