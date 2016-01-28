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

namespace JAMK.IT.IIO11300
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

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            Lotto lotto = new Lotto();
            string selectedGame = cbGame.Text;
            int draws = int.Parse(txtDraws.Text);
            switch (selectedGame)
            {
                case "Lotto":             
                    lotto.SetupGame(selectedGame, draws);
                    break;
                case "Viking Lotto":
                    lotto.SetupGame(selectedGame, draws);
                    break;
                case "EuroJackpot":
                    lotto.SetupGame(selectedGame, draws);
                    break;
                default:
                    break;
            }
            rollNumbers(lotto);
        }

        private void rollNumbers(Lotto lotto)
        {
            for (int i = 0; i < lotto.TotalDraws; i++)
            {
                int[] numbers = lotto.GetPrimaryNumbers();
                int[] specials = lotto.GetSpecialNumbers(numbers);
                printResults(lotto, numbers, specials);
            }
        }

        private void printResults(Lotto lotto, int[] numbers, int[] specials)
        {
            Paragraph para = new Paragraph();
            FlowDocument doc = txtNumbers.Document;
            para.Margin = new Thickness(0);
            for (int i = 0; i < numbers.Length; i++)
            {
                Run run = new Run(numbers[i].ToString() + " ");
                para.Inlines.Add(run);
            }
            if (lotto.SelectedGame != "Viking Lotto")         
            {
                for (int i = 0; i < specials.Length; i++)
                {
                    Run run = new Run(specials[i].ToString() + " ");
                    run.Foreground = Brushes.Red;
                    para.Inlines.Add(run);
                }
            }
            doc.Blocks.Add(para);
            txtNumbers.AppendText("\n");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtNumbers.Document.Blocks.Clear();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
