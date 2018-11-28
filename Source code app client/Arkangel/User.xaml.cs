using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.DirectoryServices;
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
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        public User()
        {
            InitializeComponent();

            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlConn_monitorUser = new SQLiteCommand(@"SELECT DISTINCT * FROM monitor_user,current_user WHERE monitor_user.id = current_user.id", connect);
                    SQLiteDataReader mU = sqlConn_monitorUser.ExecuteReader();
                    while (mU.Read())
                    {
                        if (mU["enable"].ToString() == "1")
                            rad_allUser.IsChecked = true;
                        else
                            rad_allUser.IsChecked = false;

                        if (mU["enable"].ToString() == "2")
                            rad_currentUser.IsChecked = true;
                        else
                            rad_currentUser.IsChecked = false;

                        if (mU["enable"].ToString() == "3")
                            rad_followingUser.IsChecked = true;
                        else
                            rad_followingUser.IsChecked = false;
                    }
                    SQLiteCommand sqlConn_UserList1 = new SQLiteCommand(@"SELECT user FROM user_list WHERE id = (SELECT current_user.id FROM current_user)", connect);
                    // SQLiteDataReader ul1 = sqlConn_UserList1.ExecuteReader();
                    object re = sqlConn_UserList1.ExecuteScalar();
                    if (re == null)
                    Functions.FindUsers();
                    SQLiteCommand sqlConn_UserList = new SQLiteCommand(@"SELECT user FROM user_list WHERE id = (SELECT current_user.id FROM current_user)", connect);
                    SQLiteDataReader ul = sqlConn_UserList.ExecuteReader();
                    // ad 2 lan // bug
                    //Console.WriteLine(ul.StepCount);
                    
                    while (ul.Read())
                    {
                        //Console.WriteLine(ul.GetString(0));
                        //if (ul["user"].ToString() != null)
                        //{
                            user_list.Items.Add(ul.GetString(0));
                       // }
                    }
                }
                connect.Close();
            }
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    int enable = 0;
                    string currentUser = Environment.UserName;
                    if (rad_allUser.IsChecked == true) enable = 1;
                    if (rad_currentUser.IsChecked == true) enable = 2;
                    if (rad_followingUser.IsChecked == true) enable = 3;
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"UPDATE monitor_user SET enable= " + enable + ",current_user='" + currentUser + "' WHERE monitor_user.id = (SELECT current_user.id FROM current_user)", connect);
                    sqlComm_Alert.ExecuteNonQuery();
                    SQLiteCommand sqlConn_Del = new SQLiteCommand(@"DELETE FROM user_list WHERE user_list.id = (SELECT current_user.id FROM current_user) ", connect);
                    sqlConn_Del.ExecuteNonQuery();
                    for (int i = 0; i < user_list.Items.Count; i++)
                    {
                        SQLiteCommand sqlConn = new SQLiteCommand(@"INSERT INTO user_list VALUES ((SELECT current_user.id FROM current_user),'" + user_list.Items[i].ToString() + "')", connect);
                        sqlConn.ExecuteNonQuery();
                    }

                }
            }
            Functions.syncUp();
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            user_list.Items.Remove(user_list.SelectedItem);
            if(user_list.Items.Count > 0)
                user_list.SelectedItem = user_list.Items[0];
        }

        private void btn_LoadUser_Click(object sender, RoutedEventArgs e)
        {
            if (user_list.Items.Count > 0)
            {
                user_list.Items.Clear();
                user_list.Items.Refresh();
                user_list.Items.Remove(user_list.SelectedItems);
            }
            DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
            DirectoryEntry admGroup = localMachine.Children.Find("administrators", "group");
            object members = admGroup.Invoke("members", null);
            foreach (object groupMember in (IEnumerable)members)
            {
                DirectoryEntry member = new DirectoryEntry(groupMember);
                user_list.Items.Add(member.Name.ToString());
            }
        }
    }
}
