using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day17
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var probeSimulation = new ProbeSimulation(input);

            return probeSimulation.GetHighestPoint().ToString();
        }

        public string SolveDayStar2(string input)
        {
            var probeSimulation = new ProbeSimulation(input);
            var relevantProbes = probeSimulation.GetRelevantProbes().OrderBy(o => o.InitialVelocity.X).ThenBy(o => o.InitialVelocity.Y); ;
            return relevantProbes.Count().ToString();
        }

        public class ProbeSimulation
        {
            public Rectangle TargetArea { get; set; }


            public ProbeSimulation(string input)
            {
                TargetArea = new Rectangle(input);
            }

            public int GetHighestPoint()
            {
                return (int)GetRelevantProbes().Max(p => p.MaxY);
            }

            public List<Probe> GetRelevantProbes()
            {
                BlockingCollection<Probe> probes = new();

                Parallel.For(0, 500, (x) =>
                {
                    Parallel.For(-500, 500, (y) =>
                    {
                        if (x == 0 && y == 0)
                            return;

                        var probe = new Probe(new Vector2(x, y));
                        while (TargetArea.HasPassed(probe) == false)
                        {
                            probe.SimulateStep();
                        }

                        if (TargetArea.Overlaps(probe))
                            probes.Add(probe);
                    });
                });

                return probes.ToList();
            }
        }

        public class Rectangle
        {
            public int X1 { get; set; }

            public int X2 { get; set; }

            public int Y1 { get; set; }

            public int Y2 { get; set; }

            public Rectangle(string input)
            {
                int xStart = input.IndexOf("x=") + 2;
                int xEnd = input.IndexOf(",", xStart);
                string xValuesString = input.Substring(xStart, xEnd - xStart);
                int[] xValues = xValuesString.Split("..").Select(o => int.Parse(o)).ToArray();
                X1 = xValues[0];
                X2 = xValues[1];

                int yStart = input.IndexOf("y=") + 2;
                int yEnd = input.Length;
                string yValuesStrings = input.Substring(yStart, yEnd - yStart);
                int[] yValues = yValuesStrings.Split("..").Select(o => int.Parse(o)).ToArray();
                Y1 = yValues[0];
                Y2 = yValues[1];
            }

            public bool Overlaps(Probe probe)
            {
                return probe.Positions.Any(v => Overlaps(v));
            }

            public bool HasPassed(Probe probe)
            {
                var vector = probe.Position;

                if (vector.X > X2)
                    return true;

                if (vector.Y < Y1)
                    return true;

                return false;
            }

            private bool Overlaps(Vector2 vector)
            {
                if (vector.X < X1 || vector.X > X2)
                    return false;

                if (vector.Y < Y1 || vector.Y > Y2)
                    return false;

                return true;
            }

            public override string ToString()
            {
                return $"x={X1}..{X2}, y={Y1}..{Y2}";
            }
        }

        public class Probe
        {
            public List<Vector2> Positions { get; } = new List<Vector2>();

            public Vector2 Position => Positions.Last();

            public Vector2 Velocity { get; set; }

            public Vector2 InitialVelocity { get; set; }

            public float MaxY { get; set; }

            public Probe(Vector2 velocity)
            {
                Positions.Add(Vector2.Zero);
                Velocity = velocity;
                InitialVelocity = velocity;
                MaxY = int.MinValue;
            }

            public void SimulateStep()
            {
                var newPosition = new Vector2(Position.X + Velocity.X, Position.Y + Velocity.Y);

                var vNewX = Velocity.X;
                if (Velocity.X > 0)
                    vNewX--;
                else if (Velocity.X < 0)
                    vNewX++;

                if (newPosition.Y > MaxY)
                    MaxY = newPosition.Y;

                Positions.Add(newPosition);

                var newY = Velocity.Y - 1;
                Velocity = new Vector2(vNewX, newY);
            }

            public override string ToString()
            {
                return $"{InitialVelocity.X} {InitialVelocity.Y}";
            }
        }
    }
}
