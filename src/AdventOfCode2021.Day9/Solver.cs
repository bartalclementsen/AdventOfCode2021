using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day9
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var heightMap = new HeightMap(input);
            var lowPoints = heightMap.GetLowPoints();
            var totalRiskLevel = lowPoints.Sum(o => o.RiskLevel);

            var basins = heightMap.GetBasinsFromLowPoints(lowPoints);

            return totalRiskLevel.ToString();
        }

        public string SolveDayStar2(string input)
        {
            var heightMap = new HeightMap(input);
            var lowPoints = heightMap.GetLowPoints();
            var basins = heightMap.GetBasinsFromLowPoints(lowPoints);
            

            return basins.Take(3).Select(o => o.Points.Count).Aggregate((a,b) => a*b).ToString();
        }

        public class Basin
        {
            public Point LowPoint { get; }

            public HashSet<Point> Points { get; } = new HashSet<Point>();

            public Basin(Point lowPoint)
            {
                LowPoint = lowPoint;
            }

        }

        public class Point : IEquatable<Point?>
        {
            public int X { get; }

            public int Y { get; }

            public int Height { get; }

            public int RiskLevel => Height + 1;

            public Point(int x, int y, int height)
            {
                X = x;
                Y = y;
                Height = height;
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as Point);
            }

            public bool Equals(Point? other)
            {
                return other != null &&
                       X == other.X &&
                       Y == other.Y &&
                       Height == other.Height;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y, Height);
            }

            public override string ToString()
            {
                return $"{Height} ({X},{Y})";
            }
        }

        public class HeightMap
        {
            private readonly int[,] map;
            private readonly int width;
            private readonly int height;

            public HeightMap(string input)
            {
                var lines = input.SplitByNewLine();
                width = lines.ElementAt(0).Length;
                height = lines.Count();

                map = new int[width, height];
                for (var i = 0; i < height; i++)
                {
                    var line = lines.ElementAt(i).ToCharArray();

                    for (var j = 0; j < width; j++)
                    {
                        map[j, i] = int.Parse(line[j].ToString());
                    }
                }
            }

            public List<Point> GetLowPoints()
            {
                List<Point> lowPoints = new();

                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        var point = new Point(j, i, map[j, i]);
                        if (IsLowPoint(point))
                        {
                            map[j, i] = map[j, i];
                            lowPoints.Add(point);
                        }
                    }
                }

                return lowPoints;
            }

            public bool IsLowPoint(Point point)
            {
                int x = point.X;
                int y = point.Y;
                int value = point.Height;

                int? left = x > 0 ? map[x - 1, y] : null;
                int? right = x + 1 < width ? map[x + 1, y] : null;
                int? up = y > 0 ? map[x, y - 1] : null;
                int? down = y + 1 < height ? map[x, y + 1] : null;

                if (left <= value)
                    return false;
                if (right <= value)
                    return false;
                if (up <= value)
                    return false;
                if (down <= value)
                    return false;

                return true;
            }

            private Point GetPoint(int x, int y)
            {
                return new Point(x, y, map[x, y]);
            }

            private Point? TryGetLeftPointFrom(Point point)
            {
                return TryGetPoint(point.X - 1, point.Y);
            }

            private Point? TryGetRightPointFrom(Point point)
            {
                return TryGetPoint(point.X + 1, point.Y);
            }

            private Point? TryGetUpPointFrom(Point point)
            {
                return TryGetPoint(point.X, point.Y - 1);
            }

            private Point? TryGetDownPointFrom(Point point)
            {
                return TryGetPoint(point.X, point.Y + 1);
            }

            private Point? TryGetPoint(int x, int y)
            {
                if (x > -1 && x < width && y > -1 && y < height)
                {
                    return GetPoint(x, y);
                }

                return null;
            }


            internal List<Basin> GetBasinsFromLowPoints(List<Point> lowPoints)
            {
                return lowPoints.Select(p => GetBasinFromLowPoint(p)).OrderByDescending(o => o.Points.Count).ToList();
            }

            private Basin GetBasinFromLowPoint(Point lowPoint)
            {
                Basin basin = new(lowPoint);

                Queue<Point> pointsToCheck = new Queue<Point>();
                pointsToCheck.Enqueue(lowPoint);
                while (pointsToCheck.Any())
                {
                    var point = pointsToCheck.Dequeue();
                    basin.Points.Add(point);

                    List<Point?> possiblePoints = new List<Point?>()
                    {
                        TryGetLeftPointFrom(point),
                        TryGetRightPointFrom(point),
                        TryGetUpPointFrom(point),
                        TryGetDownPointFrom(point)
                    };

                    foreach(var possiblePoint in possiblePoints)
                    {
                        if (possiblePoint != null && possiblePoint.Height != 9 && basin.Points.Contains(possiblePoint) == false)
                        {
                            pointsToCheck.Enqueue(possiblePoint);
                        }
                    }
                }

                return basin;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                for (var i = 0; i < height; i++)
                {
                    if (i != 0)
                        sb.AppendLine();

                    for (var j = 0; j < width; j++)
                    {
                        sb.Append(map[j, i]);
                    }
                }

                return sb.ToString();
            }
        }
    }
}
