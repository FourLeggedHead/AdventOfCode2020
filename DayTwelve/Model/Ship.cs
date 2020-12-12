using System;
using System.Collections.Generic;
using System.Text;

namespace DayTwelve.Model
{
    public enum Orientation
    {
        North,
        West,
        South,
        East
    }

    public class Ship
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public Orientation Orientation { get; set; }

        public Ship()
        {
            Latitude = 0;
            Longitude = 0;
            Orientation = Orientation.East;
        }

        void MoveForward(Instruction instruction)
        {
            switch (Orientation)
            {
                case Orientation.North:
                    Latitude += instruction.Value;
                    break;
                case Orientation.West:
                    Longitude += instruction.Value;
                    break;
                case Orientation.South:
                    Latitude -= instruction.Value;
                    break;
                case Orientation.East:
                    Longitude -= instruction.Value;
                    break;
                default:
                    break;
            }
        }

        public void Move(Instruction instruction)
        {
            switch (instruction.Direction)
            {
                case Direction.North:
                    Latitude += instruction.Value;
                    break;
                case Direction.West:
                    Longitude += instruction.Value;
                    break;
                case Direction.South:
                    Latitude -= instruction.Value;
                    break;
                case Direction.East:
                    Longitude -= instruction.Value;
                    break;
                case Direction.Left:
                    Orientation = (Orientation)(((int)Orientation + instruction.Value / 90) % 4);
                    break;
                case Direction.Rigth:
                    Orientation = (Orientation)(((int)Orientation + 4 - instruction.Value / 90) % 4);
                    break;
                case Direction.Forward:
                    MoveForward(instruction);
                    break;
                default:
                    break;
            }
        }

        public void MoveTowardWaypoint(ref Waypoint waypoint, Instruction instruction)
        {
            if (instruction.Direction == Direction.Forward)
            {
                Latitude += instruction.Value * waypoint.Latitude;
                Longitude += instruction.Value * waypoint.Longitude;
            }
            else
            {
                waypoint.Move(instruction);
            }
        }

        public long ManhattanDistance() => Math.Abs(Latitude) + Math.Abs(Longitude);

        public override string ToString()
        {
            return $"Latitude: {Latitude}; Longitude {Longitude}; Orientation: {Orientation}";
        }
    }
}
