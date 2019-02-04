using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using AMDb.Droid;
using AMDb.Services;
using Newtonsoft.Json;

namespace AMDb.Droid
{
    [Activity(Label = "Movie list", Theme = "@style/LightTheme")]
    public class MovieListActivity : Activity
    {
        private List<MovieModel> _movies;
        private ListView _listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); // call to base func

            // Extract values passed down via Intent
            var jsonMovies = this.Intent.GetStringExtra("movieList");
            this._movies = JsonConvert.DeserializeObject<List<MovieModel>>(jsonMovies);

            // Explicit feedback and empty view if no results were found
            if (_movies == null || _movies.Count == 0) {

                // Feedback provided if no results were found
                Toast.MakeText(ApplicationContext, "No Results Found", ToastLength.Long).Show();
                SetContentView(Resource.Layout.EmptyScreen);
                var noResultsToolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
                this.SetActionBar(noResultsToolbar);
                this.ActionBar.Title = "No Results";
                return;
            }

            // Set content view to XML layout and extract list view
            SetContentView(Resource.Layout.SearchResults);
            _listView = this.FindViewById<ListView>(Resource.Id.listView);

            // Set listview adapter to movie list adapter that builds up movie list
            // Set toolbar as "action bar" and give it appropriate title
            _listView.Adapter = new MovieListAdapter(this, this._movies);
            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = "Movie Search";

            // Listens to cell click and "redirects" user to detail page
            ListenToCellClicks();
        }

        // Listens for cell click and starts the detail movie view activity
        // passes down movie information in json bundle for detail view
        public void ListenToCellClicks()
        {
            this._listView.ItemClick += (sender, args) => {

                var intent = new Intent(this, typeof(MovieDetailActivity));
                intent.PutExtra("movie", JsonConvert.SerializeObject(_movies[args.Position]));
                this.StartActivity(intent);
            };
        }

    }
}