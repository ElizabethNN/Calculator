using System;
using Calculator.Iteractor;

namespace Calculator.BasicFunctions
{
    class Functions
    {
        public static Operand plus(Operand first, Operand second) {
            return first + second;
        }
        public static Operand minus(Operand first, Operand second)
        {
            return first - second;
        }
        public static Operand divide(Operand first, Operand second)
        {
            return first / second;
        }
        public static Operand multiple(Operand first, Operand second)
        {
            return first * second;
        }

        public static Operand quotient(Operand first, Operand second) {
            var result = new Operand();
            result = Math.Floor(first / second);
            return result;
        }

        public static Operand modulo(Operand first, Operand second) {
            return first - (quotient(first, second) * second);
        }
    }
}
