namespace LAB3
{
    public class FootBallPlayer : Sportsman
    {
        public enum Position
        {
            Goalkeeper,
            Defender,
            Central,
            Forward

            //... add more...
        }

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

        public new static string ID => nameof(FootBallPlayer);

        public int NumberOnTShirt { get; set; }
        public Position BestPlayingPosition { get; set; }
        public double Sprint100Meters { get; set; }

        public new string Description(bool showAge)
        {
            return
                $"{base.Description(showAge)}\nClass Description {ID}: Position: {BestPlayingPosition.ToString("G")}, Sprint 100 Meters: {Sprint100Meters.ToString()}, Number on T-Shirt: {NumberOnTShirt.ToString()}";
        }
        public new string Description()
        {
            return Description(showAge: false);
        }
    }
}