using Foundation;
using IOSSQLite.models;
using System;
using System.Collections.Generic;
using System.IO;
using UIKit;

namespace IOSSQLite
{


    public partial class ContactViewController : UITableViewController
    {
        private string pathToDb;
        private IList<ContactModel> contacts;
        public ContactViewController (IntPtr handle) : base (handle)
        {
           contacts = new List<ContactModel>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            pathToDb = Path.Combine(docFolder, "contacts.db");

            using (var conn = new SQLite.SQLiteConnection(pathToDb))
            {
                conn.CreateTable<ContactModel>();
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            contacts = new List<ContactModel>();
            using (var conn = new SQLite.SQLiteConnection(pathToDb))
            {
                var qu = conn.Table<ContactModel>();
                foreach (var c in qu)
                {
                    contacts.Add(c);
                    TableView.ReloadData();
                }
            }
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return contacts.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //return base.GetCell(tableView, indexPath);
            UITableViewCell cell = tableView.DequeueReusableCell("CellT");
            var data = contacts[indexPath.Row];
            cell.TextLabel.Text = data.Name;
            cell.DetailTextLabel.Text = data.Email;
            return cell;
        }
    }
}