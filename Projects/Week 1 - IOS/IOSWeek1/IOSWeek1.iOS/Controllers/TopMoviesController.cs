using CoreGraphics;
using UIKit;
using System.Collections.Generic;
using IOSWeek1.Services;

namespace IOSWeek1.iOS.Controllers
{
    public class TopMoviesController : UITableViewController
    {
        private List<MovieModel> _topMovieModelList;
        private bool _userNavigatedFromAnotherTab;
        private bool _atRoot;

        public TopMoviesController()
        {
            // Initialize variables and indicate tab item
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.TopRated, 1);
            this.View.BackgroundColor = UIColor.White;
            _topMovieModelList = new List<MovieModel>();
            _userNavigatedFromAnotherTab = true;
            _atRoot = true;
        }

        public override void ViewDidAppear(bool animated)
        {
            // Initialize view, background and title
            base.ViewDidAppear(animated);
            this.Title = "Top Rated";
            this.View.BackgroundColor = UIColor.White;

            // Function listenes to the event of user clicking any tab
            // If that tab is not the tab containing current view (index 1)
            // We deduce that user will be navigating from another tab once he returns from view
            this.ParentViewController.TabBarController.ViewControllerSelected += (sender, e) => {
                
                if (ParentViewController.TabBarController.SelectedIndex != 1) {
                    
                    _userNavigatedFromAnotherTab = true;
                } else {
                    if (this.NavigationController.ViewControllers.Length <= 1) {
                        _atRoot = true;
                    } else { _atRoot = false; }
                }
            };

            // If user navigates to view from another tab
            // view is cleared (data and source), then reloaded
            // In case movie list is empty, reload (should not happen)
            if ( (_userNavigatedFromAnotherTab && _atRoot) || _topMovieModelList.Count == 0) {
                
                _topMovieModelList = new List<MovieModel>();
                this.TableView.Source = new MovieListDataSource(null, _onSelectedMovies);
                this.TableView.ReloadData();

                var TopMovies = GenerateTopMoviesViewAsync();
                _userNavigatedFromAnotherTab = false;
            }
        }

        // Uses MovieDBService object to fetch top rated movies
        // Meanwhile displays a spinner
        // Then alters tableViewSource to display top rated movies
        public async System.Threading.Tasks.Task<List<MovieModel>> GenerateTopMoviesViewAsync()
        {   
            var loadSpinner = LoadSpinner();
            View.AddSubview(loadSpinner);

            MovieDBService server = new MovieDBService();
            List<MovieModel> movieList = await server.GetTopMoviesViewAsync();

            this.TableView.Source = new MovieListDataSource(movieList, _onSelectedMovies);
            this.TableView.ReloadData();

            loadSpinner.StopAnimating();

            _topMovieModelList = movieList;


            return movieList;
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
