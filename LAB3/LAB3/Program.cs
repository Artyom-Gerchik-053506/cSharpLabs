using System;

namespace LAB3
{
    internal class Program
    {
        public static string UserInput()
        {
            return Console.ReadLine();
        }

        private static void Main()
        {
            var canPerfromNextLogic = true;
            while (canPerfromNextLogic)
            {
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Swimming");
                Console.WriteLine("2 - FootBall");
                var userInput = UserInput();
                switch (userInput)
                {
                    case "0":
                        canPerfromNextLogic = false;
                        break;
                    case "1":
                        var sm = new SwimmingManager();
                        sm.SwimmingSwitch();
                        break;
                    case "2":
                        var fm = new FootBallManager();
                        fm.FootBallSwitch();
                        break;
                    default:
                        Console.WriteLine("Wrong Input. Please, Select Proper Task.");
                        break;
                }
            }
        }
    }
}