using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day14
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
@"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("1588", result);
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
            Assert.Equal("3247", result);
        }

        [Fact]
        public void Star2_Test_1()
        {
            // Arrange
            string input =
@"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("2188189693529", result);
        }

        //        [Fact]
        //        public void Star2_Solve()
        //        {
        //            // Arrange
        //            string input = File.ReadAllText("data1.txt");

        //            Solver solver = new();

        //            // Act
        //            var result = solver.SolveDayStar2(input);

        //            // Assert

        //            string expectedOutput =
        //@"###...##..####.#....###..#..#.####.###.
        //#..#.#..#....#.#....#..#.#..#.#....#..#
        //#..#.#......#..#....###..####.###..#..#
        //###..#.##..#...#....#..#.#..#.#....###.
        //#.#..#..#.#....#....#..#.#..#.#....#...
        //#..#..###.####.####.###..#..#.#....#...";

        //            output.WriteLine(result);
        //            Assert.Equal(expectedOutput, result);
        //        }
    }
}