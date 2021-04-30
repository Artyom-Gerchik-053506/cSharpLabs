using System;

namespace LAB3
{
    public class FootBallPlayer : Sportsman, IMyOwnInterfaceForFootBallPlayers
    {
        public bool Stumbled()
        {
            var random = new Random();
            if (random.Next(0, 2) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public enum Position
        {
            Goalkeeper,
            Defender,
            Central,
            Forward

            //... add more...
        }

        public struct Anthropology
        {
            public double FootSize;
            public double JumpHeight;
            public double JumpLength;
            public int HeartBeatAfter100MetersSprint;
        }

        public Anthropology FootBallPlayerAnthropology;

        public FootBallPlayer(string name, int numberOnTShirt, Position bestPlayingPosition, double sprint100Meters,
            int age, int salary)
        {
            Name = name;
            NumberOnTShirt = numberOnTShirt;
            BestPlayingPosition = bestPlayingPosition;
            Sprint100Meters = sprint100Meters;
            Age = age;
            Salary = salary;
        }

        public new static string Id => nameof(FootBallPlayer);
        public int NumberOnTShirt { get; set; }
        public Position BestPlayingPosition { get; set; }
        public double Sprint100Meters { get; set; }

        public new string Description()
        {
            return Description(DescriptionInfoState.Default);
        }

        public override string Description(DescriptionInfoState state)
        {
            return
                $"{base.Description(state)}\nClass Description {Id}: Position: {BestPlayingPosition.ToString("G")}, Sprint 100 Meters: {Sprint100Meters.ToString()}, Number on T-Shirt: {NumberOnTShirt.ToString()}";
        }

        protected override string SpecificInfo()
        {
            return $"\nAntropology metrics:\nFoot Size: {FootBallPlayerAnthropology.FootSize.ToString()},\n" +
                   $"Jump Height: {FootBallPlayerAnthropology.JumpHeight.ToString()},\n" +
                   $"Jump Length: {FootBallPlayerAnthropology.JumpLength.ToString()},\n" +
                   $"Heart Beat After 100 Meters Sprint: {FootBallPlayerAnthropology.HeartBeatAfter100MetersSprint.ToString()}.";
        }
    }
}