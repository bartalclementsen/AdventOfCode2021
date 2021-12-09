using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day9
{
    public class DayUnitTest1
    {
        private readonly ITestOutputHelper output;

        public DayUnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Star1_Test_1()
        {
            // Arrange
            string input =
@"2199943210
3987894921
9856789892
8767896789
9899965678";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("15", result);
        }

        [Fact]
        public void Star1_Solve()
        {
            // Arrange
            string input = File.ReadAllText("data1.txt");

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            output.WriteLine(result);
            Assert.Equal("633", result);
        }

        [Fact]
        public void Star2_Test_1()
        {
            // Arrange
            string input =
@"2199943210
3987894921
9856789892
8767896789
9899965678";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("1134", result);
        }

        //        [Theory]
        //        [InlineData("be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb", "fdgacbe cefdb cefbgd gcbe", "8394")]
        //        [InlineData("edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec", "fcgedb cgb dgebacf gc", "9781")]
        //        [InlineData("fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef", "cg cg fdcagb cbg", "1197")]
        //        [InlineData("fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega", "efabcd cedba gadfec cb", "9361")]
        //        [InlineData("aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga", "gecf egdcabf bgf bfgea", "4873")]
        //        [InlineData("fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf", "gebdcfa ecba ca fadegcb", "8418")]
        //        [InlineData("dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf", "cefg dcbef fcge gbcadfe", "4548")]
        //        [InlineData("bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd", "ed bcgafe cdgba cbgef", "1625")]
        //        [InlineData("egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg", "gbdfcae bgc cg cgb", "8717")]
        //        [InlineData("gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc", "fgae cfgab fg bagce", "4315")]
        //        public void Star2_Test_1(string a, string b, string c)
        //        {
        //            // Arrange
        //            string input = $"{a} | {b}";

        //            Solver solver = new();

        //            // Act
        //            var result = solver.SolveDayStar2(input);

        //            // Assert
        //            Assert.Equal(c, result);
        //        }

        //        [Fact]
        //        public void Star2_Test_2()
        //        {
        //            // Arrange
        //            string input =
        //@"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
        //edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
        //fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
        //fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
        //aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
        //fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
        //dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
        //bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
        //egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
        //gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

        //            Solver solver = new();

        //            // Act
        //            var result = solver.SolveDayStar2(input);

        //            // Assert
        //            Assert.Equal("61229", result);
        //        }

        //        [Fact]
        //        public void Star2_Solve()
        //        {
        //            // Arrange
        //            string input = File.ReadAllText("data1.txt");

        //            Solver solver = new();

        //            // Act
        //            var result = solver.SolveDayStar2(input);

        //            // Assert
        //            output.WriteLine(result);
        //            Assert.Equal("973499", result);
        //        }
    }
}