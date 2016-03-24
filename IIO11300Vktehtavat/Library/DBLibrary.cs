using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Library
{
    class DBLibrary
    {
       public static DataTable GetTestData()
       {
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("author", typeof(string));
            table.Columns.Add("country", typeof(string));
            table.Columns.Add("year", typeof(int));

            table.Rows.Add(11, "Pekka Lipposen seikkailut", "Outsider", "Suomi", 1950);
            table.Rows.Add(12, "Kissa koira", "Lammas", "Suomi", 1987);

            return table;
        }
        public static DataTable GetBooks(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT id, name, author, country, year FROM books";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable table = new DataTable("Books");
                    adapter.Fill(table);
                    connection.Close();
                    return table;
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public static int UpdateBook(string connectionString, int id, string name, string author, string country, int year)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE books SET name = @name, author = @author, country = @country, year = @year WHERE id = @id";
                    //string query = string.Format("UPDATE books SET name = @name, author = '{2}', country = '{3}', year = {4} WHERE id = {0}", id, name, author, country, year);
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    /*SqlParameter sp = new SqlParameter("Name", SqlDbType.NVarChar);
                    sp.Value = name;
                    command.Parameters.Add(sp):*/
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@country", country);
                    command.Parameters.AddWithValue("@year", year);
                    int count = command.ExecuteNonQuery();
                    connection.Close();
                    return count;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int InsertBook(string connectionString, string name, string author, string country, int year)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO books (name, author, country, year) VALUES (@name, @author, @country, @year)";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@country", country);
                    command.Parameters.AddWithValue("@year", year);
                    int count = command.ExecuteNonQuery();
                    connection.Close();
                    return count;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int DeleteBook(string connectionString, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM books WHERE id = @id";
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);
                    int count = command.ExecuteNonQuery();
                    connection.Close();
                    return count;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
