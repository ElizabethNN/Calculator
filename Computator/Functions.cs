using System;
using System.Collections.Generic;

namespace Calculator.Computator
{
    class Functions
    {
        public delegate double unaryDelegate(double x);
        public delegate double binaryDelegate(double x, double y);

        public static readonly Dictionary<string, unaryDelegate> unary_functions = new Dictionary<string, unaryDelegate>{ 
                                                                                                                          {"neg", negative}, 
                                                                                                                          {"sin", sinus},
                                                                                                                          {"cos", cosine},
                                                                                                                          {"tg",  tangent},
                                                                                                                          {"ctg", cotangent},
                                                                                                                          {"!",   factorial},
                                                                                                                          {"sqrt", squareRoot },
                                                                                                                          {"log",  logarithm2},
                                                                                                                          {"ln",  logarithmE},
                                                                                                                          {"lg",  logarithm10},
                                                                                                                          {"abs", absolute},
                                                                                                                          {"asin", arksinus},
                                                                                                                          {"acos", arkcosine},
                                                                                                                          {"atg",  arktangent},
                                                                                                                          {"actg", arkcotangent}
                                                                                                                                             }; //Factorial truncates number (for now)
        public static readonly Dictionary<string, binaryDelegate> binary_functions = new Dictionary<string, binaryDelegate>{
                                                                                                                          { "^", power},
                                                                                                                          { "*", multiplication},
                                                                                                                          { "/", divide},
                                                                                                                          { "+", plus},
                                                                                                                          { "-", minus}
                                                                                                                                                   };

        static double negative(double number)
        {
            return -1 * number;
        }
        
        static double sinus(double number)
        {
            return (double)Math.Sin((double)number);
        }

        static double cosine(double number)
        {
            return (double)Math.Cos((double)number);
        }
        static double tangent(double number)
        {
            return (double)Math.Tan((double)number);
        }
        static double cotangent(double number)
        {
            return (double)(1/Math.Tan((double)number));
        }
        static double power(double x, double y)
        {
            return (double)Math.Pow((double)x, (double)y);
        }
        static double multiplication(double x, double y)
        {
            return x * y;
        }
        static double divide(double x, double y)
        {
            return x / y;
        }
        static double plus(double x, double y)
        {
            return x + y;
        }
        static double minus(double x, double y)
        {
            return x - y;
        }
        static double factorial(double x)
        {
            x = Math.Truncate(x);
            double y = 1;
            for (double i = 1; i <= x; i++)
            {
                y *= i;
            }
            return y;
        }
        static double squareRoot(double x)
        {
            return (double)Math.Sqrt((double)x);
        }
        static double logarithm2(double number)
        {
            return (double)Math.Log((double)number, 2);
        }
        static double logarithmE(double number)
        {
            return (double)Math.Log((double)number);
        }
        static double logarithm10(double number)
        {
            return (double)Math.Log10((double)number);
        }
        static double absolute(double number)
        {
            return (double)Math.Abs((double)number);
        }

        static double arksinus(double number)
        {
            return (double)Math.Asin((double)number);
        }

        static double arkcosine(double number)
        {
            return (double)Math.Acos((double)number);
        }
        static double arktangent(double number)
        {
            return (double)Math.Atan((double)number);
        }
        static double arkcotangent(double number)
        {
            return (double)Math.Atan((double)(1/number));
        }
    }
}