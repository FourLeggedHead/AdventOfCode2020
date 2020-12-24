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

                var floor = new Dictionary<(int X, int Y), Color>();

                foreach (var path in pathes)
                {
                    var position = FindPosition(path);

                    if (floor.ContainsKey(position))
                    {
                        floor[position] = floor[position] == Color.Black ? Color.White : Color.Black;
                    }
                    else
                    {
                        floor.Add(position, Color.Black);
                    }
                }

                Console.WriteLine(floor.Count(t => t.Value == Color.Black));

                for (int day = 0; day < 100; day++)
                {
                    var newLayout = new Dictionary<(int X, int Y), Color>();

                    AddMissingWhiteTiles(floor);
                    //Print(floor);

                    foreach (var tile in floor)
                    {
                        var adjacentTiles = FindAdjacentTiles(tile.Key, floor);
                        var blackTilesCount = adjacentTiles.Count(tile => tile.Value == Color.Black);

                        var color = tile.Value;
                        if (color == Color.Black && (blackTilesCount == 0 || blackTilesCount > 2))
                        {
                            color = Color.White;
                        }

                        if (color == Color.White && blackTilesCount == 2)
                        {
                            color = Color.Black;
                        }

                        newLayout.Add(tile.Key, color);
                    }

                    floor.Clear();
                    floor = newLayout;

                    Console.WriteLine(floor.Count(t => t.Value == Color.Black));
                }
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

        static (int X, int Y) MoveToNextTile((int X, int Y) position, string move)
        {
            switch (move)
            {
                case "e":
                    return (position.X + 2, position.Y);
                case "se":
                    return (position.X + 1, position.Y + 1);
                case "sw":
                    return (position.X - 1, position.Y + 1);
                case "w":
                    return (position.X - 2, position.Y);
                case "nw":
                    return (position.X - 1, position.Y - 1);
                case "ne":
                    return (position.X + 1, position.Y - 1);
                default:
                    return position;
            }
        }

        static Dictionary<(int X, int Y), Color> FindAdjacentTiles((int X, int Y) position, Dictionary<(int X, int Y), Color> floor)
        {
            var adjacentTiles = new Dictionary<(int X, int Y), Color>();

            var nePosition = (position.X + 1, position.Y - 1);
            if (floor.ContainsKey(nePosition)) adjacentTiles.Add(nePosition, floor[nePosition]);

            var ePosition = (position.X + 2, position.Y);
            if (floor.ContainsKey(ePosition)) adjacentTiles.Add(ePosition, floor[ePosition]);

            var sePosition = (position.X + 1, position.Y + 1);
            if (floor.ContainsKey(sePosition)) adjacentTiles.Add(sePosition, floor[sePosition]);

            var swPosition = (position.X - 1, position.Y + 1);
            if (floor.ContainsKey(swPosition)) adjacentTiles.Add(swPosition, floor[swPosition]);

            var wPosition = (position.X - 2, position.Y);
            if (floor.ContainsKey(wPosition)) adjacentTiles.Add(wPosition, floor[wPosition]);

            var nwPosition = (position.X - 1, position.Y - 1);
            if (floor.ContainsKey(nwPosition)) adjacentTiles.Add(nwPosition, floor[nwPosition]);

            return adjacentTiles;
        }

        static void AddMissingWhiteTiles(Dictionary<(int X, int Y), Color> floor)
        {
            var minX = floor.Min(t => t.Key.X) - 1;
            var maxX = floor.Max(t => t.Key.X) + 1;
            var minY = floor.Min(t => t.Key.Y) - 1;
            var maxY = floor.Max(t => t.Key.Y) + 1;

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        if (!floor.ContainsKey((x, y))) floor.Add((x, y), Color.White);
                    }
                }
            }
        }

        static void Print(Dictionary<(int X, int Y), Color> floor)
        {

            var minX = floor.Min(t => t.Key.X) + 1;
            var maxX = floor.Max(t => t.Key.X) - 1;
            var minY = floor.Min(t => t.Key.Y) + 1;
            var maxY = floor.Max(t => t.Key.Y) - 1;

            for (int y = minY; y <= maxY; y++)
            {
                var line = new StringBuilder();
                for (int x = minX; x <= maxX; x++)
                {
                    if ((x + y) % 2 != 0)
                    {
                        line.Append(' ');
                    }
                    else
                    {
                        if (floor[(x, y)] == Color.Black) line.Append('#');
                        else line.Append('.');
                    }
                }
                Console.WriteLine(line.ToString());
            }

            Console.WriteLine();
        }
    }

    enum Color
    {
        White,
        Black
    }
}
