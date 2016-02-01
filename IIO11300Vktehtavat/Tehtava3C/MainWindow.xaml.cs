using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tehtava3C
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtFolder.Text = ConfigurationManager.AppSettings["DirPath"].ToString();
        }


        private void btnGetFiles_Click(object sender, RoutedEventArgs e)
        {
            try {
                DirectoryInfo dinfo = new DirectoryInfo(txtFolder.Text);
                FileInfo[] files = dinfo.GetFiles("*.txt");
                foreach (FileInfo file in files)
                {
                    lbFiles.Items.Add(file.Name);
                }
                tbNotification.Text = "Found " + lbFiles.Items.Count.ToString() + " files";
            }
            catch(Exception ex)
            {
                tbNotification.Text = "Not a valid path";
            }
        }

        private void btnCombine_Click(object sender, RoutedEventArgs e)
        {
            string path = txtFolder.Text;
            StringBuilder sb = new StringBuilder();
            try { 
                for (int i = 0; i < lbFiles.Items.Count; i++)
                {
                    //System.Diagnostics.Debug.WriteLine(lbFiles.Items[i].ToString());
                    //System.Diagnostics.Debug.WriteLine(path + "\\" + lbFiles.Items[i].ToString());
                    sb.Append(File.ReadAllText(path + "\\" + lbFiles.Items[i].ToString()));
                }
            }
            catch(Exception ex) 
            {
                tbNotification.Text = "Cannot read from file";
            }
            //System.Diagnostics.Debug.WriteLine(sb.ToString());
            try {

                File.WriteAllText(txtResultFile.Text, sb.ToString());
            }
            catch(Exception ex)
            {
                tbNotification.Text = "Not a valid file path";
            }
        }
    }
}
