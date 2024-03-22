using INFR2100U.CardSpace;
using INFR2100U.CountrySpace;
using INFR2100U.ContinentSpace;
using System.Collections.Generic;
using System;
using System.Linq;

namespace INFR2100U.PlayerSpace
{
    public class Player
    {
        #region PUBLIC_DATA
        public PlayerColour playerColour { get; private set; } = PlayerColour.None;
        public int turnOrder { get; private set; }
        public List<Country> controlledTerritory = new List<Country>();
        public List<Card> cards = new List<Card>();
        public bool isElimiated = false;
        #endregion


        #region PRIVATE_DATA
        private int setTraded = 0;
        private bool cardTradeBonus = false;
        private bool successfulConquer = false;
        private int armyUsed = 0;
        private int reinforcmentOwned = 0;
        #endregion


        public Player(int turn, PlayerColour colour, int startingArmies)
        {
            // Default Setting on load
            setTraded = 0;
            cardTradeBonus = false;
            reinforcmentOwned = 0;

            // Clear list
            controlledTerritory.Clear();
            cards.Clear();

            // Set player values
            turnOrder = turn;
            playerColour = colour;
            reinforcmentOwned = startingArmies;
        }


        /// <summary>
        /// Adds the country to the list of controlled countries.
        /// </summary>
        /// <param name="country">The country the player gained.</param>
        public void GetTerritory(Country country)
        {
            if (!controlledTerritory.Contains(country))
            {
                controlledTerritory.Add(country);
            }
        }

        /// <summary>
        /// Trades a set and gain bonus armies.
        /// </summary>
        public int TradeSet()
        {
            List<Card> set = FindSet();

            // Exit if the set is less than 3 cards
            if (set.Count < 3)
            {
                return 0;
            }

            // Remove the cards from the list
            cards.Remove(set[0]);
            cards.Remove(set[1]);
            cards.Remove(set[2]);

            setTraded++;

            // Checks if any card has a country on it that the player owns for bonus armies on trade.
            foreach (Country controlledCountry in controlledTerritory)
            {
                if ((set[0].country == controlledCountry || set[1].country == controlledCountry || set[2].country == controlledCountry) && cardTradeBonus == false)
                {
                    cardTradeBonus = true;
                }
            }

            return SetBonus();
        }

        public void PlayerTurn()
        {
            Reinforcement();
            Attack();
            Fortification();
            EndTurn();
        }


        #region GAME_PHASE
        /// <summary>
        /// Reienforcement Phase. Also known as the start of the turn.
        /// </summary>
        private void Reinforcement()
        {
            Console.WriteLine("\nReinforcement Phase");
            Console.WriteLine("-------------------");


            #region Gain_Army
            int gainedArmy = Convert.ToInt32(MathF.Floor(controlledTerritory.Count / 3));

            // Get bonus for owning a continent
            foreach (Continent continent in Map.map)
            {
                if (continent.CheckIfSoleOwned(this))
                {
                    gainedArmy += continent.controlValue;

                }
            }

            // Auto trade a set when the player has 5 cards.
            if (cards.Count == 5)
            {
                gainedArmy += TradeSet();
            }

            // Give a minimum of 3 armies
            if (gainedArmy < 3)
            {
                reinforcmentOwned += 3;
            }
            else
            {
                Console.WriteLine($"You gained {gainedArmy} at the start of your turn.\n");
                reinforcmentOwned += gainedArmy;
            }
        #endregion


        PLACE_ARMY:
            // Query amount of which territory to reinforce
            Console.WriteLine($"You have {reinforcmentOwned} armies to use.\n");
            Console.Write("Choose the name of a territory you wish to reinforce - ");

            string displayOwnedTerritory = "";

            // List the territories and the population
            foreach (Country owned in controlledTerritory)
            {
                if (owned != controlledTerritory.First())
                {
                    displayOwnedTerritory += ", ";
                }

                displayOwnedTerritory += $"{owned.countryName} - [{owned.Population}]";
            }
            displayOwnedTerritory += ": ";
            Console.Write(displayOwnedTerritory);


            #region Find_Owned_Territory
            string reinforceTerritory = "";
            bool validOption = false;
            Country reinforce = null;
            while (validOption == false)
            {
                reinforceTerritory = Console.ReadLine();

                foreach (Country owned in controlledTerritory)
                {
                    if (owned.countryName.ToLower() == reinforceTerritory.ToLower())
                    {
                        validOption = true;
                        reinforce = owned;
                    }
                }

                if (validOption == false)
                {
                    Console.Write("Please choose a valid option: ");
                }
            }
            #endregion


            #region Add_Reinforcements
            Console.Write("\nHow many armies do you want to add? ");
            int reinforcementUsed = Convert.ToInt32(Console.ReadLine());

            while (reinforcementUsed > reinforcmentOwned && reinforcmentOwned < 0)
            {
                Console.Write($"{reinforcementUsed} is not a valid option. Please choose positive number that is less than {reinforcmentOwned}");
                reinforcementUsed = Convert.ToInt32(Console.ReadLine());
            }

            reinforce.Population += reinforcementUsed;
            reinforcmentOwned -= reinforcementUsed;
            armyUsed += reinforcementUsed;

            if (reinforcmentOwned == 0)
            {
                Console.WriteLine("No more reinforcements to place. Reinforcement Phase automatically ends.");
                return;
            }

            Console.Write($"You have {reinforcmentOwned} armies to use. Do you want to reinforce another territory? (Y/N): ");
            string again = "";

            while (again.ToUpper() != "Y" && again.ToUpper() != "N")
            {
                again = Console.ReadLine();
            }

            if (again == "Y")
            {
                goto PLACE_ARMY;
            }
            #endregion
        }

        /// <summary>
        /// Attack Phase
        /// </summary>
        private void Attack()
        {
            Console.WriteLine("\nAttack Phase");
            Console.WriteLine("------------");
        ATTACK_AGAIN:
            List<Country> ignore = new List<Country>(); // Create list of countries they can ignore.

            // Query player if they wish to attack
            #region Player_Attack?

            string attackAgain = "";

            while (attackAgain.ToUpper() != "Y" && attackAgain.ToUpper() != "N")
            {
                Console.Write($"Do you want to attack? (Y/N): ");
                attackAgain = Console.ReadLine();
            }

            if (attackAgain.ToUpper() == "N")
            {
                return;
            }

        #endregion

        // Query player which territory they want to attack from
        #region Choose_Attack_Point

        CHOOSE_ATTACKER:
            Console.Write("\nChoose territory you want to attack from - ");
            Country attackingTerritory = null;

            string owned = "";

            // Lists the valid territories (population is greater than 1
            List<Country> validAttackers = new List<Country>();
            foreach (Country ownedCountry in controlledTerritory)
            {
                if (ownedCountry != controlledTerritory.First())
                {
                    owned += ", ";
                }

                if (ownedCountry.Population > 1 || ignore.Contains(ownedCountry) == false)
                {
                    owned += $"{ownedCountry.countryName} - [{ownedCountry.Population}]";
                    validAttackers.Add(ownedCountry);
                }
            }
            owned += ": ";
            Console.Write(owned);


            string attackingName = Console.ReadLine();
            // Finds the territory they want to attack from
            foreach (Country attackOrigin in validAttackers)
            {
                if (attackOrigin.countryName.ToLower() == attackingName.ToLower())
                {
                    attackingTerritory = attackOrigin;
                    break;
                }
            }
            #endregion

            #region Choose_Enemy_Territory
            List<Country> enemyCountryList = new List<Country>();
            string enemyCountries = "";

            // Finds the neighboring (adjacent) countries of the territory players want to attack from
            foreach (Continent continent in Map.map)
            {
                foreach (Country enemy in continent.countries.GetNeighbors(attackingTerritory))
                {

                    if (continent.countries.GetNeighbors(attackingTerritory).Count == 0)
                    {
                        break; // Wrong continent
                    }

                    // Displays the enemy country and the player who owns it
                    if (enemy.owner != this)
                    {
                        if (enemyCountries != "")
                        {
                            enemyCountries += ", ";
                        }

                        enemyCountries += $"{enemy.countryName} - [{enemy.owner.playerColour}]";
                        enemyCountryList.Add(enemy);
                    }
                }


                if (enemyCountryList.Count > 0)
                {
                    break; // Exits the loop when the attacker is found.
                }

            }

            // Ensures that the player cannot attack from a territory that they own all adjacent territories
            if (enemyCountryList.Count == 0)
            {
                Console.WriteLine($"\nYou own the territories adjacent to {attackingTerritory.countryName}");
                ignore.Add(attackingTerritory);
                goto CHOOSE_ATTACKER;
            }

            Console.WriteLine($"\nAdjacent Countries to {attackingTerritory.countryName}: {enemyCountries}");

            Console.Write("\nChoose a territory to attack: ");
            string enemyTerritory = Console.ReadLine();
            Country defendingTerritory = null;
            foreach (Country enemyCountry in enemyCountryList)
            {
                if (enemyCountry.countryName.ToLower() == enemyTerritory.ToLower())
                {
                    defendingTerritory = enemyCountry;
                }
            }
            #endregion

            #region Attack
            int diceCount = 0;

            while (diceCount < 1 || diceCount > 3 && diceCount < attackingTerritory.Population)
            {
                Console.Write("Choose the number of dice to roll (1-3). You must have 1 more army than dice rolled: ");
                diceCount = Convert.ToInt32(Console.ReadLine());
            }

            int attackResult = GetHighestRoll(diceCount);
            int defendResult = defendingTerritory.owner.Defend(defendingTerritory);
            Console.Write($"\n{playerColour} rolled {attackResult}, while {defendingTerritory.owner.playerColour} rolled {defendResult}. ");
            if (attackResult > defendResult)
            {
                Console.WriteLine($"{playerColour} defeated {defendingTerritory.owner.playerColour}.");
                defendingTerritory.Population--;
            }
            else
            {
                Console.WriteLine($"{defendingTerritory.owner.playerColour} defeated {playerColour}.");
                attackingTerritory.Population--;
            }

            if (defendingTerritory.Population <= 0)
            {
                defendingTerritory.owner.LostTerritory(defendingTerritory);

                defendingTerritory.owner = this;
                controlledTerritory.Add(defendingTerritory);

                defendingTerritory.Population = diceCount;

                attackingTerritory.Population -= diceCount;

                successfulConquer = true;

                Console.WriteLine($"{playerColour} conquered {defendingTerritory.countryName}.");
            }

            #endregion

            goto ATTACK_AGAIN;
        }

        /// <summary>
        /// Rolls for defending your territory.
        /// </summary>
        /// <param name="defending">The country that is defending.</param>
        /// <returns>The highest roll needed for the attack phase.</returns>
        public int Defend(Country defending)
        {
            Console.WriteLine($"\nPlayer {playerColour}. Roll to defend.");

            int defendRoll = 0;

            while (defendRoll < 1 || defendRoll > 2 && defending.Population >= defendRoll)
            {
                Console.Write($"Choose the number of dice to roll (1-2). Number of dice cannot exceed the number of armies you have ({defending.Population}): ");
                defendRoll = Convert.ToInt32(Console.ReadLine());
            }

            return GetHighestRoll(defendRoll);
        }

        /// <summary>
        /// Fortification Phase
        /// </summary>
        private void Fortification()
        {
            Console.WriteLine("\nFortification Phase");
            Console.WriteLine("-------------------");

            // Query whether the player wants to fortify any territory.
            #region Player_Fortify?
            string fortify = "";

            while (fortify.ToUpper() != "Y" && fortify.ToUpper() != "N")
            {
                Console.Write("Do you want to fortify? (Y/N): ");
                fortify = Console.ReadLine();
            }

            if (fortify.ToUpper() == "N")
            {
                return;
            }
            #endregion


            // Choose where they want to move armies from
            #region Choose_Start
            Console.WriteLine("Choose the name of a territory you wish to move armies from - ");
            string displayOwnedTerritory = "";

            // Display all territories and their populations
            foreach (Country owned in controlledTerritory)
            {
                if (owned.Population == 1)
                {
                    continue; // Ignore places that cannot have armies moved from
                }

                if (owned != controlledTerritory.First())
                {
                    displayOwnedTerritory += ", ";
                }

                displayOwnedTerritory += $"{owned.countryName} - [{owned.Population}]";
            }
            displayOwnedTerritory += ": ";
            Console.Write(displayOwnedTerritory);

            // Checks for valid territory to move armies from.
            string startTerritory = "";
            Country start = null;
            bool validOption = false;
            while (validOption == false)
            {
                startTerritory = Console.ReadLine();

                foreach (Country owned in controlledTerritory)
                {
                    if (owned.countryName.ToLower() == startTerritory.ToLower() && owned.Population > 1)
                    {
                        validOption = true;
                        start = owned;
                    }
                }
            }
            #endregion


            // Choose territory to fortify
            #region Choose_Fortify
            Console.Write("Choose a territory you want to fortify - ");
            string adjacentOwned = "";
            List<Country> fortifiable = new List<Country>();

            // List all contected territory players can move their army to
            foreach (Continent continent in Map.map)
            {
                // Finds the starting territory and gets the list of adjacent territories
                foreach (Country country in continent.countries)
                {
                    if (country.countryName.ToLower() == startTerritory.ToLower() && country.owner == this)
                    {
                        foreach (Country adjacent in continent.countries.GetNeighbors(country))
                        {
                            if (adjacent != continent.countries.GetNeighbors(country).First())
                            {
                                adjacentOwned += ", ";
                            }

                            adjacentOwned += $"{adjacent.countryName} - [{adjacent.Population}]";
                            fortifiable.Add(adjacent);
                        }
                    }
                }
            }
            adjacentOwned += ": ";
            Console.Write(adjacentOwned);

            // Gets the territory the players wants to fortify
            string forifyTerritory = "";
            Country choice = null;
            do
            {
                forifyTerritory = Console.ReadLine();

                foreach (Country option in fortifiable)
                {
                    if (option.countryName.ToLower() == forifyTerritory.ToLower())
                    {
                        choice = option;
                    }
                }

            } while (choice == null);
            #endregion


            // Move armies
            #region Fortify
            int moveAmount = -1;
            while (moveAmount < 0 && moveAmount >= start.Population)
            {
                Console.WriteLine("Choose how many armies you wish to move (a miminum of 1 army must stay behind): ");
                moveAmount = Convert.ToInt32(Console.ReadLine());
            }

            choice.Population += moveAmount;
            start.Population -= moveAmount;
            #endregion
        }

        /// <summary>
        /// End turn
        /// </summary>
        private void EndTurn()
        {
            if (successfulConquer)
                DrawCard();

            successfulConquer = false;

            Console.WriteLine($"Player {playerColour}'s turn ends.");
        }

        /// <summary>
        /// Draw and add card to the list of cards. If there is 5 or more cards, trade a set.
        /// </summary>
        public void DrawCard()
        {
            Card newCard = new Card();

            cards.Add(newCard);

            if (cards.Count >= 5)
            {
                reinforcmentOwned += TradeSet();
            }
        }
        #endregion


        #region HELPERS
        public void SetupPlayerTerritory()
        {
            if (armyUsed == 0)
            {
                int population = (int)Math.Ceiling((double)reinforcmentOwned / controlledTerritory.Count);

                foreach (Country owned in controlledTerritory)
                {
                    owned.Population = population;
                    armyUsed += population;
                    reinforcmentOwned -= (reinforcmentOwned - population) >= 0 ? population : reinforcmentOwned;
                }
            }
        }


        /// <summary>
        /// Grants bonus armies depending on the number of sets given.
        /// </summary>
        /// <returns>The bonus army gained from giving a set.</returns>
        private int SetBonus()
        {
            int bonusArmy = 0;
            if (cardTradeBonus)
            {
                bonusArmy = 2;
                cardTradeBonus = false;
            }

            return 5 + bonusArmy + (5 * (setTraded - 1)) < 3 ? 3 : 5 + bonusArmy + (5 * (setTraded - 1));
        }

        /// <summary>
        /// Finds a set of matching or different cards.
        /// </summary>
        /// <returns>A list of cards that forms a set, either all the same or all different.</returns>
        private List<Card> FindSet()
        {
            List<Card> set = new List<Card>();

            // Checks for match sets
            Predicate<Card> match = card => card.symbol.Equals(cards[0].symbol);
            set = cards.FindAll(match);

            // Set not formed
            if (set.Count < 3)
            {
                List<Card> exempt = set; // Hold the duplicate

                set.Clear(); // Clears the return variable

                set.Add(exempt[0]); // Adds the 1 duplicate to the set

                foreach (Card card in cards)
                {
                    foreach (Card ignore in exempt)
                    {
                        if (!card.Equals(ignore))
                        {
                            set.Add(card); // Add card to set
                        }
                    }

                    // Exit when set is formed
                    if (set.Count == 3)
                    {
                        break;
                    }
                }
            }

            return set;
        }

        /// <summary>
        /// Gets the highest roll from a number of dice rolls.
        /// </summary>
        /// <param name="diceCount">The number of dice to roll.</param>
        /// <returns></returns>
        private int GetHighestRoll(int diceCount)
        {
            int highest = 0;

            List<int> rolls = new List<int>();
            for (int i = 0; i < diceCount; i++)
            {
                rolls.Add(new Random().Next(1, 7)); // Max is exclusive
            }

            highest = rolls.First();
            foreach (int roll in rolls)
            {
                highest = highest < roll ? highest : roll;
            }

            return highest;
        }

        /// <summary>
        /// Removes a territory from your control.
        /// </summary>
        /// <param name="lost">The country lost.</param>
        public void LostTerritory(Country lost)
        {
            controlledTerritory.Remove(lost);

            if (controlledTerritory.Count == 0)
            {
                isElimiated = true;
            }
        }
        #endregion
    }



    public enum PlayerColour
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Yellow = 4,
        Cyan = 5,
        Magenta = 6,
        Grey = 7,
    }
}

