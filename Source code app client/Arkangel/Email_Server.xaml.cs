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
    /// Interaction logic for Email_Server.xaml
    /// </summary>
    public partial class Email_Server : UserControl
    {
        public Email_Server()
        {
            InitializeComponent();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM EmailDelivery,current_user WHERE EmailDelivery.id = current_user.id",connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        tb_stmp.Text = data["smpt"].ToString();
                        tb_uname.Text = data["uname"].ToString();
                        tb_password.Password = data["password"].ToString();
                        tb_sendto.Text = data["sendto"].ToString();
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
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"UPDATE EmailDelivery SET sendto = '"+tb_sendto.Text +"',smpt='"+tb_stmp.Text+"',uname='"+tb_uname.Text+"',password='"+tb_password.Password+"' WHERE EmailDelivery.id = (SELECT current_user.id FROM current_user)",connect);
                    sqlComm_Alert.ExecuteNonQuery();
                }
            }

        }
    }
}
