using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day1
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            List<int> inputs = input.SplitAndConvertToIntByNewLine();

            int increasedCounter = 0;

            for(int i = 0; i < inputs.Count - 1; i++)
            {
                int num1 = inputs[i];
                int num2 = inputs[i + 1];
                if (num1 < num2)
                {
                    increasedCounter++;
                }
            }

            return increasedCounter.ToString();
        }

        public string SolveDayStar2(string input)
        {
            List<int> inputs = input
                .SplitAndConvertToIntByNewLine();

            List<int> newInput = new List<int>();

            for (int i = 0; i < inputs.Count - 2; i++)
            {
                int num1 = inputs[i];
                int num2 = inputs[i + 1];
                int num3 = inputs[i + 2];
                int newNum = num1 + num2 + num3;
                newInput.Add(newNum);
            }

            return SolveDayStar1(string.Join(Environment.NewLine, newInput));
        }
    }
}
