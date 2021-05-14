using System;

namespace LAB3
{
    public abstract class Human : ITakeBlood
    {
        public Blood takeBlood()
        {
            Random rnd = new Random();
            Blood temp = new Blood();
            temp.ID = rnd.Next();
            return temp;
        }

        public enum DescriptionInfoState
        {
            Default,
            ShowAge,
            SpecificInfo
        }

        public static string Id => nameof(Human);
        public string Name { get; set; }
        public int Age { get; set; }

        public string Description()
        {
            return $"Class Description {Id}: Name: {Name}";
        }

        protected abstract string SpecificInfo();

        public virtual string Description(DescriptionInfoState state)
        {
            switch (state)
            {
                case DescriptionInfoState.Default:
                    return Description();
                case DescriptionInfoState.ShowAge:
                    return $"{Description()}, Age: {Age.ToString()}";
                case DescriptionInfoState.SpecificInfo:
                    return $"{Description()}, {SpecificInfo()}";
            }

            return null;
        }
    }
}