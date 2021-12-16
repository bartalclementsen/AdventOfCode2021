using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode2021.Day16
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

        [Fact]
        public void Star1_Test_2()
        {
            // Arrange
            string input = @"38006F45291200";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("9", result);
        }

        [Fact]
        public void Star1_Test_3()
        {
            // Arrange
            string input = @"EE00D40C823060";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("14", result);
        }

        [Fact]
        public void Star1_Test_4()
        {
            // Arrange
            string input = @"8A004A801A8002F478";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("16", result);
        }

        [Fact]
        public void Star1_Test_5()
        {
            // Arrange
            string input = @"620080001611562C8802118E34";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("12", result);
        }

        [Fact]
        public void Star1_Test_6()
        {
            // Arrange
            string input = @"C0015000016115A2E0802F182340";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("23", result);
        }

        [Fact]
        public void Star1_Test_7()
        {
            // Arrange
            string input = @"A0016C880162017C3686B18A3D4780";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar1(input);

            // Assert
            Assert.Equal("31", result);
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
            Assert.Equal("986", result);
        }

        [Fact]
        public void Star2_Test_1()
        {
            // Arrange
            string input = @"C200B40A82";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("3", result);
        }

        [Fact]
        public void Star2_Test_2()
        {
            // Arrange
            string input = @"04005AC33890";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("54", result);
        }

        [Fact]
        public void Star2_Test_3()
        {
            // Arrange
            string input = @"880086C3E88112";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("7", result);
        }

        [Fact]
        public void Star2_Test_4()
        {
            // Arrange
            string input = @"CE00C43D881120";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("9", result);
        }

        [Fact]
        public void Star2_Test_5()
        {
            // Arrange
            string input = @"D8005AC2A8F0";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("1", result);
        }

        [Fact]
        public void Star2_Test_6()
        {
            // Arrange
            string input = @"F600BC2D8F";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("0", result);
        }

        [Fact]
        public void Star2_Test_7()
        {
            // Arrange
            string input = @"9C005AC2F8F0";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("0", result);
        }

        [Fact]
        public void Star2_Test_8()
        {
            // Arrange
            string input = @"9C0141080250320F1802104A08";

            Solver solver = new();

            // Act
            var result = solver.SolveDayStar2(input);

            // Assert
            Assert.Equal("1", result);
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
            Assert.Equal("18234816469452", result);
        }
    }
}