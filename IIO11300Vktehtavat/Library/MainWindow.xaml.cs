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

namespace Library
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

        private void btnGetTestBooks_Click(object sender, RoutedEventArgs e)
        {
            dgBooks.DataContext = BookLibrary.GetTestBooks();
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //set stackpanel datacontext
            spBooks.DataContext = dgBooks.SelectedItem;
        }

        private void btnGetSQLBooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgBooks.DataContext = BookLibrary.GetBooks(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book current = spBooks.DataContext as Book;
                if(BookLibrary.UpdateBook(current) > 0)
                {
                    MessageBox.Show("Updated");
                }
                else
                {
                    MessageBox.Show("Update failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(btnAdd.Content.ToString() == "Uusi")
                {
                    Book newBook = new Book(0);
                    newBook.Name = "Anna kirjan nimi";
                    spBooks.DataContext = newBook;
                    btnAdd.Content = "Tallenna uusi kantaan";
                }
                else
                {
                    Book current = spBooks.DataContext as Book;
                    BookLibrary.InsertBook(current);
                    dgBooks.DataContext = BookLibrary.GetBooks(true);
                    btnAdd.Content = "Uusi";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book current = spBooks.DataContext as Book;
                BookLibrary.DeleteBook(current);
                dgBooks.DataContext = BookLibrary.GetBooks(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
