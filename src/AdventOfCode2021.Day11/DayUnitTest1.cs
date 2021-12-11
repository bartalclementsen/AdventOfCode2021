using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day11
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
@"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("1656", result);
        }

//        [Fact]
//        public void Star1_Solve()
//        {
//            // Arrange
//            string input = File.ReadAllText("data1.txt");

//            Solver solver = new();

//            // Act
//            var result = solver.SolveDayStar1(input);

//            // Assert
//            output.WriteLine(result);
//            Assert.Equal("316851", result);
//        }



//        [Fact]
//        public void Star2_Test_1()
//        {
//            // Arrange
//            string input =
//@"[({(<(())[]>[[{[]{<()<>>
//[(()[<>])]({[<{<<[]>>(
//{([(<{}[<>[]}>{[]{[(<()>
//(((({<>}<{<{<>}{[]{[]{}
//[[<[([]))<([[{}[[()]]]
//[{[{({}]{}}([{[{{{}}([]
//{<[[]]>}<{[{[{[]{()[[[]
//[<(<(<(<{}))><([]([]()
//<{([([[(<>()){}]>(<<{{
//<{([{{}}[<[[[<>{}]]]>[]]";

//            Solver solver = new();

//            // Act
//            var result = solver.SolveDayStar2(input);

//            // Assert
//            Assert.Equal("288957", result);
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
//            Assert.Equal("2182912364", result); // 63101831 to low

//            // 2288035482 to high
//        }
    }
}