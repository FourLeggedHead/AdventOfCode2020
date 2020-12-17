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

                Print(pocket);

                var cycles = 6;
                for (int i = 0; i < cycles; i++)
                {
                    var missingCubes = new Dictionary<int,Cube>();
                    foreach (var cube in pocket)
                    {
                        foreach (var missingCube in cube.Value.FindMissingNeighbors(pocket))
                        {
                            if(!missingCubes.ContainsKey(missingCube.Key))
                                missingCubes.Add(missingCube.Key, missingCube.Value);
                        }
                    }

                    var updatedPocket = new Dictionary<int, Cube>();
                    foreach (var cube in pocket.Union(missingCubes))
                    {
                        updatedPocket.Add(cube.Key, new Cube(cube.Value));
                    }

                    foreach (var cube in updatedPocket)
                    {
                        cube.Value.UpdateState(pocket);
                    }

                    pocket.Clear();
                    foreach (var cube in updatedPocket)
                    {
                        pocket.Add(cube.Key,cube.Value);
                    }
                }

                Print(pocket);

                Console.WriteLine(pocket.Count(c => c.Value.State == CubeState.Active));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " - " + ex.StackTrace);
            }
        }

        static void Print(Dictionary<int, Cube> pocket)
        {
            var minX = pocket.Min(c => c.Value.X);
            var maxX = pocket.Max(c => c.Value.X);
            var minY = pocket.Min(c => c.Value.Y);
            var maxY = pocket.Max(c => c.Value.Y);
            var minZ = pocket.Min(c => c.Value.Z);
            var maxZ = pocket.Max(c => c.Value.Z);

            for (int z = minZ; z <= maxZ; z++)
            {
                Console.WriteLine(z);
                for (int x = minX; x <= maxX; x++)
                {
                    for (int y = minY; y <= maxY; y++)
                    {
                        var state = pocket.Where(c => c.Value.X == x && c.Value.Y == y && c.Value.Z == z).Select(c => c.Value.State).ElementAt(0);

                        if (state == CubeState.Active) Console.Write("#");
                        else Console.Write(".");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
