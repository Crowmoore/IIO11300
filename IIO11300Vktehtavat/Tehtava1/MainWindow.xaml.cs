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
using JAMK.IT.IIO11300;

namespace JAMK.IT.IIO11300
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            double windowWidth = double.Parse(txtWidth.Text);
            double windowHeight = double.Parse(txtHeight.Text);
            double windowBorder = double.Parse(txtBorder.Text);
            try
            {
                double area;
                area = BusinessLogicWindow.CalculateArea(windowWidth, windowHeight);
                txtArea.Text = area.ToString("0.##") + " m^2";

                double perimeter;
                perimeter = BusinessLogicWindow.CalculatePerimeter(windowWidth, windowHeight);
                txtBorderPerimeter.Text = perimeter.ToString("0.##") + " m";

                double borderArea;
                borderArea = BusinessLogicWindow.CalculateBorderArea(windowWidth, windowHeight, windowBorder);
                txtBorderArea.Text = borderArea.ToString("0.##") + " m^2";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //MessageBox.Show("Ikkunan koko laskettu");
            }
        }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
      //käynnissä olevan sovelluksen sulkeminen
      Application.Current.Shutdown();
    }

    private void btnOO_Click(object sender, RoutedEventArgs e)
    {
            //olion avulla lasketaan pinta-ala, piiri ja hinta
            MyWindow window = new MyWindow();
            window.Width = double.Parse(txtWidth.Text);
            window.Height = double.Parse(txtHeight.Text);
            //pinta-alan laskeminen kutsumalla metodia
            txtArea.Text = window.CalculateArea().ToString();
            //pinta-ala on olion ominaisuus
            txtArea.Text = window.Area.ToString();
    }
}


}
