using System;
using System.Collections.Generic;
using System.Threading;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using IOSWeek1.MovieDownload;

namespace IOSWeek1.Services
{
    public class MovieDBService
    {
        private MovieDBSettings _settings;
        private IApiMovieRequest _movieApi;
        public List<MovieModel> movieList;

        public MovieDBService() {
            
            // Register settings with MovieDBSettings class
            // Create query API and search by movieField value
            _settings = new MovieDBSettings();
            MovieDbFactory.RegisterSettings(_settings);
            _movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            movieList = new List<MovieModel>();
        }

        // Get local filepath and download image from API
        public async System.Threading.Tasks.Task<string> DownloadPosterAsync(string posterPath)
        {
            ImageDownloader imgdl = new ImageDownloader(new StorageClient());
            string localFilePath = imgdl.LocalPathForFilename(posterPath);
            if (localFilePath != ""){
                await imgdl.DownloadImage(posterPath, localFilePath, CancellationToken.None);
                return localFilePath;
            }


            return "";
        }

        // Extract three starring actors from MovieCredit object by movie ID
        public async System.Threading.Tasks.Task<string> GetThreeCastMembersAsync(int id)
        {
            ApiQueryResponse<MovieCredit> responseCast = await _movieApi.GetCreditsAsync(id);
            string movieCast = "";

            for (int i = 0; i < 3; i++){

                if (i == responseCast.Item.CastMembers.Count) break;
                if (i != 0) movieCast = movieCast + ", ";
                movieCast = movieCast + responseCast.Item.CastMembers[i].Name;
            }


            return movieCast;
        }

        // Extract movie runtime from Movie object by movie ID
        public async System.Threading.Tasks.Task<string> GetRuntimeAsync(int id)
        {
            ApiQueryResponse<Movie> tm_movie = await _movieApi.FindByIdAsync(id);
            string runtime = tm_movie.Item.Runtime.ToString();


            return runtime;
        }
    }
}
