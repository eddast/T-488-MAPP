using System;

using Foundation;
using UIKit;

namespace IOSWeek1.iOS.Views
{
    public partial class PersonCollectionCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("PersonCollectionCell");
        public static readonly UINib Nib;

        static PersonCollectionCell()
        {
            Nib = UINib.FromName("PersonCollectionCell", NSBundle.MainBundle);
        }

        protected PersonCollectionCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
