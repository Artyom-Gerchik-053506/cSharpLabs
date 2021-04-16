namespace LAB3
{
    public class Sportsman : Human
    {
        public new static string ID => nameof(Sportsman);

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
            return Description(descriptionInfoState.Default);
        }

        public override string Description(descriptionInfoState state)
        {
            return $"{base.Description(state)}\nClass Description {ID}: Salary: {Salary.ToString()}";
        }

        protected override string specificInfo()
        {
            return "";
        }
    }
}