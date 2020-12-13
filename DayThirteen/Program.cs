using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DayThirteen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Thirteen");

            var path = @"Resources/Input.txt";

            try
            {
                var input = FileReader.ReadAllLines(path);

                var timestamp = int.Parse(input.ElementAt(0));

                var busIds = input.ElementAt(1).Split(',').Where(b => b != "x").Select(b => int.Parse(b)).ToList();

                var waits = busIds.Select(b => b * (int)((timestamp / b) + 1) - timestamp).ToList();

                var smalestWait = waits.Min();

                Console.WriteLine(busIds[waits.IndexOf(smalestWait)] * smalestWait);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
