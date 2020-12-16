using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DaySixteen.Model
{
    public class Rule
    {
        public string Name { get; set; }
        public int FirstStart { get; set; }
        public int FirstEnd { get; set; }
        public int SecondStart { get; set; }
        public int SecondEnd { get; set; }

        public Rule(string rule)
        {
            var match = Regex.Match(rule, @"(?<Name>\D+\s*\D+): (?<FS>\d+)-(?<FE>\d+) or (?<SS>\d+)-(?<SE>\d+)");

            if (match.Success)
            {
                Name = match.Groups["Name"].Value;
                FirstStart = int.Parse(match.Groups["FS"].Value);
                FirstEnd = int.Parse(match.Groups["FE"].Value);
                SecondStart = int.Parse(match.Groups["SS"].Value);
                SecondEnd = int.Parse(match.Groups["SE"].Value);
            }
        }

        public bool Validate(int value)
        {
            return (value >= FirstStart && value <= FirstEnd)
                || (value >= SecondStart && value <= SecondEnd);
        }
    }
}
