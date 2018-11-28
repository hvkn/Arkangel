using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Arkangel
{
    /// <summary>
    /// Interaction logic for CB_File.xaml
    /// </summary>
    public partial class CB_File : Window
    {
        public CB_File()
        {
            InitializeComponent();
            System.Collections.Specialized.StringCollection listFile = null;
            listFile = System.Windows.Clipboard.GetFileDropList();
            //  Console.WriteLine(listFile.ToString());
            if (listFile.Count != 0)
            {
                this.Show();
                string[] array = new string[1000];

                listFile.CopyTo(array, 0);
                //   listFile.CopyTo(array, 1);
                int n = listFile.Count;
                for (int i = 0; i < n; i++)
                {
                    list_file.Items.Add(array[i]);
                }
            }
        }
        public System.Collections.Specialized.StringCollection SwapClipboardFileDropList(
            System.Collections.Specialized.StringCollection replacementList)
        {
            System.Collections.Specialized.StringCollection returnList = null;
            if (System.Windows.Clipboard.ContainsFileDropList())
            {
                returnList = System.Windows.Clipboard.GetFileDropList();
                System.Windows.Clipboard.SetFileDropList(replacementList);
            }
            return returnList;
        }
        private void bt_change_Click(object sender, RoutedEventArgs e)
        {
            list_file.Items.Clear();
            System.Collections.Specialized.StringCollection listFile = new System.Collections.Specialized.StringCollection();
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in fileDialog.FileNames)
                {
                    listFile.Add(file);
                    list_file.Items.Add(file);
                }
                SwapClipboardFileDropList(listFile);
            }
        }
    }
}
