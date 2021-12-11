using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day11
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var cavern = new Cavern(input);
            var s = cavern.ToString();

            int totalSteps = 100;
            for(int i=0; i<totalSteps; i++)
            {
                cavern.SimulateStep();
            }

            return cavern.TotalFlashes.ToString();
        }

        public string SolveDayStar2(string input)
        {
            return "";
        }

        public record Octopus(int x, int y, int value);

        private class Cavern
        {
            public int TotalFlashes { get; private set; }

            private int[,] dumboOctopuses = new int[10, 10];

            public Cavern(string input)
            {
                // Parse
                var lines = input.SplitByNewLine();
                for (var x = 0; x < lines.Count(); x++)
                {
                    var line = lines.ElementAt(x);

                    for (var y = 0; y < line.Length; y++)
                    {
                        dumboOctopuses[x, y] = int.Parse(line[y].ToString());
                    }
                }
            }

            private Octopus? TryGetOctopus(int x, int y)
            {
                if (x < 0 || x >= 10)
                    return null;

                if (y < 0 || y >= 10)
                    return null;

                return new Octopus(x, y, dumboOctopuses[x, y]);
            }

            internal void SimulateStep()
            {
                
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                for(int x=0; x<10; x++)
                {
                    if (x != 0)
                        sb.AppendLine();

                    for (int y =0; y<10;y++)
                    {
                        sb.Append(dumboOctopuses[x, y]);    
                    }
                }

                return sb.ToString();
            }
        }
    }
}
