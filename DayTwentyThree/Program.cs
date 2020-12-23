using System;
using System.Linq;
using FourLeggedHead.Tools;
using System.Collections.Generic;

namespace DayTwentyThree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day TwentyThree");

            try
            {
                //var input = "389125467";
                var input = "167248359";

                var circle = new List<int>(input.Select(c => int.Parse(c.ToString())));

                var circleLength = circle.Count;
                var currentCupIndex = 0;

                for (int move = 1; move <= 100; move++)
                {
                    var currentCup = circle[currentCupIndex];

                    var asideCups = new List<int>();
                    for (int i = 1; i <= 3; i++)
                    {
                        var cup = circle[(currentCupIndex + i) % circleLength];
                        asideCups.Add(cup);
                    }

                    var destinationCup = currentCup == 1 ? circle.Max() : currentCup - 1;
                    while (asideCups.Contains(destinationCup))
                    {
                        destinationCup = destinationCup == 1 ? circle.Max() : destinationCup - 1;
                    }

                    foreach (var cup in asideCups)
                    {
                        circle.Remove(cup);
                    }

                    circle.InsertRange(circle.IndexOf(destinationCup) + 1, asideCups);

                    while (currentCupIndex > circle.IndexOf(currentCup))
                    {
                        var cup = circle.Last();
                        circle.Remove(cup);
                        circle.Insert(0, cup);
                    }

                    while (currentCupIndex < circle.IndexOf(currentCup))
                    {
                        var cup = circle.First();
                        circle.Remove(cup);
                        circle.Add(cup);
                    }

                    currentCupIndex = (currentCupIndex + 1) % circleLength;

                    Console.WriteLine($"Move: {move} - " + string.Join(' ', circle));
                }

                while (circle.First() != 1)
                {
                    var cup = circle.First();
                    circle.Remove(cup);
                    circle.Add(cup);
                }

                circle.Remove(1);
                Console.WriteLine(string.Join(null, circle));

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        static int FindNewTargetDestination(List<int> circle, int targetDestination)
        {
            var min = circle.Min();
            var max = circle.Max();

            if (targetDestination - 1 < min) return max;
            else return targetDestination - 1;
        }
    }
}
