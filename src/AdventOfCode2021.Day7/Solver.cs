using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Day7
{
    public static class IntExtensions
    {
        public static int Median(this IEnumerable<int> ints)
        {
            int mean = 0;
            if (ints.Count() % 2 == 0)
            {
                var num2Position = ints.Count() / 2;
                var num1Position = num2Position - 1;
                mean = (ints.ElementAt(num1Position) + ints.ElementAt(num2Position)) / 2;
            }
            else
            {
                var numPosition = ints.Count() / 2;
                mean = ints.ElementAt(numPosition);
            }
            return mean;
        }

        public static int Mean(this IEnumerable<int> ints)
        {
            return (int)Math.Round((double)ints.Sum() / ints.Count(), 0);
        }

        public static double TriangleNumber(double n)
        {
            return Math.Pow(n, 2) + n / 2;
        }
    }

    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            List<int> horizontalPositions = input.Split(",").Select(o => int.Parse(o)).OrderBy(o => o).ToList();

            int target = horizontalPositions.Median();

            int totalFuleNeeded = 0;
            foreach (var horizontalPosition in horizontalPositions)
            {
                var fuleNeeded = Math.Abs(horizontalPosition - target);
                totalFuleNeeded += fuleNeeded;
            }
            
            return totalFuleNeeded.ToString();
        }

        public string SolveDayStar2(string input)
        {
            List<int> horizontalPositions = input.Split(",").Select(o => int.Parse(o)).OrderBy(o => o).ToList();

            var min = horizontalPositions.Min();
            var max = horizontalPositions.Max();
            int minTotalFuelNeeded = int.MaxValue;

            for (int i = min; i <= max; i++)
            {
                int target = i;
                int totalFuleNeeded = 0;
                foreach (var horizontalPosition in horizontalPositions)
                {
                    var movementNeeded = Math.Abs(horizontalPosition - i);
                    var fuelNeeded = (int)((Math.Pow(movementNeeded, 2) + movementNeeded) / 2); //n'th triangle number (i.e. SUM N -> 1)
                    totalFuleNeeded += fuelNeeded;
                }
                if(totalFuleNeeded < minTotalFuelNeeded)
                {
                    minTotalFuelNeeded = totalFuleNeeded;
                }
            }

            return minTotalFuelNeeded.ToString();
        }
    }
}
