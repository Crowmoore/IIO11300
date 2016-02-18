using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
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

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var xml = XDocument.Load("E:\\Koulu\\IIO11300\\IIO11300Vktehtavat\\WineCellar\\Wines.xml").Root;
            datagrid.DataContext = xml;
        }

        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            var xml = XDocument.Load("E:\\Koulu\\IIO11300\\IIO11300Vktehtavat\\WineCellar\\Wines.xml").Root;
            datagrid.DataContext = xml;
        }
    }
}
