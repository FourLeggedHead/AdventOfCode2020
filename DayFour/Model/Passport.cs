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

        public bool IsValidSimple()
        {
            return Byr != null && Iyr != null && Eyr != null &&
                Hgt != null && Hcl != null && Ecl != null && Pid != null;
        }

        public bool IsValidComplex()
        {
            return ByrIsValid() && IyrIsValid() && EyrIsValid()
                && HgtIsValid() && HclIsValid() && EclIsValid() && PidIsValid();
        }

        private bool ByrIsValid()
        {
            return Byr != null && Regex.IsMatch(Byr, @"\d{4}") && int.Parse(Byr) >= 1920 && int.Parse(Byr) <= 2002;
        }

        private bool IyrIsValid()
        {
            return Iyr != null && Regex.IsMatch(Iyr, @"\d{4}") && int.Parse(Iyr) >= 2010 && int.Parse(Iyr) <= 2020;
        }

        private bool EyrIsValid()
        {
            return Eyr != null && Regex.IsMatch(Eyr, @"\d{4}") && int.Parse(Eyr) >= 2020 && int.Parse(Eyr) <= 2030;
        }

        private bool HgtIsValid()
        {
            if (Hgt == null) return false;

            if (Regex.IsMatch(Hgt,@"\d+cm"))
            {
                var cmHeight = int.Parse(Regex.Match(Hgt, @"(?<Height>\d+)cm").Groups["Height"].Value);

                if (cmHeight >= 150 && cmHeight <= 193) return true;
                else return false;
            }

            if (Regex.IsMatch(Hgt, @"\d+in"))
            {
                var cmHeight = int.Parse(Regex.Match(Hgt, @"(?<Height>\d+)in").Groups["Height"].Value);

                if (cmHeight >= 59 && cmHeight <= 76) return true;
                else return false;
            }

            return false;
        }

        private bool HclIsValid()
        {
            return Hcl != null && Regex.IsMatch(Hcl, @"#[0-9,a-f]{6}");
        }

        private bool EclIsValid()
        {
            return Ecl != null && Regex.IsMatch(Ecl, @"amb|blu|brn|gry|grn|hzl|oth");
        }

        public bool PidIsValid()
        {
            //return Pid != null && Regex.IsMatch(Pid, @"\d{9}");
            return Pid != null && Regex.IsMatch(Pid, @"\d+") && Pid.Length == 9;
        }

        public override string ToString()
        {
            return $"{Byr},{Iyr},{Eyr},{Hgt},{Hcl},{Ecl},{Pid},{Cid}";
        }
    }
}
