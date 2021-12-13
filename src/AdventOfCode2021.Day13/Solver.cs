using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day13
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var paperFoldingInstructions = new PaperFoldingInstructions(input);

            paperFoldingInstructions.PerformFold(paperFoldingInstructions.FoldInstructions.First());

            return paperFoldingInstructions.CountVisiblePoints().ToString();
        }

        public string SolveDayStar2(string input)
        {
            var paperFoldingInstructions = new PaperFoldingInstructions(input);

            paperFoldingInstructions.PerformAllFolds();

            return paperFoldingInstructions.ToString();
        }

        public class PaperFoldingInstructions
        {
            public List<FoldInstruction> FoldInstructions { get; } = new();

            private readonly HashSet<Point> _points = new();

            public PaperFoldingInstructions(string input)
            {
                foreach(var line in input.SplitByNewLine())
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    else if (line.StartsWith("fold"))
                    {
                        FoldInstructions.Add(new FoldInstruction(line));
                    }
                    else
                    {
                        _points.Add(new Point(line));
                    }
                }
            }

            public int CountVisiblePoints() => _points.Count;

            public void PerformAllFolds()
            {
                foreach (var foldInstruction in FoldInstructions)
                {
                    PerformFold(foldInstruction);
                }
            }

            public void PerformFold(FoldInstruction foldInstruction)
            {
                if (foldInstruction.FoldDirection == FoldDirection.Up)
                {
                    // Remove point on the line
                    var pointsToMove = _points.Where(p => p.Y >= foldInstruction.Value);
                    foreach (var point in _points.ToList())
                    {
                        if (point.Y == foldInstruction.Value)
                        {
                            _points.Remove(point);
                        }
                        else if (point.Y > foldInstruction.Value)
                        {
                            _points.Remove(point);
                            point.Y = foldInstruction.Value + foldInstruction.Value - point.Y;
                            if (_points.Contains(point) == false)
                            {
                                _points.Add(point);
                            }
                        }
                    }
                }
                else if (foldInstruction.FoldDirection == FoldDirection.Left)
                {
                    var pointsToMove = _points.Where(p => p.X >= foldInstruction.Value);
                    foreach (var point in _points.ToList())
                    {
                        if (point.X == foldInstruction.Value)
                        {
                            _points.Remove(point);
                        }
                        else if (point.X > foldInstruction.Value)
                        {
                            _points.Remove(point);
                            point.X = foldInstruction.Value + foldInstruction.Value - point.X;
                            if (_points.Contains(point) == false)
                            {
                                _points.Add(point);
                            }
                        }
                    }
                }
            }

            public override string ToString()
            {
                var _pointGrid = GetGrid();

                StringBuilder sb = new();

                for (int y = 0; y < _pointGrid.GetLength(1); y++)
                {
                    if (y != 0)
                        sb.AppendLine();

                    for(int x = 0; x < _pointGrid.GetLength(0); x++)
                    {
                        Point? point = _pointGrid[x, y];
                        sb.Append(point == null ? "." : "#");
                    }
                }

                return sb.ToString();
            }

            private Point?[,] GetGrid()
            {
                var pointGrid = new Point?[_points.Max(p => p.X) + 1, _points.Max(p => p.Y) + 1];

                foreach (var point in _points)
                {
                    pointGrid[point.X, point.Y] = point;
                }

                return pointGrid;
            }
        }

        public enum FoldDirection
        {
            Up,
            Left
        }

        public class FoldInstruction
        {
            public FoldDirection FoldDirection { get; }

            public int Value { get; }

            public FoldInstruction(string input)
            {
                string direction = input.Substring(11, 1);
                string value = input.Substring(13);

                FoldDirection = direction switch
                {
                    "x" => FoldDirection.Left,
                    "y" => FoldDirection.Up,
                    _ => throw new ArgumentException(nameof(input), "Unknown fold direction " + direction)
                };

                Value = int.Parse(value);
            }
        }

        public class Point : IEquatable<Point?>
        {
            public int X { get; set; }

            public int Y { get; set; }

            public Point(string input)
            {
                var inputs = input.Split(",");
                X = int.Parse(inputs[0]);
                Y = int.Parse(inputs[1]);
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as Point);
            }

            public bool Equals(Point? other)
            {
                return other != null &&
                       X == other.X &&
                       Y == other.Y;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y);
            }
        }
    }
}
