using System.Collections.Generic;

namespace LAB3
{
    public class SwimmersSlariesComparer : IComparer<Swimmer>
    {
        public int Compare(Swimmer first, Swimmer second)
        {
            if (first.Salary == second.Salary)
            {
                return 0;
            }
            else if (first.Salary < second.Salary)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}