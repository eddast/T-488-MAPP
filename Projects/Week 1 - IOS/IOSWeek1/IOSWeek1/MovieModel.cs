using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSWeek1
{
    public class MovieModel
    {
        public MovieInfo movie;
        public string cast;
        public string posterPath;
        public string runtime;

        public MovieModel() { }
        public MovieModel ( MovieInfo movie, string cast, string posterPath, string runtime ) {
            this.movie = movie; this.cast = cast; this.posterPath = posterPath; this.runtime = runtime;
        }
    }
}
