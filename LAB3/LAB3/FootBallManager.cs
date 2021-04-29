using System;

namespace LAB3
{
    public class FootBallManager : Manager
    {
        private State state = State.Initial;
        private FootBallTeam team1;
        private FootBallTeam team2;

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
            anthropologyStruct.footSize = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.jumpHeight = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.jumpLength = Math.Round(random.NextDouble(), 2);
            anthropologyStruct.heartBeatAfter100MetersSprint = random.Next(60, 140);
            return anthropologyStruct;
        }

        private FootBallTeam GenerateRandomTeam(string Name)
        {
            var Team = new FootBallTeam(Name);
            for (var index = 0; index < FootBallTeam.numberOfPlayers; index++)
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
                }

                Team[index] = player;
            }

            return Team;
        }

        public string PlayVersus(FootBallTeam team1, FootBallTeam team2)
        {
            var randomScore = new Random();
            var team1Goals = randomScore.Next(0, 5);
            var team2Goals = randomScore.Next(0, 5);
            return $"Score Is: {team1Goals.ToString()}:{team2Goals.ToString()}";
        }

        private void ShowUserMenu()
        {
            switch (state)
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
                        switch (state)
                        {
                            case State.Initial:
                                team1 = GenerateRandomTeam("Team1");
                                team2 = GenerateRandomTeam("Team2");
                                state = State.TeamsGenerated;
                                break;
                            case State.TeamsGenerated:
                                Console.WriteLine(PlayVersus(team1, team2));
                                break;
                        }

                        break;
                    case "2":
                        if (state == State.TeamsGenerated)
                        {
                            team1.Description(Human.descriptionInfoState.ShowAge);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input.");
                        }

                        break;
                    case "3":
                        if (state == State.TeamsGenerated)
                        {
                            team2.Description(Human.descriptionInfoState.ShowAge);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input.");
                        }

                        break;
                    case "4":
                        if (state == State.TeamsGenerated)
                        {
                            team1.Description(Human.descriptionInfoState.SpecificInfo);
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input.");
                        }

                        break;
                    case "5":
                        if (state == State.TeamsGenerated)
                        {
                            team2.Description(Human.descriptionInfoState.SpecificInfo);
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