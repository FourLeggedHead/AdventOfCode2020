using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using System.Text;

namespace DayTwenty.Model
{
    public class Puzzle
    {
        public int Size { get; set; }
        public List<Tile> Tiles { get; set; }

        public Puzzle(IEnumerable<Tile> tiles)
        {
            Size = (int)Math.Sqrt(tiles.Count());
            Tiles = tiles.ToList();
        }

        public IEnumerable<(string Edge, Side Side, int TileId)> ListAllEdges(IEnumerable<Tile> tiles)
        {
            var edges = new List<(string Edge, Side Side, int TileId)>();
            foreach (var tile in tiles)
            {
                foreach (var edge in tile.Edges)
                {
                    edges.Add((edge.Value, edge.Key, tile.Id));
                }
            }
            return edges.OrderBy(e => e.Edge);
        }

        public void Solve()
        {
            var queue = new Queue<Tile>(Tiles);
            var matched = new Queue<Tile>();

            var firstTile = queue.Dequeue();
            matched.Enqueue(firstTile);

            while (matched.Count > 0)
            {
                firstTile = matched.Dequeue();

                var visited = new HashSet<Tile>();

                while (queue.Count > visited.Count)
                {
                    var secondTile = queue.Dequeue();

                    var isMatching = false;

                    foreach (var permutation in secondTile.Permutations)
                    {
                        var edges = ListAllEdges(new Tile[] { firstTile, permutation });

                        var matchedEdges = edges
                        .Window(2)
                        .Where(w => w.Select(t => t.Edge).Distinct().Count() == 1)
                        .Where(w => w[0].Side == Matching(w[1].Side));

                        if (matchedEdges.Any())
                        {
                            if (matchedEdges.Count() != 1) throw new Exception("Houston, we have a problem!");

                            var match = matchedEdges.ElementAt(0);

                            firstTile.AttachTile(secondTile, match[0].Side);
                            secondTile.AttachTile(firstTile, match[1].Side);

                            secondTile.Pixels = permutation.Pixels;
                            secondTile.Edges = permutation.Edges;

                            isMatching = true;

                            break;
                        }
                    }

                    if (!isMatching)
                    {
                        queue.Enqueue(secondTile);
                        visited.Add(secondTile);
                    }
                    else
                    {
                        matched.Enqueue(secondTile);
                    }
                }
            }

            foreach (var tile in Tiles)
            {
                queue = new Queue<Tile>(Tiles.Where(t => t.Id != tile.Id).Where(t => !t.IsAttachedTo(tile)));

                var visited = new HashSet<Tile>();

                while (queue.Count > visited.Count)
                {
                    var secondTile = queue.Dequeue();

                    var edges = ListAllEdges(new Tile[] { tile, secondTile });

                    var matchedEdges = edges
                    .Window(2)
                    .Where(w => w.Select(t => t.Edge).Distinct().Count() == 1)
                    .Where(w => w[0].Side == Matching(w[1].Side));

                    if (matchedEdges.Any())
                    {
                        if (matchedEdges.Count() != 1) throw new Exception("Houston, we have a problem!");

                        var match = matchedEdges.ElementAt(0);

                        tile.AttachTile(secondTile, match[0].Side);
                        secondTile.AttachTile(tile, match[1].Side);
                    }
                    else
                    {
                        queue.Enqueue(secondTile);
                        visited.Add(secondTile);
                    }
                }
            }
        }

        public static Side Matching(Side side)
        {
            switch (side)
            {
                case Side.Top:
                    return Side.Bottom;
                case Side.Left:
                    return Side.Right;
                case Side.Bottom:
                    return Side.Top;
                case Side.Right:
                    return Side.Left;
                default:
                    return Side.Top;
            }
        }

        public Tile GetTopLeftCorner()
        {
            var topLeftCorners = Tiles
                     .Where(t => t.Bottom != null && t.Left == null && t.Right != null && t.Top == null)
                     .ToDictionary(t => t.Id);

            if (topLeftCorners.Count != 1) throw new Exception("Puzzle has no or more than one top left corner.");

            return topLeftCorners.ElementAt(0).Value;
        }

        public Tile GetTopRightCorner()
        {
            var topRightCorners = Tiles
                    .Where(t => t.Bottom != null && t.Left != null && t.Right == null && t.Top == null)
                    .ToDictionary(t => t.Id);

            if (topRightCorners.Count != 1) throw new Exception("Puzzle has no or more than one top right corner.");

            return topRightCorners.ElementAt(0).Value;
        }

        public Tile GetBottomLeftCorner()
        {
            var bottomLeftCorners = Tiles
                    .Where(t => t.Bottom == null && t.Left == null && t.Right != null && t.Top != null)
                    .ToDictionary(t => t.Id);

            if (bottomLeftCorners.Count != 1) throw new Exception("Puzzle has no or more than one bottom left corner.");

            return bottomLeftCorners.ElementAt(0).Value;
        }

        public Tile GetBottomRightCorner()
        {
            var bottomRightCorners = Tiles
                    .Where(t => t.Bottom == null && t.Left != null && t.Right == null && t.Top != null)
                    .ToDictionary(t => t.Id);

            if (bottomRightCorners.Count != 1) throw new Exception("Puzzle has no or more than one bottom rigth corner.");

            return bottomRightCorners.ElementAt(0).Value;
        }

        public void Print()
        {
            var mostLeftTile = GetTopLeftCorner();

            while (mostLeftTile != null)
            {
                var tile = mostLeftTile;

                var builders = new List<StringBuilder>();
                for (int i = 0; i < Tile.TILE_DIMENSION + 1; i++)
                {
                    builders.Add(new StringBuilder());
                }

                while (tile != null)
                {
                    for (int j = 0; j < Tile.TILE_DIMENSION; j++)
                    {
                        for (int i = 0; i < Tile.TILE_DIMENSION; i++)
                        {
                            if (tile.Pixels[i, j] == 1) builders[j].Append('#');
                            else builders[j].Append('.');
                        }
                        builders[j].Append(' ');
                    }
                    builders[Tile.TILE_DIMENSION].Append(tile.Id + new String(' ', Tile.TILE_DIMENSION - 4));

                    tile = tile.Right;
                }

                foreach (var builder in builders)
                {
                    Console.WriteLine(builder.ToString());
                }

                mostLeftTile = mostLeftTile.Bottom;
            }
        }
    }
}
