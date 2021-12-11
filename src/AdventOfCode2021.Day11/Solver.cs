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
            for (int i = 0; i < totalSteps; i++)
            {
                cavern.SimulateStep();
            }

            return cavern.TotalFlashes.ToString();
        }

        public string SolveDayStar2(string input)
        {
            var cavern = new Cavern(input);

            int totalSteps = 0;
            while (cavern.DidAllFlash == false)
            {
                cavern.SimulateStep();
                totalSteps++;
            }

            return totalSteps.ToString();
        }

        public class Octopus
        {
            public int X { get; }

            public int Y { get; }

            public int Value { get; set; }

            public bool CanFlash => Value > 9;

            public Octopus? NOctopus { get; set; }

            public Octopus? NEOctopus { get; set; }

            public Octopus? EOctopus { get; set; }

            public Octopus? SEOctopus { get; set; }

            public Octopus? SOctopus { get; set; }

            public Octopus? SWOctopus { get; set; }

            public Octopus? WOctopus { get; set; }

            public Octopus? NWOctopus { get; set; }

            public Octopus(int x, int y, int value)
            {
                X = x;
                Y = y;
                Value = value;
            }

            public List<Octopus> GetAllNeighbors()
            {
                List<Octopus> neighbors = new();

                if (NOctopus != null)
                    neighbors.Add(NOctopus);

                if (NEOctopus != null)
                    neighbors.Add(NEOctopus);

                if (EOctopus != null)
                    neighbors.Add(EOctopus);

                if (SEOctopus != null)
                    neighbors.Add(SEOctopus);

                if (SOctopus != null)
                    neighbors.Add(SOctopus);

                if (SWOctopus != null)
                    neighbors.Add(SWOctopus);

                if (WOctopus != null)
                    neighbors.Add(WOctopus);

                if (NWOctopus != null)
                    neighbors.Add(NWOctopus);

                return neighbors;
            }

            public void Flash()
            {
                Value = 0;

                foreach(var v in GetAllNeighbors().Where(n => n.Value != 0))
                {
                    v.Value += 1;
                }
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        private class Cavern
        {
            public int TotalFlashes { get; private set; }

            private Octopus[,] dumboOctopuses = new Octopus[10, 10];

            public bool DidAllFlash { get; private set; } = false;

            public Cavern(string input)
            {
                // Parse
                var lines = input.SplitByNewLine();
                for (var x = 0; x < lines.Count(); x++)
                {
                    var line = lines.ElementAt(x);

                    for (var y = 0; y < line.Length; y++)
                    {
                        dumboOctopuses[x, y] = new Octopus(x,y, int.Parse(line[y].ToString()));
                    }
                }

                for (var x = 0; x < lines.Count(); x++)
                {
                    var line = lines.ElementAt(x);

                    for (var y = 0; y < line.Length; y++)
                    {
                        var o = dumboOctopuses[x, y];

                        o.NOctopus = GetNOctupus(o);
                        o.NEOctopus = GetNEOctupus(o);
                        o.EOctopus = GetEOctupus(o);
                        o.SEOctopus = GetSEOctupus(o);
                        o.SOctopus = GetSOctupus(o);
                        o.SWOctopus = GetSWOctupus(o);
                        o.WOctopus = GetWOctupus(o);
                        o.NWOctopus = GetNWOctupus(o);
                    }
                }
            }

            private Octopus? TryGetOctopus(int x, int y)
            {
                if (x < 0 || x >= 10)
                    return null;

                if (y < 0 || y >= 10)
                    return null;

                return dumboOctopuses[x, y];
            }

            private Octopus? GetWOctupus(Octopus octopus) => TryGetOctopus(octopus.X - 1, octopus.Y);

            private Octopus? GetNWOctupus(Octopus octopus) => TryGetOctopus(octopus.X - 1, octopus.Y - 1);

            private Octopus? GetNOctupus(Octopus octopus) => TryGetOctopus(octopus.X, octopus.Y - 1);

            private Octopus? GetNEOctupus(Octopus octopus) => TryGetOctopus(octopus.X + 1, octopus.Y - 1);

            private Octopus? GetEOctupus(Octopus octopus) => TryGetOctopus(octopus.X + 1, octopus.Y);

            private Octopus? GetSEOctupus(Octopus octopus) => TryGetOctopus(octopus.X + 1, octopus.Y + 1);

            private Octopus? GetSOctupus(Octopus octopus) => TryGetOctopus(octopus.X, octopus.Y + 1);

            private Octopus? GetSWOctupus(Octopus octopus) => TryGetOctopus(octopus.X - 1, octopus.Y + 1);

           
            internal void SimulateStep()
            {
                Queue<Octopus> octopusToFlash = new Queue<Octopus>();

                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        Octopus octopus = TryGetOctopus(x, y)!;
                        octopus.Value += 1;

                        if (octopus.CanFlash)
                            octopusToFlash.Enqueue(octopus);
                    }
                }

                while(octopusToFlash.Any())
                {
                    Octopus o = octopusToFlash.Dequeue();

                    // maybe it already has flashed
                    if (o.CanFlash == false)
                        continue;

                    o.Flash();
                    TotalFlashes += 1;

                    foreach (var n in o.GetAllNeighbors().Where(n => n.CanFlash))
                    {
                        octopusToFlash.Enqueue(n);
                    }
                }

                var didAllFlash = true;
                for (int x = 0; x < 10; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        Octopus octopus = TryGetOctopus(x, y)!;
                        if(octopus.Value != 0)
                        {
                            didAllFlash = false;
                                break;
                        }
                            
                    }

                    if (didAllFlash == false)
                        break;
                }

                DidAllFlash = didAllFlash;
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
