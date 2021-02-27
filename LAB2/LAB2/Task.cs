using System;

namespace LAB2
{
    class Task
    {
        public static string Description()
        {
            return "I'm Just Exit Task.";
        }

        public virtual bool PerformTaskLogic()
        {
            return false;
        }

        public string UserInput()
        {
            return Console.ReadLine();
        }
    }
}