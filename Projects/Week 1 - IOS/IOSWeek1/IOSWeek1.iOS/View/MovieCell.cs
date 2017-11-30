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
            int imageSize = 65;

            this._imageView = new UIImageView() {
                Frame = new CGRect(5, 5, imageSize, imageSize),

            };
            this._titleAndYear = new UILabel()
            {
                Frame = new CGRect( 75, 15, this.ContentView.Bounds.Width, 20 ),
                Font = UIFont.FromName( "Helvetica-Bold", 17f ),
                TextColor = UIColor.FromRGB( 0, 122, 255 ),
                BackgroundColor = UIColor.Clear
            };

            this._starring = new UILabel()
            {
                Frame = new CGRect( 75, 40, this.ContentView.Bounds.Width, 20 ),
                Font = UIFont.FromName( "Helvetica-Oblique", 13f ),
                TextColor = UIColor.FromRGB( 153, 153, 102 ),
                BackgroundColor = UIColor.Clear
            };

            this.ContentView.AddSubviews( new UIView[] { this._imageView, this._titleAndYear, this._starring } );

            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public void UpdateCell(string name, string year, string cast, string imageName) 
        {
            if (imageName != null && imageName != "") { this._imageView.Image = UIImage.FromFile(imageName); }
            this._titleAndYear.Text = name.ToUpper() + " (" + year + ")";
            this._starring.Text = cast;
        }
    }
}