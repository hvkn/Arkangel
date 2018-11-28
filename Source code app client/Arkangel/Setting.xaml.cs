using System;
using System.Collections.Generic;
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
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SQLite;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * FROM Setting,current_user WHERE Setting.id=current_user.id", connect);
                    SQLiteDataReader data = sqlComm.ExecuteReader();
                    while (data.Read())
                    {
                        tb_Keystroke.Text = data["textLog"].ToString();
                        tb_Webcam.Text = data["webcamLog"].ToString();
                        tb_Scrshot.Text = data["screenshotLog"].ToString();
                        tb_Wslogs.Text = data["websiteLog"].ToString();
                        if (data["keystrokeMode"].ToString() == "0") cbb_keystroke.SelectedIndex = 0; else cbb_keystroke.SelectedIndex = 1;
                        tb_profilepath.Text = data["profilePath"].ToString();
                    }
                    connect.Close();
                }
            }
        }

        private void bt_quit_Click(object sender, RoutedEventArgs e)
        {
            
            if (System.Windows.Forms.MessageBox.Show("Do you save setting data", "Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {
                        string textLog = tb_Keystroke.Text;
                        string webcamLog = tb_Webcam.Text;
                        string screenshotLog = tb_Scrshot.Text;
                        int keystrokeMode = cbb_keystroke.SelectedIndex;
                        string profilePath = tb_profilepath.Text;
                        SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"UPDATE Setting SET textLog= '" + textLog + "', webcamLog='" + webcamLog + "', screenshotLog= '" + screenshotLog + "',keystrokeMode=" + keystrokeMode + ",profilePath='" + profilePath + "',websiteLog='"+tb_Wslogs.Text+"'  WHERE Setting.id = (SELECT current_user.id FROM current_user)", connect);
                        sqlComm_Alert.ExecuteNonQuery();
                    }
                    connect.Close();
                }
                Functions.syncUp();
            }
            Close();
        }

        private void bt_browserText_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    tb_Keystroke.Text = fbd.SelectedPath.ToString();
                }
            }
        }
        private void bt_browserWebcam_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    tb_Webcam.Text = fbd.SelectedPath.ToString();
                }
            }
        }

        private void bt_browerScrshot_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    tb_Scrshot.Text = fbd.SelectedPath.ToString();
                }
            }
        }

        private void bt_profile_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void bt_brower_Wslogs_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    tb_Wslogs.Text = fbd.SelectedPath.ToString();
                }
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
             DragMove(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Owner.IsEnabled = false;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
        }
    }
}
