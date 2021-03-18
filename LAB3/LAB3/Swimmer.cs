using System;

namespace LAB3
{
    public class Swimmer : Sportsman
    {
        public enum SwimmingStyle
        {
            Freestyle,

            Butterfly
            // add more
        }

        private readonly double[] swimmingStyleTiming100Meters =
            new double[Enum.GetValues(typeof(SwimmingStyle)).Length];

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
            return Description(showAge: false);
        }

        public new string Description(bool showAge)
        {
            return
                $"{base.Description(showAge)}\nClass Description {ID}: 100 Meters ButterFly: {this[SwimmingStyle.Butterfly].ToString()}, 100 Meters FreeStyle: {this[SwimmingStyle.Freestyle].ToString()}";
        }
    }
}