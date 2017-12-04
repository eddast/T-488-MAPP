using CoreGraphics;
using UIKit;
using System.Collections.Generic;
using IOSWeek1.Services;
using IOSWeek1.MovieDownload;

namespace IOSWeek1.iOS
{
    public class IOSMainViewController : UIViewController
    {
        // Set initial coordinate values for item placement
        private double StartX, StartY, Height;

        // Adding this controller to a new tab indicating search
        public IOSMainViewController() {
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search , 0); 
            StartX = 20; StartY = 80; Height = 50;
        }

        public override void ViewDidLoad() {

            // Call base view and white background set (in case it's not default)
            base.ViewDidLoad(); this.View.BackgroundColor = UIColor.White; Title = "Movie Search";

            // field label, input field and button make up the
            // main view main window screen. This is generated and added to subview
            var headingLabel = HeadingLabel();
            var promptLabel = PromptLabel();
            var movieField = MovieField();
            var searchMovieButton = SearchMovieButton(movieField);

            this.View.AddSubviews(new UIView[] { headingLabel, promptLabel, movieField, searchMovieButton });
        }

        // Heading set
        // Prompts user to enter movie title words
        private UILabel HeadingLabel()
        {
            var headingLabel = new UILabel()
            {
                Frame = new CGRect(5, StartY,
                                   this.View.Bounds.Width - StartX,
                                   Height * 2),
                TextAlignment = UITextAlignment.Center,
                Text = "Movie Search".ToUpper(),
                Font = UIFont.FromName("BanglaSangamMN-Bold", 30f),
                TextColor = UIColor.FromRGB(0, 122, 255)
            };

            return headingLabel;
        }

        // User prompt label set
        // Prompts user to enter movie title words
        private UILabel PromptLabel()
        {
            StartY += (Height * 2);

            var promptLabel = new UILabel() {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter words in movie title: ",
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.FromName("BanglaSangamMN", 15f)
            };

            return promptLabel;
        }

        // Add text field to enter words in movie
        // Then add text field to view
        private UITextField MovieField() {
            var movieField = new UITextField() {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            return movieField;
        }
        // Adds a search button for movie field value
        private UIButton SearchMovieButton(UITextField movieField) {

            StartY = StartY + 2 * Height + 20;
            
            var searchMovieButton = UIButton.FromType(UIButtonType.RoundedRect);
            searchMovieButton.Frame = new CGRect(this.View.Bounds.Width/2-100/2, StartY, 100, Height);
            searchMovieButton.SetTitle("Get movie", UIControlState.Normal);
            searchMovieButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            searchMovieButton.BackgroundColor = UIColor.FromRGB(5, 93, 207);


            return AddButtonFunction(searchMovieButton, movieField);
        }

        // Adds function to button and returns it
        private UIButton AddButtonFunction(UIButton searchMovieButton, UITextField movieField) {
            
            searchMovieButton.TouchUpInside += async (sender, args) => {
                List<MovieModel> movieModelList = new List<MovieModel>();

                // Minimize keyboard on click, disable button
                // and add load spinner to indicate background process
                movieField.ResignFirstResponder();
                searchMovieButton.Enabled = false;
                searchMovieButton.BackgroundColor = UIColor.LightGray;
                var loadSpinner = LoadSpinner();

                if (movieField.Text != "" && movieField.Text != null) {

                    // Initiate movie server to query web service
                    MovieDBService server = new MovieDBService();
                    movieModelList = await server.GetMovieListByTitleAsync(movieField.Text);
                    foreach (MovieModel movie in movieModelList) {

                        var bdpath = DownloadPosterAsync(movie.movie.BackdropPath).Result;
                        var ppath = DownloadPosterAsync(movie.movie.PosterPath).Result;
                        movie.backdropPath = bdpath;
                        movie.posterPath = ppath;
                    }

                } else { movieModelList = null; }

                // Once the MovieListController has been added to NavigationController
                // The load spinner stops animating and thereby hides and button is clickable again
                this.NavigationController.PushViewController(new MovieListController(movieModelList, "Movie List"), true);

                loadSpinner.StopAnimating();
                searchMovieButton.Enabled = true;
                searchMovieButton.BackgroundColor = UIColor.FromRGB(5, 93, 207);
                movieField.Text = "";
            };

            return searchMovieButton;
        }

        private async System.Threading.Tasks.Task<string> DownloadPosterAsync(string path) {

            ImageDownloader imgdl = new ImageDownloader(new StorageClient());
            return await imgdl.DownloadMovieImageAsync(path);
        }

        // Creates and omptimizes spinner displayed while query is processed
        private UIActivityIndicatorView LoadSpinner( ) {
            
            var loadSpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            loadSpinner.Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height);
            loadSpinner.AutoresizingMask = UIViewAutoresizing.All; this.View.AddSubview(loadSpinner);
            loadSpinner.StartAnimating();

            return loadSpinner;
        }
    }
}