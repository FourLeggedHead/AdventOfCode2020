using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace DayTwentyTwo.Model
{
    public static class DecksExtensions
    {
        public static void PrintDecks(this Dictionary<string, Queue<int>> decks)
        {
            var firstPlayer = decks.Keys.ElementAt(0);
            var secondPlayer = decks.Keys.ElementAt(1);

            Console.WriteLine(string.Join(',', decks[firstPlayer])
                    + " | " + string.Join(',', decks[secondPlayer]));
            Console.WriteLine();
        }

        public static int GetSequenceHashCode<T>(this IEnumerable<T> sequence)
        {
            const int seed = 487;
            const int modifier = 31;

            unchecked
            {
                return sequence.Aggregate(seed, (current, item) =>
                    (current * modifier) + item.GetHashCode());
            }
        }
    }
}
