using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
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

namespace Tehtava8
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

        private void btnGetClients_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable table = new DataTable("Customers");
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.Database))
                {
                    string query = "SELECT firstname, lastname, address, city FROM vCustomers";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(table);
                    connection.Close();
                }
                lbClients.ItemsSource = table.AsDataView();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void lbClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            spRight.DataContext = e.AddedItems[0];
        }
    }
}
