using FourLeggedHead.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace DayTwentyFour
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day TwentyFour");

            try
            {
                var pathes = FileReader.ReadAllLines(@"Resources/input.txt").ToList();

                var tiles = new Dictionary<(int X, int Y), Color>();

                foreach (var path in pathes)
                {
                    var position = FindPosition(path);

                    if (tiles.ContainsKey(position))
                    {
                        tiles[position] = tiles[position] == Color.Black ? Color.White : Color.Black;
                    }
                    else
                    {
                        tiles.Add(position, Color.Black);
                    }
                }

                Console.WriteLine(tiles.Count(t => t.Value == Color.Black));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        static (int X, int Y) FindPosition(string path)
        {
            var position = (0, 0);

            var moveBuilder = new StringBuilder();
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == 'n' || path[i] == 's')
                {
                    moveBuilder.Append(path[i]);
                }
                else
                {
                    moveBuilder.Append(path[i]);
                    position = MoveToNextTile(position, moveBuilder.ToString());
                    moveBuilder.Clear();
                }
            }

            return position;
        }

        static (int X, int Y) MoveToNextTile((int X, int Y) tile, string move)
        {
            switch (move)
            {
                case "e":
                    return (tile.X + 2, tile.Y);
                case "se":
                    return (tile.X + 1, tile.Y + 1);
                case "sw":
                    return (tile.X - 1, tile.Y + 1);
                case "w":
                    return (tile.X - 2, tile.Y);
                case "nw":
                    return (tile.X - 1, tile.Y - 1);
                case "ne":
                    return (tile.X + 1, tile.Y - 1);
                default:
                    return tile;
            }
        }
    }

    enum Color
    {
        White,
        Black
    }
}
