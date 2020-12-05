using System;
using System.IO;
using System.Collections.Generic;

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

                var area = new List<char[]>();

                foreach (var line in lines)
                {
                    area.Add(line.ToCharArray());
                }

                var patternWidth = area[0].Length;

                var treeCount = 0;

                for (int i = 0; i < area.Count; i++)
                {
                    if (area[i][3 * i % patternWidth] == '#') treeCount++;
                }

                Console.WriteLine($"The number of trees encountered is {treeCount}.");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
