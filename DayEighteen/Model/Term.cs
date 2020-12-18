using System;
using System.Collections.Generic;
using System.Text;

namespace DayEighteen.Model
{
    public class Term
    {
        public string Value { get; set; }
        public int Order { get; set; }

        public Term(string value)
        {
            Value = value;
        }
    }
}
