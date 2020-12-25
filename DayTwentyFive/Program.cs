using FourLeggedHead.IO;
using System;
using System.Linq;

namespace DayTwentyFive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Twenty Five");

            var cardPublicKey = 14222596L;
            var doorPublicKey = 4057428L;

            // Door loop size

            var divider = 20201227L;

            var doorSecretLoop = 0L;
            var value = 1L;
            var subjectNumber = 7L;
            while (!(value == doorPublicKey))
            {
                value = (value * subjectNumber) % divider;
                doorSecretLoop++;
            }
            Console.WriteLine($"Door Secret Loop: {doorSecretLoop}");

            // Encryption key

            subjectNumber = cardPublicKey;
            value = 1L;
            for (int i = 0; i < doorSecretLoop; i++)
            {
                value = (value * subjectNumber) % divider;
            }
            Console.WriteLine($"Encryption key: {value}");
        }
    }
}
