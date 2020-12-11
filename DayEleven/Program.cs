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

                var seatingArea = new List<Seat>();
                for (int i = 0; i < seatRows.Count(); i++)
                {
                    var row = seatRows.ElementAt(i);
                    seatingArea.AddRange(row.Select(c => new Seat(i, row.IndexOf(c), c)));
                }

                var changes = 0;

                do
                {
                    var changedSeatingArea = new List<Seat>();

                    foreach (var seat in seatingArea)
                    {
                        var changedSeat = seat.Copy();

                        if (seat.State == State.Empty &&
                                seat.FindAdjacentSeats(seatingArea).All(s => s.State == State.Empty))
                        {
                            changedSeat.State = State.Occupied;
                            changes++;
                        }

                        if (seat.State == State.Occupied &&
                            seat.FindAdjacentSeats(seatingArea).Count(s => s.State == State.Occupied) >= 4)
                        {
                            changedSeat.State = State.Empty;
                            changes++;
                        }

                        changedSeatingArea.Add(changedSeat);
                    }

                    seatingArea = changedSeatingArea;

                } while (changes > 0);

                Console.WriteLine(seatingArea.Count(s => s.State == State.Occupied));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
