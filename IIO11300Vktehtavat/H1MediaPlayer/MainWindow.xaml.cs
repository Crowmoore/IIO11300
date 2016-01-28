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

namespace H1MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlaying = false;
        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }
        private void IniMyStuff()
        {
            //Kaikki alustukset tänne
            txtPath.Text = "D:\\H3090\\CoffeeMaker.mp4";
            btnPause.IsEnabled = false;
            btnStop.IsEnabled = false;
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.InitialDirectory = "D:\\H3090";
            ofd.Filter = "Media-file mp4|*.mp4|All files|*.*";
            Nullable<bool> result = ofd.ShowDialog();
            if(result == true)
            {
                txtPath.Text = ofd.FileName;
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtPath.Text.Length > 0 && System.IO.File.Exists(txtPath.Text))
                {
                    mediaElement.Source = new Uri(txtPath.Text);
                    mediaElement.Play();
                    isPlaying = true;
                    SetButtonState();
                }
                else
                {
                    MessageBox.Show("File " + txtPath.Text + " not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }       
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if(!isPlaying)
            {
                mediaElement.Play();
                isPlaying = true;
                btnPause.Content = "Pause";
                SetButtonState();
            }
            else
            {
                mediaElement.Pause();
                
                btnPause.Content = "Resume";
                SetButtonState();
                isPlaying = false;
            }
            
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            isPlaying = false;
            SetButtonState();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void SetButtonState()
        {
            btnPlay.IsEnabled = !isPlaying;
            btnPause.IsEnabled = isPlaying;
            btnStop.IsEnabled = isPlaying;         
        }
    }
}
