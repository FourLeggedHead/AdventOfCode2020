using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
