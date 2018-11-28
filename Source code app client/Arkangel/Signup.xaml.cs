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
using System.Windows.Shapes;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void btn_Signup_Click(object sender, RoutedEventArgs e)
        {
            string sign_up = "";
            if (tb_password.Password==tb_repassword.Password && IsValidEmail(tb_username.Text))
            {
                sign_up= Functions.Signup(tb_username.Text, tb_password.Password);
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {

                        if (sign_up != "Error")
                        {
                            MainWindow bs = new MainWindow();
                            bs._username.Text = tb_username.Text;
                            //Update token xuong database
                            SQLiteCommand getid = new SQLiteCommand(@"UPDATE current_user SET id=1,token='" + sign_up.Split('\"')[1] + "'", connect);
                            getid.ExecuteNonQuery();
                            SQLiteCommand upusername = new SQLiteCommand(@"UPDATE Users SET id=1, username= '" + tb_username.Text + "', password='" + Functions.Base64Encode(tb_password.Password) + "' ", connect);
                            upusername.ExecuteNonQuery();
                            Functions.syncDown();
                            Functions.CheckUser();
                            bs.Show();
                            this.Owner.Close();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("That email is used already");
                        }
                        connect.Close();
                    }
                }
            }
            
        }

        private void tb_username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(IsValidEmail(tb_username.Text))
            {
                true_mail.Visibility = Visibility.Visible;
                false_mail.Visibility = Visibility.Hidden;
            }
            else
            {
                false_mail.Visibility = Visibility.Visible;
                true_mail.Visibility = Visibility.Hidden;
            }
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void tb_repassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tb_password.Password == tb_repassword.Password)
            {
                true_password.Visibility = Visibility.Visible;
                false_password.Visibility = Visibility.Hidden;
            }
            else
            {
                true_password.Visibility = Visibility.Hidden;
                false_password.Visibility = Visibility.Visible;
            }
        }
    }
}
