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
                const int cupCount = 1000000;
                const int moveCount = 10000000;

                //var input = "389125467";
                var input = "167248359";

                var circle = new LinkedList<int>(input.Select(c => (int )char.GetNumericValue(c)));

                for (int i = 10; i <= cupCount; i++)
                {
                    circle.AddLast(i);
                }

                var cupLookupTable = new Dictionary<int, LinkedListNode<int>>();
                var currentCupNode = circle.First;
                do
                {
                    cupLookupTable[currentCupNode.Value] = currentCupNode;
                    currentCupNode = currentCupNode.Next;
                } while (currentCupNode != null);

                var circleLength = circle.Count;
                currentCupNode = circle.First;

                for (int move = 1; move <= moveCount; move++)
                {
                    // Put cups aside
                    var asideCups = new List<LinkedListNode<int>>();
                    var cupNode = currentCupNode;
                    for (int i = 1; i <= 3; i++)
                    {
                        cupNode = cupNode.Next ?? circle.First;
                        asideCups.Add(cupNode);
                    }

                    foreach (var cup in asideCups)
                    {
                        circle.Remove(cup);
                    }

                    // Find destination
                    var destinationCup = currentCupNode.Value == 1 ? circle.Max() : currentCupNode.Value - 1;
                    var asideCupNumbers = asideCups.Select(c => c.Value);
                    while (asideCupNumbers.Contains(destinationCup))
                    {
                        destinationCup = destinationCup == 1 ? circle.Max() : destinationCup - 1;
                    }

                    var destincationCupNode = cupLookupTable[destinationCup];

                    // Add back cups put aside
                    cupNode = destincationCupNode;
                    foreach (var cup in asideCups)
                    {
                        circle.AddAfter(cupNode, cup);
                        cupNode = cup;
                    }

                    // Find next current cup
                    currentCupNode = currentCupNode.Next ?? circle.First;
                }

                //while (circle.First() != 1)
                //{
                //    var cup = circle.First();
                //    circle.Remove(cup);
                //    circle.AddLast(cup);
                //}
                //circle.Remove(1);
                //Console.WriteLine(string.Join(null, circle));

                var oneNode = circle.Find(1);
                var nextNode = oneNode.Next ?? circle.First;
                var nextToSecondNode = nextNode.Next ?? circle.First;

                long output = (long)nextNode.Value * (long)nextToSecondNode.Value;

                Console.WriteLine(output);
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
