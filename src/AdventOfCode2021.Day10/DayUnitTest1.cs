using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day10
{
    public class DayUnitTest1
    {
        private readonly ITestOutputHelper output;

        public DayUnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("()")]
        [InlineData("[]")]
        [InlineData("([])")]
        [InlineData("{()()()}")]
        [InlineData("<([{}])>")]
        [InlineData("[<>({}){}[([])<>]]")]
        [InlineData("(((((((((())))))))))")]
        public void Star1_Test_1(string input)
        {
            // Arrange
            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("0", result);
        }

        [Theory]
        [InlineData("(]")]
        [InlineData("{()()()>")]
        [InlineData("(((()))}")]
        [InlineData("<([]){()}[{}])")]
        public void Star1_Test_2(string input)
        {
            // Arrange
            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.NotEqual("0", result);
        }

        [Fact]
        public void Star1_Test_3()
        {
            // Arrange
            string input =
@"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("26397", result);
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
            Assert.Equal("316851", result);
        }



        [Fact]
        public void Star2_Test_3()
        {
            // Arrange
            string input =
@"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("288957", result);
        }

        [Fact]
        public void Star2_Solve()
        {
            // Arrange
            string input = File.ReadAllText("data1.txt");

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            output.WriteLine(result);
            Assert.Equal("2182912364", result); // 63101831 to low

            // 2288035482 to high
        }
    }
}