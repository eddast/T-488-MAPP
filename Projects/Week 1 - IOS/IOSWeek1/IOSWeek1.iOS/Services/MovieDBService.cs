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

        public async System.Threading.Tasks.Task<List<MovieModel>> GetMovieListByTitleAsync(string title)
        {
            List <MovieModel> movieModelList = new List<MovieModel>();

            // Conduct query and await response
            // If query returns no result, movieList becomes a null list
            ApiSearchResponse<MovieInfo> response_m = await _movieApi.SearchByTitleAsync(title);
            IReadOnlyList<MovieInfo> movieInfoList = response_m.Results;

            if ( response_m.Results.Count == 0 ) { return null; }

            foreach (MovieInfo movie in movieInfoList)
            {

                // Get poster path, starring cast and movie runtime
                // Then create a model with those values and add it to list
                var localFilePath = await DownloadPosterAsync(movie.PosterPath);
                var movieCast = await GetThreeCastMembersAsync(movie.Id);
                var runtime = await GetRuntimeAsync(movie.Id);

                MovieModel movieModel = new MovieModel(movie, movieCast, localFilePath, runtime);
                movieModelList.Add(movieModel);
            }


            return movieModelList;
        }

        /*public async System.Threading.Tasks.Task<List<MovieModel>> GetTopRatedMoviesListAsync()
        {
            List<MovieModel> movieModelList = new List<MovieModel>();

            // Conduct query and await response
            // If query returns no result, movieList becomes a null list
            var response_m = await _movieApi.GetTopRatedAsync();
            IReadOnlyList<MovieInfo> movieInfoList = response_m.Results;

            foreach (MovieInfo movie in movieInfoList)
            {

                // Get poster path, starring cast and movie runtime
                // Then create a model with those values and add it to list
                MovieDBService server = new MovieDBService();
                var localFilePath = await server.DownloadPosterAsync(movie.PosterPath);
                var movieCast = await server.GetThreeCastMembersAsync(movie.Id);
                var runtime = await server.GetRuntimeAsync(movie.Id);
                MovieModel topRatedMovie = new MovieModel(movie, movieCast,
                                                          localFilePath, runtime);
                movieModelList.Add(topRatedMovie);
            }

            return movieModelList;
        }*/

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
