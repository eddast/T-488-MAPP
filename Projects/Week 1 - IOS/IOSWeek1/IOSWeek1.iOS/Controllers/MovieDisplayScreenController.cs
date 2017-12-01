using CoreGraphics;
using UIKit;

namespace IOSWeek1.iOS
{
    public class MovieDisplayScreenController : UIViewController
    {
        // Helper values for positioning subviews in cell
        private int _subviewX, _subviewY;
        private int _standardSpacing;
        private int _estHeadingWidth, _estInfoWidth;
        private int _imageHeight;
        private int _imageWidth;

        // Movie model being displayed and poster imageview object
        private UIImageView _imageView;
        private UIImageView _imageBackdropView;
        private MovieModel _movie;


        public MovieDisplayScreenController(MovieModel movie) {

            // Initialize position values and movie model
            _subviewX = 20; _subviewY = 265;
            _standardSpacing = 10;
            _estHeadingWidth = 30;
            _estInfoWidth = 20;
            _imageHeight = 150;
            _imageWidth = (int)(_imageHeight * 0.67);
            this._movie = movie;
        }

        public override void ViewDidLoad()
        {
            // Call base view and white background set (in case it's not default) and title
            base.ViewDidLoad(); this.View.BackgroundColor = UIColor.White; Title = "Movie Information";

            // Generate subviews and add them to view
            var _movieTitle = _MovieTitle();
            var _movieInfo = _MovieInfo();
            var _seperatorLine = _SeperatorLine();
            var _movieDescription = _MovieDescription();
            this._imageView = _ImageView();
            this._imageBackdropView = _ImageBackdropView();
            this.View.AddSubviews( new UIView[] { _imageBackdropView,
                                                  _imageView,
                                                  _movieTitle,
                                                  _movieInfo,
                                                  _seperatorLine,
                                                  _movieDescription });
        }

        // Generates frame and image of backdrop image
        private UIImageView _ImageBackdropView()
        {
            var imageView = new UIImageView()
            {
                Frame = new CGRect(0, 50,
                                   this.View.Bounds.Width, 200),
                
            };
            if (_movie.backdropPath != null && _movie.backdropPath != ""){
                imageView.Image = UIImage.FromFile(_movie.backdropPath);
            }


            return imageView;
        }

        // Formats movie title in movie detail view
        private UILabel _MovieTitle()
        {
            var movieName = _movie.movie.Title.ToUpper();
            var movieYear = "(" + _movie.movie.ReleaseDate.Year.ToString() + ")";

            var movieTitle = new UILabel() {

                Frame = new CGRect(_subviewX, _subviewY,
                                   this.View.Bounds.Width - (_subviewX + _standardSpacing),
                                   _estHeadingWidth),
                Text = movieName + " " + movieYear,
                Font = UIFont.FromName("BanglaSangamMN-Bold", 20f),
                TextColor = UIColor.FromRGB(0, 122, 255),
            };


            return movieTitle;
        }

        // Format genre and runtime text of movie
        private UILabel _MovieInfo()
        {
            _subviewY = _subviewY + _estHeadingWidth + _standardSpacing;
                
            var movieInfo = new UILabel() {

                Frame = new CGRect(_subviewX, _subviewY,
                                   this.View.Bounds.Width - _subviewX,
                                   _estInfoWidth),
                Text = _movie.runtime + " mins | " + get_movie_genres(),
                Font = UIFont.FromName("BanglaSangamMN", 13f),
                TextColor = UIColor.FromRGB(153, 153, 102),
            };


            return movieInfo;
        }

        // Models a very fake seperator line
        private UILabel _SeperatorLine()
        {
            _subviewY = _subviewY + _estInfoWidth + _standardSpacing;

            var seperatorLine = new UILabel() {
                
                Frame = new CGRect(_subviewX, _subviewY,
                                   this.View.Bounds.Width - (_subviewX + _subviewX), 1),
                BackgroundColor = UIColor.FromRGB(222, 222, 222)
            };


            return seperatorLine;
        }

        // Generates frame and image of poster
        private UIImageView _ImageView()
        {
            _subviewY = _subviewY + _standardSpacing;
            var imageView = new UIImageView() {
                Frame = new CGRect(_subviewX, _subviewY,
                                   _imageWidth, _imageHeight),
                Image = UIImage.FromFile(_movie.posterPath)
            };


            return imageView;
        }

        // Models movie description text view
        private UITextView _MovieDescription()
        {
            var desXpos = _subviewX + (_imageWidth + _standardSpacing);
            _subviewY = _subviewY + (_standardSpacing * 2);
            var movieDescription = new UITextView() {
                
                Frame = new CGRect(_subviewX + (_imageWidth + _standardSpacing),
                                   _subviewY, this.View.Bounds.Width - (desXpos + _standardSpacing),
                                   this.View.Bounds.Height),
                Text = _movie.movie.Overview
            };


            return movieDescription;
        }

        // Gets movie genres from the MoveInfo object
        private string get_movie_genres( )
        {
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