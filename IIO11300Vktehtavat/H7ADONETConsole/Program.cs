using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace H7ADONETConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //create connection
                string connectionString = Properties.Settings.Default.Database;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //create command and execute
                    string query = "SELECT asioid, firstname, lastname, date FROM presences WHERE asioid = 'H3090'";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Prepare();
                    SqlDataReader reader = command.ExecuteReader();
                    //browse results
                    if(reader.HasRows)
                    {
                        int number = 0;
                        while(reader.Read())
                        {
                            Console.WriteLine("{0} {1} {2} {3}",
                                reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                            number++;
                        }
                        Console.WriteLine("Rows " + number);
                    }
                    reader.Close();
                    connection.Close();
                    Console.WriteLine("Connection closed");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
