using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using CoreGraphics;

namespace IOSWeek1.iOS.Views
{
    class PersonCell : UITableViewCell
    {
        private UIImageView _imageView;
        private const double ImageHeight = 33;
        private UILabel _headingLabel;
        private UILabel _subheadinglabel;

        public void UpdateCell(string name, string year, string imageName) {
            this._imageView.Image = UIImage.FromFile(imageName);
            this._headingLabel.Text = name;
            this._subheadinglabel.Text = year;
        }

        public PersonCell ( NSString cellID ) : base( UITableViewCellStyle.Default, cellID) {

            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;

            this._imageView = new UIImageView() { // ContentView = öll cellan
                Frame = new CGRect(this.ContentView.Bounds.Width-60, 5, ImageHeight, ImageHeight),
            };

            this._headingLabel = new UILabel() {
                Frame = new CGRect(5, 5, this.ContentView.Bounds.Width - 60, 25),
                Font = UIFont.FromName("Cochin-BoldItalic", 22f),
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear //Þarf kannski ekki tho
            };

            this._subheadinglabel = new UILabel()
            {
                Frame = new CGRect(100, 25, 100, 20),
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                TextAlignment = UITextAlignment.Center,
                BackgroundColor = UIColor.Clear //Þarf kannski ekki tho
            };

            this.ContentView.AddSubviews( new UIView[] { this._imageView, this._headingLabel, this._subheadinglabel } );

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }
    }
}