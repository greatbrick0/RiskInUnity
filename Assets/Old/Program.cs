using INFR2100U.ContinentSpace;
using INFR2100U.CountrySpace;
//using INFR2100U.Graphics;
using INFR2100U.PlayerSpace;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

class Program : MonoBehaviour
{
    private void Start()
    {
        string[] args = { };
        Main(args);
    }

    public static void Main(string[] args)
    {
        bool gameOver = false; 

        Map.MakeMapGraph(); // Generate map in code

        #region Player_Join
        int playerCount = 0;
        do
        {
            Console.Write("Select the number of players (minimum of 3 and maximum of 6): ");
            playerCount = Convert.ToInt32(Console.ReadLine());

        } while (playerCount < 3 && playerCount > 6);


        int startArmyCount = 35 - (5 * (playerCount - 3));

        List<Player> players = new List<Player>();

        for (int i = 0; i < playerCount && i < 7; i++)
        {
            players.Add(new Player(i+1, (PlayerColour)i+1, startArmyCount));
        }


        // Assign country randomly to players at the start of the game
        foreach (Country country in Map.AllCountries)
        {
            if (country.owner == null)
            {
                int randomPlayer = new Random().Next(0, players.Count);

                // Ensure (relatively) equal territory control for all players
                while (players[randomPlayer].controlledTerritory.Count >= (int)Math.Ceiling((float)Map.AllCountries.Count / players.Count))
                {
                    randomPlayer = new Random().Next(0, players.Count);
                } 

                players[randomPlayer].GetTerritory(country);
                country.owner = players[randomPlayer];
            }
        }

        // Populate the newly controlled territory before the games starts.
        foreach(Player player in players)
        {
            player.SetupPlayerTerritory();
        }
        #endregion

        //using Game risk = new Game(1200, 614, "Risk - INFR2100U");
        //risk.Run();

        #region Gameplay
        while (gameOver == false)
        {
            if (players.Count == 1)
            {
                gameOver = true;

                Console.WriteLine($"Game Over. Player {players[0].playerColour} wins");
            }

            foreach(Player currentPlayerTurn in players)
            {
                if (currentPlayerTurn.isElimiated)
                {
                    Console.WriteLine($"Player {currentPlayerTurn.playerColour} has been elimiated.");
                    players.Remove(currentPlayerTurn);
                }
                else
                {
                    Console.WriteLine($"Player {currentPlayerTurn.playerColour}'s turn:");

                    currentPlayerTurn.PlayerTurn();
                }
            }
        }
        #endregion


        /*#region DISPLAY_MAP
        Console.WriteLine("Map:");
        Console.WriteLine("______________________________\n");

        // Iterate through each Continent
        foreach (Continent continent in Map.map)
        {
            Console.WriteLine($"{continent.continentName.ToUpper()} - Control Value: {continent.controlValue}");
            Console.WriteLine("--------------------------------");

            // Iterate through each country in the continent
            string continentCountries = "[";
            foreach (Country country in continent.countries)
            {
                if (country != continent.countries.FirstNode)
                {
                    continentCountries += ", ";
                }

                continentCountries += $"({country.position}) {country.countryName}";
            }
            continentCountries += "]";

            Console.WriteLine(continentCountries + "\n");
        }
        #endregion
        */
    }
}