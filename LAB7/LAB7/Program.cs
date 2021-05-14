using System;
using System.Collections.Generic;

namespace LAB7
{
    class Program
    {
        static long getNumerator()
        {
            long tempNumerator = 0;
            bool test = true;
            while (test)
            {
                Console.Write("Enter Numerator: ");
                try
                {
                    tempNumerator = Convert.ToInt64(Console.ReadLine());
                    test = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Only Integer Allowed");
                }
            }

            return tempNumerator;
        }

        static long getDenominator()
        {
            long tempDenominator = 0;
            while (tempDenominator <= 0)
            {
                Console.Write("Enter Denominator: ");
                try
                {
                    tempDenominator = Convert.ToInt64(Console.ReadLine());
                    if (tempDenominator <= 0)
                    {
                        Console.WriteLine("Enter A Natural Number.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Only Integer Allowed");
                }
            }

            return tempDenominator;
        }

        static void printResult(string action, RationalNumber myNum)
        {
            myNum.Simplify();
            Console.WriteLine();
            Console.WriteLine(action);
            Console.WriteLine(myNum.ToString());
            Console.WriteLine(myNum.ToString("whole part"));
            Console.WriteLine(myNum.ToString("decimal"));
            Console.WriteLine(myNum.ToString("decimal1"));
            Console.WriteLine(myNum.ToString("decimal2"));
            Console.WriteLine(myNum.ToString("decimal3"));
            Console.WriteLine(myNum.ToString("decimal4"));
            Console.WriteLine(myNum.ToString("decimal5"));
        }

        static void printResult(string action, bool myNum)
        {
            Console.WriteLine();
            Console.WriteLine(action);
            Console.WriteLine(myNum ? "true" : "false");
        }

        enum MathAction
        {
            Initial,
            Plus,
            Minus,
            Divide,
            Multiply,
            Greater,
            Less,
            GreaterOrEqual,
            LessOrEqual,
            Equal,
            NotEqual
        }

        static private MathAction statement = MathAction.Initial;

        static bool getAction()
        {
            Console.WriteLine();
            Console.Write("Your Choice: ");
            string action = Console.ReadLine();
            if (action.Contains("+"))
            {
                statement = MathAction.Plus;
            }
            else if (action.Contains("-"))
            {
                statement = MathAction.Minus;
            }
            else if (action.Contains("/"))
            {
                statement = MathAction.Divide;
            }
            else if (action.Contains("*"))
            {
                statement = MathAction.Multiply;
            }
            else if (action.Contains(">="))
            {
                statement = MathAction.GreaterOrEqual;
            }
            else if (action.Contains("<="))
            {
                statement = MathAction.LessOrEqual;
            }
            else if (action.Contains(">"))
            {
                statement = MathAction.Greater;
            }
            else if (action.Contains("<"))
            {
                statement = MathAction.Less;
            }
            else if (action.Contains("=="))
            {
                statement = MathAction.Equal;
            }
            else if (action.Contains("!="))
            {
                statement = MathAction.NotEqual;
            }

            return statement == MathAction.Initial ? false : true;
        }

        enum switchStatement
        {
            Initial,
            NumsAreReady
        }

        static void CompareEqualsVSDoubleEqual(RationalNumber first)
        {
            bool tempForEquals = true;
            switchStatement localState = switchStatement.Initial;
            RationalNumber secondNum = new RationalNumber(1, 1);
            while (tempForEquals)
            {
                switch (localState)
                {
                    case switchStatement.Initial:
                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - Create New Second Number");
                        Console.WriteLine("2 - Assign First Number To Second Number");
                        Console.WriteLine();
                        Console.Write("Your Choice: ");
                        string tempStr = Console.ReadLine();
                        switch (tempStr)
                        {
                            case ("0"):
                                tempForEquals = false;
                                break;
                            case ("1"):
                                secondNum = new RationalNumber(first.Numerator, first.Denominator);
                                Console.WriteLine("Created.");
                                Console.WriteLine();
                                localState = switchStatement.NumsAreReady;
                                break;
                            case ("2"):
                                secondNum = first;
                                Console.WriteLine("Assigned");
                                localState = switchStatement.NumsAreReady;
                                break;
                            default:
                                Console.WriteLine("RedNeck.");
                                break;
                        }

                        break;
                    case switchStatement.NumsAreReady:
                        Console.WriteLine("0 - Back");
                        Console.WriteLine("1 - Create New Second Number");
                        Console.WriteLine("2 - Assign First Number To Second Number");
                        Console.WriteLine("3 - Print Result Equals VS ==");
                        Console.WriteLine();
                        Console.Write("Your Choice: ");
                        tempStr = Console.ReadLine();
                        switch (tempStr)
                        {
                            case ("0"):
                                tempForEquals = false;
                                break;
                            case ("1"):
                                secondNum = new RationalNumber(first.Numerator, first.Denominator);
                                Console.WriteLine("Created.");
                                Console.WriteLine();
                                localState = switchStatement.NumsAreReady;
                                break;
                            case ("2"):
                                secondNum = first;
                                Console.WriteLine("Assigned");
                                Console.WriteLine();
                                localState = switchStatement.NumsAreReady;
                                break;
                            case ("3"):
                                printResult("First Number: ", first);
                                printResult("Second Number: ", secondNum);
                                Console.WriteLine();
                                unsafe
                                {
                                    //  https://stackoverflow.com/a/10861731
                                    TypedReference tr1 = __makeref(first);
                                    IntPtr ptr1 = **(IntPtr**) (&tr1);
                                    Console.WriteLine($"Address Of First Number: {ptr1.ToString()}");
                                    TypedReference tr2 = __makeref(secondNum);
                                    IntPtr ptr2 = **(IntPtr**) (&tr2);
                                    Console.WriteLine($"Address Of Second Number: {ptr2.ToString()}");
                                }

                                Console.WriteLine($"first.Equals(secondNum): {(first.Equals(secondNum)).ToString()}");
                                Console.WriteLine($"first == secondNum: {(first == secondNum).ToString()}");
                                Console.WriteLine();
                                break;
                            default:
                                Console.WriteLine("RedNeck.");
                                break;
                        }

                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            switchStatement state = switchStatement.Initial;
            RationalNumber MyRationalNumber1 = new RationalNumber(1, 1);
            RationalNumber MyRationalNumber2 = new RationalNumber(1, 1);
            while (true)
            {
                switch (state)
                {
                    case (switchStatement.Initial):
                        Console.WriteLine();
                        Console.WriteLine("0 - Exit");
                        Console.WriteLine("1 - Enter Numbers, Entering Numerator & Denominator");
                        Console.WriteLine("2 - Enter Numbers, Entering Like A String (1 / 5) || (-1 / 5) || 0.2");
                        Console.Write("Your Choice: ");
                        string input = Console.ReadLine();
                        Console.WriteLine();
                        bool tempBoolForParsingStr = true;
                        switch (input)
                        {
                            case ("0"):
                            {
                                Console.WriteLine("BB.");
                                return;
                            }
                            case ("1"):
                            {
                                Console.WriteLine("Enter First Rational Number: ");
                                MyRationalNumber1 = new RationalNumber(getNumerator(), getDenominator());
                                Console.WriteLine();
                                Console.WriteLine("Second Rational Number: ");
                                MyRationalNumber2 = new RationalNumber(getNumerator(), getDenominator());
                                Console.WriteLine();
                                MyRationalNumber1.Simplify();
                                MyRationalNumber2.Simplify();
                                state = switchStatement.NumsAreReady;
                                break;
                            }
                            case ("2"):
                                while (tempBoolForParsingStr)
                                {
                                    Console.Write("Enter First Rational Number: ");
                                    string first = Console.ReadLine();
                                    try
                                    {
                                        MyRationalNumber1 = new RationalNumber(first);
                                        tempBoolForParsingStr = false;
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("RedNeck.");
                                    }

                                    Console.WriteLine();
                                }

                                tempBoolForParsingStr = true;
                                while (tempBoolForParsingStr)
                                {
                                    Console.Write("Second Rational Number: ");
                                    string second = Console.ReadLine();
                                    try
                                    {
                                        MyRationalNumber2 = new RationalNumber(second);
                                        tempBoolForParsingStr = false;
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("RedNeck.");
                                    }

                                    Console.WriteLine();
                                }

                                MyRationalNumber1.Simplify();
                                MyRationalNumber2.Simplify();
                                state = switchStatement.NumsAreReady;
                                break;
                            default:
                                Console.WriteLine("RedNeck.");
                                break;
                        }

                        break;
                    case (switchStatement.NumsAreReady):
                        Console.WriteLine();
                        Console.WriteLine("0 - Exit");
                        Console.WriteLine("1 - Info About Numbers");
                        Console.WriteLine("2 - Unary Operations For First Number");
                        Console.WriteLine("3 - Unary Operations For Second Number");
                        Console.WriteLine("4 - Binary Operations");
                        Console.WriteLine("5 - Casting Operations");
                        Console.WriteLine("6 - Array Sorter");
                        Console.WriteLine("7 - List Sorter");
                        Console.WriteLine("8 - Equals VS ==");
                        Console.WriteLine();
                        Console.Write("Your Choice: ");
                        string userInput = Console.ReadLine();
                        switch (userInput)
                        {
                            case ("0"):
                                Console.WriteLine("BB.");
                                return;
                            case ("1"):
                                printResult("First Number", MyRationalNumber1);
                                printResult("Second Number", MyRationalNumber2);
                                break;
                            case ("2"):
                                printResult("Unary + For First Number", +MyRationalNumber1);
                                printResult("Unary - For First Number", -MyRationalNumber1);
                                break;
                            case ("3"):
                                printResult("Unary + For Second Number", +MyRationalNumber2);
                                printResult("Unary - For Second Number", -MyRationalNumber2);
                                break;
                            case ("4"):
                                bool tempState = true;
                                while (tempState)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("Enter Action(+, -, /, *, <, >, <=, >=, ==, !=)");
                                    if (getAction())
                                    {
                                        tempState = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("RedNeck.");
                                        Console.WriteLine();
                                    }
                                }

                                switch (statement)
                                {
                                    case (MathAction.Plus):
                                        printResult("First + Second", MyRationalNumber1 + MyRationalNumber2);
                                        break;
                                    case (MathAction.Minus):
                                        printResult("First - Second", MyRationalNumber1 - MyRationalNumber2);
                                        break;
                                    case (MathAction.Divide):
                                        printResult("First / Second", MyRationalNumber1 / MyRationalNumber2);
                                        break;
                                    case (MathAction.Multiply):
                                        printResult("First * Second", MyRationalNumber1 * MyRationalNumber2);
                                        break;
                                    case (MathAction.GreaterOrEqual):
                                        printResult("First >= Second", MyRationalNumber1 >= MyRationalNumber2);
                                        break;
                                    case (MathAction.LessOrEqual):
                                        printResult("First <= Second", MyRationalNumber1 <= MyRationalNumber2);
                                        break;
                                    case (MathAction.Greater):
                                        printResult("First > Second", MyRationalNumber1 > MyRationalNumber2);
                                        break;
                                    case (MathAction.Less):
                                        printResult("First < Second", MyRationalNumber1 < MyRationalNumber2);
                                        break;
                                    case (MathAction.Equal):
                                        printResult("First == Second", MyRationalNumber1 == MyRationalNumber2);
                                        break;
                                    case (MathAction.NotEqual):
                                        printResult("First != Second", MyRationalNumber1 != MyRationalNumber2);
                                        break;
                                }

                                break;
                            case ("5"):
                                bool tempForCastWhile = true;
                                while (tempForCastWhile)
                                {
                                    Console.WriteLine("0 - Back");
                                    Console.WriteLine("1 - Cast Rational Number From Float");
                                    Console.WriteLine("2 - Cast Float From Rational Number");
                                    Console.WriteLine();
                                    Console.Write("Your Choice: ");
                                    string tempForCast = Console.ReadLine();
                                    switch (tempForCast)
                                    {
                                        case ("0"):
                                            tempForCastWhile = false;
                                            break;
                                        case ("1"):
                                            bool tempBoolCast = true;
                                            decimal castValue = 0;
                                            string test;
                                            Console.WriteLine();
                                            while (tempBoolCast)
                                            {
                                                try
                                                {
                                                    Console.Write("Enter Float Value (3.4, -5.078, 8.123456): ");
                                                    //castValue = Convert.ToSingle(Console.ReadLine());
                                                    test = Console.ReadLine();
                                                    if (test.Contains(","))
                                                    {
                                                        throw new FormatException("RedNeck, ',' Is Not Allowed.");
                                                    }

                                                    castValue = Convert.ToDecimal(test);
                                                    tempBoolCast = false;
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("RedNeck.");
                                                }
                                            }

                                            RationalNumber cast = new RationalNumber(castValue);
                                            cast.Simplify();
                                            printResult($"From {castValue} Float To Rational: ", cast);
                                            break;
                                        case ("2"):
                                            Console.WriteLine();
                                            Console.WriteLine("Enter Rational Number: ");
                                            RationalNumber tempRnForCast =
                                                new RationalNumber(getNumerator(), getDenominator());
                                            Console.WriteLine();
                                            float tempRnForCastInFloat = tempRnForCast;
                                            Console.WriteLine(
                                                $"Your Rational Number In Float: {tempRnForCastInFloat.ToString()}");
                                            break;
                                        default:
                                            Console.WriteLine("RedNeck.");
                                            Console.WriteLine();
                                            break;
                                    }
                                }

                                break;
                            case ("6"):
                                RationalNumber[] rationalNumbersArray = Array.Empty<RationalNumber>();
                                var rationalNumbersListForArray = new List<RationalNumber>();
                                bool stateForArraySort = true;
                                Console.WriteLine();
                                while (stateForArraySort)
                                {
                                    Console.WriteLine("0 - Back");
                                    Console.WriteLine("1 - Add Element");
                                    Console.WriteLine("2 - Info About Array");
                                    Console.WriteLine("3 - Sort Array");
                                    Console.Write("Your Choice: ");
                                    string tempStringForArray = Console.ReadLine();
                                    switch (tempStringForArray)
                                    {
                                        case ("0"):
                                            stateForArraySort = false;
                                            break;
                                        case ("1"):
                                            Console.WriteLine();
                                            Console.Write("Enter A Rational Number(1 / 4 || 2 / 5 || -3/98, etc.): ");
                                            string tempRN = Console.ReadLine();
                                            // RationalNumber numberToAdd = ;
                                            rationalNumbersListForArray.Add(new RationalNumber(tempRN));
                                            rationalNumbersArray = rationalNumbersListForArray.ToArray();
                                            break;
                                        case ("2"):
                                            for (int index = 0; index < rationalNumbersArray.Length; index++)
                                            {
                                                printResult($"Index: {index}, Number: ", rationalNumbersArray[index]);
                                            }

                                            break;
                                        case ("3"):
                                            Array.Sort(rationalNumbersArray, new RationalNumber(1, 1));
                                            //doesn't work. maybe need to implement compare in another class...
                                            break;
                                        default:
                                            Console.WriteLine("RedNeck.");
                                            Console.WriteLine();
                                            break;
                                    }
                                }

                                break;
                            case ("7"):
                                var rationalNumbersList = new List<RationalNumber>();
                                bool stateForListSort = true;
                                Console.WriteLine();
                                while (stateForListSort)
                                {
                                    Console.WriteLine("0 - Back");
                                    Console.WriteLine("1 - Add Element");
                                    Console.WriteLine("2 - Info About List");
                                    Console.WriteLine("3 - Sort List");
                                    Console.Write("Your Choice: ");
                                    string tempStringForList = Console.ReadLine();
                                    switch (tempStringForList)
                                    {
                                        case ("0"):
                                            stateForListSort = false;
                                            break;
                                        case ("1"):
                                            Console.WriteLine();
                                            Console.Write("Enter A Rational Number(1 / 4 || 2 / 5 || -3/98, etc.): ");
                                            string tempRN = Console.ReadLine();
                                            rationalNumbersList.Add(new RationalNumber(tempRN));
                                            break;
                                        case ("2"):
                                            for (int index = 0; index < rationalNumbersList.Count; index++)
                                            {
                                                printResult($"Index: {index}, Number: ", rationalNumbersList[index]);
                                            }

                                            break;
                                        case ("3"):
                                            rationalNumbersList.Sort();
                                            break;
                                        default:
                                            Console.WriteLine("RedNeck.");
                                            Console.WriteLine();
                                            break;
                                    }
                                }

                                break;
                            case ("8"):
                                CompareEqualsVSDoubleEqual(MyRationalNumber1);
                                break;
                            default:
                                Console.WriteLine("RedNeck.");
                                break;
                        }

                        break;
                }
            }
        }
    }
}