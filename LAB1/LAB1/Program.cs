using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LAB1
{
    enum CountryAnalysisState
    {
        OK,
        FirstLetterDoNotMatchLastLetter,
        CountryUsed,
        NoSuchCountry
    }

    enum GameState
    {
        ComputerTurn,
        UserTurn,
        UserNumberOfTriesEnded,
        ComputerCannotFindCountry,
        LoadingLevel,
        Error
    }

    class CountryBrain
    {
        public void loadCountriesFromFile(string fileName)
        {
            avaliableCountries = File.ReadAllLines(fileName).ToList();
            if (avaliableCountries.Count == 0)
            {
                gameState = GameState.Error;
            }
            else
            {
                gameState = GameState.ComputerTurn;
            }
        }

        private List<string> avaliableCountries = new List<string> { };
        private List<string> usedCountries = new List<string> { };
        private string lastShownCountry;
        public int countOfTries { get; private set; } = 3;

        private bool isUserHaveAnyTries()
        {
            countOfTries--;
            return countOfTries > 0;
        }

        private void moveToUsedCountries(string countryToUse)
        {
            int indexOfCountry = avaliableCountries.FindIndex(countryToFind =>
                countryToFind.Equals(countryToUse, StringComparison.OrdinalIgnoreCase));

            if (indexOfCountry != -1)
            {
                usedCountries.Add(avaliableCountries[indexOfCountry]);
                avaliableCountries.RemoveAt(indexOfCountry);
            }
        }

        private string generateRandomCountry()
        {
            Random rand = new Random();
            int countryIndex = rand.Next(0, avaliableCountries.Count);

            string localGenerateRandomCountry = avaliableCountries[countryIndex];

            return localGenerateRandomCountry;
        }

        private CountryAnalysisState compare(string countryToAnalyze)
        {
            string lastChar = lastShownCountry.Substring(lastShownCountry.Length - 1, 1);
            string firstChar = countryToAnalyze.Substring(0, 1);

            if (lastChar.ToLower() != firstChar.ToLower())
            {
                return CountryAnalysisState.FirstLetterDoNotMatchLastLetter;
            }

            if (usedCountries.FindIndex(countryToFind =>
                countryToFind.Equals(countryToAnalyze, StringComparison.OrdinalIgnoreCase)) != -1)
            {
                return CountryAnalysisState.CountryUsed;
            }

            if (avaliableCountries.FindIndex(countryToFind =>
                countryToFind.Equals(countryToAnalyze, StringComparison.OrdinalIgnoreCase)) == -1)
            {
                return CountryAnalysisState.NoSuchCountry;
            }

            moveToUsedCountries(countryToAnalyze);
            lastShownCountry = countryToAnalyze;
            return CountryAnalysisState.OK;
        }

        public GameState gameState = GameState.LoadingLevel;

        public string selectNextCountry()
        {
            string localSelectNextCountry = "";
            if (lastShownCountry == null)
            {
                localSelectNextCountry = generateRandomCountry();
                gameState = GameState.UserTurn;
            }
            else
            {
                string lastChar = lastShownCountry.Substring(lastShownCountry.Length - 1, 1);

                int lastSymbolCountry = avaliableCountries.FindIndex(countryToFind =>
                    countryToFind.StartsWith(lastChar, StringComparison.OrdinalIgnoreCase));

                if (lastSymbolCountry == -1)
                {
                    gameState = GameState.ComputerCannotFindCountry;
                }
                else
                {
                    localSelectNextCountry = avaliableCountries[lastSymbolCountry];
                    gameState = GameState.UserTurn;
                }
            }

            moveToUsedCountries(localSelectNextCountry);
            lastShownCountry = localSelectNextCountry;

            return localSelectNextCountry;
        }

        public bool isUserCountryIncorrect(string userInput)
        {
            bool isUserCountryIncorrect = true;
            if (String.IsNullOrEmpty(userInput))
            {
                return isUserCountryIncorrect;
            }

            switch (compare(userInput))
            {
                case CountryAnalysisState.CountryUsed:
                    Console.WriteLine("Country Used");
                    isUserCountryIncorrect = true;
                    break;

                case CountryAnalysisState.NoSuchCountry:
                    Console.WriteLine("NoSuchCountry");
                    isUserCountryIncorrect = true;
                    break;

                case CountryAnalysisState.FirstLetterDoNotMatchLastLetter:
                    Console.WriteLine("FirstLetterDoNotMatchLastLetter");
                    isUserCountryIncorrect = true;
                    break;

                case CountryAnalysisState.OK:
                    //to select next country by computer
                    isUserCountryIncorrect = false;
                    gameState = GameState.ComputerTurn;
                    break;
            }

            if (isUserCountryIncorrect)
            {
                if (isUserHaveAnyTries() == false)
                {
                    gameState = GameState.UserNumberOfTriesEnded;
                }
            }

            return isUserCountryIncorrect;
        }


        class Program
        {
            static void Main()
            {
                //implemented:
                //1 random country
                //2 remove from array
                //3 get userInput
                //4 check last compchar == first userchar
                //4.1 if !match goto step 3
                //5 check is this country in array exist
                //5.1 if !exist goto step 3
                //6 if exist get last char
                //7 search first country wich starts with userInput last char
                //8 number of tries for user
                //9 read file
                //not implemented:
                //10 scan array of countries for first letters set
                //11 hints (???)
                //12 levels (Countries -> Capitals -> etc.)

                CountryBrain countriesBrain = new CountryBrain();
                while (true)
                {
                    switch (countriesBrain.gameState)
                    {
                        case GameState.ComputerTurn:
                            Console.WriteLine("My Country: " + countriesBrain.selectNextCountry());
                            break;
                        case GameState.UserTurn:
                            Console.WriteLine("Your Country: ");
                            string userInput = "";
                            userInput = Console.ReadLine();
                            if (countriesBrain.isUserCountryIncorrect(userInput))
                            {
                                Console.WriteLine(
                                    "Wrong Country. Try Again. Lifes left: " + countriesBrain.countOfTries);
                            }

                            break;
                        case GameState.UserNumberOfTriesEnded:
                            Console.WriteLine("U Loose. As NumberOFTriesEnded");
                            return;
                            break;
                        case GameState.ComputerCannotFindCountry:
                            Console.WriteLine("Cannot find Country. You Win.");
                            return;
                            break;
                        case GameState.LoadingLevel:
                            Console.WriteLine("Loading Level From File. Please Wait.");
                            string fileName = "countries.txt";
                            countriesBrain.loadCountriesFromFile(fileName);
                            break;
                        case GameState.Error:
                            Console.WriteLine("Some Error. Need Debug. Stop procces.");
                            return;
                            break;
                    }
                }
            }
        }
    }
}