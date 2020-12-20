using DayNineteen.Model;
using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DayNineteen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Nineteen");

            try
            {
                var input = FileReader.ReadAllLines(@"Resources/input.txt");

                var rules = input.TakeWhile(i => i != "").Select(r => new Rule(r)).ToDictionary(r => r.Id);
                var validMessages = rules[0].ListValidMessages(rules);

                var affectedRules = rules.Values.Where(r => r.HasChild(rules, 8) || r.HasChild(rules, 11));

                var messages = input.Skip(rules.Count + 1).ToList();
                var validatedMessage = messages.Where(m => validMessages.Contains(m)).ToList();
                Console.WriteLine(validatedMessage.Count());

                var invalidatedMessaged = messages.Where(m => !validatedMessage.Contains(m)).ToList();
                Console.WriteLine(invalidatedMessaged.Count());

				// Adgvaedjrfhgæoiwetbjpw

                var rulesDictionary = new Dictionary<string, string>(rules
					.Select(r => new KeyValuePair<string, string>(r.Key.ToString(), r.Value.Expression)));
				
				var processed = new Dictionary<string, string>();

				string BuildRegex(string input)
				{
					if (processed.TryGetValue(input, out var s))
						return s;

					var orig = rulesDictionary[input];
					if (orig.StartsWith('\"'))
						return processed[input] = orig.Replace("\"", "");

					if (!orig.Contains("|"))
						return processed[input] = string.Join("", orig.Split().Select(BuildRegex));

					return processed[input] =
						"(" +
						string.Join("", orig.Split().Select(x => x == "|" ? x : BuildRegex(x))) +
						")";
				}

				var regex = new Regex("^" + BuildRegex("0") + "$");
				Console.WriteLine(messages.Count(regex.IsMatch).ToString());

				regex = new Regex($@"^({BuildRegex("42")})+(?<open>{BuildRegex("42")})+(?<close-open>{BuildRegex("31")})+(?(open)(?!))$");
				Console.WriteLine(messages.Count(regex.IsMatch).ToString());
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
