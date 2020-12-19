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
        public bool AffectedByChange { get; set; }

        public Rule(string rule)
        {
            var matchRule = Regex.Match(rule, @"(?<Id>\d+): (?<Expression>.+)");

            if (!matchRule.Success)
                throw new ArgumentException($"Input {rule} is not a valid rule.");

            Id = int.Parse(matchRule.Groups["Id"].Value);
            Expression = matchRule.Groups["Expression"].Value;

            Children = ListUniqueChildren(Expression);

            AffectedByChange = false;
        }

        List<int> ListUniqueChildren(string expression)
        {
            if (expression.Contains('"')) return new List<int>();

            return expression.Split(' ').ToList()
                .Where(e => e != "|").Select(i => int.Parse(i))
                .Distinct().OrderBy(i => i).ToList();
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

        public bool HasChild(Dictionary<int, Rule> rules, int ruleId)
        {
            if (!rules.ContainsKey(ruleId)) return false;

            var visited = new HashSet<int>();

            var queue = new Queue<int>();
            queue.Enqueue(Id);

            while (queue.Any())
            {
                var id = queue.Dequeue();

                if (visited.Contains(id)) continue;

                visited.Add(id);
                
                if (id == ruleId) return true;

                var children = rules[id].Children;
                if (children.Count > 0)
                {
                    foreach (var child in children)
                    {
                        if (!visited.Contains(child))
                        {
                            queue.Enqueue(child);
                        }
                    }
                }
            }

            return false;
        }
    }
}
