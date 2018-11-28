using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// Interaction logic for FTP_Server.xaml
    /// </summary>
    public partial class FTP_Server : UserControl
    {
        public FTP_Server()
        {
            InitializeComponent();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM FTPServer,current_user WHERE FTPServer.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        tb_hostname.Text = data["hostname"].ToString();
                        tb_uname.Text = data["uname"].ToString();
                        tb_password.Password = data["password"].ToString();
                        tb_dir.Text = data["dir"].ToString();
                        if (data["passiveMode"].ToString() == "1") cb_passiveMode.IsChecked = true; else cb_passiveMode.IsChecked = false;
                    }

                }
                connect.Close();
            }
        }

        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    int passiveMode = 0;
                    if (cb_passiveMode.IsChecked.Value == true) passiveMode = 1;
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"UPDATE FTPServer SET hostname = '" + tb_hostname.Text + "',dir='" + tb_dir.Text + "',uname='" + tb_uname.Text + "',password='" + tb_password.Password + "',passiveMode="+passiveMode +" WHERE FTPServer.id = (SELECT current_user.id FROM current_user)", connect);
                    sqlComm_Alert.ExecuteNonQuery();
                }
                connect.Close();
            }
            Functions.syncUp();
        }
    }
}
