namespace LAB2
{
    class Program
    {
        static void Main()
        {
            TaskSelector taskSelector = new TaskSelector();
            bool canPerformNextTask = true;
            while (canPerformNextTask)
            {
                Task task = taskSelector.SelectTask();
                canPerformNextTask = task.PerformTaskLogic();
            }
        }
    }
}