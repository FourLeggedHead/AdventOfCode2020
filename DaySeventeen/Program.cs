using DaySeventeen.Model;
using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DaySeventeen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Seventeem");
            try
            {
                var input = FileReader.ReadAllLines(@"Resources/input.txt");

                var pocket = new Dictionary<int, Cube>();

                for (int i = 0; i < input.Count(); i++)
                {
                    for (int j = 0; j < input.ElementAt(i).Length; j++)
                    {
                        var cube = new Cube(i, j, 0, input.ElementAt(i)[j]);
                        pocket.Add(cube.GetHashCode(), cube);
                    }
                }

                var cycles = 6;
                for (int i = 0; i < cycles; i++)
                {
                    foreach (var cube in pocket)
                    {
                        var missingCubes = cube.Value.FindMissingNeighbors(pocket);
                        foreach (var missingCube in missingCubes)
                        {
                            pocket.Add(missingCube.Key, missingCube.Value);
                        }
                    }

                    var updatedPocket = new Dictionary<int, Cube>();
                    foreach (var cube in pocket)
                    {
                        updatedPocket.Add(cube.Key, cube.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " - " + ex.StackTrace);
            }
        }
    }
}
