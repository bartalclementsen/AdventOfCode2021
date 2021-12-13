using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day13
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
@"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("17", result);
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
            Assert.Equal("729", result);
        }

        [Fact]
        public void Star2_Test_3()
        {
            // Arrange
            string input =
@"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            string expectedOutput =
@"#####
#...#
#...#
#...#
#####";

            Assert.Equal(expectedOutput, result);
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

            string expectedOutput =
@"###...##..####.#....###..#..#.####.###.
#..#.#..#....#.#....#..#.#..#.#....#..#
#..#.#......#..#....###..####.###..#..#
###..#.##..#...#....#..#.#..#.#....###.
#.#..#..#.#....#....#..#.#..#.#....#...
#..#..###.####.####.###..#..#.#....#...";

            output.WriteLine(result);
            Assert.Equal(expectedOutput, result);
        }
    }
}