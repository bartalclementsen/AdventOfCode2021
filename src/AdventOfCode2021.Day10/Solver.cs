using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day10
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            return Solve(input)
                .Where(o => o.Type == ResultType.Corrupted)
                .Sum(o => o.Score)
                .ToString();
        }

        public string SolveDayStar2(string input)
        {
            var incompeleteScores = Solve(input)
                .Where(o => o.Type == ResultType.Incomplete)
                .OrderBy(o => o.Score)
                .Select(o => o.Score)
                .ToList();

            var middleIndex = incompeleteScores.Count - (incompeleteScores.Count / 2) - 1;
            return incompeleteScores[middleIndex].ToString();
        }

        private enum ResultType
        {
            Corrupted,
            Incomplete
        }

        private record Result(ResultType Type, long Score, string Line);

        private List<Result> Solve(string input)
        {
            var lines = input.SplitByNewLine();

            List<Result> resultSet = new List<Result>();
            foreach (var line in lines)
            {
                var chunks = line.ToList();

                // Remove all that are OK
                bool hasRemovedPair = false;
                do
                {
                    hasRemovedPair = false;

                    for (int i = 0; i < chunks.Count - 1; i++)
                    {
                        if (ChunkMatch(chunks[i], chunks[i + 1]))
                        {
                            chunks.RemoveAt(i);
                            chunks.RemoveAt(i);
                            i = i - 1;
                            hasRemovedPair = true;
                        }
                    }


                } while (hasRemovedPair);

                char? unexpectedChunk = null;
                // Find first that is inconsistent
                for (int i = 0; i < chunks.Count - 1; i++)
                {
                    if (ChunkIsOpen(chunks[i]) && ChunkIsClosed(chunks[i + 1]))
                    {
                        unexpectedChunk = chunks[i + 1];
                        break;
                    }
                }

                if (unexpectedChunk != null)
                {
                    // Is Corupted
                    long points = GetCorruptedScore(unexpectedChunk.Value);
                    resultSet.Add(new Result(ResultType.Corrupted, points, line));
                }
                else
                {
                    long incompeleteScore = 0;
                    for (int i = chunks.Count - 1; i >= 0; i--)
                    {
                        var closingChunk = GetClosingChunk(chunks[i]);
                        incompeleteScore *= 5;
                        incompeleteScore += GetIncompeleteScore(closingChunk);
                    }

                    resultSet.Add(new Result(ResultType.Incomplete, incompeleteScore, line));
                }
            }

            return resultSet;
        }

        private char GetClosingChunk(char chunk)
        {
            return chunk switch
            {
                '(' => ')',
                '[' => ']',
                '{' => '}',
                '<' => '>',
                _ => throw new NotImplementedException("Unknown chunk " + chunk)
            };
        }

        private long GetIncompeleteScore(char chunk)
        {
            return chunk switch
            {
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4,
                _ => throw new NotImplementedException("Unknown chunk " + chunk)
            };
        }

        private long GetCorruptedScore(char chunk) 
        {
            return chunk switch
            {
                ')' => 3,
                ']' => 57,
                '}' => 1197,
                '>' => 25137,
                _ => throw new NotImplementedException("Unknown chunk " + chunk)
            };
        }

        private bool ChunkMatch(char chunk1, char chunk2)
        {
            return chunk1 == '{' && chunk2 == '}' ||
                chunk1 == '(' && chunk2 == ')' ||
                chunk1 == '[' && chunk2 == ']' ||
                chunk1 == '<' && chunk2 == '>';
        }

        private bool ChunkIsOpen(char chunk1)
        {
            return chunk1 == '{' ||
                chunk1 == '(' ||
                chunk1 == '[' ||
                chunk1 == '<' ;
        }

        private bool ChunkIsClosed(char chunk2)
        {
            return chunk2 == '}' ||
                chunk2 == ')' ||
                chunk2 == ']' ||
                chunk2 == '>';
        }
    }
}
