using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;

namespace IOSWeek1.Droid
{
    public class MovieListAdapter : BaseAdapter<MovieModel>
    {
        private readonly Activity _context;
        private readonly List<MovieModel> _movies;

        public MovieListAdapter(Activity context, List<MovieModel> movies)
        {
            this._context = context;
            this._movies = movies;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // Reuse existing view if available
            // Otherwise create new
            var view = convertView;
            if (view == null) {
                view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieItem, null);
            }

            // Set list cell to movie in position of movie list
            // Format text and image view according to movie information
            MovieModel movie = this._movies[position];
            string movieAndYear = movie.movie.Title.ToUpper() + " (" + movie.movie.ReleaseDate.Year.ToString() + ")";
            var movieYearText = view.FindViewById<TextView>(Resource.Id.name);
            movieYearText.Text = movieAndYear;
            movieYearText.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
            var movieCast = view.FindViewById<TextView>(Resource.Id.cast).Text = movie.cast;
            movieYearText.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
            ImageView posterImage = view.FindViewById<ImageView>(Resource.Id.posterView);
            GlideImageIntoImageView(movie.posterPath, posterImage);


            return view;
        }

        // Uses glide to fetch URI asynchroniously when view loads
        private void GlideImageIntoImageView(string imagePathURI, ImageView imageView)
        {
            if (imagePathURI != "" && imagePathURI != null) {

                Glide.With(_context).Load("http://image.tmdb.org/t/p/original" + imagePathURI).Into(imageView);
            }
        }

        // Override necessary functions for a working fragment pager adapter
        public override long GetItemId(int position) { return position; }
        public override int Count => this._movies.Count;
        public override MovieModel this[int position] => this._movies[position];
    }
}