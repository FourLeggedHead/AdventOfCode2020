using System;
using System.Collections.Generic;
using System.Text;

namespace DayTwelve.Model
{
    public class Waypoint
    {
        public int Latitude { get; set; }
        public int Longitude { get; set; }

        public Waypoint()
        {
            Latitude = 1;
            Longitude = -10;
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
                    int newLatitudeLeft;
                    int newLongitudeLeft;
                    switch (instruction.Value)
                    {
                        case 90:
                            newLatitudeLeft = -Longitude;
                            newLongitudeLeft = Latitude;
                            break;
                        case 180:
                            newLatitudeLeft = -Latitude;
                            newLongitudeLeft = -Longitude;
                            break;
                        case 270:
                            newLatitudeLeft = Longitude;
                            newLongitudeLeft = -Latitude;
                            break;
                        default:
                            throw new ArgumentException($"Value for rotation is not valid");
                    }

                    Latitude = newLatitudeLeft;
                    Longitude = newLongitudeLeft;

                    break;
                case Direction.Rigth:
                    int newLatitudeRight;
                    int newLongitudeRight;
                    switch (instruction.Value)
                    {
                        case 90:
                            newLatitudeRight = Longitude;
                            newLongitudeRight = -Latitude;
                            break;
                        case 180:
                            newLatitudeRight = -Latitude;
                            newLongitudeRight = -Longitude;
                            break;
                        case 270:
                            newLatitudeRight = -Longitude;
                            newLongitudeRight = Latitude;
                            break;
                        default:
                            throw new ArgumentException($"Value for rotation is not valid");
                    }

                    Latitude = newLatitudeRight;
                    Longitude = newLongitudeRight;

                    break;
                default:
                    break;
            }
        }

        public override string ToString()
        {
            return $"Latitude: {Latitude}; Longitude {Longitude}";
        }
    }
}
