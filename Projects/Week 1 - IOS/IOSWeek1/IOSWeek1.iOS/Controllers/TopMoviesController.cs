using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using System;
using IOSWeek1;
using UIKit;
using System.Collections.Generic;
using IOSWeek1.MovieDownload;
using System.Threading;

namespace IOSWeek1.iOS.Controllers
{
    public class TopMoviesController : UITableViewController
    {
        private List<MovieModel> topMovieModelList;
        public bool userNavigatedFromAnotherTab = true;

        public TopMoviesController() {
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1);
            this.View.BackgroundColor = UIColor.White;
            topMovieModelList = new List<MovieModel>();
            userNavigatedFromAnotherTab = true;

        }

        public override void ViewDidAppear(bool animated) {
            
            base.ViewDidAppear(animated);
            Title = "Top Rated"; this.View.BackgroundColor = UIColor.White;

            this.ParentViewController.TabBarController.ViewControllerSelected += (sender, e) =>
            {
                if (ParentViewController.TabBarController.SelectedIndex == 0) { userNavigatedFromAnotherTab = true; }
            };

            if (userNavigatedFromAnotherTab) {
                
                this.TableView.Source = new MovieListDataSource(null, _onSelectedMovies);
                this.TableView.ReloadData();
                GenerateTopMoviesViewAsync();
                userNavigatedFromAnotherTab = false;
            }
        }

        private async System.Threading.Tasks.Task GenerateTopMoviesViewAsync()
        {
            var loadSpinner = LoadSpinner(); View.AddSubview(loadSpinner);

            topMovieModelList = new List<MovieModel>();

            // Register settings with MovieDBSettings class
            // Create query API and search by movieField value
            MovieDBSettings set = new MovieDBSettings();
            MovieDbFactory.RegisterSettings(set);
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            // Conduct query and await response
            // If query returns no result, movieList becomes a null list
            var response_m = await movieApi.GetTopRatedAsync();
            IReadOnlyList<MovieInfo> topRatedList = response_m.Results;

            foreach (MovieInfo topmovie in topRatedList)
            {

                ImageDownloader imgdl = new ImageDownloader(new StorageClient());
                string localFilePath = imgdl.LocalPathForFilename(topmovie.PosterPath);
                if (localFilePath != "")
                {
                    await imgdl.DownloadImage(topmovie.PosterPath, localFilePath, CancellationToken.None);
                }

                ApiQueryResponse<MovieCredit> response_cast = await movieApi.GetCreditsAsync(topmovie.Id);
                string movie_cast = "";

                for (int i = 0; i < 3; i++)
                {

                    if (i == response_cast.Item.CastMembers.Count) { break; }
                    if (i != 0) { movie_cast = movie_cast + ", "; }
                    movie_cast = movie_cast + response_cast.Item.CastMembers[i].Name;
                }

                ApiQueryResponse<Movie> tm_movie = await movieApi.FindByIdAsync(topmovie.Id);
                string runtime = tm_movie.Item.Runtime.ToString();

                MovieModel topmoviemodel = new MovieModel(topmovie, movie_cast, localFilePath, runtime);
                topMovieModelList.Add(topmoviemodel);

            }
            // Once the MovieListController has been added to NavigationController
            // The load spinner stops animating and thereby hides and button is clickable again
            //this.NavigationController.PushViewController(new MovieListController(topMovieModelList, "Movie List"), false);
            this.TableView.Source = new MovieListDataSource(topMovieModelList, _onSelectedMovies);
            this.TableView.ReloadData();
            loadSpinner.StopAnimating();
        }

        // Creates and omptimizes spinner displayed while query is processed
        private UIActivityIndicatorView LoadSpinner() {
            var loadSpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            loadSpinner.Frame = new CGRect(100, 100, this.View.Bounds.Width / 2, 30);
            loadSpinner.AutoresizingMask = UIViewAutoresizing.All; this.View.AddSubview(loadSpinner);
            loadSpinner.StartAnimating();

            return loadSpinner;
        }

        private void _onSelectedMovies(int row) {
            this.NavigationController.PushViewController(new MovieDisplayScreenController( topMovieModelList[row]), true);
        }
    }

}
