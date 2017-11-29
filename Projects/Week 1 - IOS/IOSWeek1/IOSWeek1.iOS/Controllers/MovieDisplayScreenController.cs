using CoreGraphics;
using DM.MovieApi.MovieDb.Movies;
using System.Collections.Generic;
using UIKit;

namespace IOSWeek1.iOS
{
    public class MovieDisplayScreenController : UIViewController
    {
        private MovieInfo _movie;

        public MovieDisplayScreenController(MovieInfo movie) { this._movie = movie; }

        public override void ViewDidLoad()
        {
            // Call base view and white background set (in case it's not default) and title
            base.ViewDidLoad(); this.View.BackgroundColor = UIColor.White; Title = "Movie Information";

            int StartX = 50; int StartY = 100;

            var movieTitle = new UILabel() {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width-100, 40),
                Text = _movie.Title,
                TextColor = UIColor.FromRGB(38, 127, 0),
            }; var movieInfo = new UILabel() {
                Frame = new CGRect(StartX, StartY + 40, this.View.Bounds.Width - 100, 20),
                BackgroundColor = UIColor.FromRGB(43, 150, 0),
                Text = get_movie_genres(),
                TextColor = UIColor.FromRGB(38, 127, 0),
            }; var movieDescription = new UITextView () {
                Frame = new CGRect(StartX, StartY + 60, this.View.Bounds.Width - 100, 300),
                BackgroundColor = UIColor.FromRGB(43, 150, 0),
                Text = _movie.Overview,
                TextColor = UIColor.FromRGB(38, 127, 0),
            };
            this.View.AddSubviews( new UIView[] { movieTitle, movieInfo, movieDescription });
        }

        private string get_movie_genres( ) {

            var genres = _movie.Genres;
            string Genres = ""; int iteration = 0;
            foreach (DM.MovieApi.MovieDb.Genres.Genre genre in genres) {
                if (iteration != 0) { Genres = Genres + ", " + genre.ToString(); }
                else { Genres = Genres + genre.ToString(); } iteration++;
            }

            return Genres;
        }
       
    }
}