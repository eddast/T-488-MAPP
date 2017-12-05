using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using IOSWeek1;

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

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // re-use an existing view, if one is available
            var view = convertView;

            if (view == null) {
                view = this._context.LayoutInflater.Inflate(Resource.Layout.MovieItem, null);
            }

            var movie = this._movies[position];

            var _name = view.FindViewById<TextView>(Resource.Id.name).Text =    movie.movie.Title.ToUpper() + " (" +
                                                                                movie.movie.ReleaseDate.Year.ToString() + ")";
            var _cast = view.FindViewById<TextView>(Resource.Id.cast).Text = movie.cast;


            ImageView posterImage = view.FindViewById<ImageView>(Resource.Id.posterView);
            var ppath = movie.movie.PosterPath;
            if (ppath != "" && ppath != null) {
                posterImage.SetImageResource(0);
                Glide .With(_context).Load("http://image.tmdb.org/t/p/original" + movie.movie.PosterPath).Into(posterImage);
            }

            posterImage.Focusable = false;
            posterImage.FocusableInTouchMode = false;
            posterImage.Clickable = false;
                  

            return view;
        }

        public override int Count => this._movies.Count;

        public override MovieModel this[int position] => this._movies[position];
    }
}