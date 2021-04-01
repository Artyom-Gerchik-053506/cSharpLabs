using System;
using System.Drawing;
using System.Runtime.InteropServices;
using myOwnDLL;

namespace LAB4
{
    class Win32
    {
        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
    }

    class cPlusPlusDLL
    {
        [DllImport("../../../Project1.dll", CallingConvention = CallingConvention.StdCall)]
        internal static extern void myMatrix(int rows, int collums);
    }
    
    class Program
    {
        static void Main()
        {
            string userInput;

            IntPtr desktopPtr = Win32.GetDC(IntPtr.Zero);
            Graphics g = Graphics.FromHdc(desktopPtr);
            String drawString = "BSUIR RULEZ THE WORLD";
            float x = 1700.0F;
            float y = 100.0F;
            StringFormat drawFormat = new StringFormat();
            drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
            while (true)
            {
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Color Animated String (10 sec)");
                Console.WriteLine("2 - Some UnsafeSection From  C# .DLL");
                Console.WriteLine("3 - Some UnsafeCode From  C++ .DLL");

                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case ("0"):
                        return;
                    case ("1"):
                        for (int counter = 0; counter <= 200; counter++)
                        {
                            Random rnd = new Random();
                            int red = rnd.Next(0, 256);
                            int green = rnd.Next(0, 256);
                            int blue = rnd.Next(0, 256);
                            Font drawFont = new Font("Arial", 80);
                            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(red, green, blue));
                            g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                        }

                        break;
                    case ("2"):
                        Class1.someFuncWithPointers();
                        break;
                    case ("3"):
                        string matrixCollums;
                        string matrixRows;

                        Console.WriteLine("Enter Number Of Rows: ");
                        matrixRows = Console.ReadLine();
                        int matrixRowsInt = Int32.Parse(matrixRows);

                        Console.WriteLine("Enter Number Of Collums: ");
                        matrixCollums = Console.ReadLine();
                        int matrixCollumsInt = Int32.Parse(matrixRows);

                        cPlusPlusDLL.myMatrix(matrixRowsInt, matrixCollumsInt);
                        break;
                    default:
                        Console.WriteLine("Wrong Input!");
                        break;
                }
            }
            
        }
    }
}