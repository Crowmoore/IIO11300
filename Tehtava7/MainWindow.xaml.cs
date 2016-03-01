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

namespace Tehtava7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> characterTypes = new List<int>();
 
        public MainWindow()
        {
            InitializeComponent();
            int upperCount = 0;
            int lowerCount = 0;
            int numCount = 0;
            int specialCount = 0;
            characterTypes.Add(upperCount);
            characterTypes.Add(lowerCount);
            characterTypes.Add(numCount);
            characterTypes.Add(specialCount);
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            string password = txtPassword.Text;        
            int charCount = password.Count();

            characterTypes[0] = password.Count(c => char.IsUpper(c));
            characterTypes[1] = password.Count(c => char.IsLower(c));
            characterTypes[2] = password.Count(c => char.IsNumber(c));
            characterTypes[3] = password.Count(c => !char.IsLetterOrDigit(c));

            int usedTypes = GetUsedTypes(characterTypes);

            UpdateCount(characterTypes, charCount);
            CheckPasswordStrength(charCount, usedTypes);         
        }
        private void UpdateCount(List<int> characterTypes, int charCount)
        {
            tbChars.Text = "Characters: " + charCount;
            tbUpper.Text = "Uppercase: " + characterTypes[0];
            tbLower.Text = "Lowercase: " + characterTypes[1];
            tbNums.Text = "Numbers: " + characterTypes[2];
            tbSpecials.Text = "Specials: " + characterTypes[3];
        }
        private int GetUsedTypes(List<int> characterTypes)
        {
            int types = 0;
            foreach (int type in characterTypes)
            {
                if (type != 0)
                {
                    types++;
                }
            }
            return types;
        }
        private void CheckPasswordStrength(int charCount, int usedTypes)
        {
            if (charCount < 8 || usedTypes == 1)
            {
                spResult.Background = new SolidColorBrush(Colors.Orange);
                tbResult.Text = "Bad";
            }
            if (charCount < 12 && usedTypes == 2)
            {
                spResult.Background = new SolidColorBrush(Colors.Yellow);
                tbResult.Text = "Fair";
            }
            if (charCount < 16 && usedTypes == 3)
            {
                spResult.Background = new SolidColorBrush(Colors.LightGreen);
                tbResult.Text = "Moderate";
            }
            if (charCount >= 16 && usedTypes == 4)
            {
                spResult.Background = new SolidColorBrush(Colors.Green);
                tbResult.Text = "Good";
            }
        }
    }
}
