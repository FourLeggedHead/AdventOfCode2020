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

                var messages = input.Skip(rules.Count + 1).ToList();
                Console.WriteLine(messages.Count(m => validMessages.Contains(m)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
