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
    /// Interaction logic for Target.xaml
    /// </summary>
    
    public partial class Target : UserControl
    {
        int check;
        public Target()
        {
            check= 0;
            InitializeComponent();
            
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT DISTINCT * FROM Targets,current_user WHERE Targets.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {

                        if (data["enAllApp"].ToString() == "1")
                        {
                            cb_enAllApp.IsChecked = true;
                        }
                        else cb_enAllApp.IsChecked = false;
                        if ((string)data["enFollowApp"].ToString() == "1")
                        {
                            cb_FollowApp.IsChecked = true;
                        }
                        else cb_FollowApp.IsChecked = false;
                    }
                    SQLiteCommand sqlComm_AlertList = new SQLiteCommand(@"SELECT byApp FROM TargetList WHERE id =(SELECT current_user.id FROM current_user)", connect);
                    SQLiteDataReader data2 = sqlComm_AlertList.ExecuteReader();
                    while (data2.Read())
                    {
                        target_list.Items.Add(data2["byApp"].ToString());
                    }
                }
                
            }
            if (cb_FollowApp.IsChecked.Value == true)
            {
                target_list.IsEnabled = true;
            }
            else
            {
                target_list.IsEnabled = false;
            }
        }

        private void bt_ByApp_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            Target_ByApp target_ByApp = new Target_ByApp(this);
            target_ByApp.Show();
        }

        private void bt_delete_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            target_list.Items.Remove(target_list.SelectedItem);
        }

        private void bt_byname_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            Target_ByName target_ByName = new Target_ByName(this);
            target_ByName.Show();
               
        }

        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            if(check==1)
            {
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    connect.Open();
                    int enAllApp = 0;
                    int enFollowApp = 0;
                    if (cb_enAllApp.IsChecked.Value == true) enAllApp = 1;
                    if (cb_FollowApp.IsChecked.Value == true) enFollowApp = 1;
                    SQLiteCommand sqlUpdateAlert = new SQLiteCommand(@"UPDATE Targets SET enAllApp=" + enAllApp + ", enFollowApp=" + enFollowApp + " WHERE Targets.id IN (SELECT current_user.id from current_user)", connect);
                    sqlUpdateAlert.ExecuteNonQuery();
                    //   sqlUpdateAlert.Dispose();
                    SQLiteCommand getuser = new SQLiteCommand(@"SELECT id FROM current_user", connect);
                    SQLiteDataReader data = getuser.ExecuteReader();
                    while (data.Read())
                    {
                        string user = data["id"].ToString();
                        SQLiteCommand sqldelete = new SQLiteCommand(@"DELETE from TargetList where id =" + user, connect);
                        sqldelete.ExecuteNonQuery();
                        for (int i = 0; i < target_list.Items.Count; i++)
                        {

                            SQLiteCommand sqlUpdateAlertList = new SQLiteCommand(@"INSERT INTO TargetList VALUES (" + user + ",'" + target_list.Items[i].ToString() + "','')", connect);
                            sqlUpdateAlertList.ExecuteNonQuery();
                        }
                    }
                    connect.Close();
                }
                Functions.syncUp();
            }
            
        }

        private void cb_enAllApp_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            cb_FollowApp.IsChecked = false;
            target_list.IsEnabled = false;
        }

        private void cb_FollowApp_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            cb_enAllApp.IsChecked = false;
            target_list.IsEnabled = true;
        }
    }
}
