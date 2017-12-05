using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Com.Bumptech.Glide;
using Newtonsoft.Json;

namespace IOSWeek1.Droid
{
    [Activity(Label = "Droid Week 2", MainLauncher = true, Theme = "@style/LightTheme", Icon = "@drawable/icon")]
    public class MovieDetailActivity : Activity
    {
        MovieModel _movie;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.MovieDetail);

            var jsonStr = this.Intent.GetStringExtra("movie");
            _movie = JsonConvert.DeserializeObject<MovieModel>(jsonStr);

            this.FindViewById<TextView>(Resource.Id.title).Text = _movie.title.ToUpper() + " (" + _movie.year + ")";
            this.FindViewById<TextView>(Resource.Id.details).Text = _movie.runtime + " mins | " + _movie.genres;
            this.FindViewById<TextView>(Resource.Id.overview).Text = _movie.movie.Overview;


            ImageView posterImage = this.FindViewById<ImageView>(Resource.Id.poster);
            ImageView backdropImage = this.FindViewById<ImageView>(Resource.Id.backdrop);

            var ppath = _movie.posterPath;
            if (ppath != "" && ppath != null) {
                posterImage.SetImageResource(0);
                Glide.With(this).Load("http://image.tmdb.org/t/p/original" + ppath).Into(posterImage);
            }

            var bpath = _movie.backdropPath;
            if (bpath != "" && bpath != null) {
                backdropImage.SetImageResource(0);
                Glide.With(this).Load("http://image.tmdb.org/t/p/original" + bpath).Into(backdropImage);
            }

        }

    }
}