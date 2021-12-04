using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2021.Day4
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var bingoGame = new BingoGame(input);

            while (bingoGame.WinnerFound == false)
            {
                bingoGame.DrawNextNumber();
            }

            int? index = bingoGame?.GetWinningHash();


            return index?.ToString() ?? "UNKNOWN";
        }

        public string SolveDayStar2(string input)
        {
            var bingoGame = new BingoGame(input);

            while (bingoGame.AreAllWinnersFound == false)
            {
                bingoGame.DrawNextNumber();
            }

            int? index = bingoGame?.GetLastWinningHash();


            return index?.ToString() ?? "UNKNOWN";
        }

        public class BingoGame
        {
            public bool WinnerFound => _winners.Any();

            public bool AreAllWinnersFound => _playingBingoBoards.Any() == false;


            private int _bingoNumberIndex = -1;

            private readonly List<int> _bingoNumbers;
            private readonly List<BingoBoard> _bingoBoards = new();
            private readonly List<BingoBoard> _playingBingoBoards = new();
            private readonly List<BingoBoard> _winners = new();

            public BingoGame(string input)
            {
                IEnumerable<string>? bingoInput = input.SplitByNewLine();
                _bingoNumbers = bingoInput.ElementAt(0).Split(",").Select(bn => int.Parse(bn)).ToList();

                for (int i = 2; i < bingoInput.Count(); i += 6)
                {
                    _bingoBoards.Add(new BingoBoard(bingoInput.Skip(i).Take(5)));
                }
                _playingBingoBoards.AddRange(_bingoBoards);
            }

            public override string ToString()
            {
                StringBuilder sb = new();

                sb.AppendLine(string.Join(",", _bingoNumbers));

                foreach (var board in _playingBingoBoards)
                {
                    sb.AppendLine();
                    sb.AppendLine(board.ToString());
                }

                return sb.ToString();
            }

            public void DrawNextNumber()
            {
                _bingoNumberIndex++;
                if (_bingoNumberIndex > _bingoNumbers.Count - 1)
                    throw new IndexOutOfRangeException("Can not draw more numbers");

                int currentNumber = GetCurrentNumeber();

                foreach (var board in _playingBingoBoards.ToList())
                {
                    board.PlayNumber(currentNumber);
                    if (board.HasWon)
                    {
                        _winners.Add(board);
                        _playingBingoBoards.Remove(board);
                    }
                }
            }

            public int? GetWinningHash() => CalulateWinningHash(GetWinningBoard());

            public int? GetLastWinningHash() => CalulateWinningHash(GeLastWinningBoard());

            private int? CalulateWinningHash(BingoBoard? bingoBoard) => bingoBoard?.GetSumOfUnmarkedNumbers() * GetCurrentNumeber();

            private int GetCurrentNumeber() => _bingoNumbers[_bingoNumberIndex];

            private BingoBoard? GetWinningBoard() => _winners.FirstOrDefault();

            private BingoBoard? GeLastWinningBoard() => _winners.LastOrDefault();
        }

        public class BingoBoard
        {
            public bool HasWon
            {
                get
                {
                    if (_rows.Any(r => r.HasWon))
                        return true;

                    for (int i = 0; i < 5; i++)
                    {
                        bool columnWon = true;

                        for (int j = 0; j < 5; j++)
                        {
                            if (_rows[j].Numbers[i].IsChecked == false)
                            {
                                columnWon = false;
                                break;
                            }
                        }


                        if (columnWon)
                            return true;
                    }


                    return false;
                }
            }

            private readonly List<BinogRow> _rows;

            public BingoBoard(IEnumerable<string> rows)
            {
                _rows = rows.Select(r => new BinogRow(r)).ToList();
            }

            internal int GetSumOfUnmarkedNumbers()
            {
                return _rows.SelectMany(b => b.Numbers).Where(n => n.IsChecked == false).Sum(o => o.Number);
            }

            internal void PlayNumber(int currentNumber)
            {
                foreach (var row in _rows)
                {
                    if (row.TryPlayNumber(currentNumber))
                        break;
                }
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();

                foreach (var row in _rows)
                {
                    sb.AppendLine(row.ToString());
                }
                return sb.ToString();
            }
        }

        public class BinogRow
        {
            public bool HasWon => Numbers.All(n => n.IsChecked);

            public List<BingoNumber> Numbers { get; }

            public BinogRow(string numbers)
            {
                Numbers = Regex.Matches(numbers, @"\d+").Select(m => new BingoNumber(m.Value)).ToList();
            }

            internal bool TryPlayNumber(int number)
            {
                var foundNumber = Numbers.FirstOrDefault(n => n.Number == number);
                if (foundNumber != null)
                {
                    foundNumber.Check();
                    return true;
                }

                return false;
            }

            public override string ToString()
            {
                return string.Join(" ", Numbers);
            }
        }

        public class BingoNumber
        {
            public int Number { get; }

            public bool IsChecked { get; private set; }

            public BingoNumber(string number)
            {
                Number = int.Parse(number);
            }

            public void Check()
            {
                IsChecked = true;
            }

            public override string ToString()
            {
                string value = Number.ToString("00");
                value += IsChecked ? "X" : "O";
                return value;
            }
        }
    }
}
