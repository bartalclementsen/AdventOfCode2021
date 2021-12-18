using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode2021.Day17
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var probeSimulation = new ProbeSimulation(input);

            return "";
        }

        public string SolveDayStar2(string input)
        {
            return "";
        }

        public class ProbeSimulation
        {
            public Rectangle TargetArea { get; set; }

            public Probe Probe { get; set; }

            public ProbeSimulation(string input)
            {
                TargetArea = new Rectangle(input);
                Probe = new Probe();
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                for (int y = 0; y >= TargetArea.Y1; y--)
                {
                    if (y != 0)
                        sb.AppendLine();

                    for (int x = 0; x <= TargetArea.X2; x++)
                    {
                        Vector2 v = new Vector2(x, y);
                        if (TargetArea.Overlaps(v))
                            sb.Append("T");
                        else if (Probe.Position == v)
                            sb.Append("#");
                        else
                            sb.Append(".");
                    }
                }
                

                return sb.ToString();
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

            public bool Overlaps(Vector2 vector)
            {
                if (vector.X < X1 || vector.X > X2)
                    return false;

                if (vector.Y < Y1 || vector.Y > Y2)
                    return false;

                return true;
            }
        }

       

        public class Probe
        {
            public Vector2 Position { get; set; }

            public Vector2 Velocity { get; set; }

            public void SimulateStep()
            {
                var newX = Position.X + Velocity.X;
                if (newX > 0)
                    newX--;
                else if (newX < 0)
                    newX++;

                var newY = Position.Y + Velocity.Y;
                newY--;

                var newPosition = new Vector2(newX, newY);



                Position = newPosition;
            }
        }
    }
}
