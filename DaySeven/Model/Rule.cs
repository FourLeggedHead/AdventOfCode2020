using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DaySeven.Model
{
    public class Rule
    {
        public string Color { get; set; }
        public List<int> BagCounts { get; set; }
        public List<string> BagColors { get; set; }

        public Rule(string rule)
        {
            var ruleMatch = Regex.Match(rule, @"(?<OuterBag>[a-z\s]+) bags contain((\s?(?<BagCounts>\d+)\s(?<BagColors>[a-z]+\s[a-z]+) bags?(,\s)?)+)?.");

            if (ruleMatch.Success)
            {
                Color = ruleMatch.Groups["OuterBag"].Value;

                if (ruleMatch.Groups.ContainsKey("BagColors"))
                {
                    BagColors = new List<string>();

                    foreach (Capture capture in ruleMatch.Groups["BagColors"].Captures)
                    {
                        BagColors.Add(capture.Value);
                    }
                }

                if (ruleMatch.Groups.ContainsKey("BagCounts"))
                {
                    BagCounts = new List<int>();

                    foreach (Capture capture in ruleMatch.Groups["BagCounts"].Captures)
                    {
                        BagCounts.Add(int.Parse(capture.Value));
                    }
                }
            }
        }
    }
}
