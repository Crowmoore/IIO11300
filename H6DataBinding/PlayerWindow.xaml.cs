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

namespace H6DataBinding
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        List<HockeyPlayer> players;
        int pointer;
        public PlayerWindow()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            players = TestHockeyBench.Get3Players();
            dgPlayers.ItemsSource = players;
            pointer = 0;
            SetData();
        }
        private void SetData()
        {
            spLeft.DataContext = players[pointer];
        }

        private void dgPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((dgPlayers.SelectedIndex > -1) & (dgPlayers.SelectedIndex < players.Count))
            {
                pointer = dgPlayers.SelectedIndex;
                SetData();
            }
        }
    }
}
