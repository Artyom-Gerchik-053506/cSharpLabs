namespace LAB3
{
    public class Human
    {
        public static string ID => nameof(Human);
        public string Name { get; set; }
        public int Age { get; set; }

        public string Description()
        {
            return $"Class Description {ID}: Name: {Name}";
        }

        public virtual string Description(bool showAge)
        {
            if (showAge)
            {
                return $"{Description()}, Age: {Age.ToString()}";
            }

            return Description();
        }
    }
}