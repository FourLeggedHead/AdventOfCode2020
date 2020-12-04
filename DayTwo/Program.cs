using System;
using System.IO;
using DayTwo.Model;

namespace DayTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code Day Two!");

            var path = @"Resources/input.txt";

            try
            {
                if (!File.Exists(path)) throw new FileNotFoundException();

                var lines = File.ReadAllLines(path);

                if (lines.Length == 0) throw new Exception($"File is empty.");

                int validPasswordCount = 0;
                foreach (var line in lines)
                {
                    var passwordRecord = new PasswordRecord(line);
                    if (passwordRecord.IsValid()) validPasswordCount++;
                }

                Console.WriteLine(validPasswordCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
