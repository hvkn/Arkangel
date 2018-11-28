using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Target_ByApp.xaml
    /// </summary>
    public partial class Target_ByApp : Window
    {
        private Target _target = null;
        public Target_ByApp(Target target)
        {
            
            InitializeComponent();
            _target = target;
            Process[] processlist = Process.GetProcesses();

            foreach (Process p in processlist)
            {
                if (!String.IsNullOrEmpty(p.MainWindowTitle))
                {
                    list_app.Items.Add(p.MainWindowTitle);
                }
            }
        }

        private void bt_add_Click(object sender, RoutedEventArgs e)
        {
            _target.target_list.Items.Add(list_app.SelectedItem);
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
