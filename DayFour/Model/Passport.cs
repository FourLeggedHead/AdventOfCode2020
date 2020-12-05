using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace DayFour.Model
{
     public class Passport
     {
        public string Byr { get; set; }
        public string Iyr { get; set; }
        public string Eyr { get; set; }
        public string Hgt { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }
        public string Cid { get; set; }

        public Passport(string passport)
        {
            var match = Regex.Match(passport, @"byr:(?<Value>\S+)");
            if (match.Success) Byr = match.Groups["Value"].Value;

            match = Regex.Match(passport, @"iyr:(?<Value>\S+)");
            if (match.Success) Iyr = match.Groups["Value"].Value;

            match = Regex.Match(passport, @"eyr:(?<Value>\S+)");
            if (match.Success) Eyr = match.Groups["Value"].Value;

            match = Regex.Match(passport, @"hgt:(?<Value>\S+)");
            if (match.Success) Hgt = match.Groups["Value"].Value;

            match = Regex.Match(passport, @"hcl:(?<Value>\S+)");
            if (match.Success) Hcl = match.Groups["Value"].Value;

            match = Regex.Match(passport, @"ecl:(?<Value>\S+)");
            if (match.Success) Ecl = match.Groups["Value"].Value;

            match = Regex.Match(passport, @"pid:(?<Value>\S+)");
            if (match.Success) Pid = match.Groups["Value"].Value;

            match = Regex.Match(passport, @"cid:(?<Value>\S+)");
            if (match.Success) Cid = match.Groups["Value"].Value;
        }

        public bool IsValid()
        {
            return Byr != null && Iyr != null && Eyr != null &&
                Hgt != null && Hcl != null && Ecl != null && Pid != null;
        }
     }
}
