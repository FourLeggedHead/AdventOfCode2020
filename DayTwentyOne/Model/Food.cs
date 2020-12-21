using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace DayTwentyOne.Model
{
    public class Food
    {
        public List<string> Ingredients { get; set; }
        public List<string> Allergens { get; set; }

        public Food(string food)
        {
            var matchFood = Regex.Match(food, @"(?<Ingredients>(\w+\s)+)[(]contains\s(?<Allergens>(\w+[,]*\s*)+)[)]");

            if (!matchFood.Success) throw new ArgumentException("This is not food.");

            Ingredients = matchFood.Groups["Ingredients"]
                .Value.Split(" ")
                .Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

            Allergens = matchFood.Groups["Allergens"]
                .Value.Split(",")
                .Select(a => a.Trim()).ToList();
        }
    }
}
