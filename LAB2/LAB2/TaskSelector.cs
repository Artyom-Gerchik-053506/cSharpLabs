using System;
using System.Text;

namespace LAB2
{
    class TaskSelector : Task
    {
        public Task SelectTask()
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("Please, Select Task: \n");
            stb.Append($"0 - {Task.Description()} \n");
            stb.Append($"1 - {Task1.Description()} \n");
            stb.Append($"2 - {Task2.Description()} \n");
            stb.Append($"3 - {Task3.Description()}");
            Console.WriteLine(stb.ToString());
            while (true)
            {
                string userInput = UserInput();
                switch (userInput)
                {
                    case ("0"):
                        return new Task();
                        break;
                    case ("1"):
                        return new Task1();
                        break;
                    case ("2"):
                        return new Task2();
                        break;
                    case ("3"):
                        return new Task3();
                        break;
                    default:
                        Console.WriteLine("Wrong Input. Please, Select Propper Task.");
                        break;
                }
            }
        }
    }
}