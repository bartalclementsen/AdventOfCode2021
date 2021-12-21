using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day21
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
            string input = @"Player 1 starting position: 4
Player 2 starting position: 8";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("739785", result);
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
            Assert.Equal("906093", result);
        }

        [Fact]
        public void Star2_Test_1()
        {
            // Arrange
            string input = @"Player 1 starting position: 4
Player 2 starting position: 8";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("444356092776315", result);
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
            Assert.Equal("274291038026362", result);
        }
    }
}