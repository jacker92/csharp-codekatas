using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculatorv1
{
    public class Calculator
    {
        public string GetResult(string text)
        {
            try
            {
                int iOp = GetOperationIndex(text);

                string op = text.Substring(iOp, 1);
                double op1 = Convert.ToDouble(text.Substring(0, iOp));
                double op2 = Convert.ToDouble(text.Substring(iOp + 1, text.Length - iOp - 1));

                if (op == "+")
                {
                    return "=" + (op1 + op2);
                }
                else if (op == "-")
                {
                    return "=" + (op1 - op2);
                }
                else if (op == "*")
                {
                    return "=" + (op1 * op2);
                }
                else
                {
                    return "=" + (op1 / op2);
                }
            }
            catch (Exception exc)
            {
                return "Error!";
            }
        }

        private int GetOperationIndex(string text)
        {
            int iOp = 0;
            if (text.Contains("+"))
            {
                iOp = text.IndexOf("+");
            }
            else if (text.Contains("-"))
            {
                iOp = text.IndexOf("-");
            }
            else if (text.Contains("*"))
            {
                iOp = text.IndexOf("*");
            }
            else if (text.Contains("/"))
            {
                iOp = text.IndexOf("/");
            }
            else
            {
                //error
            }

            return iOp;
        }
    }
}
