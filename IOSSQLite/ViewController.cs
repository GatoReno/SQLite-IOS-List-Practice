using Foundation;
using IOSSQLite.models;
using System;
using System.IO;
using UIKit;

namespace IOSSQLite
{
    public partial class ViewController : UIViewController
    {
        private string pathToDb;
        public ViewController(IntPtr handle) : base(handle)
        {

            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            pathToDb = Path.Combine(docFolder, "contacts.db");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            SaveItemBtn.Clicked += SaveBtn_Clicked;
        }

        private void SaveBtn_Clicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            using (var conn = new SQLite.SQLiteConnection(pathToDb))
            {
                conn.Insert(new ContactModel() {
                    Name = NameContact.Text,
                    Email = EmailContact.Text
                });
            }
            NavigationController.PopToRootViewController(true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}