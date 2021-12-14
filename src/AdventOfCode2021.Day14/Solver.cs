using System;
using System.Collections.Generic;
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


        public class PairInsertionRule : IEquatable<PairInsertionRule?>
        {
            public string Pair { get; set; }

            public string Element { get; set; }

            public PairInsertionRule(string input)
            {
                var inputs = input.Split(" -> ");
                Pair = inputs[0];
                Element = inputs[1];
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
                       Pair == other.Pair;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Pair);
            }
        }

        public class PolymerSolver
        {
            public string PolymerTemplate { get; set; }

            public Dictionary<string, PairInsertionRule> PairInsertionRules { get; set; }

            public PolymerSolver(string input)
            {
                var lines = input.SplitByNewLine();
                PolymerTemplate = lines.ElementAt(0);

                PairInsertionRules = lines.Skip(2).Select(l => new PairInsertionRule(l)).ToDictionary(p => p.Pair, p => p);
            }

            public void DoPolymerInsertionRounds(int rounds)
            {
                for (int round = 0; round < rounds; round++)
                {
                    string template = PolymerTemplate;
                    for (int i = 0; i < template.Length - 1; i++)
                    {
                        string pair = template.Substring(i, 2);
                        if (PairInsertionRules.TryGetValue(pair, out PairInsertionRule? pairInsertionRule))
                        {
                            template = PolymerTemplate.Insert(i + 1, pairInsertionRule.Element);
                            i++;
                        }
                    }
                    PolymerTemplate = template;
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
                sb.AppendLine(new string(PolymerTemplate));

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
