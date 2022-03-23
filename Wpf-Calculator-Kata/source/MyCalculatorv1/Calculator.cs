using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
                return ComputeResult(text);
            }
            catch (Exception exc)
            {
                if (exc is ArithmeticException || exc is EvaluateException || exc is SyntaxErrorException)
                {
                    return "Error!";
                }

                throw;
            }
        }

        private static string ComputeResult(string text)
        {
            var res = new DataTable().Compute(text, null);

            if (Convert.ToString(res, CultureInfo.InvariantCulture) == "NaN")
            {
                throw new ArithmeticException();
            }

            if (TextIsExpression(res))
            {
                return text;
            }

            return $"{text}={res}";
        }

        private static bool TextIsExpression(object res)
        {
            return res.ToString() == "True" || res.ToString() == "False";
        }
    }
}
