using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day17
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
            string input = @"D2FE28";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("6", result);
        }


        //[Fact]
        //public void Star1_Solve()
        //{
        //    // Arrange
        //    string input = File.ReadAllText("data1.txt");

        //    Solver solver = new();

        //    // Act
        //    var result = solver.SolveDayStar1(input);

        //    // Assert
        //    output.WriteLine(result);
        //    Assert.Equal("986", result);
        //}

        //[Fact]
        //public void Star2_Test_1()
        //{
        //    // Arrange
        //    string input = @"C200B40A82";

        //    Solver solver = new();

        //    // Act
        //    var result = solver.SolveDayStar2(input);

        //    // Assert
        //    Assert.Equal("3", result);
        //}

        //[Fact]
        //public void Star2_Solve()
        //{
        //    // Arrange
        //    string input = File.ReadAllText("data1.txt");

        //    Solver solver = new();

        //    // Act
        //    var result = solver.SolveDayStar2(input);

        //    // Assert

        //    output.WriteLine(result);
        //    Assert.Equal("18234816469452", result);
        //}
    }
}