using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Interaction logic for Screenshot.xaml
    /// </summary>
    public partial class Screenshot : UserControl
    {
        
        int check;
        public Screenshot()
        {
            InitializeComponent();
            check = 0;
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT DISTINCT * FROM Screenshot,current_user WHERE Screenshot.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        if (data["enable"].ToString() == "1") cb_enable.IsChecked = true; else cb_enable.IsChecked = false;
                        if (data["enDel"].ToString() == "1") cb_enDel.IsChecked = true; else cb_enDel.IsChecked = false;
                        if (data["doubleScr"].ToString() == "1") cb_doubleScr.IsChecked = true; else cb_doubleScr.IsChecked = false;
                        if (data["timeNuser"].ToString() == "1") cb_timeNuser.IsChecked = true; else cb_timeNuser.IsChecked = false;
                        tb_hours.Text = data["hours"].ToString();
                        tb_minutes.Text = data["minutes"].ToString();
                        cb_daysDel.Text = data["daysDel"].ToString();
                        double qa;
                        double.TryParse(data["quality"].ToString(), out qa);
                        sld_quaity.Value = qa;
                    }
                    connect.Close();
                }
            }
            ProcessStartInfo startupload = new ProcessStartInfo();
            startupload.WorkingDirectory = @"..\..\module\";
            startupload.FileName = "upScreenshot.exe";
            startupload.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(startupload);
            int hours;
            int minutes;
            int.TryParse(tb_hours.Text, out hours);
            int.TryParse(tb_minutes.Text,out minutes);
            
            if (cb_enable.IsChecked == true)
            {
                MainWindow.aTimer_scrshot.Stop();
                Functions.SetTimerScreenShot(hours * 1200 * 1000 + minutes * 60 * 1000);
                //aTimer.Stop();
                //SetTimer(hour * 1200 * 1000 + minute * 60 * 1000);
                MainWindow.aTimer_scrshot.Start();
            }
            else
            {
                if(MainWindow.aTimer_scrshot.Enabled)
                    MainWindow.aTimer_scrshot.Stop();
            }
        }

        private void bt_OK_Click(object sender, RoutedEventArgs e)
        {
            if (check==1)
            {
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {
                        int enable = 0;
                        int hour = 0;
                        int minute = 0;
                        int timeNuser = 0;
                        int doubleScr = 0;
                        int enDel = 0;
                        int daysDel = 0;
                        int quality = 0;
                        if (cb_doubleScr.IsChecked == true) doubleScr = 1;
                        if (cb_enDel.IsChecked == true) enDel = 1;
                        if (cb_timeNuser.IsChecked == true) timeNuser = 1;
                        quality = (int)sld_quaity.Value;
                        if (cb_enable.IsChecked.Value == true) enable = 1;
                        if (enable == 1 && (!int.TryParse(tb_hours.Text, out hour) || hour < 0 || !int.TryParse(tb_minutes.Text, out minute) || minute < 0 || (minute == 0 && hour == 0)))
                        {
                            MessageBox.Show("Invalid hours, minutes", "Fail");
                        }
                        else if (enDel==1&&(!int.TryParse(cb_daysDel.Text,out daysDel)||daysDel<=0))
                            MessageBox.Show("Invalid days input", "Fail");
                        else
                        {
                            if (enDel == 0)
                            {
                                SQLiteCommand sqlComm_Alert1 = new SQLiteCommand(@"UPDATE Screenshot SET enable= " + enable + ", hours=" + hour + ", minutes=" + minute + ",timeNuser=" + timeNuser + ",doubleScr=" + doubleScr + ",enDel=" + enDel + ",daysDel=" + daysDel + ",quality=" + quality + " WHERE Screenshot.id = (SELECT current_user.id FROM current_user)", connect);
                                sqlComm_Alert1.ExecuteNonQuery();
                            }
                            else
                            {
                                SQLiteCommand sqlComm_Alert1 = new SQLiteCommand(@"UPDATE Screenshot SET enable= " + enable + ", hours=" + hour + ", minutes=" + minute + ",timeNuser=" + timeNuser + ",doubleScr=" + doubleScr + ",enDel=" + enDel + ",daysDel=" + daysDel + ",quality=" + quality + ",datetime='"+ DateTime.Now.ToString() +"' WHERE Screenshot.id = (SELECT current_user.id FROM current_user)", connect);
                                sqlComm_Alert1.ExecuteNonQuery();
                            }
                            if (enable == 1)
                            {
                                MainWindow.aTimer_scrshot.Stop();
                                Functions.SetTimerScreenShot(hour * 1200 * 1000 + minute * 60 * 1000);
                                //aTimer.Stop();
                                //SetTimer(hour * 1200 * 1000 + minute * 60 * 1000);
                                MainWindow.aTimer_scrshot.Start();
                            }
                            else
                            {
                                MainWindow.aTimer_scrshot.Stop();
                            }
                        }
                    }
                }
                Console.WriteLine("done");
                Functions.syncUp();
            }
        }

        private void cb_enable_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void tb_hours_TextChanged(object sender, TextChangedEventArgs e)
        {
            check = 1;
        }

        private void tb_minutes_TextChanged(object sender, TextChangedEventArgs e)
        {
            check = 1;
        }

        private void cb_timeNuser_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_doubleScr_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_enDel_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
        }

        private void cb_daysDel_TextChanged(object sender, TextChangedEventArgs e)
        {
            check = 1;
        }

        private void sld_quaity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            check = 1;
        }
    }
}
