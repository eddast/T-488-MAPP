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
        private List<MovieModel> _topMovieModelList;
        private bool _userNavigatedFromAnotherTab;

        public TopMoviesController()
        {
            // Initialize variables and indicate tab item
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1);
            this.View.BackgroundColor = UIColor.White;
            _topMovieModelList = new List<MovieModel>();
            _userNavigatedFromAnotherTab = true;

        }

        public override void ViewWillAppear(bool animated)
        {
            // Initialize view, background and title
            base.ViewWillAppear(animated);
            this.Title = "Top Rated";
            this.View.BackgroundColor = UIColor.White;

            // Function listenes to the event of user clicking any tab
            // If that tab is not the tab containing current view (index 1)
            // We deduce that user will be navigating from another tab once he returns from view
            this.ParentViewController.TabBarController.ViewControllerSelected += (sender, e) => {
                
                if (ParentViewController.TabBarController.SelectedIndex != 1) {
                    
                    _userNavigatedFromAnotherTab = true;
                }
            };

            // If user navigates to view from another tab
            // view is cleared (data and source), then reloaded
            // In case movie list is empty, reload (should not happen)
            if (_userNavigatedFromAnotherTab || _topMovieModelList.Count == 0) {

                _topMovieModelList = new List<MovieModel>();
                this.TableView.Source = new MovieListDataSource(null, _onSelectedMovies);
                this.TableView.ReloadData();
                GenerateTopMoviesViewAsync();
                _userNavigatedFromAnotherTab = false;
            }
        }

        private async System.Threading.Tasks.Task GenerateTopMoviesViewAsync()
        {
            var loadSpinner = LoadSpinner(); View.AddSubview(loadSpinner);

            _topMovieModelList = new List<MovieModel>();

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
                _topMovieModelList.Add(topmoviemodel);
            }

            // Movie list is ready
            this.TableView.Source = new MovieListDataSource(_topMovieModelList, _onSelectedMovies);
            this.TableView.ReloadData();
            loadSpinner.StopAnimating();
        }

        // Creates and omptimizes spinner displayed while query is processed
        private UIActivityIndicatorView LoadSpinner() {

            int spinnerWidth = 50; int spinnerHeight = 50;
            int spinnerX = (int) this.View.Bounds.Width/2 - spinnerWidth/2;
            int spinnerY = 30;
            var loadSpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            loadSpinner.Frame = new CGRect(spinnerX, spinnerY, spinnerWidth, spinnerHeight);
            loadSpinner.AutoresizingMask = UIViewAutoresizing.All; this.View.AddSubview(loadSpinner);
            loadSpinner.StartAnimating();

            return loadSpinner;
        }

        // Implementation of onSelected cell function
        // To keep it in controller (logic part)
        private void _onSelectedMovies(int row) {
            
            this.NavigationController.PushViewController(new MovieDisplayScreenController( _topMovieModelList[row]), true);
        }
    }

}
