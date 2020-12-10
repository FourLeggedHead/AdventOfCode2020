using System;
using System.Collections.Generic;
using System.Text;

namespace DayTen.Model
{
    public class Adapter
    {
        public int Joltage { get; set; }
        public bool Used { get; set; }

        public Adapter(int joltage)
        {
            Joltage = joltage;
            Used = false;
        }

        public override string ToString()
        {
            return Joltage + " : " + Used;
        }
    }
}
