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
    /// Interaction logic for Alert_Add.xaml
    /// </summary>
    /// 

    public partial class Alert_Add : Window
    {
        private Alert _form = null;
        public Alert_Add(Alert form)
        {
            InitializeComponent();
            _form = form;
        }

        private void bt_Add_Click(object sender, RoutedEventArgs e)
        {
            _form.keyword_list.Items.Add(tb_text.Text);
            Close();
        }

        private void bt_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    
}
