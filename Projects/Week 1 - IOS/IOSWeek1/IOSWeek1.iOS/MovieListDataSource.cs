using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;

namespace IOSWeek1.iOS
{
    // Movie list data source implements a table view
    public class MovieListDataSource : UITableViewSource
    {
        private readonly List<string> _movieList;
        public readonly NSString movieListCellId = new NSString("MovieListCell");

        public MovieListDataSource(List<string> movieList) { _movieList = movieList; }

        // Design cells for table in table view
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {

            // Once cell disappears from the screen, it's reused for memory management purposes
            var cell = tableView.DequeueReusableCell((NSString)this.movieListCellId);
            if (cell == null) {
                cell = new UITableViewCell(UITableViewCellStyle.Default, this.movieListCellId);
            } cell.TextLabel.Text = this._movieList[indexPath.Row];

            return cell;
        }

        // Determines rows in section
        public override nint RowsInSection(UITableView tableview, nint section) {
           if (_movieList == null) { return 0; }
           return this._movieList.Count;
        }

    }

}
