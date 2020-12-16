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
                foreach (var ticket in nearbyTickets)
                {
                    foreach (var number in ticket)
                    {
                        if (rules.Any(r => r.Validate(number)))
                            continue;
                        scanningRate += number;
                    }
                }

                Console.WriteLine(scanningRate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
