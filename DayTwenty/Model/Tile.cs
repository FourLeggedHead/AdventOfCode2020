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
        Right
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
        public List<Tile> Permutations { get; set; }


        public Tile(string[] tile)
        {
            if (tile.Length != TILE_DIMENSION + 1) throw new ArgumentException("Input for tile is incorrect");

            var matchTitle = Regex.Match(tile[0], @"Tile (?<Id>\d+):");
            if (!matchTitle.Success) throw new ArgumentException("Input for tile is incorrect");
            Id = int.Parse(matchTitle.Groups["Id"].Value);

            Pixels = new byte[TILE_DIMENSION, TILE_DIMENSION];
            for (int j = 0; j < TILE_DIMENSION; j++)
            {
                var line = tile[j + 1].ToCharArray();

                for (int i = 0; i < TILE_DIMENSION; i++)
                {
                    if (line[i] == '#') Pixels[i, j] = 1;
                    else Pixels[i, j] = 0;
                }
            }

            FindEdges();

            ListAllPermutations();
        }

        public Tile(int id, byte[,] pixels)
        {
            Id = id;
            Pixels = pixels;

            FindEdges();
        }

        void FindEdges()
        {
            Edges = new Dictionary<Side, string>();

            var builder = new StringBuilder();
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                builder.Append(Pixels[i, 0].ToString());
            }
            Edges[Side.Top] = builder.ToString();

            builder = new StringBuilder();
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                builder.Append(Pixels[i, TILE_DIMENSION - 1].ToString());
            }
            Edges[Side.Bottom] = builder.ToString();

            builder = new StringBuilder();
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                builder.Append(Pixels[0, i].ToString());
            }
            Edges[Side.Left] = builder.ToString();

            builder = new StringBuilder();
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                builder.Append(Pixels[TILE_DIMENSION - 1, i].ToString());
            }
            Edges[Side.Right] = builder.ToString();
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

        public Tile TurnLeft(Tile from)
        {
            var pixels = new byte[TILE_DIMENSION, TILE_DIMENSION];
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                for (int j = 0; j < TILE_DIMENSION; j++)
                {
                    pixels[i, j] = from.Pixels[i, j];
                }
            }

            for (int x = 0; x < TILE_DIMENSION / 2; x++)
            {
                // Consider elements 
                // in group of 4 in 
                // current square 
                for (int y = x; y < TILE_DIMENSION - x - 1; y++)
                {
                    // store current cell 
                    // in temp variable 
                    var temp = pixels[x, y];

                    // move values from 
                    // right to top 
                    pixels[x, y] = pixels[y, TILE_DIMENSION - 1 - x];

                    // move values from 
                    // bottom to right 
                    pixels[y, TILE_DIMENSION - 1 - x] = pixels[TILE_DIMENSION - 1 - x,TILE_DIMENSION - 1 - y];

                    // move values from 
                    // left to bottom 
                    pixels[TILE_DIMENSION - 1 - x, TILE_DIMENSION - 1 - y] = pixels[TILE_DIMENSION - 1 - y, x];

                    // assign temp to left 
                    pixels[TILE_DIMENSION - 1 - y, x] = temp;
                }
            }

            return new Tile(from.Id, pixels);
        }

        public Tile FlipHorizontally(Tile from)
        {
            var pixels = new byte[TILE_DIMENSION, TILE_DIMENSION];
            for (int i = 0; i < TILE_DIMENSION; i++)
            {
                for (int j = 0; j < TILE_DIMENSION; j++)
                {
                    pixels[i, j] = from.Pixels[i, TILE_DIMENSION - 1 - j];
                }
            }

            return new Tile(from.Id, pixels);
        }

        void ListAllPermutations()
        {
            Permutations = new List<Tile>();

            Permutations.Add(new Tile(Id, Pixels));

            var tile = TurnLeft(this);
            Permutations.Add(tile);
            tile = TurnLeft(tile);
            Permutations.Add(tile);
            tile = TurnLeft(tile);
            Permutations.Add(tile);

            tile = FlipHorizontally(this);
            Permutations.Add(tile);

            tile = TurnLeft(tile);
            Permutations.Add(tile);
            tile = TurnLeft(tile);
            Permutations.Add(tile);
            tile = TurnLeft(tile);
            Permutations.Add(tile);
        }

        public bool IsAttachedTo(Tile tile)
        {
            if (Top != null && Top.Id == tile.Id) return true;
            if (Bottom != null && Bottom.Id == tile.Id) return true;
            if (Left != null && Left.Id == tile.Id) return true;
            if (Right != null && Right.Id == tile.Id) return true;
            return false;
        }
    }
}
