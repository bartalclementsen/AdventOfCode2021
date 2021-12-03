using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day3
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
@"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("198", result);
        }

        [Fact]
        public void Star1_Test_2()
        {
            // Arrange
            string input =
@"00000
11110
11111";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("30", result);
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
        }



        [Fact]
        public void Star2_Test_1()
        {
            // Arrange
            string input =
@"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("230", result);
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
        }
    }
}