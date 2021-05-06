using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Computator
{
    interface IComputator
    {
        string calculateExpression(string[] infix_expression);
    }
}
