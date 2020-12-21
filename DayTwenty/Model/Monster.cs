using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DayTwenty.Model
{
    public class Monster
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<(int, int), byte> Pixels { get; set; }
                
        public Monster(string[] monster)
        {
            Height = monster.Length;
            Width = monster[0].Length;

            Pixels = new Dictionary<(int, int), byte>();
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
                {
                    if (monster[j][i] == '#') Pixels.Add((i, j), 1);
                    else Pixels.Add((i, j), 0);
                }
            }
        }

        public IEnumerable<(int,int)> Signature()
        {            
            return Pixels.Where(s => s.Value == 1).Select(s => s.Key);
        }
    }
}
