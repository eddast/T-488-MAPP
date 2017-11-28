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
    public class NameViewController : UIViewController
    {

        // Set initial coordinate values for item placement
        private const double StartX = 20, StartY = 80, Height = 50;

        private List<string> _namelist;

        public NameViewController(List<string> namelist) { this._namelist = namelist; }

        public override void ViewDidLoad() {

            // Call base view and white background set (in case it's not default)
            base.ViewDidLoad(); this.View.BackgroundColor = UIColor.White;

            // User prompt label set
            // Prompts user to enter movie title words
            // Then adds prompt label to view
            var promptLabel = PromptLabel( );

            // Add text field to enter words in movie
            // Then add text field to view
            var nameField = NameField( );

            // Placeholder result set and added to view
            var greetingLabel = GreetingLabel();

            // Adds a greet button and name button to next screen
            // Add function to buttons via lambda expression
            var greetButton = GreetButton( nameField, greetingLabel );
            var navigateButton = NavigationButton( nameField );
           
            this.View.AddSubviews( new UIView[] { promptLabel, nameField, greetingLabel, greetButton, navigateButton} );

        }

        UILabel PromptLabel() {

            var promptLabel = new UILabel ( ) {
                Frame = new CGRect(StartX, StartY, this.View.Bounds.Width - 2 * StartX, Height),
                Text = "Enter your name: "
            };

            return promptLabel;
        }

        UIButton NavigationButton ( UITextField nameField)
        {
            var navigateButton = UIButton.FromType(UIButtonType.RoundedRect);
            navigateButton.Frame = new CGRect(StartX, StartY + 4 * Height, this.View.Bounds.Width-2*StartX, Height );
            navigateButton.SetTitle( "See name list", UIControlState.Normal );

            navigateButton.TouchUpInside += (sender, args) => {
                nameField.ResignFirstResponder(); // minimize keyboard
                this.NavigationController.PushViewController(new NameListController( this._namelist ), true );
            };

            return navigateButton;
        }

        UITextField NameField ( ) {
            var nameField = new UITextField() {
                Frame = new CGRect(StartX, StartY + Height, this.View.Bounds.Width - 2 * StartX, Height),
                BorderStyle = UITextBorderStyle.RoundedRect
            };

            return nameField;
        }

        UIButton GreetButton( UITextField nameField, UILabel greetingLabel ) {

            var greetButton = UIButton.FromType(UIButtonType.RoundedRect);
            greetButton.Frame = new CGRect(StartX, StartY + 2 * Height, this.View.Bounds.Width - 2 * StartX, Height);
            greetButton.SetTitle("Get greeting!", UIControlState.Normal);

            greetButton.TouchUpInside += (sender, args) => {
                nameField.ResignFirstResponder(); // minimize keyboard
                greetingLabel.Text = "Hello " + nameField.Text + ", you've been added to the name list";
                this._namelist.Add( nameField.Text );
            };

            return greetButton;
        }

        UILabel GreetingLabel( ) {
            var greetingLabel = new UILabel() {
                Frame = new CGRect(StartX, StartY + 3 * Height, this.View.Bounds.Width - 2 * StartX, Height),
                Text = ""
            };

            return greetingLabel;
        }
    }
}