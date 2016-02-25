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

namespace H6DataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HockeyLeague league;
        List<HockeyTeam> leagueTeams;
        int pointer;
        public MainWindow()
        {
            InitializeComponent();
            SetupControls();
        }
        private void SetupControls()
        {
            List<String> teams = new List<string>();
            teams.Add("Ilves");
            teams.Add("JYP");
            teams.Add("Kärpät");
            cbTeams.ItemsSource = teams;

            league = new HockeyLeague();
            leagueTeams = league.GetTeams();
        }
        private void btnGetSetting_Click(object sender, RoutedEventArgs e)
        {
            //read setting UserName from App.Config
            btnGetSetting.Content = H6DataBinding.Properties.Settings.Default.UserName;
        }

        private void btnBind_Click(object sender, RoutedEventArgs e)
        {
            //bind collection to stackpanel as a datacontext
            spLeague.DataContext = leagueTeams;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //move collection pointer back by one
            try
            {
                pointer--;
                spLeague.DataContext = leagueTeams[pointer];
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                pointer++;
                spLeague.DataContext = leagueTeams[pointer];
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //add new team to collection
            HockeyTeam newTeam = new HockeyTeam();
            leagueTeams.Add(newTeam);
            pointer = leagueTeams.Count - 1;
            spLeague.DataContext = leagueTeams[pointer];
        }
    }
}
