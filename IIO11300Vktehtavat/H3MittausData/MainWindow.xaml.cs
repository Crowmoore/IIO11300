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
using JAMK.IT.IIO11300;

namespace JAMK.IT.IIO11300
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
        //luodaan lista mittausolioita varten
        List<MittausData> mittaukset;
        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }
        private void IniMyStuff()
        {
            //omat ikkunaan liittyvät alustukset
            txtToday.Text = DateTime.Today.ToShortDateString();
            mittaukset = new List<MittausData>();
        }

        private void btnAddData_Click(object sender, RoutedEventArgs e)
        {
            //luodaan uusi mittausdata olio ja näytetään se käyttäjälle
            MittausData md = new MittausData(txtClock.Text, txtData.Text);
            //lbData.Items.Add(md); testausta varten
            mittaukset.Add(md);
            ApplyChanges();
        }
        private void ApplyChanges()
        {
            lbData.ItemsSource = null;
            lbData.ItemsSource = mittaukset;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MittausData.SaveToFileV2(txtFileName.Text, mittaukset);
            MessageBox.Show("Tiedot tallennettu");
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mittaukset = MittausData.ReadFromFile(txtFileName.Text);
                ApplyChanges();
                MessageBox.Show("Tiedot haettu onnistuneesti");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSerialize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Serialisointi.SerialisoiXml(txtFileName.Text, mittaukset);
                MessageBox.Show("Serialisointi onnistui");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeserialize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mittaukset = Serialisointi.DeSerialisoiXml(txtFileName.Text);
                ApplyChanges();
                MessageBox.Show("Deserialisointi onnistui");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSerializeBin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Serialisointi.Serialisoi(txtFileName.Text, mittaukset);
                MessageBox.Show("Serialisointi onnistui");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeserializeBin_Click(object sender, RoutedEventArgs e)
        {
            object obj = new object();
            try
            {
                Serialisointi.DeSerialisoi(txtFileName.Text, ref obj);
                mittaukset = obj as List<MittausData>;
                ApplyChanges();
                MessageBox.Show("Deserialisointi onnistui");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
