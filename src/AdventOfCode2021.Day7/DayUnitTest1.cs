using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day7
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
            string input = @"16,1,2,0,4,2,7,1,2,14";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("37", result);
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
            Assert.Equal("345197", result);
        }

        [Fact]
        public void Star2_Test_1()
        {
            // Arrange
            string input = @"16,1,2,0,4,2,7,1,2,14";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("168", result);
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
            Assert.Equal("96361606", result);
        }
    }
}