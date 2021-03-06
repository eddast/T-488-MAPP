﻿using DM.MovieApi.MovieDb.Movies;
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
        public string cast;
        public string posterPath;
        public string backdropPath;
        public string runtime;

        public MovieModel() { }
        public MovieModel ( MovieInfo movie, string cast, string posterPath, string backdropPath, string runtime ) {
            this.movie = movie;
            this.cast = cast;
            this.posterPath = posterPath;
            this.backdropPath = backdropPath;
            this.runtime = runtime;
        }
    }
}
