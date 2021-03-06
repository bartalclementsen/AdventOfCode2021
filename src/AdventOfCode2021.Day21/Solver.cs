using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day21
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var game = new Game(input);
            game.Play();

            string history = game.GetHistory();
            return game.GetGameIndex().ToString();
        }

        public string SolveDayStar2(string input)
        {
            var game = new Game2(input);
            game.Play();

            return game.GetGameIndex().ToString();
        }

        public class GameState : IEquatable<GameState?>
        {
            public bool Player1Turn { get; set; }

            public int Player1Position { get; set; }

            public int Player1Score { get; set; }

            public int Player2Position { get; set; }

            public int Player2Score { get; set; }

            public long Total { get; set; }

            public GameState()
            { }

            public GameState(GameState gameState)
            {
                Player1Turn = gameState.Player1Turn;
                Player1Position = gameState.Player1Position;
                Player1Score = gameState.Player1Score;
                Player2Position = gameState.Player2Position;
                Player2Score = gameState.Player2Score;
                Total = gameState.Total;
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as GameState);
            }

            public bool Equals(GameState? other)
            {
                return other != null &&
                       Player1Turn == other.Player1Turn &&
                       Player1Position == other.Player1Position &&
                       Player1Score == other.Player1Score &&
                       Player2Position == other.Player2Position &&
                       Player2Score == other.Player2Score;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Player1Turn, Player1Position, Player1Score, Player2Position, Player2Score);
            }
        }
        public class Game2
        {
            private readonly List<Player> _players = new List<Player>();
            private long _player1Wins = 0;
            private long _player2Wins = 0;

            public Game2(string input)
            {
                var inputs = input.SplitByNewLine();
                _players.Add(new Player(inputs.ElementAt(0)));
                _players.Add(new Player(inputs.ElementAt(1)));
            }

            public void Play()
            {
                long maxSize = 0;

                var rootGameState = new GameState()
                {
                    Player1Turn = true,
                    Player1Position = _players[0].CurrentPositon,
                    Player1Score = 0,
                    Player2Position = _players[1].CurrentPositon,
                    Player2Score = 0,
                    Total = 1
                };

                Dictionary<int, int> rolls = new Dictionary<int, int>
                {
                    {9, 1},
                    {8, 3},
                    {7, 6},
                    {6, 7},
                    {5, 6},
                    {4, 3},
                    {3, 1},
                };

                HashSet<GameState> gameStates = new HashSet<GameState>();
                gameStates.Add(rootGameState);

                while (gameStates.Any())
                {
                    var gameState = gameStates.First();
                    gameStates.Remove(gameState);

                    foreach (var roll in rolls)
                    {
                        GameState newGameState = new GameState(gameState)
                        {
                            Player1Turn = gameState.Player1Turn == false,
                            Total = gameState.Total * roll.Value
                        };

                        if (gameState.Player1Turn)
                        {
                            var newPosition = gameState.Player1Position + roll.Key;
                            if (newPosition > 10)
                                newPosition -= 10;

                            newGameState.Player1Position = newPosition;
                            newGameState.Player1Score = gameState.Player1Score + newPosition;
                        }
                        else
                        {
                            var newPosition = gameState.Player2Position + roll.Key;
                            if (newPosition > 10)
                                newPosition -= 10;

                            newGameState.Player2Position = newPosition;
                            newGameState.Player2Score = gameState.Player2Score + newPosition;
                        }

                        if (newGameState.Player1Score >= 21)
                        {
                            _player1Wins += newGameState.Total;
                        }
                        else if (newGameState.Player2Score >= 21)
                        {
                            _player2Wins += newGameState.Total;
                        }
                        else
                        {
                            if(gameStates.TryGetValue(newGameState, out var oldGameState))
                            {
                                oldGameState.Total += newGameState.Total;
                            }
                            else
                            {
                                gameStates.Add(newGameState);
                            }
                        }
                    }
                }
            }



            public long GetGameIndex()
            {
                return Math.Max(_player1Wins, _player2Wins);
            }
        }

        public class Game
        {
            private readonly List<Player> _players = new List<Player>();

            private IDice _dice;

            private StringBuilder _history = new StringBuilder();

            public Game(string input)
            {
                var inputs = input.SplitByNewLine();
                _players.Add(new Player(inputs.ElementAt(0)));
                _players.Add(new Player(inputs.ElementAt(1)));
                _dice = new DeterministicDice();
            }

            public void Play()
            {
                int index = -1;
                while(true)
                {
                    index = (index + 1) % _players.Count;

                    int roll1 = _dice.Roll();
                    int roll2 = _dice.Roll();
                    int roll3 = _dice.Roll();

                    _players[index].MovePlayer(roll1 + roll2 + roll3);
                    _history.AppendLine($"Player {_players[index].PlayerIndex} rolls {roll1} {roll2} {roll3} and moves to space {_players[index].CurrentPositon} for a total score of {_players[index].Score}");

                    if (_players[index].Score >= 1000)
                        break;
                }
            }



            public long GetGameIndex()
            {
                return  _players.First(p => (p.Score >= 1000) == false).Score * _dice.GetNumberOfRolls();
            }

            public string GetHistory() 
            {
                return _history.ToString();
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, _players);
            }
        }

        public interface IDice
        {
            public int Roll();

            public long GetNumberOfRolls();
        }

        public class DiracDice : IDice
        {
            public long GetNumberOfRolls()
            {
                throw new NotImplementedException();
            }

            public int Roll()
            {
                throw new NotImplementedException();
            }
        }

        public class DeterministicDice : IDice
        {
            private int _index;

            public int Roll()
            {
                return ++_index;
            }

            public long GetNumberOfRolls() => _index;
        }

        public class Player
        {
            private int _startingPosition;

            public int CurrentPositon { get; private set; }

            public int PlayerIndex { get; private set; }

            public long Score { get; private set; }

            public Player(string input)
            {

                _startingPosition = int.Parse(
                    input.Substring(input.IndexOf(":") + 2)
                );

                CurrentPositon = _startingPosition;

                PlayerIndex = int.Parse(
                     input.Substring(7, input.IndexOf("starting") - 8)
                );
            }

            public void MovePlayer(int positions)
            {
                var newPosition = (CurrentPositon + positions) % 10;
                if (newPosition == 0)
                    newPosition = 10;

                CurrentPositon = newPosition;
                Score += CurrentPositon;
            }

            public override string ToString()
            {
                return $"Player {PlayerIndex} starting position: {_startingPosition}";
            }
        }
    }
}
