using System;

namespace LAB3
{
    public class FootBallManager : Manager
    {
        public bool PerformFight(IMyOwnInterfaceForFootBallTeam myTeam)
        {
            return myTeam.Fight();
        }
        
        private State _state = State.Initial;
        private FootBallTeam _team1;
        private FootBallTeam _team2;

        public bool PerformInterface(IMyOwnInterfaceForFootBallPlayers myObject)
        {
            return myObject.Stumbled();
        }

        private static FootBallPlayer.Position GenerateRandomPosition()
        {
            var randomPosition = new Random();
            var tempPosition = randomPosition.Next(0, 4);
            return (FootBallPlayer.Position) tempPosition;
        }

        private FootBallPlayer.Anthropology generateRandomAntropology()
        {
            var random = new Random();
            FootBallPlayer.Anthropology anthropologyStruct;
            anthropologyStruct.FootSize = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.JumpHeight = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.JumpLength = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.HeartBeatAfter100MetersSprint = random.Next(60, 140);
            return anthropologyStruct;
        }

        private FootBallTeam GenerateRandomTeam(string name)
        {
            var team = new FootBallTeam(name);
            for (var index = 0; index < FootBallTeam.NumberOfPlayers; index++)
            {
                var random = new Random();
                var randomSprint100Meters = Math.Round(random.NextDouble() * 10 + 5, 2);
                var randomAge = random.Next(16, 35);
                //inserting a negative values to check property works good.
                var randomSalary = random.Next(-1000, 3000);
                var randomNameIndex = random.Next(0, ArrayOfRandomNamesForGenerate.Count);
                var player = new FootBallPlayer($"{ArrayOfRandomNamesForGenerate[randomNameIndex]}", index,
                    GenerateRandomPosition(), randomSprint100Meters, randomAge, randomSalary);
                player.FootBallPlayerAnthropology = generateRandomAntropology();
                if (PerformInterface(player))
                {
                    player.Sprint100Meters = 100500;
                    player.FootBallPlayerAnthropology.HeartBeatAfter100MetersSprint = 228;
                }

                team[index] = player;
            }

            return team;
        }

        public string PlayVersus(FootBallTeam team1, FootBallTeam team2)
        {
            if (PerformFight(team1) && PerformFight(team2))
            {
                return "Fight On The Field, Game Canceled!";
            }
            
            var randomScore = new Random();
            var team1Goals = randomScore.Next(0, 5);
            var team2Goals = randomScore.Next(0, 5);
            return $"Score Is: {team1Goals.ToString()}:{team2Goals.ToString()}";
        }

        private void ShowUserMenu()
        {
            switch (_state)
            {
                case State.Initial:
                    Console.WriteLine("0 - Back");
                    Console.WriteLine("1 - Generate Teams");
                    break;
                case State.TeamsGenerated:
                    Console.WriteLine("0 - Back");
                    Console.WriteLine("1 - Play");
                    Console.WriteLine("2 - Info About First Team");
                    Console.WriteLine("3 - Info About Second Team");
                    Console.WriteLine("4 - Info About First Team & Antropology Metrics");
                    Console.WriteLine("5 - Info About Second Team & Antropology Metrics");
                    break;
            }
        }

        public void FootBallSwitch()
        {
            var footBallManagerControlsUserInput = true;
            while (footBallManagerControlsUserInput)
            {
                ShowUserMenu();
                var userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "0":
                        footBallManagerControlsUserInput = false;
                        break;
                    case "1":
                        switch (_state)
                        {
                            case State.Initial:
                                _team1 = GenerateRandomTeam("Team1");
                                _team2 = GenerateRandomTeam("Team2");
                                _state = State.TeamsGenerated;
                                break;
                            case State.TeamsGenerated:
                                Console.WriteLine(PlayVersus(_team1, _team2));
                                break;
                        }

                        break;
                    case "2":
                        if (_state == State.TeamsGenerated)
                        {
                            _team1.Description(Human.DescriptionInfoState.ShowAge);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input.");
                        }

                        break;
                    case "3":
                        if (_state == State.TeamsGenerated)
                        {
                            _team2.Description(Human.DescriptionInfoState.ShowAge);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input.");
                        }

                        break;
                    case "4":
                        if (_state == State.TeamsGenerated)
                        {
                            _team1.Description(Human.DescriptionInfoState.SpecificInfo);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input.");
                        }

                        break;
                    case "5":
                        if (_state == State.TeamsGenerated)
                        {
                            _team2.Description(Human.DescriptionInfoState.SpecificInfo);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input.");
                        }

                        break;
                    default:
                        Console.WriteLine("Wrong Input.");
                        break;
                }
            }
        }

        private enum State
        {
            Initial,
            TeamsGenerated
        }
    }
}