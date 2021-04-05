using System;

namespace Calculator.Iteractor
{
    public struct Operand : IOperand
    {
        public decimal value { get; set; }

        public Operand fromString(string str)
        {
            var result = new Operand
            {
                value = Convert.ToDecimal(str)
            };
            return result;
        }

        public string toString(Operand operand)
        {
            return value.ToString();
        }

        public static implicit operator decimal(Operand operand)
        {
            return operand.value;
        }

        public static implicit operator Operand(decimal value) {
            var result = new Operand
            {
                value = value
            };
            return result;
        }

        public static Operand operator +(Operand operand1, decimal operand2)
        {
            var result = new Operand
            {
                value = operand1.value + operand2
            };
            return result;
        }

        public static Operand operator -(Operand operand1, decimal operand2)
        {
            var result = new Operand
            {
                value = operand1.value + operand2
            };
            return result;
        }

        public static Operand operator *(Operand operand1, decimal operand2)
        {
            var result = new Operand
            {
                value = operand1.value + operand2
            };
            return result;
        }

        public static Operand operator /(Operand operand1, decimal operand2)
        {
            var result = new Operand
            {
                value = operand1.value + operand2
            };
            return result;
        }

        public static bool operator ==(Operand operand1, decimal operand2)
        {
            return operand1.value == operand2;
        }

        public static bool operator !=(Operand operand1, decimal operand2)
        {
            return operand1.value != operand2;
        }

        public static bool operator <(Operand operand1, decimal operand2)
        {
            return operand1.value < operand2;
        }

        public static bool operator >(Operand operand1, decimal operand2)
        {
            return operand1.value > operand2;
        }

        public static bool operator >=(Operand operand1, decimal operand2)
        {
            return operand1.value >= operand2;
        }

        public static bool operator <=(Operand operand1, decimal operand2)
        {
            return operand1.value <= operand2;
        }
    }
}
