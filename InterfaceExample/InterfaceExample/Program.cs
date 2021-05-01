using System;

namespace InterfaceExample
{
    interface IFirst
    {
        int SomeMethod();
    }

    interface ISecondToWorkWithIFirst
    {
        void DoSomething(IFirst iFirstThing);
    }

    class WorkWithInterfaces : ISecondToWorkWithIFirst
    {
        public void DoSomething(IFirst iFirstThing)
        {
            Console.WriteLine(iFirstThing.SomeMethod());
        }
    }

    class WorkWithIFirstFirst : IFirst
    {
        public int SomeMethod()
        {
            return 1;
        }
    }

    class WorkWithIFirstTen : IFirst
    {
        public int SomeMethod()
        {
            return 10;
        }
    }

    class WorkWithIFirstFifty : IFirst
    {
        public int SomeMethod()
        {
            return 50;
        }
    }

    class WorkWithFirstRandom : IFirst
    {
        public int SomeMethod()
        {
            Random randomInt = new Random();
            return randomInt.Next(0, 100500);
        }
    }

    class Program
    {
        static void Main()
        {
            ISecondToWorkWithIFirst testMe = new WorkWithInterfaces();
            testMe.DoSomething(new WorkWithIFirstFirst());
            testMe.DoSomething(new WorkWithIFirstTen());
            testMe.DoSomething(new WorkWithIFirstFifty());
            testMe.DoSomething(new WorkWithFirstRandom());
        }
    }
}