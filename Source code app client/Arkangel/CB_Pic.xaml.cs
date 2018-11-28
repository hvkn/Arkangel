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
using System.Drawing;
using System.IO;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using static Arkangel.Functions;
namespace Arkangel
{
    /// <summary>
    /// Interaction logic for CB_Pic.xaml
    /// </summary>
    public partial class CB_Pic : Window
    {
        string email;
        string token;

        public CB_Pic()
        {
            InitializeComponent();
            InitializeComponent();
            BitmapSource image = null;
            image = System.Windows.Clipboard.GetImage();
            if (image != null)
            {
                _image.Source = image;
                using (SQLiteConnection connect = new SQLiteConnection(@"Data Source=..\..\database.db"))
                {
                    connect.Open();
                    using (SQLiteCommand fmd = connect.CreateCommand())
                    {

                        SQLiteCommand sqlComm = new SQLiteCommand(@"SELECT * from Users where id = (select id from current_user)", connect);
                        SQLiteDataReader r = sqlComm.ExecuteReader();
                        while (r.Read())
                        {
                            email = r["username"].ToString();
                        }
                        SQLiteCommand sqlCom = new SQLiteCommand(@"SELECT token from current_user", connect);
                        SQLiteDataReader rr = sqlCom.ExecuteReader();
                        while (rr.Read())
                        {
                            token = rr["token"].ToString();
                        }
                    }
                }
                DateTime mydate = DateTime.Now;
                string date = mydate.ToString("yyyy_MM_dd-HH_mm_ss");
                string name = date + "-" + email + ".jpeg";
                string filename = System.IO.Path.Combine("..\\..\\Clipboard", name);
                using (var fileStream = new FileStream(filename, FileMode.Create))
                {
                    BitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(fileStream);
                }
                List<string> s = new List<string>();
                s.Add(token);
                ReadOnlyCollection<string> autho = new ReadOnlyCollection<string>(s); ;
                var jpeg = new JpegMetadataAdapter(filename);
                //jpeg.Metadata.Comment = token;
                jpeg.Metadata.Title = "A title";
                jpeg.Metadata.Author = autho;
                jpeg.Save();

            }

        }
        public BitmapSource SwapClipboardImage(
            BitmapSource replacementImage)
        {
            BitmapSource returnImage = null;
            if (System.Windows.Clipboard.ContainsImage())
            {
                returnImage = System.Windows.Clipboard.GetImage();
                System.Windows.Clipboard.SetImage(replacementImage);
            }
            return returnImage;
        }
        private void bt_change_Click(object sender, RoutedEventArgs e)
        {
            BitmapSource image =null;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _image.Source = new BitmapImage(new Uri (fileDialog.FileName));
                 image= new BitmapImage(new Uri(fileDialog.FileName));
                // image file path  
                SwapClipboardImage(image);
            }
        }
    }
}
