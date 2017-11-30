using CoreGraphics;
using DM.MovieApi.MovieDb.Movies;
using System.Collections.Generic;
using UIKit;

namespace IOSWeek1.iOS
{
    public class MovieDisplayScreenController : UIViewController
    {
        private UIImageView _imageView;
        private MovieModel _movie;

        public MovieDisplayScreenController(MovieModel movie) { this._movie = movie; }

        public override void ViewDidLoad()
        {
            // Call base view and white background set (in case it's not default) and title
            base.ViewDidLoad(); this.View.BackgroundColor = UIColor.White; Title = "Movie Information";

            int StartX = 50; int StartY = 100;

            var movieTitle = new UILabel() {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width-100, 30),
                Text = _movie.movie.Title.ToUpper() + " (" + _movie.movie.ReleaseDate.Year.ToString() + ")",
                Font = UIFont.FromName("Helvetica-Bold", 22f),
                TextColor = UIColor.FromRGB(0, 122, 255),
            };
            var movieInfo = new UILabel() {
                Frame = new CGRect(StartX, StartY + 40, this.View.Bounds.Width - 100, 20),
                Text = _movie.runtime + " mins | " + get_movie_genres(),
                Font = UIFont.FromName("Helvetica", 13f),
                TextColor = UIColor.FromRGB(153, 153, 102),
            };
            var seperatorLine = new UILabel() {
                Frame = new CGRect(StartX - 30, StartY + 70, this.View.Bounds.Width - 50, 1),
                BackgroundColor = UIColor.FromRGB(222, 222, 222)
            };
            var movieDescription = new UITextView () {
                Frame = new CGRect(StartX + 90, StartY + 80, this.View.Bounds.Width - 150, 300),
                Text = _movie.movie.Overview
            };
            this._imageView = new UIImageView() {
                Frame = new CGRect(StartX - 30, StartY + 90, 100, 150),
            }; this._imageView.Image = UIImage.FromFile(_movie.posterPath);


            this.View.AddSubviews( new UIView[] { _imageView, movieTitle, movieInfo, seperatorLine, movieDescription });
        }

        // Gets movie genres from the MoveInfo object
        private string get_movie_genres( ) {

            var genres = _movie.movie.Genres;
            string Genres = ""; int iteration = 0;

            foreach (DM.MovieApi.MovieDb.Genres.Genre genre in genres) {
                if (iteration != 0) { Genres = Genres + ", " + genre.Name; }
                else { Genres = Genres + genre.Name; } iteration++;
            }

            return Genres;
        }
    }
}