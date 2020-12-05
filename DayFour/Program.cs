using System;
using System.IO;
using System.Collections.Generic;
using DayFour.Model;
using System.Linq;

namespace DayFour  
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Four");

            var path = @"Resources/input.txt";

            try
            {
                if (!File.Exists(path)) throw new FileNotFoundException();

                var lines = File.ReadAllLines(path);

                if (lines.Length == 0) throw new Exception($"File is empty.");

                var Passports = new List<Passport>();
                var passportRecord = String.Empty;

                for (var i=0; i<lines.Length;i++)
                {
                    if (lines[i] != String.Empty) passportRecord += lines[i] + " ";

                    if (lines[i] == String.Empty || i == lines.Length - 1)
                    {
                        Passports.Add(new Passport(passportRecord));
                        passportRecord = String.Empty;
                    }
                }

                Console.WriteLine($"The count of valid (simple) passports is {Passports.Count(p=>p.IsValidSimple())}.");
                Console.WriteLine($"The count of valid (complex) passports is {Passports.Count(p => p.IsValidComplex())}.");

                var validPassports = Passports.Where(p => p.IsValidComplex()).Select(p => p.ToString());

                File.WriteAllLines(@"Resources/output.txt", validPassports);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
