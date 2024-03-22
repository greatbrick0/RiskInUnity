using INFR2100U.GraphSpace;
using INFR2100U.CountrySpace;
using INFR2100U.PlayerSpace;

namespace INFR2100U.ContinentSpace
{
    public class Continent
    {
        public string continentName { get; private set; }
        public int controlValue { get; private set; }
        public Graph<Country> countries = new Graph<Country>();
        public bool CheckIfSoleOwned(Player player)
        {
            foreach (Country country in countries)
            {
                if (country.owner != player) return false;
            }
            return true;
        }
        public Player GetFirstOwner() { return countries.FirstNode.owner; }

        public Continent(string name, Graph<Country> subGraph, int control)
        {
            continentName = name;
            countries = subGraph;
            controlValue = control;
        }
    }
}
