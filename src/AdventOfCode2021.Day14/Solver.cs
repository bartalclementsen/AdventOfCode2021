using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day14
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            PolymerSolver polymerSolver = new (input);

            polymerSolver.DoPolymerInsertionRounds(10);

            return polymerSolver.GetMostCommonSubtractLeastCommonCount().ToString();
        }

        public string SolveDayStar2(string input)
        {
            PolymerSolver polymerSolver = new(input);

            string s = polymerSolver.ToString();

            polymerSolver.DoPolymerInsertionRounds(40);

            return polymerSolver.GetMostCommonSubtractLeastCommonCount().ToString();
        }


        public class PolymerSolver
        {
            private List<Pair> _pairs;

            public Dictionary<Pair, PairInsertionRule> PairInsertionRules { get; set; }

            public PolymerSolver(string input)
            {
                var lines = input.SplitByNewLine();

                var polymerTemplate = lines.ElementAt(0);
                _pairs = new List<Pair>();
                for (var i = 0; i < polymerTemplate.Length - 1; i++)
                {
                    _pairs.Add(new Pair(polymerTemplate.Substring(i, 2)));
                }

                PairInsertionRules = lines.Skip(2).Select(l => new PairInsertionRule(l)).ToDictionary(p => p.Pair, p => p);
            }

            public void DoPolymerInsertionRounds(int rounds)
            {

                for (int round = 0; round < rounds; round++)
                {
                    List<Pair> newPairs = new List<Pair>();
                    foreach (var pair in _pairs)
                    {
                        var rule = PairInsertionRules[pair];

                        newPairs.Add(new Pair(pair.A, rule.Element, pair.Count));
                        newPairs.Add(new Pair(rule.Element, pair.B, pair.Count));
                    }

                    //Merge pairs and sum count
                    _pairs = newPairs.GroupBy(p => p).Select(p => new Pair(p.First().A, p.First().B, p.Sum(p2 => p2.Count))).ToList();
                }
            }

            public long GetMostCommonSubtractLeastCommonCount()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(_pairs.First().A);
                foreach (var pair in _pairs)
                {
                    sb.Append(pair.B);
                }

                var v = _pairs.GroupBy(p => p.B).Select(g => new { Letter = g.Key, Count = g.Sum(l => l.Count) }).OrderBy(g => g.Count);
                var first = v.First();
                var last = v.Last();                
                return last.Count - first.Count;
            }
        }

        public class Pair : IEquatable<Pair?>, IEquatable<char[]?>
        {
            public char A { get; private set; }

            public char B { get; private set; }

            public long Count { get; set; }

            public Pair(string pair)
            {
                A = pair[0];
                B = pair[1];
                Count = 1;
            }

            public Pair(char a, char b, long count)
            {
                A = a;
                B = b;
                Count = count;
            }

            public void SetPair(char a, char b)
            {
                A = a;
                B = b;
            }

            public bool Equals(char[]? other)
            {
                return other != null &&
                    other.Length == 2 &&
                    A == other[0] &&
                    B == other[1];
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as Pair);
            }

            public bool Equals(Pair? other)
            {
                return other != null &&
                       A == other.A &&
                       B == other.B;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(A, B);
            }

            public override string ToString()
            {
                return string.Join("", A, B) + " (" + Count + ")";
            }

        }

        public class PairInsertionRule : IEquatable<PairInsertionRule?>, IEquatable<Pair?>
        {
            public Pair Pair { get; set; }

            public char Element { get; set; }

            public PairInsertionRule(string input)
            {
                var inputs = input.Split(" -> ");
                Pair = new Pair(inputs[0]);
                Element = inputs[1][0];
            }

            public override string ToString()
            {
                return $"{Pair} -> {Element}";
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as PairInsertionRule);
            }

            public bool Equals(PairInsertionRule? other)
            {
                return other != null &&
                       EqualityComparer<Pair>.Default.Equals(Pair, other.Pair);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Pair);
            }

            public bool Equals(Pair? other)
            {
                return Pair.Equals(other);
            }
        }
    }
}
