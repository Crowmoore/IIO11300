using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H6DataBinding
{
    public class HockeyPlayer
    {

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
