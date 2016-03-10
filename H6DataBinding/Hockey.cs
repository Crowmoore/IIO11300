using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6DataBinding
{
    public static class TestHockeyBench
    {
        public static List<HockeyPlayer> Get3Players()
        {
            List<HockeyPlayer> players = new List<HockeyPlayer>();
            players.Add(new HockeyPlayer("Teemu Selänne", "8"));
            players.Add(new HockeyPlayer("Jarkko Immonen", "26"));
            players.Add(new HockeyPlayer("Ville Peltonen", "16"));
            return players;
        }
    }
    public class HockeyPlayer : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                Notify("Name");
                Notify("NameAndNumber");
            }
        }
        private string number;
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                Notify("Number");
                Notify("NameAndNumber");
            }
        }
        public string NameAndNumber
        {
            get
            {
                return name + "#" + number;
            }
        }

        public HockeyPlayer()
        {

        }

        public HockeyPlayer(string name, string number)
        {
            this.name = name;
            this.number = number;
        }
        //INotifyPropertyChanged (interface) implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void Notify(string propName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }

    public class HockeyTeam
    {
        #region Properties
        //obs! public field doesn't work with xaml binding, needs to be a property
        public string Name { get; set; }
        public string City { get; set; }
        #endregion
        #region Constructors
        public HockeyTeam ()
        {
            Name = "Anonymous";
            City = "None";
        }
        public HockeyTeam(string teamName, string city)
        {
            Name = teamName;
            City = city;
        }
        public override string ToString()
        {
            return Name + "@" + City;
        }
        #endregion
    }

    public class HockeyLeague
    {
        List<HockeyTeam> teams = new List<HockeyTeam>();
        public HockeyLeague ()
        {
            teams.Add(new HockeyTeam("Ilves", "Tampere"));
            teams.Add(new HockeyTeam("Tappara", "Tampere"));
            teams.Add(new HockeyTeam("JYP", "Jyväskylä"));
            teams.Add(new HockeyTeam("HIFK", "Helsinki"));
            teams.Add(new HockeyTeam("Kärpät", "Oulu"));
        }
        //Method that returns the teams of the league
        public List<HockeyTeam> GetTeams()
        {
            return teams;
        }
    }
}
