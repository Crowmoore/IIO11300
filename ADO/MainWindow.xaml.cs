//Koodannut ja testannut toimivaksi 6.3.2014 EsaSalmik
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JAMK.ICT.Data;
using JAMK.ICT.ADOBlanco;

namespace JAMK.ICT.ADOBlanco
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
        private string connectionString;
        private string tableName;
        private DataTable table; //Every method in UI uses this datatable
        private DataView view;
    public MainWindow()
    {
      InitializeComponent();
      IniMyStuff();
    }

    private void IniMyStuff()
    {
            //TODO täytetään combobox asiakkaitten maitten nimillä
            //esimerkki kuinka App.Configissa oleva connectionstring luetaan
            try
            {
                connectionString = Properties.Settings.Default.Tietokanta;
                lbMessages.Content = connectionString;
                tableName = Properties.Settings.Default.Taulu;
                GetDataFromSQL();
                FillComboBox();
            }
            catch (Exception ex)
            {
                lbMessages.Content = ex.Message;
            }
    }
    private void FillComboBox()
        {
            //Get from datatable
            var result = (from c in table.AsEnumerable() select c.Field<string>("City")).Distinct().ToList();
            cbCities.ItemsSource = result;
  
        }
        private void GetDataFromSQL()
        {
            string message = "";
            table = DBPlacebo.GetAllCustomersFromSQLServer(connectionString, tableName, "", out message);
            view = table.DefaultView;
            dgCustomers.ItemsSource = table.DefaultView;
            lbMessages.Content = message;
        }
    private void btnGet3_Click(object sender, RoutedEventArgs e)
    {
            dgCustomers.ItemsSource = DBPlacebo.GetTestCustomers().DefaultView;
    }

    private void btnGetAll_Click(object sender, RoutedEventArgs e)
    {
             dgCustomers.ItemsSource = view;
    }

    private void btnGetFrom_Click(object sender, RoutedEventArgs e)
    {
            //Get clients from database
            string message = "";
            string city = "";
            try
            {
                if(cbCities.SelectedIndex >= 0)
                {
                    city = cbCities.SelectedValue.ToString();
                    view.RowFilter = string.Format("City = '{0}'", city);            
                }
                
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                lbMessages.Content = message;
            }
        }

        private void btnYield_Click(object sender, RoutedEventArgs e)
        {
            JAMK.ICT.JSON.JSONPlacebo2015 roskaa = new JAMK.ICT.JSON.JSONPlacebo2015();
            MessageBox.Show(roskaa.Yield());
        }
    }
}
