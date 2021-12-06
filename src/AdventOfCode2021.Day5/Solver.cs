using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day5
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            World world = new(input);

            var v = world.ToString();

            return world.GetTheNumberOfPointsWhereAtLeastTwoLinesOverlap().ToString();
        }

        public string SolveDayStar2(string input)
        {
            World world = new(input, onlyHorizontalAndVertical: false);

            var v = world.ToString();

            return world.GetTheNumberOfPointsWhereAtLeastTwoLinesOverlap().ToString();
        }

        private class World
        {
            public List<LineSegment> LineSegments { get; }

            public World(string input, bool onlyHorizontalAndVertical = true)
            {
                LineSegments = input.SplitAndConvertByNewLine(l => new LineSegment(l));
                if(onlyHorizontalAndVertical)
                    LineSegments = LineSegments.Where(ls => ls.IsHorizontalOrVertical).ToList();
            }

            public int GetTheNumberOfPointsWhereAtLeastTwoLinesOverlap()
            {
                BlockingCollection<Point> overlappingPoints = new BlockingCollection<Point>();
                
                var maxY = GetMaxY() + 1;
                var maxX = GetMaxX() + 1;

                Parallel.For(0, maxY, (y) =>
                {
                    Parallel.For(0, maxX, (x) =>
                    {
                        Point point = new Point(x, y);
                        var overlappingLines = LineSegments.Where(ls => ls.IsPointOnSegment(point));

                        if (overlappingLines.Count() > 1)
                        {
                            overlappingPoints.Add(point);
                        }
                    });
                });

                return overlappingPoints.Count();
            }

            public override string ToString()
            {
                string[,] array = new string[GetMaxX() + 1, GetMaxY() + 1];

                var maxY = GetMaxY() + 1;
                var maxX = GetMaxX() + 1;

                Parallel.For(0, maxY, (y) =>
                {
                    Parallel.For(0, maxX, (x) =>
                    {
                        Point point = new Point(x, y);
                        var overlappingLines = LineSegments.Where(ls => ls.IsPointOnSegment(point));

                        if (overlappingLines.Any())
                        {
                            array[x, y] = overlappingLines.Count().ToString();
                        }
                        else
                        {
                            array[x, y] = ".";
                        }
                    });
                });

                StringBuilder sb = new StringBuilder();

                for (int y = 0; y <= GetMaxY(); y++)
                {
                    for (int x = 0; x <= GetMaxX(); x++)
                    {
                        sb.Append(array[x, y]);
                    }

                    sb.AppendLine();
                }

                return sb.ToString();
            }

            private int GetMaxX() => LineSegments.Max(ls => ls.GetMaxX());

            private int GetMaxY() => LineSegments.Max(ls => ls.GetMaxY());
        }

        private struct LineSegment
        {
            public Point Point1 { get; }

            public Point Point2 { get; }

            public bool IsHorizontalOrVertical => Point1.X == Point2.X || Point1.Y == Point2.Y;

            public LineSegment(string input)
            {
                var points = input.Split(" -> ");
                Point1 = new Point(points[0]);
                Point2 = new Point(points[1]);
            }

            public int GetMaxX()
            {
                return (int)Math.Max(Point1.X, Point2.X);
            }

            public int GetMaxY()
            {
                return (int)Math.Max(Point1.Y, Point2.Y);
            }

            public bool IsPointOnSegment(Point point)
            {
                float crossproduct = (point.Y - Point1.Y) * (Point2.X - Point1.X) - (point.X - Point1.X) * (Point2.Y - Point1.Y);
                if (Math.Abs(crossproduct) > 0)
                    return false;

                float dotproduct = (point.X - Point1.X) * (Point2.X - Point1.X) + (point.Y - Point1.Y) * (Point2.Y - Point1.Y);
                if (dotproduct < 0)
                    return false;

                float squaredlength = (Point2.X - Point1.X) * (Point2.X - Point1.X) + (Point2.Y - Point1.Y) * (Point2.Y - Point1.Y);
                if (dotproduct > squaredlength)
                    return false;

                return true;
            }

            public override string ToString()
            {
                return $"{Point1} -> {Point2}";
            }
        }

        private struct Point
        {
            public float X { get; }

            public float Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Point(string input)
            {
                var coordinates = input.Split(",");
                X = int.Parse(coordinates[0]);
                Y = int.Parse(coordinates[1]);
            }

            public override string ToString()
            {
                return $"{X},{Y}";
            }
        }
    }
}
