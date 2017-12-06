using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using IOSWeek1.Droid;
using IOSWeek1.Services;
using Newtonsoft.Json;

namespace IOSWeek1.Droid
{
    [Activity(Label = "Movie list", Theme = "@style/LightTheme")]
    public class MovieListActivity : ListActivity
    {
        private List<MovieModel> _movies;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Extract values passed down via Intent
            var jsonMovies = this.Intent.GetStringExtra("movieList");
            this._movies = JsonConvert.DeserializeObject<List<MovieModel>>(jsonMovies);
            if (_movies.Count == 0) { Toast.MakeText(ApplicationContext, "No results", ToastLength.Long).Show(); }

            this.ListAdapter = new MovieListAdapter(this, this._movies);

            this.ListView.ItemClick += (sender, args) => {
                
                var intent = new Intent(this, typeof(MovieDetailActivity));
                intent.PutExtra("movie", JsonConvert.SerializeObject(_movies[args.Position]));
                this.StartActivity(intent);
            };
        }

    }
}