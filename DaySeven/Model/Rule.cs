using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DaySeven.Model
{
    public class Rule
    {
        public string Color { get; set; }
        public List<string> Colors { get; set; }

        public Rule(string rule)
        {
            var ruleMatch = Regex.Match(rule, @"(?<OuterBag>[a-z\s]+) bags contain((\s?\d+\s(?<InnerBags>[a-z]+\s[a-z]+) bags?(,\s)?)+)?.");

            if (ruleMatch.Success)
            {
                Color = ruleMatch.Groups["OuterBag"].Value;

                if (ruleMatch.Groups.ContainsKey("InnerBags"))
                {
                    Colors = new List<string>();

                    foreach (Capture capture in ruleMatch.Groups["InnerBags"].Captures)
                    {
                        Colors.Add(capture.Value);
                    }
                }
            }
        }
    }
}
