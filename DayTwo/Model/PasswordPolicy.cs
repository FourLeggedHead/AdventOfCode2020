using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DayTwo.Model
{
    public class PasswordPolicy
    {
        public const string POLICY_PATTER = @"(?<MinCount>\d+)-(?<MaxCount>\d+)\s+(?<Key>[a-z])";

        public int MinKeyCount { get; set; }
        public int MaxKeyCount { get; set; }
        public char Key { get; set; }

        public PasswordPolicy(string policy)
        {
            var policyMatch = Regex.Match(policy, POLICY_PATTER);

            if (!policyMatch.Success)
                throw new ArgumentException("Policy is invalid.",nameof(policy));

            MinKeyCount = int.Parse(policyMatch.Groups["MinCount"].Value);
            MaxKeyCount = int.Parse(policyMatch.Groups["MaxCount"].Value);
            Key = policyMatch.Groups["Key"].Value.ToCharArray()[0];
        }
    }
}
