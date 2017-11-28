using CoreGraphics;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using System;
using IOSWeek1;
using UIKit;

namespace IOSWeek1.iOS
{
    public class IOSMainViewController : UIViewController
    {

        // Set initial coordinate values for item placement
        private const double StartX = 20, StartY = 80, Height = 50;

        public override void ViewDidLoad( ) {
            // Call base view and white background set (in case it's not default)
            base.ViewDidLoad(); this.View.BackgroundColor = UIColor.White;

            // User prompt label set
            // Prompts user to enter movie title words
            // Then adds prompt label to view
            var promptLabel = new UILabel( ) {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter words in movie title: "
            }; this.View.AddSubview(promptLabel);

            // Add text field to enter words in movie
            // Then add text field to view
            var movieField = new UITextField()
            {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            }; this.View.AddSubview(movieField);

            // Adds a search button for movie field value
            // Then adds button to view
            var searchMovieButton = UIButton.FromType(UIButtonType.RoundedRect);
            searchMovieButton.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            searchMovieButton.SetTitle("Get movie", UIControlState.Normal);
            this.View.AddSubview(searchMovieButton);

            // Placeholder result set and added to view
            var movieLabelResult = new UILabel()
            {
                Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height),
                Text = ""
            }; this.View.AddSubview(movieLabelResult);

            // Add function to button via lambda expression
            searchMovieButton.TouchUpInside += async (sender, args) => {
                movieField.ResignFirstResponder(); // minimize keyboard

                // Register settings with MovieDBSettings class
                MovieDBSettings set = new MovieDBSettings(); MovieDbFactory.RegisterSettings(set);

                // Create query API and search by movieField value
                // Display head result from movieField
                // Handle NULL exception
                var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
                if (movieField.Text != "" && movieField.Text != null)
                {
                    ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(movieField.Text);
                    movieLabelResult.Text = response.Results[0].Title;
                }
                else { movieLabelResult.Text = "(Must provide query substring)"; }
            };
        }
    }
}