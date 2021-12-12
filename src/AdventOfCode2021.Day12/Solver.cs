using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day12
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var caves = new CaveSystem(input);

            var numberOfDistinctPaths = caves.GetNumberOfDistinctPaths();

            return numberOfDistinctPaths.ToString();
        }

        public string SolveDayStar2(string input)
        {
            var caves = new CaveSystem(input);

            var numberOfDistinctPaths = caves.GetNumberOfDistinctPaths(canVisitASmallCaveTwice: true);

            return numberOfDistinctPaths.ToString();
        }

        public class CaveSystem
        {
            private readonly Cave _startCave;

            public CaveSystem(string input)
            {
                Dictionary<string, Cave> _caves = new();

                foreach (var caveConnectinInput in input.SplitByNewLine())
                {
                    string[] caves = caveConnectinInput.Split("-");

                    Cave cave1 = GetOrCreateCave(caves[0], _caves);
                    Cave cave2 = GetOrCreateCave(caves[1], _caves);

                    cave1.AddConnection(cave2);
                    cave2.AddConnection(cave1);
                }

                _startCave = _caves.Values.Single(c => c.IsStart);
            }

            public int GetNumberOfDistinctPaths(bool canVisitASmallCaveTwice = false)
            {
                List<Route> finishedRoutes = new();

                Queue<Route> possibleRoutes = new();
                possibleRoutes.Enqueue(new Route(_startCave, canVisitASmallCaveTwice));

                while (possibleRoutes.Any())
                {
                    Route possibleRoute = possibleRoutes.Dequeue();
                    IEnumerable<Cave> possibleCaves = possibleRoute.GetPossiblCaves();

                    foreach (Cave possibleCave in possibleCaves)
                    {
                        Route newRoute = new(possibleRoute);
                        newRoute.GoToCave(possibleCave);

                        if (newRoute.IsFinished)
                            finishedRoutes.Add(newRoute);
                        else
                            possibleRoutes.Enqueue(newRoute);
                    }
                }

                return finishedRoutes.Count;
            }

            private Cave GetOrCreateCave(string name, Dictionary<string, Cave> caves)
            {
                if (caves.TryGetValue(name, out Cave? cave) == false)
                {
                    cave = new Cave(name);
                    caves.Add(name, cave);
                }

                return cave;
            }
        }

        public class Route
        {
            public bool IsFinished => CurrentCave.IsEnd;

            public Cave CurrentCave { get; private set; }

            private bool _hasVisitedASmallCaveTwice = false;

            private readonly List<Cave> _path = new();
            private readonly bool _canVisitASmallCaveTwice = false;

            public Route(Route route)
            {
                CurrentCave = route.CurrentCave;
                _path = route._path.ToList();
                _hasVisitedASmallCaveTwice = route._hasVisitedASmallCaveTwice;
                _canVisitASmallCaveTwice = route._canVisitASmallCaveTwice;
            }

            public Route(Cave startCave, bool canVisitASmallCaveTwice = false)
            {
                CurrentCave = startCave;
                _path.Add(startCave);
                _canVisitASmallCaveTwice = canVisitASmallCaveTwice;
            }

            public void GoToCave(Cave cave)
            {
                if(cave.IsSmallCave && _path.Contains(cave))
                    _hasVisitedASmallCaveTwice = true;

                _path.Add(cave);
                CurrentCave = cave;
            }

            public IEnumerable<Cave> GetPossiblCaves()
            {
                return CurrentCave.ConnectedCaves.Where(c => CanGoToCave(c));
            }

            public override string ToString()
            {
                return string.Join(",", _path);
            }

            private bool CanGoToCave(Cave cave)
            {
                if (cave.IsBigCave)
                    return true;
                else if (_path.Contains(cave) == false)
                    return true;
                else if (_canVisitASmallCaveTwice && _hasVisitedASmallCaveTwice == false && cave.IsStart == false && cave.IsEnd == false)
                    return true;

                return false;
            }
        }

        public class Cave : IEquatable<Cave?>
        {
            public string Name { get; }

            public bool IsStart { get; }

            public bool IsEnd { get; }

            public bool IsBigCave { get; }

            public bool IsSmallCave { get; }

            public IEnumerable<Cave> ConnectedCaves => _connectedCaves.AsReadOnly();

            private List<Cave> _connectedCaves = new();

            public Cave(string name)
            {
                Name = name;
                IsStart = Name == "start";
                IsEnd = Name == "end";
                IsBigCave = IsStart == false && IsEnd == false && Name.All(c => char.IsUpper(c));
                IsSmallCave = IsStart == false && IsEnd == false && IsBigCave == false;
            }

            internal void AddConnection(Cave cave)
            {
                if(cave == this)
                    throw new ArgumentNullException(nameof(cave), "Can not add cave connection where this is not a node");

                _connectedCaves.Add(cave);
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as Cave);
            }

            public bool Equals(Cave? other)
            {
                return other != null && Name == other.Name;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Name);
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
