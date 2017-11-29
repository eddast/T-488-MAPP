using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;
using IOSWeek1.iOS.Views;

namespace IOSWeek1.iOS.Controllers
{
    public class NameListDataSource : UITableViewSource
    {
        private readonly List<Person> _personlist;
        public readonly NSString NameListCellId = new NSString("NameListCell");
        private readonly Action<int> _onSelectedPerson;

        public NameListDataSource( List<Person> nameList, Action<int> onSelectedPerson ) { this._personlist = nameList; _onSelectedPerson = onSelectedPerson; }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath) {

            var cell = (PersonCell) tableView.DequeueReusableCell((NSString)this.NameListCellId);

            if (cell == null) { 
                 cell = new PersonCell( this.NameListCellId );
            } var person = this._personlist[indexPath.Row];
            cell.UpdateCell(person.Name, person.BirthYear.ToString(), person.ImageName);

            return cell;

        }

        public override nint RowsInSection(UITableView tableview, nint section) { return this._personlist.Count; }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath) {
            tableView.DeselectRow(indexPath, true);
            this._onSelectedPerson(indexPath.Row);
        }
    }
}