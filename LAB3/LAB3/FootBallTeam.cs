using System;

namespace LAB3
{
    public class FootBallTeam
    {
        public const int numberOfPlayers = 30;

        public FootBallPlayer[] Players;
        //add if need
        /*public Sportsman[] Coaches;
        public Human[] Doctors;
        public Human[] Managers;*/

        public FootBallTeam(string name)
        {
            Name = name;
            Players = new FootBallPlayer[numberOfPlayers];
            //add if need
            /*Coaches = new Sportsman[3];
            Doctors = new Human[2];
            Managers = new Human[5];*/
        }

        public string Name { get; set; }

        public FootBallPlayer this[int index]
        {
            get => Players[index];
            set => Players[index] = value;
        }

        public void Description(bool showAge)
        {
            Console.WriteLine($"Name Of Team: {Name}\n");
            //info about footballists
            for (var index = 0; index < numberOfPlayers; index++)
                Console.WriteLine($"{this[index].Description(showAge)}\n");
        }
    }
}