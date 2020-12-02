using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DayOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code Day One!");

            var path = $"Resources/input.txt";

            try
            {
                if (!File.Exists(path)) throw new FileNotFoundException();

                var lines = File.ReadAllLines(path);

                if (lines.Length == 0) throw new Exception($"File is empty.");

                var expenses = lines.Select(int.Parse).ToList();

                for (int i = 0; i < expenses.Count; i++)
                {
                    var firstExpense = expenses[i];

                    for (int j = i+1; j < expenses.Count; j++)
                    {
                        var secondExpenses = expenses[j];

                        for (int k = j+1; k < expenses.Count; k++)
                        {
                            var thirdExpense = expenses[k];

                            if (firstExpense + secondExpenses + thirdExpense == 2020)
                            {
                                Console.WriteLine($"The first expense is {firstExpense}, the second one is {secondExpenses} and the third one {thirdExpense}.");
                                Console.WriteLine($"Their product is {firstExpense * secondExpenses * thirdExpense}");
                                return;
                            }
                        }

                        //if (firstExpense + secondExpenses == 2020)
                        //{
                        //    Console.WriteLine($"The first expense is {firstExpense} and the second one is {secondExpenses}.");
                        //    Console.WriteLine($"Their product is {firstExpense * secondExpenses}");
                        //    return;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
