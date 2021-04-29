using System;

namespace LAB3
{
    public class Swimmer : Sportsman, IMyOwnInterfaceForSwimmers
    {
        public bool ChokedWithWater()
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

        public enum SwimmingStyle
        {
            Freestyle,

            Butterfly
            // add more
        }

        public struct Anthropology
        {
            public double footSize;
            public double palmSize;
            public double lungVolume;
            public double shoulderWidth;
        }

        private readonly double[] swimmingStyleTiming100Meters =
            new double[Enum.GetValues(typeof(SwimmingStyle)).Length];

        public Anthropology SwimmerAnthropology;

        public Swimmer(string name, int age, int salary)
        {
            Name = name;
            Age = age;
            Salary = salary;
        }

        public double this[SwimmingStyle style]
        {
            get => swimmingStyleTiming100Meters[(int) style];
            set => swimmingStyleTiming100Meters[(int) style] = value;
        }

        public new static string ID => nameof(Swimmer);

        public new string Description()
        {
            return Description(descriptionInfoState.Default);
        }

        public override string Description(descriptionInfoState state)
        {
            return $"{base.Description(state)}\nClass Description {ID}: " +
                   $"100 Meters ButterFly: {this[SwimmingStyle.Butterfly].ToString()}, " +
                   $"100 Meters FreeStyle: {this[SwimmingStyle.Freestyle].ToString()},\n ";
        }

        protected override string specificInfo()
        {
            return $"\nAntropology metrics:\nFoot Size: {SwimmerAnthropology.footSize.ToString()},\n" +
                   $"Palm Size: {SwimmerAnthropology.palmSize.ToString()},\n" +
                   $"Lung Volume: {SwimmerAnthropology.lungVolume.ToString()},\n" +
                   $"Shoulder Width: {SwimmerAnthropology.shoulderWidth.ToString()}.";
        }
    }
}