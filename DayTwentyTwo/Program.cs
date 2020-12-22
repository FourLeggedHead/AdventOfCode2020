using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace DayTwentyTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day TwentyOne");

            try
            {
                var input = FileReader.ReadAllLines(@"Resources/input.txt").ToList();

                var dekcs = input.Segment(l => l.StartsWith("Player"))
                    .Select(p => p.Where(l => !l.StartsWith("Player") && !string.IsNullOrWhiteSpace(l)));

                var hands = new List<Queue<int>>();
                foreach (var deck in dekcs)
                {
                    hands.Add(new Queue<int>(deck.Select(int.Parse)));
                }

                while (hands.All(h => h.Count >0))
                {
                    var cardFirst = hands[0].Dequeue();
                    var cardSecond = hands[1].Dequeue();

                    if (cardFirst > cardSecond)
                    {
                        hands[0].Enqueue(cardFirst);
                        hands[0].Enqueue(cardSecond);
                    }
                    else
                    {
                        hands[1].Enqueue(cardSecond);
                        hands[1].Enqueue(cardFirst);
                    }
                }

                var winingHand = hands.Where(h => h.Count != 0).ElementAt(0);

                var score = 0;
                while (winingHand.Count > 0)
                {
                    score += winingHand.Count * winingHand.Dequeue();
                }

                Console.WriteLine(score);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
