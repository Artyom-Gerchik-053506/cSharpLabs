using System;

namespace LAB3
{
    public class FootBallTeam
    {
        public const int NumberOfPlayers = 30;

        public Sportsman[] Players;
        //add if need
        /*public Sportsman[] Coaches;
        public Human[] Doctors;
        public Human[] Managers;*/

        public FootBallTeam(string name)
        {
            Name = name;
            Players = new FootBallPlayer[NumberOfPlayers];
            //add if need
            /*Coaches = new Sportsman[3];
            Doctors = new Human[2];
            Managers = new Human[5];*/
        }

        public string Name { get; set; }

        public FootBallPlayer this[int index]
        {
            get => (FootBallPlayer) Players[index];
            set => Players[index] = value;
        }

        public void Description(Human.DescriptionInfoState state)
        {
            Console.WriteLine($"Name Of Team: {Name}\n");
            //info about footballists
            for (var index = 0; index < NumberOfPlayers; index++)
                Console.WriteLine($"{this[index].Description(state)}\n");
        }
    }
}