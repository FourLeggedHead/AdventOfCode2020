using DayTwentyOne.Model;
using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DayTwentyOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day TwentyOne");

            try
            {
                var input = FileReader.ReadAllLines(@"Resources/input.txt").ToList();

                var foods = input.Select(i => new Food(i));

                // Count occurences of allergens and ingredients
                var allergensCount = foods.SelectMany(f => f.Allergens).GroupBy(a => a).ToDictionary(g => g.Key, g => g.Count());
                var ingredientsCounts = foods.SelectMany(f => f.Ingredients).GroupBy(i => i).ToDictionary(g => g.Key, g => g.Count());

                // Figure out potential translations for allergens
                var allergensTranslations = new Dictionary<string, List<string>>();
                foreach (var allergen in allergensCount)
                {
                    var meanings = new List<string>();
                    foreach (var food in foods)
                    {
                        if (food.Allergens.Contains(allergen.Key))
                        {
                            if (meanings.Count == 0) meanings.AddRange(food.Ingredients);
                            else meanings = meanings.Intersect(food.Ingredients).ToList();
                        }
                    }

                    allergensTranslations.Add(allergen.Key, meanings);
                }
                                
                // Determine unique translation for allergens
                var allergensDictionary = new Dictionary<string, string>();
                while (allergensTranslations.Any(a => a.Value.Count == 1))
                {
                    var knownAllergen = allergensTranslations.First(a => a.Value.Count == 1);
                    var transtlation = knownAllergen.Value[0];
                    allergensDictionary.Add(knownAllergen.Key, transtlation);
                    allergensTranslations.Remove(knownAllergen.Key);

                    foreach (var truc in allergensTranslations)
                    {
                        if (truc.Value.Contains(transtlation))
                        {
                            truc.Value.Remove(transtlation);
                        }
                    }
                }

                // Remove known ingredients from the list of ingredients count
                foreach (var allergen in allergensDictionary)
                {
                    ingredientsCounts.Remove(allergen.Value);
                }

                Console.WriteLine(ingredientsCounts.Sum(i => i.Value));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
