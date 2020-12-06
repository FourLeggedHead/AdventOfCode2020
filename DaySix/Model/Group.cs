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

        public List<char> ListUniqueYesses()
        {
            var UniqueYesses = new List<char>();

            foreach (var person in PersonsAnswers)
            {
                UniqueYesses = UniqueYesses.Union(person.ToList()).ToList();
            }

            return UniqueYesses;
        }
    }
}
