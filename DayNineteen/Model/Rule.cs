using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace DayNineteen.Model
{
    public class Rule
    {
        public int Id { get; set; }
        public string Expression { get; set; }
        public List<int> Children { get; set; }
        public List<int> Parents{ get; set; }

        public Rule(string rule)
        {
            var matchRule = Regex.Match(rule, @"(?<Id>\d+): (?<Expression>.+)");

            if (!matchRule.Success)
                throw new ArgumentException($"Input {rule} is not a valid rule.");

            Id = int.Parse(matchRule.Groups["Id"].Value);
            Expression = matchRule.Groups["Expression"].Value;

            Children = ListUniqueChildren(Expression);
        }

        List<int> ListUniqueChildren(string expression)
        {
            if (expression.Contains('"')) return new List<int>();

            return expression.Split(' ').ToList()
                .Where(e => e != "|").Select(i => int.Parse(i))
                .Distinct().OrderBy(i => i).ToList();
        }

        public void ListParents(Dictionary<int,Rule> rules)
        {
            Parents = rules
                .Where(r => r.Value.Children.Count > 0 && r.Value.Children.Contains(Id))
                .Select(r => r.Key).ToList();
        }

        public List<string> ListValidMessages(Dictionary<int, Rule> rules)
        {
            if (Expression.Contains('"')) return new List<string>() { Expression.Trim('"') };

            var messages = new List<string>();

            var options = Expression.Split('|').Select(o => o.Trim());

            foreach (var option in options)
            {
                var opt = option.Split(" ").Select(i => int.Parse(i)).ToList();

                if (opt.Count == 1)
                {
                    messages.AddRange(rules[opt[0]].ListValidMessages(rules));
                }

                if (opt.Count == 2)
                {
                    foreach (var left in rules[opt[0]].ListValidMessages(rules))
                    {
                        foreach (var right in rules[opt[1]].ListValidMessages(rules))
                        {
                            messages.Add(left + right);
                        }
                    }
                }

                if (opt.Count == 3)
                {
                    foreach (var left in rules[opt[0]].ListValidMessages(rules))
                    {
                        foreach (var middle in rules[opt[1]].ListValidMessages(rules))
                        {
                            foreach (var right in rules[opt[2]].ListValidMessages(rules))
                            {
                                messages.Add(left + middle + right);
                            }
                        }
                    }
                }
            }

            return messages;
        }
    }
}
