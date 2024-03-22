using INFR2100U.ContinentSpace;
using INFR2100U.GraphSpace;
using INFR2100U.CountrySpace;
using System.Collections.Generic;


public static class Map
{
	public static Graph<Continent> map = new Graph<Continent>();

    #region Subgraph_Setup
    private static Graph<Country> SetupNorthAmerica()
    {
        Graph<Country> NorthAmerica = new Graph<Country>();

        Country Alaska = new Country("Alaska", 1);
        Country Alberta = new Country("Alberta", 2);
        Country CentralAmerica = new Country("Central America", 3);
        Country EasternUnitedStates = new Country("Eastern United States", 4);
        Country Greenland = new Country("Greenland", 5);
        Country NorthwestTerritory = new Country("Northwest Territory", 6);
        Country Ontario = new Country("Ontario", 7);
        Country Quebec = new Country("Quebec", 8);
        Country WesternUnitedStates = new Country("Western United States", 9);
        
        

        NorthAmerica.Add(Alaska, new List<Country> { Alberta, NorthwestTerritory });
        NorthAmerica.Add(Alberta, new List<Country> { Alaska, NorthwestTerritory, Ontario, WesternUnitedStates });
        NorthAmerica.Add(CentralAmerica, new List<Country> { EasternUnitedStates, WesternUnitedStates });
        NorthAmerica.Add(EasternUnitedStates, new List<Country> { CentralAmerica, Ontario, Quebec, WesternUnitedStates });
        NorthAmerica.Add(Greenland, new List<Country> { NorthwestTerritory, Ontario, Quebec });
        NorthAmerica.Add(NorthwestTerritory, new List<Country> { Alaska, Alberta, Greenland, Ontario });
        NorthAmerica.Add(Ontario, new List<Country> { Alberta, EasternUnitedStates, Greenland, NorthwestTerritory, Quebec, WesternUnitedStates });
        NorthAmerica.Add(Quebec, new List<Country> { EasternUnitedStates, Greenland, Ontario });
        NorthAmerica.Add(WesternUnitedStates, new List<Country> { Alberta, CentralAmerica, EasternUnitedStates, Ontario });

        return NorthAmerica;
    }

    private static Graph<Country> SetupSouthAmerica()
    {
        Graph<Country> SouthAmerica = new Graph<Country>();

        Country Argentina = new Country("Argentina", 1);
        Country Brazil = new Country("Brazil", 2);
        Country Venezuela = new Country("Venezuela", 3);
        Country Peru = new Country("Peru", 4);

        SouthAmerica.Add(Argentina, new List<Country> { Brazil, Peru });
        SouthAmerica.Add(Brazil, new List<Country> { Argentina, Venezuela, Peru });
        SouthAmerica.Add(Venezuela, new List<Country> { Brazil, Peru });
        SouthAmerica.Add(Peru, new List<Country> { Argentina, Brazil, Venezuela });

        return SouthAmerica;
    }

    private static Graph<Country> SetupEurope()
    {
        Graph<Country> Europe = new Graph<Country>();

        Country GreatBritain = new Country("Great Britain", 1);
        Country Iceland = new Country("Iceland", 2);
        Country NorthernEurope = new Country("Northern Europe", 3);
        Country Scandinavia = new Country("Scandinavia", 4);
        Country SouthernEurope = new Country("Southern Europe", 5);
        Country Ukraine = new Country("Ukraine", 6);
        Country WesternEurope = new Country("Western Europe", 7);

        Europe.Add(GreatBritain, new List<Country> { Iceland, NorthernEurope, Scandinavia, WesternEurope });
        Europe.Add(Iceland, new List<Country> { GreatBritain, Scandinavia });
        Europe.Add(NorthernEurope, new List<Country> { GreatBritain, Scandinavia, SouthernEurope, Ukraine, WesternEurope });
        Europe.Add(Scandinavia, new List<Country> { GreatBritain, Iceland, NorthernEurope, Ukraine });
        Europe.Add(SouthernEurope, new List<Country> { NorthernEurope, Ukraine, WesternEurope });
        Europe.Add(Ukraine, new List<Country> { NorthernEurope, Scandinavia, SouthernEurope });
        Europe.Add(WesternEurope, new List<Country> { GreatBritain, NorthernEurope, SouthernEurope });

        return Europe;
    }

    private static Graph<Country> SetupAfrica()
    {
        Graph<Country> Africa = new Graph<Country>();

        Country Congo = new Country("Congo", 1);
        Country EastAfrica = new Country("East Africa", 2);
        Country Egypt = new Country("Egypt", 3);
        Country Madagascar = new Country("Madagascar", 4);
        Country NorthAfrica = new Country("North Africa", 5);
        Country SouthAfrica = new Country("South Africa", 6);

        Africa.Add(Congo, new List<Country> { EastAfrica, NorthAfrica, SouthAfrica });
        Africa.Add(EastAfrica, new List<Country> { Congo, Egypt, Madagascar, SouthAfrica });
        Africa.Add(Egypt, new List<Country> { EastAfrica, NorthAfrica });
        Africa.Add(Madagascar, new List<Country> { EastAfrica, SouthAfrica });
        Africa.Add(NorthAfrica, new List<Country> { Congo, EastAfrica, Egypt });
        Africa.Add(SouthAfrica, new List<Country> { Congo, EastAfrica, Madagascar });

        return Africa;
    }

    private static Graph<Country> SetupAsia()
    {
        Graph<Country> Asia = new Graph<Country>();

        Country Afghanistan = new Country("Afghanistan", 1);
        Country China = new Country("China", 2);
        Country India = new Country("India", 3);
        Country Irkutsk = new Country("Irkutsk", 4);
        Country Japan = new Country("Japan", 5);
        Country Kamchatka = new Country("Kamchatka", 6);
        Country MiddleEast = new Country("Middle East", 7);
        Country Mongolia = new Country("Mongolia", 8);
        Country Siam = new Country("Siam", 9);
        Country Siberia = new Country("Siberia", 10);
        Country Ural = new Country("Ural", 11);
        Country Yakutsk = new Country("Yakutsk", 12);

        Asia.Add(Afghanistan, new List<Country> { China, India, MiddleEast, Ural });
        Asia.Add(China, new List<Country> { Afghanistan, India,Mongolia, Siam, Siberia, Ural });
        Asia.Add(India, new List<Country> { Afghanistan, China, MiddleEast, Siam });
        Asia.Add(Irkutsk, new List<Country> { Kamchatka, Mongolia, Siberia, Yakutsk });
        Asia.Add(Japan, new List<Country> { Kamchatka, Mongolia });
        Asia.Add(Kamchatka, new List<Country> { Irkutsk, Japan, Mongolia, Yakutsk });
        Asia.Add(MiddleEast, new List<Country> { Afghanistan, India });
        Asia.Add(Mongolia, new List<Country> { China, Irkutsk, Japan, Kamchatka, Siberia });
        Asia.Add(Siam, new List<Country> { China, India });
        Asia.Add(Siberia, new List<Country> { China, Irkutsk, Mongolia, Ural, Yakutsk });
        Asia.Add(Ural, new List<Country> { Afghanistan, China, Siberia });
        Asia.Add(Yakutsk, new List<Country> { Irkutsk, Kamchatka, Siberia });

        return Asia;
    }

    private static Graph<Country> SetupAustralia()
    {
        Graph<Country> Australia = new Graph<Country>();

        Country EasternAustralia = new Country("Eastern Australia", 1);
        Country NewGuinea = new Country("New Guinea", 2);
        Country Indoneisa = new Country("Indoneisa", 3);
        Country WesternAustralia = new Country("Western Australia", 4);

        Australia.Add(EasternAustralia, new List<Country> { NewGuinea, WesternAustralia });
        Australia.Add(NewGuinea, new List<Country> { EasternAustralia, Indoneisa, WesternAustralia });
        Australia.Add(Indoneisa, new List<Country> { NewGuinea, WesternAustralia });
        Australia.Add(WesternAustralia, new List<Country> { EasternAustralia, NewGuinea, Indoneisa });

        return Australia;
    }
    #endregion

    #region Map_Generation
    /// <summary>
    /// Creates the subgraph for each continent.
    /// </summary>
    public static void SetCountryGraph()
	{
        // North America
        SetupNorthAmerica();

        // South America
        SetupSouthAmerica();

        // Europe
        SetupEurope();

        // Africa
        SetupAfrica();

        // Asia
        SetupAsia();

        // Australia
        SetupAustralia();
	}

    /// <summary>
    /// Creates the map graph
    /// </summary>
	public static void MakeMapGraph() 
	{
        // Create Continents
        Continent NorthAmerica = new Continent("North America", SetupNorthAmerica(), 5);
        Continent SouthAmerica = new Continent("South America", SetupSouthAmerica(), 2);
        Continent Europe = new Continent("Europe", SetupEurope(), 5);
        Continent Africa = new Continent("Africa", SetupAfrica(), 3);
        Continent Asia = new Continent("Asia", SetupAsia(), 7);
        Continent Austrialia = new Continent("Australia", SetupAustralia(), 2);

        // Link Continents by Country adjacency
        #region NA_Link
        NorthAmerica.countries["Greenland"].Add(Europe.countries.FindNode("Iceland")); // Link to Greenland to Iceland (Europe)
        NorthAmerica.countries["Central America"].Add(SouthAmerica.countries.FindNode("Venezuela")); // Link to Central America to Venezuela (South America)
        NorthAmerica.countries["Alaska"].Add(Asia.countries.FindNode("Kamchatka")); // Link to Alaska to Kamchatka (Asia)
        #endregion
        
        
        #region SA_Link    
        SouthAmerica.countries["Brazil"].Add(Africa.countries.FindNode("North Africa")); // Link to Brazil to North Africa (Africa)
        SouthAmerica.countries["Venezuela"].Add(NorthAmerica.countries.FindNode("Central America")); // Link to Venezuela to Central America (North America)
        #endregion


        #region EU_Link
        Europe.countries["Southern Europe"].Add(Asia.countries.FindNode("Middle East")); // Link to Southern Europe to Middle East (Asia)
        Europe.countries["Ukraine"].Add(Asia.countries.FindNode("Afghanistan")); // Link to Ukraine to Afghanistan (Asia)
        Europe.countries["Ukraine"].Add(Asia.countries.FindNode("Middle East")); // Link to Ukraine to Middle East (Asia)
        Europe.countries["Ukraine"].Add(Asia.countries.FindNode("Ural")); // Link to Ukraine to Ural (Asia)

        Europe.countries["Iceland"].Add(NorthAmerica.countries.FindNode("Greenland")); // Link to Iceland to Greenland (North America)

        Europe.countries["Southern Europe"].Add(Africa.countries.FindNode("Egypt")); // Link to Southern Europe to Egypt (Africa)
        Europe.countries["Southern Europe"].Add(Africa.countries.FindNode("North Africa")); // Link to Southern Europe to Norht Africa (Africa)
        Europe.countries["Western Europe"].Add(Africa.countries.FindNode("North Africa")); // Link to Westeren Europe to North Africa (Africa)
        #endregion


        #region AF_Link
        Africa.countries["North Africa"].Add(SouthAmerica.countries.FindNode("Brazil")); //Link to North Africa to Brazil (South America)

        Africa.countries["Egypt"].Add(Europe.countries.FindNode("Southern Europe")); //Link to Egypt to Southern Europe (Europe)
        Africa.countries["North Africa"].Add(Europe.countries.FindNode("Southern Europe")); //Link to North Africa to Southern Europe (Europe)
        Africa.countries["North Africa"].Add(Europe.countries.FindNode("Western Europe")); //Link to North Africa to Westeren Europe (Europe)

        Africa.countries["Egypt"].Add(Asia.countries.FindNode("Middle East")); // Link to Egypt to Middle East (Asia)
        Africa.countries["East Africa"].Add(Asia.countries.FindNode("Middle East")); // Link to East Africa to Middle East (Asia)
        #endregion


        #region AS_Link
        Asia.countries["Afghanistan"].Add(Europe.countries.FindNode("Ukraine")); // Link to Afghanistan to Ukraine (Europe)
        Asia.countries["Ural"].Add(Europe.countries.FindNode("Ukraine")); // Link to Ural to Ukraine (Europe)
        Asia.countries["Middle East"].Add(Europe.countries.FindNode("Ukraine")); // Link to Middle East to Ukreaine (Europe)
        Asia.countries["Middle East"].Add(Europe.countries.FindNode("Southern Europe")); // Link to Middle East to Southern Europe (Europe)

        Asia.countries["Middle East"].Add(Africa.countries.FindNode("Egypt")); // Link to Middle East to Egypt (Africa)
        Asia.countries["Middle East"].Add(Africa.countries.FindNode("East Africa")); // Link to Middle East to East Africa (Africa)

        Asia.countries["Siam"].Add(Austrialia.countries.FindNode("Indoneisa")); // Link to Siam to Indonesia (Australia)

        Asia.countries["Kamchatka"].Add(NorthAmerica.countries.FindNode("Alaska")); // Link to Kamchatka to Alaska (North America)
        #endregion


        #region AU_Link
        Austrialia.countries["Indoneisa"].Add(Asia.countries.FindNode("Siam")); // Link to Indoneisia to Siam (Asia)
        #endregion


        // Link Continents to create map
        map.Add(NorthAmerica, new List<Continent> { SouthAmerica, Europe, Asia });
        map.Add(SouthAmerica, new List<Continent> { NorthAmerica, Africa });
        map.Add(Europe, new List<Continent> { NorthAmerica, Africa, Asia });
        map.Add(Africa, new List<Continent> { SouthAmerica, Europe, Asia });
        map.Add(Asia, new List<Continent> { NorthAmerica, Europe, Africa, Austrialia });
        map.Add(Austrialia, new List<Continent> { Asia });
	}
    #endregion

    public static List<Country> AllCountries 
    {
        get 
        {
            List<Country> result = new List<Country>();

            foreach (Continent continent in map)
            {
                foreach (Country country in continent.countries)
                {
                    if (result.Contains(country) == false)
                        result.Add(country);
                }
            }

            return result;
        }
    }
}