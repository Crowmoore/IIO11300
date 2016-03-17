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

namespace ADONET
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

        private void btnGetAttendance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //create connection
                string connectionString = Properties.Settings.Default.Database;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //create command and execute
                    string query = "SELECT asioid, firstname, lastname, date FROM presences WHERE asioid LIKE @id";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@id", "%" + txtID.Text + "%");
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgGrid.DataContext = table;

                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGetAttendanceDv_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnGetAttendanceDs_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
