using System;
using System.Linq;

namespace LAB3
{
    public class SwimmingManager : Manager
    {
        private const int CountOfSwimmers = 5;
        private State _state = State.Initial;
        private readonly Swimmer[] _arrayOfSwimmers = new Swimmer[CountOfSwimmers];

        private void SortBySalariesFromZeroToMax()
        {
            Array.Sort(_arrayOfSwimmers, new SwimmersSlariesComparer());
        }

        private Swimmer.Anthropology generateRandomAntropology()
        {
            var random = new Random();
            Swimmer.Anthropology anthropologyStruct;
            anthropologyStruct.FootSize = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.PalmSize = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.LungVolume = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.ShoulderWidth = Math.Round(random.NextDouble(), 2);
            return anthropologyStruct;
        }

        private bool PerformInterface(IMyOwnInterfaceForSwimmers myObject)
        {
            return myObject.ChokedWithWater();
        }

        private void GenerateSwimmers()
        {
            for (var index = 0; index < CountOfSwimmers; index++)
            {
                var random = new Random();
                var randomButterfly = random.NextDouble();
                var randomFreestyle = random.NextDouble();
                var randomAge = random.Next(16, 35);
                //inserting a negative values to check property works good.
                var randomSalary = random.Next(-1000, 3000);
                var randomNameIndex = random.Next(0, ArrayOfRandomNamesForGenerate.Count);
                var swimmer = new Swimmer(ArrayOfRandomNamesForGenerate[randomNameIndex], randomAge, randomSalary);
                swimmer.SwimmerAnthropology = generateRandomAntropology();
                if (PerformInterface(swimmer))
                {
                    swimmer[Swimmer.SwimmingStyle.Freestyle] = Math.Round(randomFreestyle + 5, 2);
                    swimmer[Swimmer.SwimmingStyle.Butterfly] = Math.Round(randomButterfly + 6, 2);
                }
                else
                {
                    swimmer[Swimmer.SwimmingStyle.Freestyle] = Math.Round(randomFreestyle + 0.5, 2);
                    swimmer[Swimmer.SwimmingStyle.Butterfly] = Math.Round(randomButterfly + 0.6, 2);
                }

                _arrayOfSwimmers[index] = swimmer;
            }
        }

        private void InfoAboutSwimmers()
        {
            for (var index = 0; index < CountOfSwimmers; index++)
                Console.WriteLine($"{_arrayOfSwimmers[index].Description()}\n");
        }

        private void InfoAboutSwimmers(Human.DescriptionInfoState state)
        {
            for (var index = 0; index < CountOfSwimmers; index++)
                Console.WriteLine($"{_arrayOfSwimmers[index].Description(state)}\n");
        }

        private void ShowUserMenu()
        {
            switch (_state)
            {
                case State.Initial:
                    Console.WriteLine("0 - Back");
                    Console.WriteLine("1 - Generate Swimmers");
                    break;
                case State.SwimmersAreReady:
                    Console.WriteLine("0 - Back");
                    Console.WriteLine("1 - Info About Swimmers");
                    Console.WriteLine("2 - Info About Swimmers With Age");
                    Console.WriteLine("3 - Anthropology Metrics");
                    Console.WriteLine("4 - Perform Swim FreeStyle");
                    Console.WriteLine("5 - Perform Swim ButterFly");
                    Console.WriteLine("6 - Sort By Salaries From Zero To Max");
                    break;
            }
        }

        private void SwimWithChoosenStyle(Swimmer.SwimmingStyle style)
        {
            var arrayOfSwimmersOrdered = _arrayOfSwimmers.OrderBy(a => a[style]).ToArray();
            for (var index = 2; index != -1; index--)
                Console.WriteLine(
                    $"Place: {(index + 1).ToString()} Time: {arrayOfSwimmersOrdered[index][style].ToString()} Name: {arrayOfSwimmersOrdered[index].Name}");
        }

        public void SwimmingSwitch()
        {
            var swimmingManagerControlsUserInput = true;
            while (swimmingManagerControlsUserInput)
            {
                ShowUserMenu();
                Console.WriteLine();
                Console.Write("Your Choice: ");
                var userInput = Console.ReadLine();
                Console.WriteLine();
                switch (userInput)
                {
                    case "0":
                        swimmingManagerControlsUserInput = false;
                        break;
                    case "1":
                        switch (_state)
                        {
                            case State.Initial:
                                GenerateSwimmers();
                                _state = State.SwimmersAreReady;
                                break;
                            case State.SwimmersAreReady:
                                InfoAboutSwimmers();
                                break;
                        }

                        break;
                    case "2":
                        if (_state == State.SwimmersAreReady)
                        {
                            InfoAboutSwimmers(Human.DescriptionInfoState.ShowAge);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input");
                        }

                        break;
                    case "3":
                        if (_state == State.SwimmersAreReady)
                        {
                            InfoAboutSwimmers(Human.DescriptionInfoState.SpecificInfo);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input");
                        }

                        break;
                    case "4":
                        if (_state == State.SwimmersAreReady)
                        {
                            SwimWithChoosenStyle(Swimmer.SwimmingStyle.Freestyle);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input");
                        }

                        break;
                    case "5":
                        if (_state == State.SwimmersAreReady)
                        {
                            SwimWithChoosenStyle(Swimmer.SwimmingStyle.Butterfly);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input");
                        }

                        break;
                    case "6":
                        if (_state == State.SwimmersAreReady)
                        {
                            SortBySalariesFromZeroToMax();
                            Console.WriteLine("Sorted!");
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input.");
                        }

                        break;
                    default:
                        Console.WriteLine("Wrong Input");
                        break;
                }
            }
        }

        private enum State
        {
            Initial,
            SwimmersAreReady
        }
    }
}