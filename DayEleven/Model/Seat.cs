using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DayEleven.Model
{
    public enum State
    {
        Empty,
        Occupied,
        Floor
    }

    public class Seat
    {
        public int X { get; set; }
        public int Y { get; set; }
        public State State { get; set; }

        public Seat(int x, int y, char state)
        {
            X = x;
            Y = y;

            switch (state)
            {
                case 'L':
                    State = State.Empty;
                    break;
                case '#':
                    State = State.Occupied;
                    break;
                default:
                    State = State.Floor;
                    break;
            }
        }

        public Seat(int x, int y, State state)
        {
            X = x;
            Y = y;
            State = state;
        }

        public Seat Copy()
        {
            return new Seat(X,Y,State);
        }

        public IEnumerable<Seat> FindAdjacentSeats(Seat[,] seatingArea)
        {
            var maxX = seatingArea.GetLength(0);
            var maxY = seatingArea.GetLength(1);

            for (int i = X - 1; i <= X + 1; i++)
            {
                for (int j = Y - 1; j <= Y + 1; j++)
                {
                    if (i >= 0 && j >= 0 && i < maxX && j < maxY && !(j == Y && i == X))
                        yield return seatingArea[i,j];
                }
            }
        }

        public IEnumerable<Seat> FindNearbySeats(Seat[,] seatingArea)
        {
            var maxX = seatingArea.GetLength(0);
            var maxY = seatingArea.GetLength(1);

            var nearbySeats = new List<Seat>();

            var i = X + 1;
            while (i < maxX)
            {
                if (seatingArea[i,Y].State != State.Floor)
                {
                    nearbySeats.Add(seatingArea[i, Y]);
                    break;
                }

                i++;
            }

            i = X + 1;
            var j = Y + 1;
            while (i < maxX && j < maxY)
            {
                if (seatingArea[i, j].State != State.Floor)
                {
                    nearbySeats.Add(seatingArea[i, j]);
                    break;
                }

                i++;
                j++;
            }

            j = Y + 1;
            while (j < maxY)
            {
                if (seatingArea[X, j].State != State.Floor)
                {
                    nearbySeats.Add(seatingArea[X, j]);
                    break;
                }

                j++;
            }

            i = X - 1;
            j = Y + 1;
            while (i >= 0 && j < maxY)
            {
                if (seatingArea[i, j].State != State.Floor)
                {
                    nearbySeats.Add(seatingArea[i, j]);
                    break;
                }

                i--;
                j++;
            }

            i = X - 1;
            while (i >= 0)
            {
                if (seatingArea[i, Y].State != State.Floor)
                {
                    nearbySeats.Add(seatingArea[i, Y]);
                    break;
                }

                i--;
            }

            i = X - 1;
            j = Y - 1;
            while (i >= 0 && j >= 0)
            {
                if (seatingArea[i, j].State != State.Floor)
                {
                    nearbySeats.Add(seatingArea[i, j]);
                    break;
                }

                i--;
                j--;
            }

            j = Y - 1;
            while (j >= 0)
            {
                if (seatingArea[X, j].State != State.Floor)
                {
                    nearbySeats.Add(seatingArea[X, j]);
                    break;
                }

                j--;
            }

            i = X + 1;
            j = Y - 1;
            while (i < maxX && j >= 0)
            {
                if (seatingArea[i, j].State != State.Floor)
                {
                    nearbySeats.Add(seatingArea[i, j]);
                    break;
                }

                i++;
                j--;
            }

            return nearbySeats;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} State: {State}";
        }
    }
}
