using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using System;
using IOSWeek1;
using UIKit;
using System.Collections.Generic;

namespace IOSWeek1.iOS
{
    public class IOSMainViewController : UIViewController
    {
        // Set initial coordinate values for item placement
        private const double StartX = 20, StartY = 80, Height = 50;

        public override void ViewDidLoad()
        {

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
            var promptLabel = new UILabel()
            {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter words in movie title: "
            };

            return promptLabel;
        }

        // Add text field to enter words in movie
        // Then add text field to view
        private UITextField MovieField()
        {
            var movieField = new UITextField()
            {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            return movieField;
        }

        // Placeholder result set and added to view
        private UILabel MovieLabelResult()
        {
            var movieLabelResult = new UILabel()
            {
                Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height),
                Text = ""
            };

            return movieLabelResult;
        }

        // Adds a search button for movie field value
        private UIButton SearchMovieButton(UITextField movieField)
        {
            var searchMovieButton = UIButton.FromType(UIButtonType.RoundedRect);
            searchMovieButton.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            searchMovieButton.SetTitle("Get movie", UIControlState.Normal);

            return AddButtonFunction(searchMovieButton, movieField);
        }

        // Adds function to button and returns it
        private UIButton AddButtonFunction(UIButton searchMovieButton, UITextField movieField)
        {
            searchMovieButton.TouchUpInside += async (sender, args) =>
            {

                // Minimize keyboard on click, disable button
                // and add load spinner to indicate background process
                movieField.ResignFirstResponder();
                searchMovieButton.Enabled = false;
                var loadSpinner = LoadSpinner();

                // Register settings with MovieDBSettings class
                // Create query API and search by movieField value
                MovieDBSettings set = new MovieDBSettings();
                MovieDbFactory.RegisterSettings(set);
                var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

                // Allocate a list that will contain movie titles
                List<string> movieTitles = new List<string>();

                // If input isn't null, resolve query
                // Otherwise return empty list of movieTitles
                if (movieField.Text != "" && movieField.Text != null)
                {

                    // Conduct query and await response
                    // If query returns no result, movieTitles becomes a null list
                    ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(movieField.Text);
                    var movieList = response.Results;
                    if (response.Results.Count != 0)
                    {

                        // Extract string equivalent of titles to use for next view
                        foreach (MovieInfo info in movieList)
                        {
                            movieTitles.Add(info.Title);
                        }
                    }
                    else { movieTitles = null; }
                }
                else { movieTitles = null; }

                // Once the MovieListController has been added to NavigationController
                // The load spinner stops animating and thereby hides and button is clickable again
                this.NavigationController.PushViewController(new MovieListController(movieTitles), true);
                loadSpinner.StopAnimating(); searchMovieButton.Enabled = true;
            };

            return searchMovieButton;
        }

        private UIActivityIndicatorView LoadSpinner( )
        {
            var loadSpinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
            loadSpinner.Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            loadSpinner.AutoresizingMask = UIViewAutoresizing.All; this.View.AddSubview(loadSpinner);
            loadSpinner.StartAnimating();

            return loadSpinner;
        }
    }
}