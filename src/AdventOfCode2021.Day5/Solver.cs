using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day5
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            World world = new World(input);

            return world.GetTheNumberOfPointsWhereAtLeastTwoLinesOverlap().ToString();
        }

        public string SolveDayStar2(string input)
        {
            World world = new World(input, onlyHorizontalAndVertical: false);

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

            public int GetMaxX() => LineSegments.Max(ls => ls.GetMaxX());

            public int GetMaxY() => LineSegments.Max(ls => ls.GetMaxY());

            internal int GetTheNumberOfPointsWhereAtLeastTwoLinesOverlap()
            {
                List<Point> overlappingPoints = new List<Point>();

                for (int y = 0; y <= GetMaxY(); y++)
                {
                    for (int x = 0; x <= GetMaxY(); x++)
                    {
                        Point point = new Point(x, y);
                        var overlappingLines = LineSegments.Where(ls => ls.IsPointOnSegment(point));

                        if (overlappingLines.Count() > 1)
                        {
                            overlappingPoints.Add(point);
                        }
                    }
                }

                return overlappingPoints.Count();
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                for (int y = 0; y <= GetMaxY(); y++)
                {
                    for (int x = 0; x <= GetMaxY(); x++)
                    {
                        Point point = new Point(x, y);
                        var overlappingLines = LineSegments.Where(ls => ls.IsPointOnSegment(point));

                        if (overlappingLines.Any())
                        {
                            sb.Append(overlappingLines.Count());
                        }
                        else
                        {
                            sb.Append(".");
                        }
                    }

                    sb.AppendLine();
                }

                return sb.ToString();
            }
        }

        private class LineSegment
        {
            public Point Point1 { get; }

            public Point Point2 { get; }

            public double A { get; }

            public double B { get; }

            public bool IsHorizontalOrVertical => Point1.X == Point2.X || Point1.Y == Point2.Y;

            public LineSegment(Point point1, Point point2)
            {
                Point1 = point1;
                Point2 = point2;
                if (Point2.X - Point1.X == 0)
                    A = 0;
                else
                    A = (Point2.Y - Point1.Y) / (Point2.X - Point1.X);
                B = Point1.Y - (A * Point1.X);
            }

            public LineSegment(string input)
            {
                var points = input.Split(" -> ");
                Point1 = new Point(points[0]);
                Point2 = new Point(points[1]);
                if (Point2.X - Point1.X == 0)
                    A = 1;
                else
                    A = (Point2.Y - Point1.Y) / (Point2.X - Point1.X);
                B = Point1.Y - (A * Point1.X);
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
                var crossproduct = (point.Y - Point1.Y) * (Point2.X - Point1.X) - (point.X - Point1.X) * (Point2.Y - Point1.Y);
                if (Math.Abs(crossproduct) > 0)
                    return false;

                var dotproduct = (point.X - Point1.X) * (Point2.X - Point1.X) + (point.Y - Point1.Y) * (Point2.Y - Point1.Y);
                if (dotproduct < 0)
                    return false;

                var squaredlengthba = (Point2.X - Point1.X) * (Point2.X - Point1.X) + (Point2.Y - Point1.Y) * (Point2.Y - Point1.Y);
                if (dotproduct > squaredlengthba)
                    return false;

                return true;
            }

            public override string ToString()
            {
                return $"{Point1} -> {Point2}";
            }
        }

        private class Point
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
