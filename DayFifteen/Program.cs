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

            var numbers = new List<long>() { 17, 1, 3, 16, 19, 0 };

            var saidNumbers = new Dictionary<long, long>();
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                saidNumbers.Add(numbers[i], i + 1);
            }

            var turn = numbers.Count;

            while (turn < 30000000)
            {
                var lastSpoken = numbers.Last();

                if (saidNumbers.ContainsKey(lastSpoken))
                {
                    numbers.Add(turn - saidNumbers[lastSpoken]);
                    saidNumbers[lastSpoken] = turn;
                }
                else
                {
                    numbers.Add(0);
                    saidNumbers.Add(lastSpoken, turn);
                }

                turn++;
            }

            Console.WriteLine(numbers.Last());
        }
    }
}
