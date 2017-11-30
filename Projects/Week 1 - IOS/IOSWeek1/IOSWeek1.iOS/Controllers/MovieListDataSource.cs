using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using IOSWeek1.iOS.View;
using IOSWeek1.iOS.Views;

namespace IOSWeek1.iOS
{
    // Movie list data source implements a table view
    public class MovieListDataSource : UITableViewSource
    {
        public readonly List<MovieModel> _movieModelList;
        public readonly NSString movieListCellId = new NSString("MovieListCell");
        private readonly Action<int> _onSelectedMovies;
        public MovieListDataSource(List<MovieModel> movieModelList, Action<int> onSelectedMovies) {
            _movieModelList = movieModelList;
            _onSelectedMovies = onSelectedMovies;
        }

        // Design cells for table in table view
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // Set cell height
            tableView.RowHeight = 80;

            // Once cell disappears from the screen, it's reused for memory management purposes
            var cell = (MovieCells)tableView.DequeueReusableCell(this.movieListCellId);

            // If there are no cells to reuse, we create a new cell
            if (cell == null) { cell = new MovieCells(this.movieListCellId); }

            // Extract appropriate values from movie model to pass
            // Into detail view
            var movie = this._movieModelList[indexPath.Row].movie;
            var movie_cast = this._movieModelList[indexPath.Row].cast;
            var posterPath = this._movieModelList[indexPath.Row].posterPath;

            cell.UpdateCell(movie.Title, movie.ReleaseDate.Year.ToString(), movie_cast, posterPath);

            return cell;
        }


        // Determines number of rows in section
        public override nint RowsInSection(UITableView tableview, nint section) {
           if (_movieModelList == null) { return 0; }
           return this._movieModelList.Count;
        }

        // Once row is selected, the passed in function onSelectedMovies
        // Determines action, located in the controller
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {

            tableView.DeselectRow(indexPath, true);
            this._onSelectedMovies(indexPath.Row);
        }
    }

}
