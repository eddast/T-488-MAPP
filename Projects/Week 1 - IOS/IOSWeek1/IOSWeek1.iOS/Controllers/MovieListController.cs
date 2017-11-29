using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using IOSWeek1.iOS;

namespace IOSWeek1.iOS
{
    public class MovieListController : UITableViewController
    {
        private List<MovieModel> _movieModelList;

        public MovieListController( List<MovieModel> movieModelList ){ _movieModelList = movieModelList; }

        // Movie list view: displays title
        // has a table view rendered in MovieListDataSource given list
        public override void ViewDidLoad() {

            base.ViewDidLoad();
            this.Title = "Movie list";
            this.TableView.Source = new MovieListDataSource(_movieModelList, _onSelectedMovies);
        }

        private void _onSelectedMovies(int row) {
            this.NavigationController.PushViewController(new MovieDisplayScreenController(_movieModelList[row].movie), true);
        }
    }
}