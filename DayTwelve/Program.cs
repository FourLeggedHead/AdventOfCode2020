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

                for (int i = 0; i < instrunctions.Count; i++)
                {
                    ship.Move(instrunctions[i]);
                }

                Console.WriteLine(ship.ManhattanDistance());

                ship = new Ship();
                var waypoint = new Waypoint();

                for (int i = 0; i < instrunctions.Count; i++)
                {
                    ship.MoveTowardWaypoint(ref waypoint, instrunctions[i]);
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
