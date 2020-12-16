using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using DaySixteen.Model;

namespace DaySixteen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Sixteen");

            try
            {
                var input = FileReader.ReadAllLines(@"Resources/input.txt").ToArray();

                var i = 0;
                var rules = new List<Rule>();

                while (input[i] != "")
                {
                    rules.Add(new Rule(input[i]));
                    i++;
                }

                var myTicket = new List<int>();
                if (input[++i] == "your ticket:")
                {
                    myTicket.AddRange(input[++i].Split(',').Select(int.Parse));
                    i++;
                }

                var nearbyTickets = new List<List<int>>();
                if (input[++i] == "nearby tickets:")
                {
                    i++;
                    while (i < input.Length)
                    {
                        nearbyTickets.Add(new List<int>(input[i].Split(',').Select(int.Parse)));
                        i++;
                    }
                }

                var scanningRate = 0;
                var invalidTickets = new List<List<int>>();
                foreach (var ticket in nearbyTickets)
                {
                    var valid = true;

                    foreach (var number in ticket)
                    {
                        if (rules.Any(r => r.Validate(number)))
                            continue;
                        scanningRate += number;
                        valid = false;
                    }

                    if (!valid) invalidTickets.Add(ticket);
                }

                Console.WriteLine(scanningRate);

                foreach (var ticket in invalidTickets)
                {
                    nearbyTickets.Remove(ticket);
                }

                for (int p = 0; p < rules.Count; p++)
                {
                    var valuesAtPosition = nearbyTickets.Select(t => t[p]);

                    foreach (var rule in rules.Where(r => r.Position == 0))
                    {
                        if (valuesAtPosition.Select(v => rule.Validate(v)).All(b => b == true))
                        {
                            rule.Position = p + 1;
                            break;
                        }
                    }
                }

                var indexes = rules.Where(r => r.Name.StartsWith("departure")).Select(r => r.Position);

                long multiply = 1;
                foreach (var index in indexes)
                {
                    multiply *= myTicket[index - 1];
                }

                Console.WriteLine(multiply);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
