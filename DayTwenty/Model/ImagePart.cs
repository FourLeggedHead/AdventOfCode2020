using System;
using System.Collections.Generic;
using System.Text;

namespace DayTwenty.Model
{
    public class ImagePart
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<(int, int), byte> Pixels { get; set; }

        public ImagePart(Image image, (int x,int y) origin, int width, int height)
        {
            Width = width;
            Height = height;
            Pixels = new Dictionary<(int, int), byte>();

            ExtractImage(image, origin);
        }

        void ExtractImage(Image image, (int x, int y) origin)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Pixels.Add((i,j),image.Pixels[origin.x + i, origin.y + j]);
                }
            }
        }
    }
}
