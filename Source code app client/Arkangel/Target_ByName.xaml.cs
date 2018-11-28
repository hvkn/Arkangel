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

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for Target_ByName.xaml
    /// </summary>
    public partial class Target_ByName : Window
    {
        private Target _target;
        public Target_ByName(Target target)
        {
            _target = target;
            InitializeComponent();
        }

        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void bt_Add_Click(object sender, RoutedEventArgs e)
        {
            _target.target_list.Items.Add(tb_text.Text);
        }
    }
}
