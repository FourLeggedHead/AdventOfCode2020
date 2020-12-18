using FourLeggedHead.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DayEighteen.Model;

namespace DayEighteen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Eighteen");
            try
            {
                var expressions = FileReader.ReadAllLines(@"Resources/input.txt");

                var test = expressions.ElementAt(0).EvaluateModified();

                var postfixExpressions = expressions.Select(ex => ex.ToPostFix());
                var results = expressions.Select(ex => ex.Evaluate());
                var modifiedResults = expressions.Select(ex => ex.EvaluateModified());

                Console.WriteLine(modifiedResults.Sum());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
