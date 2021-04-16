namespace LAB3
{
    public abstract class Human
    {
        public enum descriptionInfoState
        {
            Default,
            ShowAge,
            SpecificInfo
        }

        public static string ID => nameof(Human);
        public string Name { get; set; }
        public int Age { get; set; }

        public string Description()
        {
            return $"Class Description {ID}: Name: {Name}";
        }

        protected abstract string specificInfo();

        public virtual string Description(descriptionInfoState state)
        {
            switch (state)
            {
                case descriptionInfoState.Default:
                    return Description();
                    break;
                case descriptionInfoState.ShowAge:
                    return $"{Description()}, Age: {Age.ToString()}";
                    break;
                case descriptionInfoState.SpecificInfo:
                    return $"{Description()}, {specificInfo()}";
                    break;
            }

            return null;
        }
    }
}