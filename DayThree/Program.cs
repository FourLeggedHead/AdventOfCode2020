using System;
using System.IO;
using System.Collections.Generic;
using DayThree.Model;
using System.Linq;

namespace DayThree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Three");

            var path = @"Resources/input.txt";

            try
            {
                if (!File.Exists(path)) throw new FileNotFoundException();

                var lines = File.ReadAllLines(path);

                if (lines.Length == 0) throw new Exception($"File is empty.");

                // Populate the area

                var area = new List<char[]>();

                foreach (var line in lines)
                {
                    area.Add(line.ToCharArray());
                }

                // Identify the width of the pattern

                var patternWidth = area[0].Length;

                // Initialize slopes

                var slopes = new List<Slopes>();

                slopes.Add(new Slopes { Down = 1, Right = 1, EncounteredTrees = 0 });
                slopes.Add(new Slopes { Down = 1, Right = 3, EncounteredTrees = 0 });
                slopes.Add(new Slopes { Down = 1, Right = 5, EncounteredTrees = 0 });
                slopes.Add(new Slopes { Down = 1, Right = 7, EncounteredTrees = 0 });
                slopes.Add(new Slopes { Down = 2, Right = 1, EncounteredTrees = 0 });

                // Go down the slopes

                foreach (var slope in slopes)
                {
                    for (int i = 0; i < area.Count; i++)
                    {
                        if (slope.Down * i < area.Count &&
                            area[slope.Down * i][(slope.Right * i) % patternWidth] == '#') slope.EncounteredTrees++;
                    }

                    Console.WriteLine($"The number of trees encountered in the slope {slope.Right}-{slope.Down} is {slope.EncounteredTrees}.");
                }

                // Figure out the multiply

                Console.WriteLine($"The multiply is {slopes.Aggregate<Slopes, long>(1, (multiply, slope) => multiply *= slope.EncounteredTrees)}.");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
