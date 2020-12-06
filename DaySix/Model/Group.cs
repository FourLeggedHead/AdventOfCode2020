using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DaySix.Model
{
    public class Group
    {
        public List<char[]> PersonsAnswers { get; set; }

        public Group()
        {
            PersonsAnswers = new List<char[]>();
        }

        public List<char> ListUniqueAnyoneYesses()
        {
            List<char> anyoneYesses = null;

            foreach (var person in PersonsAnswers)
            {
                if (anyoneYesses == null) anyoneYesses = person.ToList();
                else anyoneYesses = anyoneYesses.Union(person.ToList()).ToList();
            }

            return anyoneYesses;
        }

        public List<char> ListUniqueEveryoneYesses()
        {
            List<char> everyYesses = null;

            foreach (var person in PersonsAnswers)
            {
                if (everyYesses == null) everyYesses = person.ToList();
                else everyYesses = everyYesses.Intersect(person.ToList()).ToList();
            }

            return everyYesses;
        }
    }
}
