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
            return "";
        }

        public string SolveDayStar2(string input)
        {
            return "";
        }

        public class ProbeSimulation
        {
            public ProbeSimulation(string input)
            {
                "target area: x=20..30, y=-10..-5";
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
