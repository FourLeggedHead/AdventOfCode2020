using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayEighteen.Model
{
    public static class Expression
    {
        public static Term ReadNextTerm(this List<Term> expression)
        {
            return expression[0];
        }

        public static void SetTermsOrder(this List<Term> expression)
        {
            var order = 0;
            for (int i = 0; i < expression.Count; i++)
            {
                if (expression[i].Value == "(") order++;
                if (expression[1].Value == ")") order--;
                expression[i].Order = order;
            }
        }

        public static string RemoveClosingParanthersis(this string term)
        {
            var list = term.ToList();
            list.RemoveAll(c => c == ')');
            return new string(list.ToArray());
        }
    }
}
