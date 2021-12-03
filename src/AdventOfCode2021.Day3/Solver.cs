using System;
using System.Collections;
using System.Linq;

namespace AdventOfCode2021.Day3
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            var diagnosticReports = input.SplitByNewLine();

            int length = diagnosticReports.ElementAt(0).Length;

            int[] zeorCounts = new int[length];
            int[] oneCounts = new int[length];

            foreach (var diagnosticReport in diagnosticReports)
            {
                for (int i = 0; i < length; i++)
                {
                    if (diagnosticReport[i] == '1')
                        oneCounts[i]++;
                    else
                        zeorCounts[i]++;
                }
            }

            bool[] gammaRateByte = new bool[length];
            bool[] epsilonRateByte = new bool[length];

            for (int i = 0; i < length; i++)
            {
                if (oneCounts[i] > zeorCounts[i])
                {
                    gammaRateByte[length - 1 - i] = true;
                    epsilonRateByte[length - 1 - i] = false;
                }
                else
                {
                    gammaRateByte[length - 1 - i] = false;
                    epsilonRateByte[length - 1 - i] = true;
                }
            }

            var gammaRate = GetIntFromBitArray(new BitArray(gammaRateByte));
            var epsilonRate = GetIntFromBitArray(new BitArray(epsilonRateByte));

            return (gammaRate * epsilonRate).ToString();
        }


        private int GetIntFromStringBitArray(string bitArray)
        {
            var bytes = bitArray.ToArray().Reverse().Select(o => o == '1').ToArray();
            return GetIntFromBitArray(new BitArray(bytes));

        }

        private int GetIntFromBitArray(BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }

        public string SolveDayStar2(string input)
        {
            var diagnosticReports = input.SplitByNewLine();
            int length = diagnosticReports.ElementAt(0).Length;

            var oxygenGeneratorRatingSet = diagnosticReports.ToList();
            for (int bitCriteria = 0; bitCriteria < length; bitCriteria++)
            {
                int zeorCounts = 0;
                int oneCounts = 0;

                foreach (var oxygenGeneratorRating in oxygenGeneratorRatingSet)
                {
                    if (oxygenGeneratorRating[bitCriteria] == '1')
                        oneCounts++;
                    else
                        zeorCounts++;
                }

                if (oneCounts < zeorCounts)
                {
                    oxygenGeneratorRatingSet.RemoveAll(o => o[bitCriteria] == '1');
                }
                else
                {
                    oxygenGeneratorRatingSet.RemoveAll(o => o[bitCriteria] == '0');
                }

                if (oxygenGeneratorRatingSet.Count == 1)
                    break;
            }

            var co2ScrubberRatingSet = diagnosticReports.ToList();
            for (int bitCriteria = 0; bitCriteria < length; bitCriteria++)
            {
                int zeorCounts = 0;
                int oneCounts = 0;

                foreach (var co2ScrubberRating in co2ScrubberRatingSet)
                {
                    if (co2ScrubberRating[bitCriteria] == '1')
                        oneCounts++;
                    else
                        zeorCounts++;
                }

                if (oneCounts < zeorCounts)
                {
                    co2ScrubberRatingSet.RemoveAll(o => o[bitCriteria] == '0');
                }
                else
                {
                    co2ScrubberRatingSet.RemoveAll(o => o[bitCriteria] == '1');
                }

                if (co2ScrubberRatingSet.Count == 1)
                    break;
            }

            var oxygenGeneratorRatingArray = GetIntFromStringBitArray(oxygenGeneratorRatingSet[0]);
            var co2ScrubberRatingArray = GetIntFromStringBitArray(co2ScrubberRatingSet[0]);

            return (oxygenGeneratorRatingArray * co2ScrubberRatingArray).ToString();
        }

        /* ----------------------------------------------------------------------------  */
        /*                               INTERNAL CLASSES                                */
        /* ----------------------------------------------------------------------------  */
        public class DiagnosticReport
        {

        }
    }
}
