using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace DayTwenty.Model
{
    public enum Side
    {
        Top,
        Left,
        Bottom,
        Right,
        Undefined
    }

    public class Tile
    {
        public const int TILE_DIMENSION = 10;

        public int Id { get; set; }
        public byte[,] Pixels { get; set; }
        public Dictionary<Side, string> Edges { get; set; }
        public Tile Top { get; set; }
        public Tile Left { get; set; }
        public Tile Bottom { get; set; }
        public Tile Right { get; set; }

        public Tile(string[] tile)
        {
            if (tile.Length != TILE_DIMENSION + 1) throw new ArgumentException("Input for tile is incorrect");

            var matchTitle = Regex.Match(tile[0], @"Tile (?<Id>\d+):");
            if (!matchTitle.Success) throw new ArgumentException("Input for tile is incorrect");
            Id = int.Parse(matchTitle.Groups["Id"].Value);

            Pixels = new byte[TILE_DIMENSION, TILE_DIMENSION];
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                var line = tile[i + 1].ToCharArray();

                for (int j = 0; j < TILE_DIMENSION; j++)
                {
                    if (line[j] == '#') Pixels[i, j] = 1;
                    else Pixels[i, j] = 0;
                }
            }

            FindEdges();
        }

        void FindEdges()
        {
            Edges = new Dictionary<Side, string>();

            var builder = new StringBuilder();
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                builder.Append(Pixels[0, i].ToString());
            }
            Edges[Side.Top] = builder.ToString();

            builder = new StringBuilder();
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                builder.Append(Pixels[TILE_DIMENSION - 1, i].ToString());
            }
            Edges[Side.Bottom] = builder.ToString();

            builder = new StringBuilder();
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                builder.Append(Pixels[i, 0].ToString());
            }
            Edges[Side.Left] = builder.ToString();

            builder = new StringBuilder();
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                builder.Append(Pixels[i, TILE_DIMENSION - 1].ToString());
            }
            Edges[Side.Right] = builder.ToString();
        }

        Side Matching( Side side)
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

        public void AttachTile(Tile tile, Side side)
        {
            switch (side)
            {
                case Side.Top:
                    Top = tile;
                    break;
                case Side.Left:
                    Left = tile;
                    break;
                case Side.Bottom:
                    Bottom = tile;
                    break;
                case Side.Right:
                    Right = tile;
                    break;
                default:
                    break;
            }
        }

        public bool IsAttached()
        {
            return !(Top == null && Bottom == null && Left == null && Right == null);
        }
    }
}
