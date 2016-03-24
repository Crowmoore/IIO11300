using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        #region Properties
        private int id;

        public int ID
        {
            get { return id; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string author;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        private string  country;

        public string  Country
        {
            get { return country; }
            set { country = value; }
        }
        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        #endregion
        #region Constructors
        public Book(int id)
        {
            this.id = id;
        }
        public Book(int id, string name, string author, string country, int year)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.country = country;
            this.year = year;
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return name + " written by " + author;
        }
        #endregion
    }

    public static class BookLibrary
    {
        private static string cs = Properties.Settings.Default.Database;
        public static List<Book> GetTestBooks()
        {
            List<Book> temp = new List<Book>();
            temp.Add(new Book(1, "Sota ja rauha", "Leo Tolstoi", "Venäjä", 1867));
            temp.Add(new Book(2, "Anna Karenina", "Leo Tolstoi", "Venäjä", 1877));
            return temp;
        }
        
        public static List<Book> GetBooks(bool useDB)
        {
            try
            {
                DataTable table;
                List<Book> books = new List<Book>();
                if (useDB)
                {
                    table = DBLibrary.GetBooks(cs);
                }
                else
                {
                    table = DBLibrary.GetTestData();
                }
                Book book;
                foreach (DataRow row in table.Rows)
                {
                    book = new Book((int)row[0]);
                    book.Name = row["name"].ToString();
                    book.Author = row["author"].ToString();
                    book.Country = row["country"].ToString();
                    book.Year = (int)row["year"];
                    books.Add(book);
                }
                return books;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int UpdateBook(Book book)
        {
            try
            {
                int count = DBLibrary.UpdateBook(cs, book.ID, book.Name, book.Author, book.Country, book.Year);
                return count;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable GetBooks()
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
        public static bool InsertBook(Book book)
        {
            try
            {
                int count = DBLibrary.InsertBook(cs, book.Name, book.Author, book.Country, book.Year);
                if(count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool DeleteBook(Book book)
        {
            try
            {
                int count = DBLibrary.DeleteBook(cs, book.ID);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
