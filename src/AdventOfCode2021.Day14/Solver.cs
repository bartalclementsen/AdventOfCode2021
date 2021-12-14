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

        public class Pair : IEquatable<Pair?>, IEquatable<char[]?>
        {
            public char A { get; private set; }

            public char B { get; private set; }

            public Pair(string pair)
            {
                A = pair[0];
                B = pair[1];
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

        public class PolymerSolver
        {
            public List<char> PolymerTemplate { get; set; }

            public Dictionary<Pair, PairInsertionRule> PairInsertionRules { get; set; }

            public PolymerSolver(string input)
            {
                var lines = input.SplitByNewLine();
                PolymerTemplate = lines.ElementAt(0).ToList();

                PairInsertionRules = lines.Skip(2).Select(l => new PairInsertionRule(l)).ToDictionary(p => p.Pair, p => p);
            }

            public void DoPolymerInsertionRounds(int rounds)
            {
                Pair pair1 = new("AB");
                Stopwatch sw = new Stopwatch();
                Debug.WriteLine("Starting");
                for (int round = 0; round < rounds; round++)
                {
                    sw.Restart();
                    Debug.WriteLine("Round " + round + " started");
                    for (int i = 0; i < PolymerTemplate.Count() - 1; i++)
                    {
                        pair1.SetPair(PolymerTemplate[i], PolymerTemplate[i+1]);

                        if (PairInsertionRules.TryGetValue(pair1, out PairInsertionRule? pairInsertionRule))
                        {
                            PolymerTemplate.Insert(i + 1, pairInsertionRule.Element);
                            i++;
                        }
                    }
                    Debug.WriteLine("Round " + round + " started. " + sw.Elapsed);
                }
            }

            public long GetMostCommonSubtractLeastCommonCount()
            {
                var orderd = PolymerTemplate.GroupBy(g => g).OrderBy(o => o.LongCount());
                var first = orderd.First();
                var last  = orderd.Last();

                return last.LongCount() - first.LongCount();
            }

            public override string ToString()
            {
                StringBuilder sb = new();
                sb.AppendLine(String.Join("", PolymerTemplate));

                foreach (var pairInsertion in PairInsertionRules)
                {
                    sb.AppendLine();
                    sb.Append(pairInsertion.ToString());
                }

                return sb.ToString();
            }
        }
        //polymer template

        //pair insertion 
    }
}
