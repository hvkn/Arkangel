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
    /// Interaction logic for FTP.xaml
    /// </summary>
    public partial class FTP : UserControl
    {
        public FTP()
        {
            InitializeComponent();
            FTP_General general = new FTP_General();
            GridMain.Children.Clear();
            GridMain.Children.Add(general);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            GridCursor.Margin = new Thickness((150 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                        FTP_General general = new FTP_General();
                        GridMain.Children.Clear();
                        GridMain.Children.Add(general);
                    break;
                case 1:
                    FTP_Server server = new FTP_Server();
                    GridMain.Children.Clear();
                    GridMain.Children.Add(server);
                    break;
                    
            }
        }
    }
}
