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
        private readonly List<string> _movieList;

        public MovieListController( List<string> movieList ){ this._movieList = movieList; }

        // Movie list view: displays title
        // has a table view rendered in MovieListDataSource given list
        public override void ViewDidLoad() {

            base.ViewDidLoad();
            this.Title = "Movie list";
            this.TableView.Source = new MovieListDataSource( this._movieList );
        }
    }

}