using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace AdventOfCode2021.Day1
{
    public class DayUnitTest1
    {
        [Fact]
        public void Star1_Test_1()
        {
            // Arrange
            string input =
@"199
200
208
210
200
207
240
269
260
263";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("7", result); 
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
            Assert.True(true);
            Debug.WriteLine(result);
        }

        [Fact]
        public void Star2_Test_1()
        {
            // Arrange
            string input =
@"199
200
208
210
200
207
240
269
260
263";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("5", result);
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
            Assert.True(true);
            Debug.WriteLine(result);
        }
    }
}