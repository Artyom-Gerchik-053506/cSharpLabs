using System;
using System.Linq;

namespace LAB3
{
    public class SwimmingManager : Manager
    {
        private const int CountOfSwimmers = 5;
        private State _state = State.Initial;
        private readonly Swimmer[] arrayOfSwimmers = new Swimmer[CountOfSwimmers];

        private Swimmer.Anthropology generateRandomAntropology()
        {
            var random = new Random();
            Swimmer.Anthropology anthropologyStruct;
            anthropologyStruct.footSize = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.palmSize = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.lungVolume = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.shoulderWidth = Math.Round(random.NextDouble(), 2);
            return anthropologyStruct;
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
                var randomNameIndex = random.Next(0, ArrayOfRandomNamesForGenerate.Length - 1);
                var swimmer = new Swimmer(ArrayOfRandomNamesForGenerate[randomNameIndex], randomAge, randomSalary);
                swimmer.SwimmerAnthropology = generateRandomAntropology();
                swimmer[Swimmer.SwimmingStyle.Freestyle] = Math.Round(randomFreestyle + 0.5, 2);
                swimmer[Swimmer.SwimmingStyle.Butterfly] = Math.Round(randomButterfly + 0.6, 2);
                arrayOfSwimmers[index] = swimmer;
            }
        }

        private void InfoAboutSwimmers()
        {
            for (var index = 0; index < CountOfSwimmers; index++)
                Console.WriteLine($"{arrayOfSwimmers[index].Description()}\n");
        }

        private void InfoAboutSwimmers(Human.descriptionInfoState state)
        {
            for (var index = 0; index < CountOfSwimmers; index++)
                Console.WriteLine($"{arrayOfSwimmers[index].Description(state)}\n");
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
                    break;
            }
        }

        private void SwimWithChoosenStyle(Swimmer.SwimmingStyle style)
        {
            var arrayOfSwimmersOrdered = arrayOfSwimmers.OrderBy(a => a[style]).ToArray();
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
                var userInput = Console.ReadLine();
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
                            InfoAboutSwimmers(Human.descriptionInfoState.ShowAge);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input");
                        }

                        break;
                    case "3":
                        if (_state == State.SwimmersAreReady)
                        {
                            InfoAboutSwimmers(Human.descriptionInfoState.SpecificInfo);
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