using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb;
using DM.MovieApi.MovieDb.Movies;
using CoreGraphics;

namespace IOSWeek1.iOS.Views
{
    class MovieCells : UITableViewCell
    {
        private UIImageView _imageView;
        private UILabel _titleAndYear;
        private UILabel _starring;

        public MovieCells(NSString cellID) : base(UITableViewCellStyle.Default, cellID)
        {
            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;

            this._imageView = new UIImageView() {
                Frame = new CGRect(5, 5, 33, 33),

            };
            this._titleAndYear = new UILabel()
            {

                Frame = new CGRect(100, 5, this.ContentView.Bounds.Width, 25),
                Font = UIFont.FromName("AmericanTypewriter", 20f),
                TextColor = UIColor.FromRGB(127, 51, 0),
                BackgroundColor = UIColor.Clear
            };

            this._starring = new UILabel()
            {
                Frame = new CGRect(5, 30, this.ContentView.Bounds.Width, 20),
                Font = UIFont.FromName("AmericanTypewriter", 12f),
                TextColor = UIColor.FromRGB(38, 127, 0),
                BackgroundColor = UIColor.Clear
            };

            this.ContentView.AddSubviews( new UIView[] { this._imageView, this._titleAndYear, this._starring } );

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public void UpdateCell(string name, string year, string cast, string imageName) 
        {
            this._imageView.Image = UIImage.FromFile(imageName);
            this._titleAndYear.Text = name + " (" + year + ")";
            this._starring.Text = cast;
        }
    }
}