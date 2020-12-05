using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using DayFive.Model;

namespace DayFive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Five");

            var path = @"Resources/input.txt";

            try
            {
                if (!File.Exists(path)) throw new FileNotFoundException();

                var lines = File.ReadAllLines(path);

                if (lines.Length == 0) throw new Exception($"File is empty.");

                var BoardingPasses = new List<BoardingPass>();

                foreach (var line in lines)
                {
                    BoardingPasses.Add(new BoardingPass(line));
                }

                Console.WriteLine($"The highest seat ID is {BoardingPasses.Max(b => b.Id)}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
