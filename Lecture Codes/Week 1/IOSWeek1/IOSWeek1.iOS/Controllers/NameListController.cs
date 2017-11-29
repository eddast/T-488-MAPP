using System;
using System.Collections.Generic;
using UIKit;

namespace IOSWeek1.iOS.Controllers
{
    public class NameListController : UITableViewController
    {
        private readonly List<Person> _personlist;

        public NameListController(List<Person> personlist) { this._personlist = personlist; }

        public override void ViewDidLoad() {
            base.ViewDidLoad();
            this.Title = "Persons";
            this.TableView.Source = new NameListDataSource(this._personlist, _onSelectedPersons); 
        }

        private void _onSelectedPersons(int row)
        {
            var okAlertController = UIAlertController.Create("Person selected", this._personlist[row].Name, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            this.PresentViewController(okAlertController, true, null);
        }
    }
}