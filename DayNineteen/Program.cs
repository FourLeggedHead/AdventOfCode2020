using DayNineteen.Model;
using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;
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

                var truc = new List<List<string>>();
                var machin = affectedRules.Where(r => r.Id != 0);
                var bidule = machin.Select(r => r.Children).SelectMany(i => i).Distinct();
                foreach (var id in bidule)
                {
                    truc.Add(rules[id].ListValidMessages(rules));
                }

                var length = truc[0][0].Length;

                var uncheckedMessaged = messages.Where(m => !validatedMessage.Contains(m)).ToList();

                Console.WriteLine(uncheckedMessaged.Count());
                var count = validatedMessage.Count();
                var counter = 0;
                foreach (var message in uncheckedMessaged)
                {
                    Console.WriteLine(counter + ": " + message);
                    if (message.Length % length != 0) continue;

                    var index = message.Length / length - 1;

                    // Start twice with 42
                    if (!(truc[0].Contains(message.Substring(0, length)) && truc[0].Contains(message.Substring(length, length)))) continue;

                    // Finished by 31
                    if (!truc[1].Contains(message.Substring(index * length, length))) continue;
                    index--;

                    var isValid = true;
                    while (index > 1)
                    {
                        if (!truc[1].Contains(message.Substring(index * length, length)))
                        {
                            if (!truc[0].Contains(message.Substring(index * length, length))) continue;
                        }
                        index--;
                    }

                    if (isValid)
                    {
                        count++;
                        Console.WriteLine("Valid : " + message);
                    }
                }

                Console.WriteLine(count);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
