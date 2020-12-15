using System;
using System.Collections.Generic;
using System.Linq;

namespace DayFifteen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Fifteen");

            //var numbers = new List<int>() { 17, 1, 3, 16, 19, 0 };
            var numbers = new List<int>() { 0, 3, 6 };

            while (numbers.Count < 2020)
            {
                var lastSpoken = numbers.Last();

                if (numbers.SkipLast(1).Any(n => n == lastSpoken))
                {
                    var lastInstanceId = numbers.SkipLast(1).ToList().FindLastIndex(n => n == lastSpoken);
                    numbers.Add(numbers.Count - lastInstanceId - 1);
                }
                else
                {
                    numbers.Add(0);
                }
            }

            Console.WriteLine(numbers.Last());
        }
    }
}
