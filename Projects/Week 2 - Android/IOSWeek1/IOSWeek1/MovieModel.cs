using DM.MovieApi.MovieDb.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSWeek1
{
    // Models a movie object with necessary information:
    // MovieInfo object, starring cast, poster path and runtime
    public class MovieModel
    {
        public MovieInfo movie;
        public string title;
        public string year;
        public string cast;
        public string posterPath;
        public string backdropPath;
        public string runtime;
        public string genres;

        public MovieModel() { }

        public MovieModel ( MovieInfo movie, string cast, string runtime ) {
            
            this.movie = movie;

            this.title = movie.Title;
            this.year = movie.ReleaseDate.Year.ToString();
            this.genres = get_movie_genres(movie);
            this.cast = cast;
            this.posterPath = movie.PosterPath;
            this.backdropPath = movie.BackdropPath;
            this.runtime = runtime;
        }

        private string get_movie_genres(MovieInfo _movie)
        {
            var genres = _movie.Genres;
            string Genres = ""; int iteration = 0;

            foreach (DM.MovieApi.MovieDb.Genres.Genre genre in genres)
            {
                if (iteration != 0) { Genres = Genres + ", " + genre.Name; }
                else { Genres = Genres + genre.Name; }
                iteration++;
            }


            return Genres;
        }

    }
}
