using System;

namespace LAB3
{
    public class Swimmer : Sportsman, IMyOwnInterfaceForSwimmers
    {
        public delegate void Finished100MetersDistance();
        public event Finished100MetersDistance Finished;
            
        
        public float SwimmedDistance(SwimmingStyle style,float time)
        {
            float time100Meters = Convert.ToSingle(this[style]);
            var result = (time * 100) / time100Meters; //how many meters swimmed on certain time
            if (result >= 100)
            {
                Finished?.Invoke();
            }
            //Console.WriteLine("Hellow from swimmer "); // logs
            // Console.WriteLine(result); // logs
            return result;
        }

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
            public double FootSize;
            public double PalmSize;
            public double LungVolume;
            public double ShoulderWidth;
        }

        private readonly double[] _swimmingStyleTiming100Meters =
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
            get => _swimmingStyleTiming100Meters[(int) style];
            set => _swimmingStyleTiming100Meters[(int) style] = value;
        }

        public new static string Id => nameof(Swimmer);

        public new string Description()
        {
            return Description(DescriptionInfoState.Default);
        }

        public override string Description(DescriptionInfoState state)
        {
            return $"{base.Description(state)}\nClass Description {Id}: " +
                   $"100 Meters ButterFly: {this[SwimmingStyle.Butterfly].ToString()}, " +
                   $"100 Meters FreeStyle: {this[SwimmingStyle.Freestyle].ToString()},\n ";
        }

        protected override string SpecificInfo()
        {
            return $"\nAntropology metrics:\nFoot Size: {SwimmerAnthropology.FootSize.ToString()},\n" +
                   $"Palm Size: {SwimmerAnthropology.PalmSize.ToString()},\n" +
                   $"Lung Volume: {SwimmerAnthropology.LungVolume.ToString()},\n" +
                   $"Shoulder Width: {SwimmerAnthropology.ShoulderWidth.ToString()}.";
        }
    }
}