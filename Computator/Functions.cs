using System;
using System.Collections.Generic;

namespace Calculator.Computator
{
    class Functions
    {
        public delegate decimal unaryDelegate(decimal x);
        public delegate decimal binaryDelegate(decimal x, decimal y);

        public static readonly Dictionary<string, unaryDelegate> unary_functions = new Dictionary<string, unaryDelegate>{ 
                                                                                                                          {"neg", negative}, 
                                                                                                                          {"sin", sinus},
                                                                                                                          {"cos", cosine},
                                                                                                                          {"tg",  tangent},
                                                                                                                          {"ctg", cotangent},
                                                                                                                          {"!",   factorial}
                                                                                                                                             }; //Factorial truncates number (for now)
        public static readonly Dictionary<string, binaryDelegate> binary_functions = new Dictionary<string, binaryDelegate>{
                                                                                                                          { "^", power},
                                                                                                                          { "*", multiplication},
                                                                                                                          { "/", divide},
                                                                                                                          { "+", plus},
                                                                                                                          { "-", minus}
                                                                                                                                                   };

        static decimal negative(decimal number)
        {
            return -1 * number;
        }
        
        static decimal sinus(decimal number)
        {
            return (decimal)Math.Sin((double)number);
        }

        static decimal cosine(decimal number)
        {
            return (decimal)Math.Sin((double)number);
        }

        static decimal tangent(decimal number)
        {
            return (decimal)Math.Tan((double)number);
        }

        static decimal cotangent(decimal number)
        {
            return (decimal)(1/Math.Tan((double)number));
        }

        static decimal power(decimal x, decimal y)
        {
            return (decimal)Math.Pow((double)x, (double)y);
        }

        static decimal multiplication(decimal x, decimal y)
        {
            return x * y;
        }

        static decimal divide(decimal x, decimal y)
        {
            return x / y;
        }

        static decimal plus(decimal x, decimal y)
        {
            return x + y;
        }

        static decimal minus(decimal x, decimal y)
        {
            return x - y;
        }

        static decimal factorial(decimal x)
        {
            x = Math.Truncate(x);
            decimal y = 1;
            for (decimal i = 1; i <= x; i++)
            {
                y *= i;
            }
            return y;
        }
    }
}