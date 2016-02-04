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


namespace Tehtava4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Pelaaja> pelaajat;
        public MainWindow()
        {
            InitializeComponent();
            string[] seurat = new string[] { "Ilves", "Tappara", "JYP", "Kärpät", "Blues", "Ässät", "TPS", "HJK" };
            pelaajat = new List<Pelaaja>();
            cbSeura.ItemsSource = seurat;
        }

        private void btnUusi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtEtunimi.Text) &&
                       !string.IsNullOrWhiteSpace(txtSukunimi.Text) &&
                       !string.IsNullOrWhiteSpace(txtHinta.Text) &&
                       cbSeura.SelectedItem != null)
                {
                    Pelaaja pelaaja = new Pelaaja(txtEtunimi.Text,
                                                    txtSukunimi.Text,
                                                    cbSeura.SelectedValue.ToString(),
                                                    int.Parse(txtHinta.Text));
                    bool containsPlayer = pelaajat.Any(Pelaaja => Pelaaja.KokoNimi == pelaaja.KokoNimi);
                    if(containsPlayer)
                    {
                        pelaaja = null;
                        txtStatus.Text = "Pelaaja on jo listassa";                     
                    }
                    else
                    {
                        pelaajat.Add(pelaaja);
                        ApplyChanges();
                        txtStatus.Text = "Uusi pelaaja luotu";
                    }                 
                }
                else
                {
                    txtStatus.Text = "Täytä kaikki kentät";
                }
            }
            catch (Exception ex)
            {
                txtStatus.Text = ex.ToString();
            }
        }

        private void btnTallenna_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedIndex = lbPelaajat.Items.IndexOf(lbPelaajat.SelectedItem);
                pelaajat[selectedIndex].Etunimi = txtEtunimi.Text;
                pelaajat[selectedIndex].Sukunimi = txtSukunimi.Text;
                pelaajat[selectedIndex].Seura = cbSeura.SelectedValue.ToString();
                pelaajat[selectedIndex].Hinta = int.Parse(txtHinta.Text);
                Clear();
                ApplyChanges();
                txtStatus.Text = "Pelaaja " + pelaajat[selectedIndex].KokoNimi + " tallennettu";
            }
            catch (Exception ex)
            {
                txtStatus.Text = ex.ToString();
            }
        }
        private void Clear()
        {
            txtEtunimi.Text = "";
            txtSukunimi.Text = "";
            txtHinta.Text = "";
            cbSeura.SelectedValue = "";
        }
        private void btnPoista_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pelaajat.RemoveAt(lbPelaajat.Items.IndexOf(lbPelaajat.SelectedItem));
                txtStatus.Text = "Pelaaja poistettu";
            }
            catch (Exception ex)
            {
                txtStatus.Text = ex.ToString();
            }
            Clear();
            ApplyChanges();
        }

        private void btnKirjoita_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pelaaja.SaveToFile(pelaajat);
                txtStatus.Text = "Pelaajat kirjoitettu tiedostoon";
            }
            catch (Exception ex)
            {
                txtStatus.Text = ex.ToString();
            }
        }

        private void btnLopeta_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ApplyChanges()
        {
            lbPelaajat.ItemsSource = null;
            lbPelaajat.ItemsSource = pelaajat;
        }

        private void lbSelection_Click(object sender, SelectionChangedEventArgs e)
        {
            if(lbPelaajat.SelectedItem != null)
            {
                int selectedIndex = lbPelaajat.Items.IndexOf(lbPelaajat.SelectedItem);
                txtEtunimi.Text = pelaajat[selectedIndex].Etunimi;
                txtSukunimi.Text = pelaajat[selectedIndex].Sukunimi;
                txtHinta.Text = pelaajat[selectedIndex].Hinta.ToString();
                cbSeura.SelectedValue = pelaajat[selectedIndex].Seura;
                txtStatus.Text = "Valittu " + pelaajat[selectedIndex].KokoNimi;
            }
            else
            {
                txtStatus.Text = "Valinta on tyhjä";
            }
        }
    }
}
