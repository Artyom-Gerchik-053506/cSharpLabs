using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LAB2
{
    class Task3 : Task
    {
        public static string Description()
        {
            return "I'm Task About Translating MounthNames to Different Languages";
        }

        public override bool PerformTaskLogic()
        {
            List<CultureInfo> culturesList = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
            List<string> culturesArray = culturesList.Select(delegate(CultureInfo culture, int index)
            {
                return index + " " + culture.EnglishName;
            }).ToList();
            Console.WriteLine(string.Join(", \n", culturesArray));
            Console.WriteLine("Write Index of Culture To Show Current Date: ");
            bool isCultureNotSelected = true;
            while (isCultureNotSelected)
            {
                string userInput = UserInput();
                try
                {
                    int selectedIndex = Convert.ToInt32(userInput);
                    if (selectedIndex >= 0 && selectedIndex < culturesList.Count)
                    {
                        Console.WriteLine(Date(cultureInfo: culturesList[selectedIndex]));
                        isCultureNotSelected = false;
                    }
                    else
                    {
                        Console.WriteLine("Select Proper Culture.");
                    }
                }
                catch
                {
                    Console.WriteLine("Error. Only Numbers Are Allowed.");
                }
            }

            return true;
        }

        private string Date(CultureInfo cultureInfo)
        {
            DateTime date1 = DateTime.Now;
            return date1.ToString("dddd dd MMMM", cultureInfo);
        }
    }
}