using System.Collections.Generic;
using UIKit;

namespace AMDb.iOS
{
    public class MovieListController : UITableViewController
    {
        private List<MovieModel> _movieModelList;
        private string _title;

        public MovieListController( List<MovieModel> movieModelList, string title ){ _movieModelList = movieModelList; _title = title; }

        // Movie list view: displays title
        // has a table view rendered in MovieListDataSource given list
        public override void ViewDidLoad() {

            base.ViewDidLoad();
            this.Title = _title;
            this.TableView.Source = new MovieListDataSource(_movieModelList, _onSelectedMovies);
        }

        // Implementation of onSelected cell function
        // To keep it in controller (logic part)
        private void _onSelectedMovies(int row) {
            
            this.NavigationController.PushViewController(new MovieDisplayScreenController(_movieModelList[row]), true);
        }
    }
}