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
using System.Windows.Shapes;
using System.Xml;

namespace Movies
{
    /// <summary>
    /// Interaction logic for CrudWindow.xaml
    /// </summary>
    public partial class CrudWindow : Window
    {
        public CrudWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string file = xdpMovies.Source.LocalPath;
            xdpMovies.Document.Save(file);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {    
            try
            {
                string file = xdpMovies.Source.LocalPath;
                XmlDocument document = xdpMovies.Document;
                XmlNode root = document.SelectSingleNode("/Movies");
                XmlNode toBeRemoved = null;
                var item = document.SelectSingleNode(string.Format("/Movies/Movie[@Name='{0}']", txtMovie.Text));
                if(item != null && MessageBox.Show("Really delete " + txtMovie.Text, "Edit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    toBeRemoved = item;
                    root.RemoveChild(toBeRemoved);
                    xdpMovies.Document.Save(file);
                    MessageBox.Show("Entry deleted");
                    lbMovies.SelectedIndex = -1;
                }           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(lbMovies.SelectedIndex >= 0)
            {
                lbMovies.SelectedIndex = -1;
                btnAdd.Content = "Save movie";
            }
            else
            {
                try
                {
                    string file = xdpMovies.Source.LocalPath;
                    //viittaus xml dokumenttiin ja sen juurielementtiin
                    XmlDocument document = xdpMovies.Document;
                    XmlNode root = document.SelectSingleNode("/Movies");
                    //luodaan uusi node
                    XmlNode newMovie = document.CreateElement("Movie");
                    //lisätään nodelle tarvittavat attribuutit
                    XmlAttribute name = document.CreateAttribute("Name");
                    name.Value = txtMovie.Text;
                    newMovie.Attributes.Append(name);
                    XmlAttribute country = document.CreateAttribute("Country");
                    country.Value = txtCountry.Text;
                    newMovie.Attributes.Append(country);
                    XmlAttribute director = document.CreateAttribute("Director");
                    director.Value = txtDirector.Text;
                    newMovie.Attributes.Append(director);
                    XmlAttribute watched = document.CreateAttribute("Checked");
                    watched.Value = cbWatched.IsChecked.Value.ToString();
                    newMovie.Attributes.Append(watched);
                    root.AppendChild(newMovie);
                    xdpMovies.Document.Save(file);
                    MessageBox.Show("New movie added");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    btnAdd.Content = "Add movie";
                }             
            }
        }
    }
}
