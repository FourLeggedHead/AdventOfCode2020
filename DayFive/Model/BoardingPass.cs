using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DayFive.Model
{
    public class BoardingPass
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Id { get; set; }

        public BoardingPass(string pass)
        {
            Row = CalculateRow(pass.Substring(0, 7));
            Column = CalculateColumn(pass.Substring(7, 3));
            Id = CalculateId(pass);
        }

        int CalculateRow(string row)
        {
            if (!Regex.IsMatch(row, @"[B,F]{7}"))
                throw new ArgumentException("This is not a boarding pass row.");

            return Convert.ToInt32(row.Replace('B', '1').Replace('F', '0'), 2);
        }

        int CalculateColumn(string column)
        {
            if (!Regex.IsMatch(column, @"[L,R]{3}"))
                throw new ArgumentException("This is not a boarding pass column.");

            return Convert.ToInt32(column.Replace('R', '1').Replace('L', '0'), 2);
        }

        int CalculateId(string pass)
        {
            if (!Regex.IsMatch(pass, @"[B,F]{7}[L,R]{3}"))
                throw new ArgumentException("This is not a boarding pass.");

            return Convert.ToInt32(pass.Replace('B', '1').Replace('F', '0').Replace('R', '1').Replace('L', '0'), 2);
        }
    }
}
