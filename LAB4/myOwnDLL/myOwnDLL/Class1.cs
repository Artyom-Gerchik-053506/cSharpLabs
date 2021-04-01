using System;

namespace myOwnDLL
{
    public static class Class1
    {
        public static void someFuncWithPointers()
        {
            unsafe
            {
                int* x;
                int y = 10;
                Console.WriteLine($"Initial Value Of Y = {y}.");
                x = &y;
                Console.WriteLine("Taking Address Of Y.");
                int** z = &x;
                Console.WriteLine("Taking Pointer To Pointer And Set Address Of Y.");
                **z = **z + 40;
                Console.WriteLine("Increase Value Of Pointer To Pointer By 40. Which Updates Y.");
                Console.WriteLine($"Updated Value of Y: {y}.");
                Console.WriteLine($"Value Of Pointer To Pointer: {**z}.");
            }
        }
    }
}