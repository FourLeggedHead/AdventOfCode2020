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
        public int W { get; set; }
        public CubeState State { get; set; }

        public Cube(int x, int y, int z, int w, CubeState state)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            State = state;
        }

        public Cube(int x, int y, int z, int w, char state)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;

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
            W = cube.W;
            State = cube.State;
        }

        public long CustomHash(int x, int y, int z, int w)
        {
            var seed = 10009;
            var factor = 9176;
            var hash = seed * factor + x.GetHashCode();
            hash = hash * factor + y.GetHashCode();
            hash = hash * factor + z.GetHashCode();
            hash = hash * factor + w.GetHashCode();
            return hash;
        }

        public List<Cube> FindExistingNeighbors(Dictionary<long, Cube> space)
        {
            var neighbors = new List<Cube>();

            for (int x = X - 1; x <= X + 1; x++)
            {
                for (int y = Y - 1; y <= Y + 1; y++)
                {
                    for (int z = Z - 1; z <= Z + 1; z++)
                    {
                        for (int w = W - 1; w <= W + 1; w++)
                        {
                            if (!(x == X && y == Y && z == Z && w == W))
                            {
                                if (space.TryGetValue(CustomHash(x, y, z, w), out Cube cube))
                                    neighbors.Add(cube);
                            } 
                        }
                    }
                }
            }

            return neighbors;
        }

        public Dictionary<long, Cube> FindMissingNeighbors(Dictionary<long, Cube> space)
        {
            var neighbors = new Dictionary<long, Cube>();

            for (int x = X - 1; x <= X + 1; x++)
            {
                for (int y = Y - 1; y <= Y + 1; y++)
                {
                    for (int z = Z - 1; z <= Z + 1; z++)
                    {
                        if (!(x == X && y == Y && z == Z))
                        {
                            for (int w = W - 1; w <= W + 1; w++)
                            {
                                if (!space.TryGetValue(CustomHash(x, y, z, w), out Cube cube))
                                    neighbors.Add(CustomHash(x, y, z, w), new Cube(x, y, z, w, CubeState.Inactive)); 
                            }
                        }
                    }
                }
            }

            return neighbors;
        }

        public void UpdateState(Dictionary<long, Cube> space)
        {
            var activeNeighborsCount = FindExistingNeighbors(space).Count(c => c.State == CubeState.Active);

            if (State == CubeState.Active && activeNeighborsCount != 2 && activeNeighborsCount != 3)
                State = CubeState.Inactive;

            if (State == CubeState.Inactive && activeNeighborsCount == 3)
                State = CubeState.Active;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z}, {W} - {State})";
        }
    }
}
