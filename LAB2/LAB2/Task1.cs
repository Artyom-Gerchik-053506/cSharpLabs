using System;

namespace LAB2
{
    class Task1 : Task
    {
        public static string Description()
        {
            return "I'm Task About Reversing String.";
        }

        public override bool PerformTaskLogic()
        {
            Console.WriteLine("Please, Insert String.");
            string userInput = UserInput();
            string[] splittedUserInput = userInput.Split(" ");
            Array.Reverse(splittedUserInput);
            Console.WriteLine(string.Join(" ", splittedUserInput));
            return true;
        }
    }
}