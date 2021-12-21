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

        public record GameState(int Round, int Player1Position, int Player1Score, int Player2Position, int Player2Score);

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
                var rootGameState = new GameState(0, _players[0].CurrentPositon, 0, _players[1].CurrentPositon, 0);

                Queue<GameState> gameStates = new Queue<GameState>();
                gameStates.Enqueue(rootGameState);

                while (gameStates.Any())
                {
                    var gameState = gameStates.Dequeue();

                    if (gameState.Round % 2 == 0)
                    {
                        for (int dice1 = 1; dice1 <= 3; dice1++)
                        {
                            for (int dice2 = 1; dice2 <= 3; dice2++)
                            {
                                for (int dice3 = 1; dice3 <= 3; dice3++)
                                {
                                    var roll = dice1 + dice2 + dice3;

                                    var newPosition = gameState.Player1Position + roll;
                                    if (newPosition > 10)
                                        newPosition -= 10;

                                    var gameState1 = gameState with
                                    {
                                        Round = gameState.Round + 1,
                                        Player1Position = newPosition,
                                        Player1Score = gameState.Player1Score + newPosition
                                    };

                                    if (gameState1.Player1Score >= 21)  //has won
                                    {
                                        _player1Wins++;
                                    }
                                    else
                                    {
                                        gameStates.Enqueue(gameState1);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= 3; i++)
                        {
                            for (int j = 1; j <= 3; j++)
                            {
                                for (int k = 1; k <= 3; k++)
                                {
                                    var roll = i + j + k;

                                    var newPosition = gameState.Player2Position + roll;
                                    if (newPosition > 10)
                                        newPosition -= 10;

                                    var gameState1 = gameState with
                                    {
                                        Round = gameState.Round + 1,
                                        Player2Position = newPosition,
                                        Player2Score = gameState.Player2Score + newPosition
                                    };

                                    if (gameState1.Player2Score >= 21)  //has won
                                    {
                                        _player2Wins++;
                                    }
                                    else
                                    {
                                        gameStates.Enqueue(gameState1);
                                    }
                                }
                            }
                        }
                    }
                }

                string s = "";
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
