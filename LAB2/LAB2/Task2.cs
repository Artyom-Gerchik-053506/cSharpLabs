using System;
using System.Collections.Generic;
using System.Linq;

namespace LAB2
{
    class Task2 : Task
    {
        public static string Description()
        {
            return "I'm Task About Searching Hexadecimal Numbers In String & Converting It To Decimal.";
        }

        public override bool PerformTaskLogic()
        {
            Console.WriteLine("Please, Insert String.");
            string userInput = UserInput();
            List<string> splittedUserInput = userInput.Split(" ").ToList();
            List<string> convertAll = splittedUserInput.ConvertAll(inputString =>
            {
                Console.WriteLine($"Trying To Convert to Decimal: {inputString}" );
                try
                {
                    long intValueTest = Convert.ToInt64(inputString, 16);
                    Console.WriteLine($"Result Of Convertion: {intValueTest}");
                    return intValueTest.ToString();
                }
                catch
                {
                    Console.WriteLine("Word Is Not Hexadecimal Returning As Is.");
                    return inputString;
                }
            });
            Console.WriteLine(string.Join(" ", convertAll));
            return true;
        }
    }
}