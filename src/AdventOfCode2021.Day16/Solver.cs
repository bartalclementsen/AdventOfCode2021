using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Day16
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var commandStreamReader = new CommandStreamReader(input);
            var command = commandStreamReader.ReadNextCommand();

            return command.GetVersionSum().ToString();
        }

        public string SolveDayStar2(string input)
        {
            var commandStreamReader = new CommandStreamReader(input);
            var command = commandStreamReader.ReadNextCommand();

            return command.GetValue().ToString();
        }









        /* ----------------------------------------------------------------------------  */
        /*                               INTERNAL CLASSES                                */
        /* ----------------------------------------------------------------------------  */
        public abstract class Command
        {
            public long Version { get; set; }

            public Command(long version)
            {
                Version = version;
            }

            public virtual long GetVersionSum()
            {
                return Version;
            }

            public virtual long GetValue()
            {
                throw new NotImplementedException();
            }
        }

        public class LiteralValueCommand : Command
        {
            public long LiteralValue { get; }

            public LiteralValueCommand(long version, long literalValue) : base(version)
            {
                LiteralValue = literalValue;
            }

            public override long GetValue() => LiteralValue;
        }

        public abstract class OperatorCommand : Command
        {
            public IEnumerable<Command> Commands { get; }

            public OperatorCommand(long version, IEnumerable<Command> commands) : base(version)
            {
                Commands = commands;
            }

            public override long GetVersionSum()
            {
                return Commands.Sum(o => o.GetVersionSum()) + base.GetVersionSum();
            }

            public override long GetValue() => Solve();

            protected abstract long Solve();
        }





        public class SumCommand : OperatorCommand
        {
            public SumCommand(long version, IEnumerable<Command> commands) : base(version, commands) { }

            protected override long Solve()
            {
                return Commands.Sum(o => o.GetValue());
            }
        }

        public class ProductCommand : OperatorCommand
        {
            public ProductCommand(long version, IEnumerable<Command> commands) : base(version, commands) { }

            protected override long Solve()
            {
                return Commands.Select(o => o.GetValue()).Aggregate((a,b) => a * b);
            }
        }

        public class MinimumCommand : OperatorCommand
        {
            public MinimumCommand(long version, IEnumerable<Command> commands) : base(version, commands) { }

            protected override long Solve()
            {
                return Commands.Min(o => o.GetValue());
            }
        }

        public class MaximumCommand : OperatorCommand
        {
            public MaximumCommand(long version, IEnumerable<Command> commands) : base(version, commands) { }

            protected override long Solve()
            {
                return Commands.Max(o => o.GetValue());
            }
        }

        public class GreaterThanCommand : OperatorCommand
        {
            public GreaterThanCommand(long version, IEnumerable<Command> commands) : base(version, commands) { }

            protected override long Solve()
            {
                var values = Commands.Cast<LiteralValueCommand>();
                var result = Commands.ElementAt(0).GetValue() > Commands.ElementAt(1).GetValue();
                return result ? 1 : 0;
            }
        }

        public class LessThanCommand : OperatorCommand
        {
            public LessThanCommand(long version, IEnumerable<Command> commands) : base(version, commands) { }

            protected override long Solve()
            {
                var values = Commands.Cast<LiteralValueCommand>();
                var result = Commands.ElementAt(0).GetValue() < Commands.ElementAt(1).GetValue();
                return result ? 1 : 0;
            }
        }

        public class EqualToCommand : OperatorCommand
        {
            public EqualToCommand(long version, IEnumerable<Command> commands) : base(version, commands) { }

            protected override long Solve()
            {
                var result = Commands.ElementAt(0).GetValue() == Commands.ElementAt(1).GetValue();
                return result ? 1 : 0;
            }
        }




        public enum PackageType
        {
            Unkown,
            Sum = 0,
            Product = 1,
            Minimum = 2,
            Maximum = 3,
            LiteralValue = 4,
            GreaterThan = 5,
            LessThan = 6,
            EqualTo = 7
        }

        public class CommandStreamReader
        {
            /* ----------------------------------------------------------------------------  */
            /*                                  PROPERTIES                                   */
            /* ----------------------------------------------------------------------------  */
            public InStreamReader _inStreamReader;

            public CommandStreamReader(string input)
            {
                _inStreamReader = new InStreamReader(input);
            }

            /* ----------------------------------------------------------------------------  */
            /*                                 CONSTRUCTORS                                  */
            /* ----------------------------------------------------------------------------  */
            public Command ReadNextCommand()
            {
                // Header
                long version = _inStreamReader.ReadVersion();
                var type = _inStreamReader.ReadPackageTypeId();

                if(type == PackageType.LiteralValue)
                {
                    return ReadLiteralValueCommand(version);
                }
                else
                {
                    return ReadOperatorCommand(version, type);
                }
            }

            /* ----------------------------------------------------------------------------  */
            /*                                PRIVATE METHODS                                */
            /* ----------------------------------------------------------------------------  */
            private LiteralValueCommand ReadLiteralValueCommand(long version)
            {
                long literal = _inStreamReader.ReadLiteral();

                return new LiteralValueCommand(version, literal);
            }

            private OperatorCommand ReadOperatorCommand(long version, PackageType type)
            {
                var lengthTypeId = _inStreamReader.ReadLengthTypeId();   
                
                // Read Sub Command
                List<Command> commands = new();
                if (lengthTypeId == true)
                {
                    long numberOfSubPackets = _inStreamReader.GetNumberOfSubPackets();
                    for(int i = 0; i < numberOfSubPackets; i++)
                    {
                        commands.Add(ReadNextCommand());
                    }
                }
                else
                {
                    long lengthOfSubpackets = _inStreamReader.ReadLengthOfSubpackets();
                    int subPackageStart = _inStreamReader.CurrentIndex;

                    while (_inStreamReader.CurrentIndex - subPackageStart < lengthOfSubpackets)
                    {
                        commands.Add(ReadNextCommand());
                    }
                }

                return type switch
                {
                    PackageType.Sum => new SumCommand(version, commands),
                    PackageType.Product => new ProductCommand(version, commands),
                    PackageType.Minimum => new MinimumCommand(version, commands),
                    PackageType.Maximum => new MaximumCommand(version, commands),
                    PackageType.GreaterThan => new GreaterThanCommand(version, commands),
                    PackageType.LessThan => new LessThanCommand(version, commands),
                    PackageType.EqualTo => new EqualToCommand(version, commands),
                    _ => throw new Exception("Unknown Type"),
                };
            }
        }





        public class InStreamReader
        {
            /* ----------------------------------------------------------------------------  */
            /*                                  PROPERTIES                                   */
            /* ----------------------------------------------------------------------------  */
            public int CurrentIndex { get; private set; } = 0;

            private string _hexStream;
            private readonly List<bool> _bits;

            /* ----------------------------------------------------------------------------  */
            /*                                 CONSTRUCTORS                                  */
            /* ----------------------------------------------------------------------------  */
            public InStreamReader(string input)
            {
                _hexStream = input;
                _bits = ConvertHexToBitArray(_hexStream);
            }

            /* ----------------------------------------------------------------------------  */
            /*                                PUBLIC METHODS                                 */
            /* ----------------------------------------------------------------------------  */
            public long ReadVersion()
            {
                var bits = Read(3);

                return ToLong(bits);
            }

            public PackageType ReadPackageTypeId()
            {
                var bits = Read(3);

                return (PackageType)ToLong(bits);
            }

            public long ReadLiteral()
            {
                List<bool> literalBits = new();
                
                bool hasNextPackage;
                do
                {
                    hasNextPackage = _bits[CurrentIndex++];
                    literalBits.AddRange(Read(4));
                }
                while (hasNextPackage);

                return ToLong(literalBits);
            }

            public bool ReadLengthTypeId()
            {
                return _bits[CurrentIndex++];
            }

            public long ReadLengthOfSubpackets()
            {
                var bits = this.Read(15);
                return ToLong(bits);
            }

            public long GetNumberOfSubPackets()
            {
                var bits = this.Read(11);
                return ToLong(bits);
            }

            /* ----------------------------------------------------------------------------  */
            /*                                PRIVATE METHODS                                */
            /* ----------------------------------------------------------------------------  */
            private List<bool> Read(int length)
            {
                var bits = _bits.GetRange(CurrentIndex, length);
                CurrentIndex += length;
                return bits;
            }

            private static long ToLong(List<bool> bits)
            {
                long value = 0;
                int count = bits.Count;
                for (int i = 0; i < count; i++)
                {
                    if (bits[count - i - 1])
                        value += Convert.ToInt64(Math.Pow(2, i));
                }
                return value;
            }

            private static List<bool> ConvertHexToBitArray(string hexData)
            {
                List<bool> bits = new(4 * hexData.Length);
                for (int i = 0; i < hexData.Length; i++)
                {
                    byte b = byte.Parse(hexData[i].ToString(), NumberStyles.HexNumber);
                    for (int j = 0; j < 4; j++)
                    {
                        bits.Insert(i * 4 + j, (b & (1 << (3 - j))) != 0);
                    }
                }
                return bits;
            }

            public override string ToString()
            {
                return $@"{_hexStream}
{string.Join("", _bits.Select(o => o ? "1" : "0"))}";
            }
        }
    }
}
