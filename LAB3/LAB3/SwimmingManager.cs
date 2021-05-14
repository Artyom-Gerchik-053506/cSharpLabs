using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LAB3
{
    public class SwimmingManager : Manager
    {
        private static int CountOfSwimmers = 5;
        private State _state = State.Initial;
        private Swimmer[] _arrayOfSwimmers = new Swimmer[CountOfSwimmers];

        public delegate void SwimmedDistance(Swimmer.SwimmingStyle style, float time);

        public event SwimmedDistance Timer;

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

        void checkIfSetupisOK()
        {
            if (CountOfSwimmers != 5)
            {
                throw new Exception("CountOfSwimmers is not setted");
            }
        }

        private delegate void SwimmerAdditionalConfigurator(Swimmer swimmer);

        private void additionalConfig(Swimmer swimmer)
        {
            var random = new Random();
            var randomButterfly = random.NextDouble();
            var randomFreestyle = random.NextDouble();
            swimmer.SwimmerAnthropology = generateRandomAntropology();
            if (PerformInterface(swimmer))
            {
                swimmer[Swimmer.SwimmingStyle.Freestyle] = Math.Round(randomFreestyle + 8, 2);
                swimmer[Swimmer.SwimmingStyle.Butterfly] = Math.Round(randomButterfly + 9, 2);
            }
            else
            {
                swimmer[Swimmer.SwimmingStyle.Freestyle] = Math.Round(randomFreestyle + 3, 2);
                swimmer[Swimmer.SwimmingStyle.Butterfly] = Math.Round(randomButterfly + 4, 2);
            }
        }

        private void GenerateSwimmers(SwimmerAdditionalConfigurator swimmerConfig)
        {
            
                try
                {
                    checkIfSetupisOK();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    CountOfSwimmers = 5;
                    _arrayOfSwimmers = new Swimmer[CountOfSwimmers];
                }

                for (var index = 0; index < CountOfSwimmers; index++)
            {
                var random = new Random();
                var randomAge = random.Next(16, 35);
                //inserting a negative values to check property works good.
                var randomSalary = random.Next(-1000, 3000);
                var randomNameIndex = random.Next(0, ArrayOfRandomNamesForGenerate.Count);
                var swimmer = new Swimmer(ArrayOfRandomNamesForGenerate[randomNameIndex], randomAge, randomSalary);
                swimmerConfig(swimmer);
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

        public string asterixOffset(float distance) // max 100
        {
            var resulotion = 100;
            string result = "|";
            float share = distance / 100;
            //Console.WriteLine($"Share: {share.ToString()}; // logs
            for (int i = 0; i < resulotion; i++)
            {
                float temp = i;
                // Console.WriteLine($"temp / resulotion : {(temp / resulotion).ToString()}"); // logs
                // Console.WriteLine($"temp+1 / resulotion : {((temp + 1.0) / resulotion).ToString()}"); // logs
                if (temp / resulotion <= share && share <= (temp + 1.0) / resulotion)
                {
                    result += "*";
                }
                else
                {
                    result += " ";
                }
            }

            result += "|";
            return result;
        }

        private void ConsoleOutput(List<float> distanceArray)
        {
            var resultString = "";
            for (int index = 0; index < distanceArray.Count; index++)
            {
                float distanceForCurrentSwimmer = distanceArray[index];
                distanceForCurrentSwimmer = Math.Min(distanceForCurrentSwimmer, 100);
                resultString += asterixOffset(distanceForCurrentSwimmer);
                resultString += "\n";
            }

            //Console.Clear();
            Console.SetCursorPosition(0, Console.CursorTop - distanceArray.Count);
            Console.Write(resultString);
        }

        private void testBeforeSwimming()
        {
            List<Swimmer> swimmersForDelete = new List<Swimmer>();
            Wada wada = new Wada();
            for (int index = 0; index < _arrayOfSwimmers.Length; index++)
            {
                try
                {
                    wada.checkForDoping(_arrayOfSwimmers[index]);
                }
                catch (Exception E)
                {
                    Console.WriteLine($"{E.Message} For {_arrayOfSwimmers[index].Name}");
                    swimmersForDelete.Add(_arrayOfSwimmers[index]);
                }
            }

            for (int jindex = 0; jindex < swimmersForDelete.Count; jindex++)
            {
                Swimmer temp = swimmersForDelete[jindex];
                _arrayOfSwimmers = _arrayOfSwimmers.Where((source, index) => source != temp).ToArray();
                CountOfSwimmers--;
            }
        }

        private void SwimWithChoosenStyle(Swimmer.SwimmingStyle style)
        {
            testBeforeSwimming();
            // Add empty lines
            foreach (var swimmer in _arrayOfSwimmers)
            {
                Console.WriteLine("");
            }

            Dictionary<Swimmer, bool> SwimmersFinsihed100Meters = new Dictionary<Swimmer, bool>();
            List<float> distancesList = new List<float>();
            for (int index = 0; index < _arrayOfSwimmers.Length; index++)
            {
                var swimmer = _arrayOfSwimmers[index];
                Timer += delegate(Swimmer.SwimmingStyle style, float time)
                {
                    var distance = swimmer.SwimmedDistance(style, time);
                    distancesList.Add(distance);
                };
                swimmer.Finished += () => SwimmersFinsihed100Meters[swimmer] = true;
            }

            int step = 1;
            while (SwimmersFinsihed100Meters.Count < _arrayOfSwimmers.Length)
            {
                float timerStep = (float) 1;
                Timer?.Invoke(style, timerStep * step);
                ConsoleOutput(distancesList);
                distancesList.Clear();
                step++;
                Thread.Sleep(1000 * (int) timerStep);
            }

            // TODO delete from Timer delagates ???
            var arrayOfSwimmersOrdered = _arrayOfSwimmers.OrderBy(a => a[style]).ToArray();
            for (var index = CountOfSwimmers - 1; index >= 0; index--)
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
                                GenerateSwimmers(swimmer => additionalConfig(swimmer));
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