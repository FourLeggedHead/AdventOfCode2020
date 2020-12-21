using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DayTwenty.Model
{
    public class Image
    {
        public int Size { get; set; }
        public byte[,] Pixels { get; set; }
        public List<Image> Permutations { get; set; }

        public Image(byte[,] pixels)
        {
            Size = (int)Math.Sqrt(pixels.Length);
            Pixels = pixels;
        }

        public Image(Puzzle puzzle)
        {
            Size = (int)Math.Sqrt(puzzle.Tiles.Count) * (Tile.TILE_DIMENSION - 2);
            Pixels = new byte[Size, Size];
            BuildImage(puzzle);
            ListAllPermutations();
        }

        public void FindMonsters()
        {
            var monster = new Monster(new string[]
                {
                    "                  # ",
                    "#    ##    ##    ###",
                    " #  #  #  #  #  #   "
                }
            );

            for (int i = 0; i < Permutations.Count; i++)
            {
                var containsMonsters = Permutations[i].ContainsMonster(monster, out int roughness);

                if (containsMonsters) Permutations[i].PrintImage();

                var truc = 0;
                for (int x = 0; x < Size; x++)
                {
                    for (int y = 0; y < Size; y++)
                    {
                        if (Pixels[x, y] == 1) truc++;
                    }
                }

                Console.WriteLine($"Permutation; {i} contains monster: {containsMonsters} and roughness is: {truc - roughness}");
            }
        }

        bool ContainsMonster(Monster monster, out int monsterPixelsCount)
        {
            var containsMoonster = false;
            var monsterPixels = new List<(int, int)>();

            for (int i = 0; i < Size - monster.Width; i++)
            {
                for (int j = 0; j < Size - monster.Height; j++)
                {
                    var imagePart = new ImagePart(this, (i, j), monster.Width, monster.Height);

                    if (imagePart.Pixels.Where(p => monster.Signature().Contains(p.Key)).All(p => p.Value == 1))
                    {
                        foreach (var position in monster.Signature())
                        {
                            if (!monsterPixels.Contains(position)) monsterPixels.Add((i + position.Item1, j + position.Item2));
                        }

                        containsMoonster = true;
                    }
                }
            }

            monsterPixelsCount = monsterPixels.Count;

            return containsMoonster;
        }

        void BuildImage(Puzzle puzzle)
        {
            var tileSize = Tile.TILE_DIMENSION - 2;

            var mostLeftTile = puzzle.GetTopLeftCorner();
            var y = 0;

            while (mostLeftTile != null)
            {
                var tile = mostLeftTile;
                var x = 0;

                while (tile != null)
                {
                    for (int i = 0; i < tileSize; i++)
                    {
                        for (int j = 0; j < tileSize; j++)
                        {
                            Pixels[x * tileSize + i, y * tileSize + j] = tile.Pixels[i + 1, j + 1];
                        }
                    }

                    tile = tile.Right;
                    x++;
                }

                mostLeftTile = mostLeftTile.Bottom;
                y++;
            }
        }

        void PrintImage()
        {
            for (int j = 0; j < Size; j++)
            {
                var line = new StringBuilder();
                for (int i = 0; i < Size; i++)
                {
                    if (Pixels[i, j] == 1) line.Append('#');
                    else line.Append('.');
                }
                Console.WriteLine(line.ToString());
            }
            Console.WriteLine(new String('_', Size));
        }

        public Image TurnLeft(Image from)
        {
            var pixels = new byte[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    pixels[i, j] = from.Pixels[i, j];
                }
            }

            for (int x = 0; x < Size / 2; x++)
            {
                for (int y = x; y < Size - x - 1; y++)
                {
                    var temp = pixels[x, y];
                    pixels[x, y] = pixels[y, Size - 1 - x];
                    pixels[y, Size - 1 - x] = pixels[Size - 1 - x, Size - 1 - y];
                    pixels[Size - 1 - x, Size - 1 - y] = pixels[Size - 1 - y, x];
                    pixels[Size - 1 - y, x] = temp;
                }
            }

            return new Image(pixels);
        }

        public Image FlipHorizontally(Image from)
        {
            var pixels = new byte[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    pixels[i, j] = from.Pixels[i, Size - 1 - j];
                }
            }

            return new Image(pixels);
        }

        void ListAllPermutations()
        {
            Permutations = new List<Image>();

            Permutations.Add(new Image(Pixels));

            var Image = TurnLeft(this);
            Permutations.Add(Image);
            Image = TurnLeft(Image);
            Permutations.Add(Image);
            Image = TurnLeft(Image);
            Permutations.Add(Image);

            Image = FlipHorizontally(this);
            Permutations.Add(Image);

            Image = TurnLeft(Image);
            Permutations.Add(Image);
            Image = TurnLeft(Image);
            Permutations.Add(Image);
            Image = TurnLeft(Image);
            Permutations.Add(Image);
        }
    }
}
