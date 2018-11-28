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
    /// Interaction logic for Alert.xaml
    /// </summary>
    /// 
    public partial class Alert : UserControl
    {
        int check;
        public Alert()
        {
            check = 0;

            InitializeComponent();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT sendMail,scrShot FROM Alerts,current_user WHERE Alerts.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {

                        if (data["sendMail"].ToString() == "1")
                        {
                            cb_sendMail.IsChecked = true;
                        }
                        else cb_sendMail.IsChecked = false;
                        if ((string)data["scrShot"].ToString() == "1")
                        {
                            cb_scrShot.IsChecked = true;
                        }
                        else cb_scrShot.IsChecked = false;
                    }
                    SQLiteCommand sqlComm_AlertList = new SQLiteCommand(@"SELECT key FROM AlertList WHERE id =(SELECT current_user.id FROM current_user)", connect);
                    SQLiteDataReader data2 = sqlComm_AlertList.ExecuteReader();
                    while (data2.Read())
                    {
                        keyword_list.Items.Add(data2["key"].ToString());
                    }

                }
            }
        }
        //synccccc
       



        private void bt_add_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            Alert_Add alert_Add = new Alert_Add(this);
            alert_Add.Show();
            
        }

        private void bt_delete_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            keyword_list.Items.Remove(keyword_list.SelectedItem);
        }
        //public class _Alert
        //{
        //    //int id;
        //    //string key;
        //}
        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            if (check == 1)
            {
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    connect.Open();
                    int scrShot = 0;
                    int sendMail = 0;
                    if (cb_scrShot.IsChecked.Value == true) scrShot = 1;
                    if (cb_sendMail.IsChecked.Value == true) sendMail = 1;
                    SQLiteCommand sqlUpdateAlert = new SQLiteCommand(@"UPDATE Alerts SET sendMail="+sendMail+", scrShot="+scrShot+" WHERE Alerts.id IN (SELECT current_user.id from current_user)", connect);
                    sqlUpdateAlert.ExecuteNonQuery();
                 //   sqlUpdateAlert.Dispose();
                    SQLiteCommand getuser = new SQLiteCommand(@"SELECT Alerts.id FROM Alerts,current_user Where Alerts.id=current_user.id",connect);
                    SQLiteDataReader data = getuser.ExecuteReader();
                    while (data.Read())
                    {
                        
                        string user = data["id"].ToString();
                        SQLiteCommand sqldelete = new SQLiteCommand(@"DELETE from AlertList where id ="+user, connect);
                        sqldelete.ExecuteNonQuery();
                        for (int i =0;i<keyword_list.Items.Count;i++)
                        {
                            SQLiteCommand sqlUpdateAlertList = new SQLiteCommand(@"INSERT INTO AlertList VALUES (" + user + ",'" + keyword_list.Items[i].ToString() + "')", connect);
                            sqlUpdateAlertList.ExecuteNonQuery();
                        }
                    }
                    connect.Close();
                }
                Functions.syncUp();
            }
        }

        private void cb_sendMail_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_scrShot_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }
    }
}
    
