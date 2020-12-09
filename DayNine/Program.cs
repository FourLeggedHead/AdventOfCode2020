using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace DayNine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Nine");

            var path = @"Resources/input.txt";

            try
            {
                var numbers = FileReader.ReadAllLines(path);

                var XmasNumbers = new List<long>(numbers.Select(n => long.Parse(n)));

                var preambleLength = 25;

                for (int i = preambleLength; i < XmasNumbers.Count; i++)
                {
                    if(!XmasNumbers.Skip(i - preambleLength).Take(preambleLength).Subsets(2).Any(s => s.Sum() == XmasNumbers[i]))
                    {
                        Console.WriteLine(XmasNumbers[i]);

                        var windowSize = 2;

                        while (windowSize < i)
                        {
                            var weakness = XmasNumbers.Take(i).Window(windowSize).FirstOrDefault(g => g.Sum() == XmasNumbers[i]);

                            if (weakness != null)
                            {
                                Console.WriteLine(weakness.Min() + weakness.Max());

                                break;
                            }

                            windowSize++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
