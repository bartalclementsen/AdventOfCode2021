using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Day8
{
    internal class Solver
    {

        /* TEMPLATE       
         * 
              0:  
             aaaa 
            b    c
            b    c
             dddd 
            e    f
            e    f
             gggg 
         */

        /* LETTERS         

              0:      1:      2:      3:      4:
             aaaa    ....    aaaa    aaaa    ....
            b    c  .    c  .    c  .    c  b    c
            b    c  .    c  .    c  .    c  b    c
             ....    ....    dddd    dddd    dddd
            e    f  .    f  e    .  .    f  .    f
            e    f  .    f  e    .  .    f  .    f
             gggg    ....    gggg    gggg    ....

              5:      6:      7:      8:      9:
             aaaa    aaaa    aaaa    aaaa    aaaa
            b    .  b    .  .    c  b    c  b    c
            b    .  b    .  .    c  b    c  b    c
             dddd    dddd    ....    dddd    dddd
            .    f  e    f  .    f  e    f  .    f
            .    f  e    f  .    f  e    f  .    f
             gggg    gggg    ....    gggg    gggg
         */
        public string SolveDayStar1(string input)
        {
            List<DigitalDisplay>? displays = input.SplitAndConvertByNewLine(o => new DigitalDisplay(o));

            var knowDigitsCount = displays.SelectMany(o => o.DigitalOutputValue).Count(o => o.IsOne || o.IsFour || o.IsSeven || o.IsEight);

            return knowDigitsCount.ToString();
        }


        /* TEMPLATE       
         * 
              0:  
             aaaa 
            b    c
            b    c
             dddd 
            e    f
            e    f
             gggg 
         */

        public string SolveDayStar2(string input)
        {
            List<DigitalDisplay>? displays = input.SplitAndConvertByNewLine(o => new DigitalDisplay(o));

            int totalOutputValue = 0;
            foreach (var digitalDisplay in displays)
            {
                var groups = digitalDisplay.UniqueSignalPatterns.SelectMany(o => o.Segments).GroupBy(o => o.Letter);

                var onePattern = digitalDisplay.UniqueSignalPatterns.First(sp => sp.IsOne);
                var fourPattern = digitalDisplay.UniqueSignalPatterns.First(sp => sp.IsFour);

                char eMap = groups.Where(g => g.Count() == 4).First().Key;
                char fMap = groups.Where(g => g.Count() == 9).First().Key;
                char bMap = groups.Where(g => g.Count() == 6).First().Key;
                char cMap = onePattern.Segments.First(s => s.Letter != fMap).Letter;
                char aMap = groups.Where(g => g.Count() == 8).First(g => g.Key != cMap).Key;
                char dMap = groups.Where(g => g.Count() == 7).First(g => fourPattern.Segments.Any(s => s.Letter == g.Key)).Key;
                char gMap = groups.Where(g => g.Count() == 7).First(g => fourPattern.Segments.Any(s => s.Letter == g.Key) == false).Key;

                Mapper mapper = new Mapper(new[] { aMap, bMap, cMap, dMap, eMap, fMap, gMap });
                int outputValue = digitalDisplay.GetOutputValue(mapper);
                totalOutputValue += outputValue;
            }



            return totalOutputValue.ToString();
        }

        public class Mapper
        {
            /* TEMPLATE       
             
                aaaa 
               b    c
               b    c
                dddd 
               e    f
               e    f
                gggg 
            */

            public string ZeroMap => L("abcefg");

            public string OneMap => L("cf");

            public string TwoMap => L("acdeg");

            public string TreeMap => L("acdfg");

            public string FourMap => L("bcdf");

            public string FiveMap => L("abdfg");

            public string SixMap => L("abdefg");

            public string SevenMap => L("acf");

            public string EightMap => L("abcdefg");

            public string NineMap => L("abcdfg");

            private char[] _map;

            public Mapper(char[] map)
            {
                _map = map;
            }

            public string L(string a)
            {
                return string.Join("", a.ToCharArray().Select(c => L(c)).OrderBy(o => o));
            }

            private char L(char a)
            {
                switch (a)
                {
                    case 'a':
                        return _map[0];
                    case 'b':
                        return _map[1];
                    case 'c':
                        return _map[2];
                    case 'd':
                        return _map[3];
                    case 'e':
                        return _map[4];
                    case 'f':
                        return _map[5];
                    case 'g':
                        return _map[6];
                    default:
                        throw new Exception("Unknonw letter");
                }
            }
        }

        public class DigitalDisplay
        {
            public List<SignalPattern> UniqueSignalPatterns { get; }

            public List<SignalPattern> DigitalOutputValue { get; }

            public DigitalDisplay(string input)
            {
                var inputTypes = input.Split(" | ");
                UniqueSignalPatterns = inputTypes[0].Split(" ").Select(o => new SignalPattern(o)).ToList();
                DigitalOutputValue = inputTypes[1].Split(" ").Select(o => new SignalPattern(o)).ToList();
            }

            public override string ToString()
            {
                return string.Join(" ", UniqueSignalPatterns) + "|" + string.Join(" ", DigitalOutputValue);
            }

            internal int GetOutputValue(Mapper mapper)
            {
                int number = 0;
                for (int i = 0; i < DigitalOutputValue.Count; i++)
                {
                    int patternNumber = DigitalOutputValue[i].ToInt(mapper);
                    number += (int)(patternNumber * Math.Pow(10, DigitalOutputValue.Count - i - 1));
                }

                return number;
            }
        }

        public class SignalPattern
        {
            public bool IsOne => Segments.Count == 2;

            public bool IsFour => Segments.Count == 4;

            public bool IsSeven => Segments.Count == 3;

            public bool IsEight => Segments.Count == 7;

            public List<Segment> Segments { get; }

            public SignalPattern(string input)
            {
                Segments = input.ToCharArray().OrderBy(c => c).Select(c => new Segment(c)).ToList();
            }

            public override string ToString()
            {
                return string.Join("", Segments);
            }

            internal int ToInt(Mapper mapper)
            {
                if (IsOne)
                    return 1;
                if (IsFour)
                    return 4;
                if (IsSeven)
                    return 7;
                if (IsEight)
                    return 8;

                var segmentLetters = ToString();
                if (segmentLetters == mapper.ZeroMap)
                    return 0;
                if (segmentLetters == mapper.TwoMap)
                    return 2;
                if (segmentLetters == mapper.TreeMap)
                    return 3;
                if (segmentLetters == mapper.FiveMap)
                    return 5;
                if (segmentLetters == mapper.SixMap)
                    return 6;
                if (segmentLetters == mapper.NineMap)
                    return 9;

                throw new Exception("Unknonw segment " + segmentLetters);
            }
        }

        public class Segment : IEquatable<Segment>
        {
            public char Letter { get; }

            public Segment(char input)
            {
                Letter = input;
            }

            public override string ToString()
            {
                return Letter.ToString();
            }

            public bool Equals(Segment? other)
            {
                return Letter == other?.Letter;
            }
        }
    }
}
