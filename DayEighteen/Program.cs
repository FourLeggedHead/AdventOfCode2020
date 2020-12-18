using DayEighteen.Model;
using FourLeggedHead.IO;
using System;
using System.Linq;

namespace DayEighteen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Eighteen");
            try
            {
                var input = FileReader.ReadAllLines(@"Resources/inputtest.txt");
                var expressions = input.Select(
                    l => l.Split('(').Select(
                        t => new Term(t.RemoveClosingParanthersis()).Value).ToList()).ToList();

                //foreach (var expression in expressions)
                //{
                //    expression.SetTermsOrder();
                //    expression.RemoveAll(t => t.Value == "(" || t.Value == ")");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
