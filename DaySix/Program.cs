using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using DaySix.Model;

namespace DaySix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Six");

            var path = @"Resources/input.txt";

            try
            {
                if (!File.Exists(path)) throw new FileNotFoundException();

                var lines = File.ReadAllLines(path);

                if (lines.Length == 0) throw new Exception($"File is empty.");

                var PassengerGroups = new List<Group>();
                var PassengerGroup = new Group();

                for (var i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != String.Empty) PassengerGroup.PersonsAnswers.Add(lines[i].ToCharArray());

                    if (lines[i] == String.Empty || i == lines.Length - 1)
                    {
                        PassengerGroups.Add(PassengerGroup);
                        PassengerGroup = new Group();
                    }
                }

                Console.WriteLine($"The sum of the yesses counts is {PassengerGroups.Select(g => g.ListUniqueYesses().Count).Sum()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
