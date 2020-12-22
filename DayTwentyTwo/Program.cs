using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using DayTwentyTwo.Model;
using System.Numerics;

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

                // Part I

                var decks = input
                    .Segment(l => l.StartsWith("Player"))
                    .ToDictionary(s => s.ElementAt(0).Trim(':'),
                                    s => new Queue<int>(s.Where(l => !l.StartsWith("Player") && !string.IsNullOrWhiteSpace(l))
                                                        .Select(int.Parse)));

                var winner = PlayingSimpleGame(decks);

                decks.PrintDecks();

                var score = 0;
                while (decks[winner].Count > 0)
                {
                    score += decks[winner].Count * decks[winner].Dequeue();
                }

                Console.WriteLine(score);

                // Part II

                decks = input
                    .Segment(l => l.StartsWith("Player"))
                    .ToDictionary(s => s.ElementAt(0).Trim(':'),
                                    s => new Queue<int>(s.Where(l => !l.StartsWith("Player") && !string.IsNullOrWhiteSpace(l))
                                                        .Select(int.Parse)));
                winner = PlayingRecursiveGame(decks);

                decks.PrintDecks();

                score = 0;
                while (decks[winner].Count > 0)
                {
                    score += decks[winner].Count * decks[winner].Dequeue();
                }

                Console.WriteLine(score);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        static string PlayingSimpleGame(Dictionary<string, Queue<int>> decks)
        {
            var firstPlayer = decks.Keys.ElementAt(0);
            var secondPlayer = decks.Keys.ElementAt(1);

            while (decks.Values.All(d => d.Count > 0))
            {
                var cardFirst = decks[firstPlayer].Dequeue();
                var cardSecond = decks[secondPlayer].Dequeue();

                if (cardFirst > cardSecond)
                {
                    decks[firstPlayer].Enqueue(cardFirst);
                    decks[firstPlayer].Enqueue(cardSecond);
                }
                else
                {
                    decks[secondPlayer].Enqueue(cardSecond);
                    decks[secondPlayer].Enqueue(cardFirst);
                }
            }

            return decks.Where(d => d.Value.Count != 0).Select(d => d.Key).ElementAt(0);
        }

        static string PlayingRecursiveGame(Dictionary<string, Queue<int>> decks)
        {
            var firstPlayer = decks.Keys.ElementAt(0);
            var secondPlayer = decks.Keys.ElementAt(1);

            var history = new HashSet<(int, int)>();

            while (decks.Values.All(d => d.Count > 0))
            {
                var decksIdentifier = (decks[firstPlayer].GetSequenceHashCode(), decks[secondPlayer].GetSequenceHashCode());

                if (history.Contains(decksIdentifier))
                {                    
                    return firstPlayer;
                }

                history.Add(decksIdentifier);

                var cardFirst = decks[firstPlayer].Dequeue();
                var cardSecond = decks[secondPlayer].Dequeue();

                if (decks[firstPlayer].Count < cardFirst || decks[secondPlayer].Count < cardSecond)
                {
                    if (cardFirst > cardSecond)
                    {
                        decks[firstPlayer].Enqueue(cardFirst);
                        decks[firstPlayer].Enqueue(cardSecond);
                    }
                    else
                    {
                        decks[secondPlayer].Enqueue(cardSecond);
                        decks[secondPlayer].Enqueue(cardFirst);
                    }
                }
                else
                {
                    var subDecks = new Dictionary<string, Queue<int>>
                    {
                        { firstPlayer, new Queue<int>(decks[firstPlayer].Take(cardFirst)) },
                        { secondPlayer, new Queue<int>(decks[secondPlayer].Take(cardSecond)) }
                    };

                    var winner = PlayingRecursiveGame(subDecks);

                    if (winner == firstPlayer)
                    {
                        decks[firstPlayer].Enqueue(cardFirst);
                        decks[firstPlayer].Enqueue(cardSecond);
                    }
                    else
                    {
                        decks[secondPlayer].Enqueue(cardSecond);
                        decks[secondPlayer].Enqueue(cardFirst);
                    }
                }
            }

            return decks.Where(d => d.Value.Count != 0).Select(d => d.Key).ElementAt(0);
        }
    }
}
