using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LAB7
{
    class RationalNumber : IEquatable<RationalNumber>, IComparable<RationalNumber>, IComparer<RationalNumber>
    {
        private long _numerator;
        private long _denominator;

        public RationalNumber(long numerator, long denominator)
        {
            _numerator = numerator;
            _denominator = denominator;
        }

        private void InitializeFromDecimal(decimal inputDecimalForInit)
        {
            bool needMinus = false;
            // 3, 025
            // 3
            int wholePart = (int) inputDecimalForInit;
            if (inputDecimalForInit < 0 && wholePart == 0)
            {
                needMinus = true;
            }

            //0.025
            decimal fractionPart = Math.Abs(inputDecimalForInit) - Math.Abs(wholePart);
            // 0.025
            decimal fractionPartForIteration = fractionPart;
            _denominator = 1;
            while (fractionPartForIteration > 0)
            {
                // 0,25  2.5
                fractionPart *= 10;
                // 0,25 2.5 (-2)
                fractionPartForIteration *= 10;
                // 0 2 
                int wholePartForIteration = (int) fractionPartForIteration;
                fractionPartForIteration -= wholePartForIteration;
                // 10 100
                _denominator *= 10;
            }

            // -1 * (10)
            if (wholePart == 0)
            {
                _numerator = (long) fractionPart;
            }
            else
            {
                _numerator = wholePart / Math.Abs(wholePart) *
                             (Math.Abs(wholePart) * _denominator + (long) fractionPart);
            }

            if (needMinus)
            {
                _numerator *= -1;
                needMinus = false;
            }
        }

        public RationalNumber(decimal inputDecimal)
        {
            InitializeFromDecimal(inputDecimal);
        }

        public RationalNumber(string input)
        {
            bool initSuccessful = false;
            // . any symbol
            // [^\d] except digit, "\d" - d is decimal not "d"
            // *(\d+) get decimal [1 ... +infinity)
            //.[^\d]* not working == .* working

            // With sign
            Regex myRegWithSign = new Regex(@"^.*(-+).*(\d+).*/.*(\d+).*$");
            foreach (Match match in myRegWithSign.Matches(input))
            {
                if (match.Groups.Count == 4)
                {
                    this._numerator = -1 * Convert.ToInt64(match.Groups[2].Value);
                    this._denominator = Convert.ToInt64(match.Groups[3].Value);
                    initSuccessful = true;
                }
            }

            if (!initSuccessful)
            {
                // No sign
                Regex myRegNoSign = new Regex(@"^.*(\d+).*/.*(\d+).*$");
                foreach (Match match in myRegNoSign.Matches(input))
                {
                    if (match.Groups.Count == 3)
                    {
                        // no sign
                        this._numerator = Convert.ToInt64(match.Groups[1].Value);
                        this._denominator = Convert.ToInt64(match.Groups[2].Value);
                        initSuccessful = true;
                    }
                }
            }

            if (!initSuccessful)
            {
                //With sing -8.5678
                Regex myRegDecimalWithSign = new Regex(@"^.*?(-+).*?(\d+).*?\..*?(\d+).*?$");
                foreach (Match match in myRegDecimalWithSign.Matches(input))
                {
                    if (match.Groups.Count == 4)
                    {
                        string tempStr = "-" + match.Groups[2].Value.ToString() + "." +
                                         match.Groups[3].Value.ToString();
                        InitializeFromDecimal(Convert.ToDecimal(tempStr));
                        initSuccessful = true;
                    }
                }
            }

            if (!initSuccessful)
            {
                //No sing 0.5678
                Regex myRegDecimal = new Regex(@"^.*?(\d+).*?\..*?(\d+).*?$");
                foreach (Match match in myRegDecimal.Matches(input))
                {
                    if (match.Groups.Count == 3)
                    {
                        string tempStr = match.Groups[1].Value.ToString() + "." + match.Groups[2].Value.ToString();
                        InitializeFromDecimal(Convert.ToDecimal(tempStr));
                        initSuccessful = true;
                    }
                }
            }

            if (!initSuccessful)
            {
                throw new Exception("Couldn't recognize string");
            }
        }

        public long Numerator
        {
            get { return _numerator; }
            private set { _numerator = value; }
        }

        public long Denominator
        {
            get { return _denominator; }
            private set { _denominator = value; }
        }

        public void Simplify()
        {
            if (_denominator == 0)
            {
                Console.WriteLine("Denominator < 0");
                return;
            }

            if (_denominator < 0)
            {
                _denominator *= -1;
                _numerator *= -1;
            }

            if (_numerator != 0 && _denominator != 0)
            {
                long greatestCommonDivisor = GetGreatestCommonDivisor(_numerator, _denominator);
                _numerator /= greatestCommonDivisor;
                _denominator /= greatestCommonDivisor;
            }
        }

        private static long GetGreatestCommonDivisor(long first, long second)
        {
            first = Math.Abs(first);
            second = Math.Abs(second);
            while (first != 0 && second != 0)
            {
                if (first > second)
                {
                    first %= second;
                }
                else
                {
                    second %= first;
                }
            }

            return first + second;
        }

        public static RationalNumber operator +(RationalNumber inputObject) // unary operator
        {
            RationalNumber temp = new RationalNumber(inputObject._numerator, inputObject._denominator);
            return temp;
        }

        public static RationalNumber operator -(RationalNumber inputObject) // unary operator
        {
            RationalNumber temp = new RationalNumber(-inputObject._numerator, inputObject._denominator);
            return temp;
        }

        public static RationalNumber operator +(RationalNumber lhs, RationalNumber rhs) //binary operator  
        {
            RationalNumber temp =
                new RationalNumber((lhs._numerator * rhs._denominator) + (rhs._numerator * lhs._denominator),
                    lhs._denominator * rhs._denominator);
            return temp;
        }

        public static RationalNumber operator -(RationalNumber lhs, RationalNumber rhs) //binary operator  
        {
            RationalNumber temp =
                new RationalNumber((lhs._numerator * rhs._denominator) - (rhs._numerator * lhs._denominator),
                    lhs._denominator * rhs._denominator);
            return temp;
        }

        public static RationalNumber operator *(RationalNumber lhs, RationalNumber rhs) //binary operator  
        {
            RationalNumber temp =
                new RationalNumber(lhs._numerator * rhs._numerator, lhs._denominator * rhs._denominator);
            return temp;
        }

        public static RationalNumber operator /(RationalNumber lhs, RationalNumber rhs) //binary operator upsidedown rhs
        {
            RationalNumber temp =
                new RationalNumber(lhs._numerator * rhs._denominator, lhs._denominator * rhs._numerator);
            return temp;
        }

        public static bool operator >(RationalNumber lhs, RationalNumber rhs) //binary operator 
        {
            return (lhs._numerator * rhs._denominator) > (rhs._numerator * lhs._denominator);
        }

        public static bool operator <(RationalNumber lhs, RationalNumber rhs) //binary operator 
        {
            return (lhs._numerator * rhs._denominator) < (rhs._numerator * lhs._denominator);
        }

        public int CompareTo(RationalNumber other)
        {
            return Compare(this, other);
        }

        public int Compare(RationalNumber lhs, RationalNumber rhs)
        {
            if (lhs.Equals(rhs))
            {
                return 0;
            }
            else if (lhs < rhs)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        // Equals compare internal things
        // == compare references

        public bool Equals(RationalNumber rnObj)
        {
            if (ReferenceEquals(rnObj, null))
            {
                return false;
            }

            return (this._numerator * rnObj._denominator) == (rnObj._numerator * this._denominator);
        }

        public override bool Equals(Object obj)
        {
            RationalNumber rnObj = obj as RationalNumber;
            return Equals(rnObj);
        }

        public override int GetHashCode()
        {
            return _denominator.GetHashCode() + _numerator.GetHashCode();
        }

        public static bool operator ==(RationalNumber lhs, RationalNumber rhs) //binary operator 
        {
            return ReferenceEquals(lhs, rhs);
        }

        public static bool operator !=(RationalNumber lhs, RationalNumber rhs) //binary operator 
        {
            return !(lhs == rhs);
        }

        public static bool operator >=(RationalNumber lhs, RationalNumber rhs) //binary operator 
        {
            return (lhs._numerator * rhs._denominator) >= (rhs._numerator * lhs._denominator);
        }

        public static bool operator <=(RationalNumber lhs, RationalNumber rhs) //binary operator 
        {
            return (lhs._numerator * rhs._denominator) <= (rhs._numerator * lhs._denominator);
        }

        public string ToString(string format = null)
        {
            string outputString = null;
            if (format != null && format.ToLower().Contains("decimal"))
            {
                float tempThisAsFloat = this.ToFloat();
                if (format.ToLower().Contains("1"))
                {
                    outputString = $"Decimal(Fraction: 1): {Math.Round(tempThisAsFloat, 1).ToString()}";
                }
                else if (format.ToLower().Contains("2"))
                {
                    outputString = $"Decimal(Fraction: 2): {Math.Round(tempThisAsFloat, 2).ToString()}";
                }
                else if (format.ToLower().Contains("3"))
                {
                    outputString = $"Decimal(Fraction: 3): {Math.Round(tempThisAsFloat, 3).ToString()}";
                }
                else if (format.ToLower().Contains("4"))
                {
                    outputString = $"Decimal(Fraction: 4): {Math.Round(tempThisAsFloat, 4).ToString()}";
                }
                else if (format.ToLower().Contains("5"))
                {
                    outputString = $"Decimal(Fraction: 5): {Math.Round(tempThisAsFloat, 5).ToString()}";
                }
                else
                {
                    outputString = $"Decimal: {tempThisAsFloat.ToString()}";
                }
            }
            else if (format != null && format.ToLower().Contains("whole"))
            {
                outputString = $"Whole Part: {(this._numerator / this._denominator).ToString()}";
            }
            else
            {
                outputString = $"Normal: {this._numerator.ToString()}/{this._denominator.ToString()}";
            }

            return outputString;
        }

        private float ToFloat()
        {
            float num = this._numerator;
            float den = this._denominator;
            return num / den;
        }

        // Note: if comment explicit int it will continue work, if comment implicit float it breaks down,
        // conclusion: looks like it casts from myObj to float then from float to int. 
        // No need for int conversion, as conversion to float in enough 
        public static implicit operator float(RationalNumber x)
        {
            return x.ToFloat();
        }

        public static explicit operator RationalNumber(float test)
        {
            return new RationalNumber((decimal) test);
        }
    }
}