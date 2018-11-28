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
    /// Interaction logic for Email.xaml
    /// </summary>
    public partial class Email : UserControl
    {
        int check;
        
        
        public Email()
        {
            
            InitializeComponent();
            Email_General general = new Email_General();
            check = 1;
            GridMain.Children.Clear();
            GridMain.Children.Add(general);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            
            GridCursor.Margin = new Thickness( (150 * index), 0, 0, 0);

            switch (index)
            {
                case 0:
                    GridMain.Children.Clear();
                    Email_General general = new Email_General();
                    GridMain.Children.Add(general);
                    check = 1;
                    break;
                case 1:
                    Email_Server server = new Email_Server();
                    GridMain.Children.Clear();
                    GridMain.Children.Add(server);
                    check = 2;
                    break;

            }
        }

        
    }
}
