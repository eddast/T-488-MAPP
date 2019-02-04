﻿using Foundation;
using UIKit;
using CoreGraphics;

namespace AMDb.iOS.Views
{
    class MovieCells : UITableViewCell
    {
        // Helper values for positioning subviews in cell
        private double imageHeight, imageWidth;
        private int _cellHeight;
        private int _standardSpacing, _standardImageSpacing;
        private int _standardEstTextHeight;
        private double _cellTextPositionX;

        // Subviews that make up a movie cell
        UIImageView _imagePosterView;
        UILabel _titleAndYear;
        UILabel _starring;

        public MovieCells(NSString cellID, int cellHeight) : base(UITableViewCellStyle.Default, cellID)
        {
            // Initialize values
            this._standardSpacing = 5;
            this._standardEstTextHeight = 20;
            this.SelectionStyle = UITableViewCellSelectionStyle.Gray;
            this._standardImageSpacing = 8;
            this.imageHeight = cellHeight - _standardImageSpacing;
            this._standardImageSpacing = _standardImageSpacing / 2;
            this.imageWidth = imageHeight * 0.67;
            this._cellTextPositionX = imageWidth + _standardSpacing*2;
            this._cellHeight = cellHeight;

            // Generate view components and add them to subview
            // All empty, but modifiable with the UpdateCell function
            this._imagePosterView = _ImageView();
            this._titleAndYear = _TitleAndYear();
            this._starring = _Starring();
            this.ContentView.AddSubviews(new UIView[] { this._imagePosterView,
                                                        this._titleAndYear,
                                                        this._starring });

            // Add disclosure indicator to make cell clickable
            this.Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        // Modifies values of a cell based on movie name, release year, cast members
        // Sets image to the image corresponding to parameter poster path
        public void UpdateCell(string name, string year, string cast, string posterPath, string borderPath)
        {
            if (posterPath != null && posterPath != "") {
                
                this._imagePosterView.Image = UIImage.FromFile(posterPath);
            }
            this._titleAndYear.Text = name.ToUpper() + " (" + year + ")";
            this._starring.Text = cast;
        }

        // Generate appropriate frame for image
        private UIImageView _ImageView()
        {
            var imageView = new UIImageView() {
                
                Frame = new CGRect(_standardImageSpacing,
                                   _standardImageSpacing,
                                   imageWidth, imageHeight),
            };

            return imageView;
        }

        // Generate and optimizes title and year text
        // And positions it's frame
        private UILabel _TitleAndYear()
        {
            var titleAndYear = new UILabel() {
                
                Frame = new CGRect((int)_cellTextPositionX, _standardSpacing * 4,
                                   this.ContentView.Bounds.Width - _cellTextPositionX,
                                   _standardEstTextHeight),
                Font = UIFont.FromName("BanglaSangamMN-Bold", 14f),
                TextColor = UIColor.FromRGB(0, 122, 255),
                BackgroundColor = UIColor.Clear
            };

            return titleAndYear;
        }

        // Generate and optimizes starring cast text
        // And positions it's frame
        private UILabel _Starring()
        {
            var starring = new UILabel() {
                Frame = new CGRect((int)_cellTextPositionX, (_standardSpacing * 2) * 4,
                                   this.ContentView.Bounds.Width - _cellTextPositionX,
                                   _standardEstTextHeight),
                Font = UIFont.FromName("BanglaSangamMN", 12f),
                TextColor = UIColor.FromRGB(153, 153, 102),
                BackgroundColor = UIColor.Clear
            };

            return starring;
        }
    }
}