using CoreGraphics;
using UIKit;
using System.Collections.Generic;
using IOSWeek1.Services;

namespace IOSWeek1.iOS
{
    public class IOSMainViewController : UIViewController
    {
        // Adding this controller to a new tab indicating search
        public IOSMainViewController() {
            this.TabBarItem = new UITabBarItem(UITabBarSystemItem.Search , 0);    
        }

        // Set initial coordinate values for item placement
        private const double StartX = 20, StartY = 80, Height = 50;

        public override void ViewDidLoad() {

            // Call base view and white background set (in case it's not default)
            base.ViewDidLoad(); this.View.BackgroundColor = UIColor.White;

            // field label, input field and button make up the
            // main view main window screen. This is generated and added to subview
            var promptLabel = PromptLabel();
            var movieField = MovieField();
            var searchMovieButton = SearchMovieButton(movieField);

            this.View.AddSubviews(new UIView[] { promptLabel, movieField, searchMovieButton });
        }

        // User prompt label set
        // Prompts user to enter movie title words
        private UILabel PromptLabel()
        {
            var promptLabel = new UILabel() {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter words in movie title: "
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

        // Placeholder result set and added to view
        private UILabel MovieLabelResult() {
            var movieLabelResult = new UILabel() {
                Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height),
                Text = ""
            };

            return movieLabelResult;
        }

        // Adds a search button for movie field value
        private UIButton SearchMovieButton(UITextField movieField) {
            
            var searchMovieButton = UIButton.FromType(UIButtonType.RoundedRect);
            searchMovieButton.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            searchMovieButton.SetTitle("Get movie", UIControlState.Normal);

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
                var loadSpinner = LoadSpinner();

                if (movieField.Text != "" && movieField.Text != null) {

                    // Initiate movie server to query web service
                    MovieDBService server = new MovieDBService();
                    movieModelList = await server.GetMovieListByTitleAsync(movieField.Text);

                } else { movieModelList = null; }

                // Once the MovieListController has been added to NavigationController
                // The load spinner stops animating and thereby hides and button is clickable again
                this.NavigationController.PushViewController(new MovieListController(movieModelList, "Movie List"), true);

                loadSpinner.StopAnimating(); searchMovieButton.Enabled = true; movieField.Text = "";
            };

            return searchMovieButton;
        }

        // Creates and omptimizes spinner displayed while query is processed
        private UIActivityIndicatorView LoadSpinner( ) {
            
            var loadSpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            loadSpinner.Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            loadSpinner.AutoresizingMask = UIViewAutoresizing.All; this.View.AddSubview(loadSpinner);
            loadSpinner.StartAnimating();

            return loadSpinner;
        }
    }
}