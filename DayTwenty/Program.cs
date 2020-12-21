using FourLeggedHead.IO;
using System;
using System.Linq;
using MoreLinq;
using DayTwenty.Model;
using System.Collections.Generic;

namespace DayTwenty
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Nineteen");

            try
            {
                var input = FileReader.ReadAllLines(@"Resources/input.txt").ToList();

                var tiles = input
                    .Segment(string.IsNullOrWhiteSpace)
                    .Select(t => new Tile(t.Where(l => l != "").ToArray()))
                    .ToDictionary(t => t.Id);

                var puzzle = new Puzzle(tiles.Values);

                puzzle.Solve();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static Side Matching(Side side)
        {
            switch (side)
            {
                case Side.Top:
                    return Side.Bottom;
                case Side.Left:
                    return Side.Right;
                case Side.Bottom:
                    return Side.Top;
                case Side.Right:
                    return Side.Left;
                default:
                    return Side.Top;
            }
        }
    }
}
