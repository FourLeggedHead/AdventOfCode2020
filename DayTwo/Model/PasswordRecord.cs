using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace DayTwo.Model
{
    public class PasswordRecord
    {
        public const string PASSWORD_RECORD_PATTERN = @"(?<Policy>.+):\s+(?<Password>[a-z]+)";
        public string Password { get; set; }
        public PasswordPolicy Policy { get; set; }

        public PasswordRecord(string record)
        {
            var recordMatch = Regex.Match(record, PASSWORD_RECORD_PATTERN);

            if (!recordMatch.Success)
                throw new ArgumentException("Password record is invalid.", nameof(record));

            Password = recordMatch.Groups["Password"].Value;
            Policy = new PasswordPolicy(recordMatch.Groups["Policy"].Value);
        }

        public bool IsValid()
        {
            var keyCount = Password.Count(c => c == Policy.Key);
            return keyCount >= Policy.MinKeyCount && keyCount <= Policy.MaxKeyCount;
        }

        public bool IsValidTwo()
        {
            return Password[Policy.MinKeyCount - 1] == Policy.Key ^ Password[Policy.MaxKeyCount - 1] == Policy.Key;
        }
    }
}
