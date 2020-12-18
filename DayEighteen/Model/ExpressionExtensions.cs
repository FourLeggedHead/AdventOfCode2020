using System;
using System.Collections.Generic;
using System.Text;

namespace DayEighteen.Model
{
    public static class ExpressionExtensions
    {
        public static string ToPostFix(this string expression)
        {
            var output = new StringBuilder();

            var tokens = expression.Replace(" ", null).ToCharArray();
            var operators = new Stack<char>();
            foreach (var token in tokens)
            {
                switch (token)
                {
                    case char t when t == '+' || t == '*':
                        operators.Push(t);
                        break;
                    case '(':
                        operators.Push(token);
                        break;
                    case ')':
                        char o;
                        while (operators.Count > 0 && (o = operators.Pop()) != '(')
                        {
                            output.Append(o);
                        }
                        break;
                    default:
                        output.Append(token);
                        break;
                }
            }

            while (operators.Count > 0)
            {
                output.Append(operators.Pop());
            }

            return output.ToString();
        }

        public static long Evaluate(this string expression)
        {
            var postfix = expression.ToPostFix().ToCharArray();
            var output = new Stack<long>();

            foreach (var token in postfix)
            {
                switch (token)
                {
                    case '+':
                        output.Push(output.Pop() + output.Pop());
                        break;
                    case '*':
                        output.Push(output.Pop() * output.Pop());
                        break;
                    default:
                        output.Push(long.Parse(token.ToString()));
                        break;
                }
            }

            return output.Pop();
        }        

        public static long EvaluateModified(this string expression)
        {
            var output = new Stack<long>();
            
            var tokens = expression.Replace(" ", null).ToCharArray();
            var operators = new Stack<char>();

            void EvaluateUntil(char stop)
            {
                while (operators.Count > 0 && operators.Peek() != '(')
                {
                    var op = operators.Pop();
                    if (op == '+')
                        output.Push(output.Pop() + output.Pop());
                    if (op == '*')
                        output.Push(output.Pop() * output.Pop());
                }
            }

            foreach (var token in tokens)
            {
                switch (token)
                {
                    case char t when t == '+' || t == '*':
                        EvaluateUntil('(');
                        operators.Push(t);
                        break;
                    case '(':
                        operators.Push(token);
                        break;
                    case ')':
                        EvaluateUntil('(');
                        operators.Pop();
                        break;
                    default:
                        output.Push(long.Parse(token.ToString()));
                        break;
                }
            }

            EvaluateUntil('(');

            return output.Pop();
        }
    }
}
