using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Com.Bumptech.Glide;
using Newtonsoft.Json;

namespace IOSWeek1.Droid
{
    [Activity(Label = "Movie Information", Theme = "@style/LightTheme")]
    public class MovieDetailActivity : Activity
    {
        private MovieModel _movie;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set toolbar title for view and dependent XML file
            SetContentView(Resource.Layout.MovieDetail);
            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar); this.ActionBar.Title = "Movie Information";

            // Unbundle json bundle passed down from "parent" activity
            var jsonMovie = this.Intent.GetStringExtra("movie");
            _movie = JsonConvert.DeserializeObject<MovieModel>(jsonMovie);

            // Format detail view information text views based on context's movie model
            // Format view's images according to movie model's URI
            var movieTitle = this.FindViewById<TextView>(Resource.Id.title);
            movieTitle.Text = _movie.title.ToUpper() + " (" + _movie.year + ")";
            movieTitle.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
            this.FindViewById<RatingBar>(Resource.Id.ratingBar).Rating = (int)_movie.vote_rate;
            this.FindViewById<TextView>(Resource.Id.details).Text = _movie.runtime + " mins" + " | " + _movie.genres;
            this.FindViewById<TextView>(Resource.Id.overview).Text = _movie.movie.Overview;
            ImageView posterImage = this.FindViewById<ImageView>(Resource.Id.poster);
            ImageView backdropImage = this.FindViewById<ImageView>(Resource.Id.backdrop);
            GlideImageIntoImageView(_movie.posterPath, posterImage);
            GlideImageIntoImageView(_movie.backdropPath, backdropImage);
        }

        // Uses glide to fetch URI asynchroniously when view loads
        private void GlideImageIntoImageView(string imagePathURI, ImageView imageView) {
            
            if (imagePathURI != "" && imagePathURI != null){
                
                Glide.With(this).Load("http://image.tmdb.org/t/p/original" + imagePathURI).Into(imageView);
            }
        }
    }
}