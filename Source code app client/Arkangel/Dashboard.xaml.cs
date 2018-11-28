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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private MainWindow _mainform=null;
        public Dashboard(MainWindow mainform)
        {
            InitializeComponent();
            string partOfDay;
            var hours = DateTime.Now.Hour;
            if (hours > 16)
            {
                partOfDay = "Good Evening";
            }
            else if (hours > 11)
            {
                partOfDay = "Good Afternoon";
            }
            else
            {
                partOfDay = "Good Morning";
            }
            tb_hello.Text = partOfDay;
           
            _mainform = mainform;
            _mainform.bt_home.Visibility = Visibility.Hidden;
        }

        private void bt_screenshot_Click(object sender, RoutedEventArgs e)
        {
            Screenshot screenshot = new Screenshot();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(screenshot);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 7;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Alert alert = new Alert();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(alert);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 8;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FTP ftp = new FTP();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(ftp);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 2;
        }

        private void btn_General_Click(object sender, RoutedEventArgs e)
        {
            General general = new General();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(general);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 0;
        }

        private void btn_Clipboard_Click(object sender, RoutedEventArgs e)
        {
            _Clipboard clipboard = new _Clipboard();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(clipboard);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 1;
        }

        private void bt_Webcam_Click(object sender, RoutedEventArgs e)
        {
            Webcam webcam = new Webcam();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(webcam);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 3;
        }

        private void bt_Email_Click(object sender, RoutedEventArgs e)
        {
            Email email = new Email();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(email);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 8;
        }

        private void bt_Target_Click(object sender, RoutedEventArgs e)
        {
            Target target = new Target();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(target);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 4;
        }

        private void bt_User_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(user);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 5;
        }

        private void bt_websiteusage_Click(object sender, RoutedEventArgs e)
        {
            Website_Usage website_Usage = new Website_Usage();
            _dasboard.Children.Clear();
            _dasboard.Children.Add(website_Usage);
            _mainform.bt_home.Visibility = Visibility.Visible;
            _mainform.allIkon.SelectedIndex = 6;
        }
    }
}
