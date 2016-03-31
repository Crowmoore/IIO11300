using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; //for ObservableCollection
using System.ComponentModel;
using System.Data.Entity;
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

namespace H10EntityFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookShopEntities ctx;
        ObservableCollection<Book> localBooks;
        ICollectionView view;
        bool isBooks;
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ctx = new BookShopEntities();
            ctx.Books.Load();
            localBooks = ctx.Books.Local;
            cbCountries.DataContext = localBooks.Select(n => n.country).Distinct();
            cbCountries.Visibility = Visibility.Visible;
            view = CollectionViewSource.GetDefaultView(localBooks);
        }
        private void btnGetClients_Click(object sender, RoutedEventArgs e)
        {
            var customers = ctx.Customers.ToList();
            dgBooks.DataContext = customers;
            isBooks = false;
        }

        private void btnGetBooks_Click(object sender, RoutedEventArgs e)
        {
            var books = ctx.Books.ToList();
            dgBooks.DataContext = localBooks;
            isBooks = true;
            cbCountries.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Book newBook;
            if (btnAdd.Content.ToString() == "Uusi")
            {
                newBook = new Book();
                newBook.name = "Anna kirjan nimi";
                spBooks.DataContext = newBook;
                btnAdd.Content = "Tallenna kantaan";
            }
            else
            {
                newBook = spBooks.DataContext as Book;
                ctx.Books.Add(newBook);
                ctx.SaveChanges();
                btnAdd.Content = "Uusi";
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Book current = spBooks.DataContext as Book;
            var retval = MessageBox.Show("Haluatko poistaa kirjan " + current.DisplayName, "Kirjakauppa huutaa", MessageBoxButton.YesNo);
            if(retval == MessageBoxResult.Yes)
            {
                ctx.Books.Remove(current);
                ctx.SaveChanges();
            }
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            view.Filter = CountryFilter;
        }

        private bool CountryFilter(object item)
        {
            if(cbCountries.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return (item as Book).country.Contains(cbCountries.SelectedItem.ToString());
            }
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(isBooks) {
                spBooks.DataContext = dgBooks.SelectedItem;
            }
            else
            {
                spCustomers.DataContext = dgBooks.SelectedItem;  
            }
        }

        private void btnGetOrders_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            Customer current = spCustomers.DataContext as Customer;
            message += string.Format("Asiakkaalla {0} in {1} tilausta: \n", current.DisplayName, current.OrderCount);
            foreach(var item in current.Orders)
            {
                message += string.Format("Tilaus {0} sisältää {1} tilausriviä:\n", item.odate, item.Orderitems.Count);
                Decimal sum = 0;
                foreach (var oitem in item.Orderitems)
                {
                    message += string.Format("- kirja {0} -- {1} kappaletta\n", oitem.Book.name, oitem.count);
                    sum += oitem.count * oitem.Book.price.Value;
                }
                message += string.Format("Tilaus yhteensä {0}\n", sum);
            }
            
            MessageBox.Show(message);
        }
    }
}
