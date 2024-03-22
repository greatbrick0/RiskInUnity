using INFR2100U.CountrySpace;
using UnityEngine;

namespace INFR2100U.CardSpace
{
    public class Card
    {
        public Country country;
        public Symbol symbol;

        /// <summary>
        /// Default Constructor used to create random cards
        /// </summary>
        public Card()
        {
            //Random random = new Random();
            int random = Random.Range(0, 3);

            //switch (random.Next(0, 3))
            switch(random)
            {
                case 0:
                    {
                        symbol = Symbol.Cavalry;
                        break;
                    }
                case 1:
                    {
                        symbol = Symbol.Artillery;
                        break;
                    }
                case 2:
                    {
                        symbol = Symbol.Infantry;
                        break;
                    }
            }

            random = Random.Range(0, Map.AllCountries.Count);
            //country = Map.AllCountries[random.Next(0, Map.AllCountries.Count)];
            country = Map.AllCountries[random];
        }
    }

    public enum Symbol
    {
        Cavalry = 0,
        Artillery = 1,
        Infantry = 2
    }
}

