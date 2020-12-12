using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DayTwelve.Model
{
    public enum Direction
    {
        North,
        West,
        South,
        East,
        Left,
        Rigth,
        Forward
    }
    public class Instruction
    {
        public Direction Direction { get; set; }
        public int Value { get; set; }

        public Instruction(string instruction)
        {
            var match = Regex.Match(instruction, @"(?<Direction>[N,S,E,W,L,R,F])(?<Values>\d+)");

            if (match.Success)
            {
                switch (match.Groups["Direction"].Value)
                {
                    case "N":
                        Direction = Direction.North;
                        break;
                    case "W":
                        Direction = Direction.West;
                        break;
                    case "S":
                        Direction = Direction.South;
                        break;
                    case "E":
                        Direction = Direction.East;
                        break;
                    case "L":
                        Direction = Direction.Left;
                        break;
                    case "R":
                        Direction = Direction.Rigth;
                        break;
                    case "F":
                        Direction = Direction.Forward;
                        break;
                    default:
                        break;
                }

                Value = int.Parse(match.Groups["Values"].Value);
            }
        }

        public override string ToString()
        {
            return $"Direction: {Direction}; Value: {Value}";
        }
    }
}
