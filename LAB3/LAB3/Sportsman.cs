namespace LAB3
{
    public class Sportsman : Human
    {
        public new static string Id => nameof(Sportsman);

        //this is a field.
        private int _salary;

        //this is a property.
        public int Salary
        {
            get { return _salary; }
            set
            {
                //preventing negatives values.
                if (value < 0)
                {
                    _salary = 0;
                }
                else
                {
                    _salary = value;
                }
            }
        }

        public new string Description()
        {
            return Description(DescriptionInfoState.Default);
        }

        public override string Description(DescriptionInfoState state)
        {
            return $"{base.Description(state)}\nClass Description {Id}: Salary: {Salary.ToString()}";
        }

        protected override string SpecificInfo()
        {
            return "";
        }
    }
}