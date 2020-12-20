using FourLeggedHead.IO;
using System;
using System.Linq;
using MoreLinq;
using DayTwenty.Model;
using System.Collections.Generic;

namespace DayTwenty
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Advent of Code 2020 - Day Nineteen");

            try
            {
                var input = FileReader.ReadAllLines(@"Resources/input.txt").ToList();

                var tiles = input
                    .Segment(string.IsNullOrWhiteSpace)
                    .Select(t => new Tile(t.Where(l => l != "").ToArray()))
                    .ToDictionary(t => t.Id);

                var edges = new List<(string Edge, Side Side, int TileId)>();
                foreach (var tile in tiles.Values)
                {
                    foreach (var edge in tile.Edges)
                    {
                        edges.Add((edge.Value, edge.Key, tile.Id));
                    }
                }
                edges = edges.OrderBy(e => e.Edge).ToList();

                var matchedEdges = edges
                    .Window(2)
                    .Where(w => w.Select(t => t.Edge).Distinct().Count() == 1);

                // Attach fully matched tiles

                var matchedEdgesAndSides = matchedEdges
                    .Where(w => w[0].Side == Matching(w[1].Side));
                foreach (var pair in matchedEdgesAndSides)
                {
                    var firstTile = tiles[pair[0].TileId];
                    var secondTile = tiles[pair[1].TileId];

                    firstTile.AttachTile(secondTile, pair[0].Side);
                    secondTile.AttachTile(firstTile, pair[1].Side);
                }

                // Attach partially matched tiles, with transformation

                var matchedEdgesNotSides = matchedEdges
                    .Where(w => w[0].Side != Matching(w[1].Side));

                var firstTileAttached = matchedEdgesNotSides.Where(w => tiles[w[0].TileId].IsAttached());
                foreach (var pair in firstTileAttached)
                {
                    var firstTile = tiles[pair[0].TileId];
                    var secondTile = tiles[pair[1].TileId];
                }

                // Check corners and unattached tiles

                var topLeftCorners = tiles.Values
                    .Where(t => t.Bottom != null && t.Left == null && t.Right != null && t.Top == null)
                    .ToDictionary(t => t.Id);

                var topRightCorners = tiles.Values
                    .Where(t => t.Bottom != null && t.Left != null && t.Right == null && t.Top == null)
                    .ToDictionary(t => t.Id);

                var bottomLeftCorners = tiles.Values
                    .Where(t => t.Bottom == null && t.Left == null && t.Right != null && t.Top != null)
                    .ToDictionary(t => t.Id);

                var bottomRightCorners = tiles.Values
                    .Where(t => t.Bottom == null && t.Left != null && t.Right == null && t.Top != null)
                    .ToDictionary(t => t.Id);

                var unattachedTiles = tiles.Values
                    .Where(t => t.Bottom == null && t.Left == null && t.Right == null && t.Top == null)
                    .ToDictionary(t => t.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
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
                    return Side.Undefined;
            }
        }
    }
}
