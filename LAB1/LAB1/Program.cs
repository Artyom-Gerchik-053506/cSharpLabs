using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LAB1
{
    enum CountryAnalysisState
    {
        Ok,
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
        public void LoadCountriesFromFile(string fileName)
        {
            avaliableCountries = File.ReadAllLines(fileName).ToList();
            if (avaliableCountries.Count == 0)
            {
                GameState = GameState.Error;
            }
            else
            {
                GameState = GameState.ComputerTurn;
            }
        }

        private List<string> avaliableCountries = new List<string> { };
        private List<string> usedCountries = new List<string> { };
        private string lastShownCountry;
        public int CountOfTries { get; private set; } = 3;

        private bool IsUserHaveAnyTries()
        {
            CountOfTries--;
            return CountOfTries > 0;
        }

        private void MoveToUsedCountries(string countryToUse)
        {
            int indexOfCountry = avaliableCountries.FindIndex(countryToFind =>
                countryToFind.Equals(countryToUse, StringComparison.OrdinalIgnoreCase));
            if (indexOfCountry != -1)
            {
                usedCountries.Add(avaliableCountries[indexOfCountry]);
                avaliableCountries.RemoveAt(indexOfCountry);
            }
        }

        private string GenerateRandomCountry()
        {
            Random rand = new Random();
            int countryIndex = rand.Next(0, avaliableCountries.Count);
            string localGenerateRandomCountry = avaliableCountries[countryIndex];
            return localGenerateRandomCountry;
        }

        private CountryAnalysisState Compare(string countryToAnalyze)
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

            MoveToUsedCountries(countryToAnalyze);
            lastShownCountry = countryToAnalyze;
            return CountryAnalysisState.Ok;
        }

        public GameState GameState = GameState.LoadingLevel;

        public string SelectNextCountry()
        {
            string localSelectNextCountry = "";
            if (lastShownCountry == null)
            {
                localSelectNextCountry = GenerateRandomCountry();
                GameState = GameState.UserTurn;
            }
            else
            {
                string lastChar = lastShownCountry.Substring(lastShownCountry.Length - 1, 1);
                int lastSymbolCountry = avaliableCountries.FindIndex(countryToFind =>
                    countryToFind.StartsWith(lastChar, StringComparison.OrdinalIgnoreCase));
                if (lastSymbolCountry == -1)
                {
                    GameState = GameState.ComputerCannotFindCountry;
                }
                else
                {
                    localSelectNextCountry = avaliableCountries[lastSymbolCountry];
                    GameState = GameState.UserTurn;
                }
            }

            MoveToUsedCountries(localSelectNextCountry);
            lastShownCountry = localSelectNextCountry;
            return localSelectNextCountry;
        }

        public bool IsUserCountryIncorrect(string userInput)
        {
            bool isUserCountryIncorrect = true;
            if (String.IsNullOrEmpty(userInput))
            {
                return isUserCountryIncorrect;
            }

            switch (Compare(userInput))
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
                case CountryAnalysisState.Ok:
                    //to select next country by computer
                    isUserCountryIncorrect = false;
                    GameState = GameState.ComputerTurn;
                    break;
            }

            if (isUserCountryIncorrect)
            {
                if (IsUserHaveAnyTries() == false)
                {
                    GameState = GameState.UserNumberOfTriesEnded;
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
                    switch (countriesBrain.GameState)
                    {
                        case GameState.ComputerTurn:
                            Console.WriteLine("My Country: " + countriesBrain.SelectNextCountry());
                            break;
                        case GameState.UserTurn:
                            Console.WriteLine("Your Country: ");
                            string userInput = "";
                            userInput = Console.ReadLine();
                            if (countriesBrain.IsUserCountryIncorrect(userInput))
                            {
                                Console.WriteLine(
                                    "Wrong Country. Try Again. Lifes left: " + countriesBrain.CountOfTries);
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
                            countriesBrain.LoadCountriesFromFile(fileName);
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