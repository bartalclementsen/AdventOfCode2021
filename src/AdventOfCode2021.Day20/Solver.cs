using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode2021.Day20
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {

            var imageOptimizer = new ImageOptimizer(input);

            var s = imageOptimizer.ToString();

            imageOptimizer.RunOptimization();

            s = imageOptimizer.ToString();

            imageOptimizer.RunOptimization();

            s = imageOptimizer.ToString();

            return imageOptimizer.LitPixelCount().ToString();
        }

        public string SolveDayStar2(string input)
        {
            var imageOptimizer = new ImageOptimizer(input);

            var s = imageOptimizer.ToString();

            for(int i = 0; i < 50; i++)
            {
                imageOptimizer.RunOptimization();
            }

            return imageOptimizer.LitPixelCount().ToString();
        }

        public class ImageOptimizer
        {
            public ReplacementPixels ReplacementPixels { get; }
            
            public Image Image { get; private set; }

            public ImageOptimizer(string input)
            {
                var lines = input.SplitByNewLine();
                ReplacementPixels = new ReplacementPixels(lines.ElementAt(0));

                Image = new Image(lines.Skip(2));

            }

            public int LitPixelCount()
            {
                return Image.GetLitPixels().Count();
            }

            internal void RunOptimization()
            {
                Image optimizedImage;
                
                if(ReplacementPixels.DoesFlip)
                    optimizedImage = new Image(Image.Width + 2, Image.Height + 2, Image.IsEmptyOn == false);
                else
                    optimizedImage = new Image(Image.Width + 2, Image.Height + 2, false);
                

                for (int y = -1; y < Image.Height + 1; y++)
                {
                    for (int x = -1; x < Image.Width + 1; x++)
                    {
                        int index = Image.CalculateOptimizationIndex(x, y);
                        char foundOptimizedPixel = ReplacementPixels.Get(index);
                        optimizedImage.SetPixel(x + 1, y + 1, foundOptimizedPixel);
                    }
                }

                Image = optimizedImage;
            }

            public override string ToString()
            {
                return $@"{ReplacementPixels}

{Image}";
            }
        }

        public class ReplacementPixels
        {
            public char[] Pixels { get; }

            public bool DoesFlip => Pixels[0] == '#';

            public ReplacementPixels(string input)
            {
                Pixels = input.ToCharArray();
            }

            public override string ToString()
            {
                return string.Join("", Pixels);
            }

            internal char Get(int index)
            {
                return Pixels[index];
            }
        }

        public class Image
        {
            public bool IsEmptyOn = false;

            private char[,] _image;
            private List<Vector2> litPixels = new List<Vector2>();

            public int Width => _image.GetLength(1);

            public int Height => _image.GetLength(0);

            public Image(int width, int height, bool isEmptyOn)
            {
                _image = new char[width, height];
                for (int y = 0; y < width; y++)
                {
                    for (int x = 0; x < height; x++)
                    {
                        _image[x, y] = isEmptyOn ? '#' : '.';
                    }
                }

                IsEmptyOn = isEmptyOn;
            }

            public void SetPixel(int x, int y, char c)
            {
                _image[x, y] = c;

                if (_image[x, y] == '#')
                    litPixels.Add(new Vector2(x, y));
            }

            public Image(IEnumerable<string> lines)
            {
                int yMax = lines.Count();
                int xMax = lines.First().Length;

                _image = new char[xMax, yMax];

                for (int y = 0; y < yMax; y++)
                {
                    var line = lines.ElementAt(y);

                    for (int x = 0; x < xMax; x++)
                    {
                        _image[x, y] = line.ElementAt(x);

                        if (_image[x, y] == '#')
                            litPixels.Add(new Vector2(x, y));
                    }
                }
            }

            public IEnumerable<Vector2> GetLitPixels() => litPixels.ToList();

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                for (int y = 0; y < _image.GetLength(1); y++)
                {
                    if (y != 0)
                        sb.AppendLine();

                    for (int x = 0; x < _image.GetLength(0); x++)
                    {
                        sb.Append(_image[x, y]);
                    }
                }

                return sb.ToString();
            }

            public int CalculateOptimizationIndex(int x, int y)
            {
                List<bool> binaryNumber = new List<bool>();
                for (int tempY = y - 1; tempY <= y + 1; tempY++)
                {
                    for (int tempX = x - 1; tempX <= x + 1; tempX++)
                    {
                        if (tempX < 0 || tempX > Width - 1)
                            binaryNumber.Add(IsEmptyOn);
                        else if (tempY < 0 || tempY > Height - 1)
                            binaryNumber.Add(IsEmptyOn);
                        else
                            binaryNumber.Add(_image[tempX, tempY] == '#');
                    }
                }

                return ToInt(binaryNumber);
            }

            private static int ToInt(List<bool> bits)
            {
                int value = 0;
                int count = bits.Count;
                for (int i = 0; i < count; i++)
                {
                    if (bits[count - i - 1])
                        value += Convert.ToInt32(Math.Pow(2, i));
                }
                return value;
            }
        }
    }
}
