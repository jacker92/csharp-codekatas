using System;
using System.Collections.Generic;
using System.Data;
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
                var res = new DataTable().Compute(text, null);

                if (TextIsExpression(res))
                {
                    return text;
                }

                return $"{text}={res}";
            }
            catch (SyntaxErrorException exc)
            {
                return "Error!";
            }
        }

        private static bool TextIsExpression(object res)
        {
            return res.ToString() == "True" || res.ToString() == "False";
        }
    }
}
