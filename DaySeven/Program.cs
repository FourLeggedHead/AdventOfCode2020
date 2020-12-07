using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using FourLeggedHead.IO;
using DaySeven.Model;

namespace DaySeven
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Seven");

            var path = @"Resources/input.txt";

            try
            {
                var rulesInput = FileReader.ReadAllLines(path);

                var ListOfRules = new List<Rule>();

                foreach (var rule in rulesInput)
                {
                    ListOfRules.Add(new Rule(rule));
                }

                var shinyRules = new List<Rule>();

                foreach (var rule in ListOfRules)
                {
                    if (FindShinyGold(ListOfRules, rule)) shinyRules.Add(rule);
                }

                Console.WriteLine(shinyRules.Select(r => r.Color).Distinct().Count());

                Console.WriteLine(CountBagsIn(ListOfRules, ListOfRules.Find(r => r.Color == "shiny gold")) - 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message,ex.StackTrace);
            }
        }

        private static bool FindShinyGold(List<Rule> listOfRules,  Rule originRule)
        {
            var queue = new Queue<Rule>();
            queue.Enqueue(originRule);

            while (queue.Any())
            {
                var rule = queue.Dequeue();

                if (rule.BagColors.Contains("shiny gold")) return true;

                if (rule.BagColors != null && rule.BagColors.Count != 0)
                {
                    foreach (var color in rule.BagColors)
                    {
                        var nextRule = listOfRules.Find(r => r.Color == color);
                        if (nextRule != null) queue.Enqueue(nextRule);
                    }
                }
            }

            return false;
        }

        private static int CountBagsIn(List<Rule> listOfRules, Rule originRule)
        {
            var bagCount = 1;

            if (originRule.BagCounts == null) return bagCount;
            else
            {
                for (int i = 0; i < originRule.BagColors.Count; i++)
                {
                    var nextRule = listOfRules.Find(r => r.Color == originRule.BagColors[i]);
                    if (nextRule != null) bagCount += originRule.BagCounts[i] * CountBagsIn(listOfRules, nextRule);
                }

                return bagCount;
            }
        }
    }
}
