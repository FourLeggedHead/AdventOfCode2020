using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using DayTen.Model;

namespace DayTen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Ten");

            var path = @"Resources/input.txt";

            try
            {
                var joltages = FileReader.ReadAllLines(path);

                var Adapters = new List<Adapter>(joltages.Select(j => new Adapter(int.Parse(j))));

                var deviceJoltage = Adapters.Max(a => a.Joltage) + 3;

                var differences = new int[3] { 0, 0, 0 };
                var joltage = 0;

                while (joltage < deviceJoltage - 3)
                {
                    var adpater = Adapters.Find(a => a.Joltage == joltage + 1);
                    if (adpater != null && !adpater.Used)
                    {
                        adpater.Used = true;
                        joltage = adpater.Joltage;
                        differences[0]++;
                        continue;
                    }

                    adpater = Adapters.Find(a => a.Joltage == joltage + 2);
                    if (adpater != null && !adpater.Used)
                    {
                        adpater.Used = true;
                        joltage = adpater.Joltage;
                        differences[1]++;
                        continue;
                    }

                    adpater = Adapters.Find(a => a.Joltage == joltage + 3);
                    if (adpater != null && !adpater.Used)
                    {
                        adpater.Used = true;
                        joltage = adpater.Joltage;
                        differences[2]++;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Houston we have a problem!");
                    }
                }

                differences[2]++;

                Console.WriteLine(differences[0] * differences[2]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
