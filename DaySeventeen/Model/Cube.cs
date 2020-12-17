using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DaySeventeen.Model
{
    public enum CubeState
    {
        Active,
        Inactive
    }

    public class Cube
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public CubeState State { get; set; }

        public Cube(int x, int y, int z, CubeState state)
        {
            X = x;
            Y = y;
            Z = z;
            State = state;
        }

        public Cube(int x, int y, int z, char state)
        {
            X = x;
            Y = y;
            Z = z;

            switch (state)
            {
                case '.':
                    State = CubeState.Inactive;
                    break;
                case '#':
                    State = CubeState.Active;
                    break;
                default:
                    break;
            }
        }

        public Cube(Cube cube)
        {
            X = cube.X;
            Y = cube.Y;
            Z = cube.Z;
            State = cube.State;
        }

        public override int GetHashCode()
        {
            return CustomHash(X, Y, Z);
        }

        public int CustomHash(int x, int y, int z)
        {
            var seed = 10009;
            var factor = 9176;
            var hash = seed * factor + x.GetHashCode();
            hash = hash * factor + y.GetHashCode();
            hash = hash * factor + z.GetHashCode();
            return hash;
        }

        public List<Cube> FindExistingNeighbors(Dictionary<int, Cube> space)
        {
            var neighbors = new List<Cube>();

            for (int x = X - 1; x <= X + 1; x++)
            {
                for (int y = Y - 1; y <= Y + 1; y++)
                {
                    for (int z = Z - 1; z <= Z + 1; z++)
                    {
                        if (!(x == X && y == Y && z == Z))
                        {
                            if (space.TryGetValue(CustomHash(x, y, z), out Cube cube))
                                neighbors.Add(cube);
                        }
                    }
                }
            }

            return neighbors;
        }

        public Dictionary<int, Cube> FindMissingNeighbors(Dictionary<int, Cube> space)
        {
            var neighbors = new Dictionary<int, Cube>();

            for (int x = X - 1; x <= X + 1; x++)
            {
                for (int y = Y - 1; y <= Y + 1; y++)
                {
                    for (int z = Z - 1; z <= Z + 1; z++)
                    {
                        if (!(x == X && y == Y && z == Z))
                        {
                            if (!space.TryGetValue(CustomHash(x, y, z), out Cube cube))
                                neighbors.Add(CustomHash(x, y, z), new Cube(x, y, z, CubeState.Inactive));
                        }
                    }
                }
            }

            return neighbors;
        }

        public void UpdateState(Dictionary<int, Cube> space)
        {
            var activeNeighborsCount = FindExistingNeighbors(space).Count(c => c.State == CubeState.Active);

            if (State == CubeState.Active && activeNeighborsCount != 2 && activeNeighborsCount != 3)
                State = CubeState.Inactive;

            if (State == CubeState.Inactive && activeNeighborsCount == 3)
                State = CubeState.Active;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z} - {State})";
        }
    }
}
