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
    /// Interaction logic for Email_General.xaml
    /// </summary>
    public partial class Email_General : UserControl
    {
        int check;
        public Email_General()
        {
            
            InitializeComponent();
            check = 0;
            
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM Email,current_user WHERE Email.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while(data.Read())
                    {
                        if (data["enable"].ToString() == "1") cb_enable.IsChecked = true; else cb_enable.IsChecked = false;
                        if (data["upKeystroke"].ToString() == "1") cb_upKeystroke.IsChecked = true; else cb_upKeystroke.IsChecked = false;
                        if (data["upScrshot"].ToString() == "1") cb_upScrshot.IsChecked = true; else cb_upScrshot.IsChecked = false;
                        if (data["upWebcam"].ToString() == "1") cb_upWebcam.IsChecked = true; else cb_upWebcam.IsChecked = false;
                        if (data["upWebsite"].ToString() == "1") cb_upWebsite.IsChecked = true; else cb_upWebsite.IsChecked = false;
                        if (data["clear"].ToString() == "1") cb_clear.IsChecked = true; else cb_clear.IsChecked = false;
                        tb_kbs.Text= data["limitSize"].ToString();
                        tb_hours.Text = data["hours"].ToString();
                        tb_minutes.Text = data["minutes"].ToString();
                    }
                }
            }
            if (cb_enable.IsChecked.Value == true)
            {
                tb_hours.IsEnabled = true;
                tb_minutes.IsEnabled = true;
            }
            else
            {
                tb_hours.IsEnabled = false;
                tb_minutes.IsEnabled = false;
            }
            if (cb_limitedSize.IsChecked.Value == false)
                tb_kbs.IsEnabled = false;
            else
                tb_kbs.IsEnabled = true;

        }

        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            if (check==1)
            {
                int enable = 0;
                int upKeyStroke = 0;
                int upScrshot = 0;
                int upWebcam = 0;
                int upWebsite = 0;
                int clear = 0;
                int upSize = 0;
                if (cb_enable.IsChecked.Value == true) enable = 1;
                if (cb_upKeystroke.IsChecked.Value == true) upKeyStroke = 1;
                if (cb_upScrshot.IsChecked.Value == true) upWebcam = 1;
                if (cb_upWebsite.IsChecked.Value == true) upWebsite = 1;
                if (cb_upScrshot.IsChecked.Value == true) upScrshot = 1;
                if (cb_upWebcam.IsChecked.Value == true) upWebcam = 1;
                if (cb_clear.IsChecked.Value == true) clear = 1;
                if (cb_limitedSize.IsChecked.Value == true) upSize = 1;
                int limitSize = 0;
                int hout = 0;
                int minutes = 0;
                if (enable == 1 && ((Int32.TryParse(tb_hours.Text, out hout) == false) || !Int32.TryParse(tb_minutes.Text, out minutes) || minutes < 0 || hout < 0 || (minutes == 0 && hout == 0)))
                {
                    MessageBox.Show("Hour, Minute should be a number", "Fail");
                }
                else if (upSize == 1 && ((!int.TryParse(tb_kbs.Text, out limitSize)) || limitSize <= 0))
                {
                    MessageBox.Show("Limit Size should be a number", "Fail");
                }
                else
                {
                    using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                    {
                        connect.Open();
                        using (SQLiteCommand fmd = connect.CreateCommand())
                        {
                            SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"UPDATE Email SET enable=" + enable + ",hours ="+hout+" ,minutes="+minutes+",upKeystroke=" + upKeyStroke + ",upScrshot=" + upScrshot + ",upWebcam=" + upWebcam + ",upWebsite=" + upWebsite + ",limitSize=" + limitSize + ",clear=" + clear + " WHERE Email.id = (SELECT current_user.id FROM current_user)", connect);
                            sqlComm_Alert.ExecuteNonQuery();
                        }
                    }
                    if (enable == 1)
                    {
                        MainWindow.aTimer_sendMail.Stop();
                        Functions.SetTimerSendMail(hout * 120 * 1000 + minutes * 60 * 1000);
                        MainWindow.aTimer_sendMail.Start();
                    }
                    else
                    {
                        MainWindow.aTimer_sendMail.Stop();
                    }
                }
            }
            
        }

        private void tb_hours_TextChanged(object sender, TextChangedEventArgs e)
        {
            check = 1;
        }

        private void tb_minutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            check = 1;
        }

        private void cb_enable_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            if (cb_enable.IsChecked.Value==false)
            {
                tb_hours.IsEnabled = false;
                tb_minutes.IsEnabled = false;
            }
            else
            {
                tb_hours.IsEnabled = true;
                tb_minutes.IsEnabled = true;
            }
        }

        private void cb_upKeystroke_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_upScrshot_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_upWebcam_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_upWebsite_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_limitedSize_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            if (cb_limitedSize.IsChecked.Value == false)
            {
                tb_kbs.IsEnabled = false;
            }
            else
                tb_kbs.IsEnabled = true;
        }

        private void tb_kbs_TextChanged(object sender, TextChangedEventArgs e)
        {
            check = 1;
        }

        private void cb_clear_Checked(object sender, RoutedEventArgs e)
        {
            check = 1;
        }
    }
}
