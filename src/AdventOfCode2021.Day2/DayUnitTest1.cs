using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day2
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
@"forward 5
down 5
forward 8
up 3
down 8
forward 2";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("150", result);
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
@"forward 5
down 5
forward 8
up 3
down 8
forward 2";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("900", result);
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