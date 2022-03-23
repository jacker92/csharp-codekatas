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
                return $"={res}";
            }
            catch (Exception exc)
            {
                return "Error!";
            }
        }
    }
}
