using FourLeggedHead.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using DayEleven.Model;

namespace DayEleven
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Ten");

            var path = @"Resources/Input.txt";

            try
            {
                var seatRows = FileReader.ReadAllLines(path);

                var maxX = seatRows.Count();
                var maxY = seatRows.ElementAt(0).Length;

                var seatingArea = new Seat[maxX, maxY];

                for (int i = 0; i < seatRows.Count(); i++)
                {
                    var row = seatRows.ElementAt(i);

                    for (int j = 0; j < row.Length; j++)
                    {
                        seatingArea[i,j] = (new Seat(i, j, row[j]));
                    }
                }

                int changes;

                do
                {
                    changes = 0;

                    var changedSeatingArea = new Seat[maxX, maxY];

                    for (int i = 0; i < maxX; i++)
                    {
                        for (int j = 0; j < maxY; j++)
                        {
                            var seat = seatingArea[i, j];
                            var changedSeat = seat.Copy();

                            var adjacentSeats = seat.FindAdjacentSeats(seatingArea);

                            if (seat.State == State.Empty && adjacentSeats != null &&
                                    adjacentSeats.All(s => s.State == State.Empty || s.State == State.Floor))
                            {
                                changedSeat.State = State.Occupied;
                                changes++;
                            }

                            if (seat.State == State.Occupied && adjacentSeats != null &&
                                adjacentSeats.Count(s => s.State == State.Occupied) >= 4)
                            {
                                changedSeat.State = State.Empty;
                                changes++;
                            }

                            changedSeatingArea[i, j] = changedSeat;
                        }
                    }

                    seatingArea = changedSeatingArea;

                    Console.WriteLine(changes);

                } while (changes > 0);

                Console.WriteLine(seatingArea.Flatten().Count(s => ((Seat)s).State == State.Occupied));

                //PrintSeatingArea(seatingArea);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }

        static void PrintSeatingArea(List<Seat> area)
        {
            for (int i = 0; i <= area.Max(s => s.X); i++)
            {
                for (int j = 0; j <= area.Max(s => s.Y); j++)
                {
                    var state = area.Find(s => s.X == i && s.Y == j).State;

                    switch (state)
                    {
                        case State.Empty:
                            Console.Write("L");
                            break;
                        case State.Occupied:
                            Console.Write("#");
                            break;
                        case State.Floor:
                            Console.Write(".");
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
