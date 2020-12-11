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

        public IEnumerable<Seat> FindAdjacentSeats(List<Seat> seatingArea)
        {
            var maxX = seatingArea.Max(s => s.X);
            var maxY = seatingArea.Max(s => s.Y);

            for (int i = X - 1; i <= X + 1; i++)
            {
                for (int j = Y - 1; j <= Y + 1; j++)
                {
                    if (i >= 0 && j >= 0 && i <= maxX && j <= maxY && !(j == Y && i == X))
                        yield return seatingArea.Find(s => s.X == i && s.Y == j);
                }
            }
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y} State: {State}";
        }
    }
}
