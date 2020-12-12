using DayTwelve.Model;
using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DayTwelve
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day twelve");

            var path = @"Resources/Input.txt";

            try
            {
                var instructionsInput = FileReader.ReadAllLines(path);

                var instrunctions = new List<Instruction>(instructionsInput.Select(i => new Instruction(i)));

                var ship = new Ship();
                Console.WriteLine(ship);
                Console.WriteLine();

                for (int i = 0; i < instrunctions.Count; i++)
                {
                    Console.WriteLine(instrunctions[i]);
                    ship.Move(instrunctions[i]);
                    Console.WriteLine(ship.ToString());
                    Console.WriteLine();
                }

                Console.WriteLine(ship.ManhattanDistance());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
