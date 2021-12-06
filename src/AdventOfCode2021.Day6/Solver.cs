using System.Linq;

namespace AdventOfCode2021.Day6
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            World world = new(input);

            int daysToSimulate = 80;
            for (int day = 0; day < daysToSimulate; day++)
            {
                world.IncrementDay();
            }

            return world.GetTotalLanterFishes().ToString();
        }

        public string SolveDayStar2(string input)
        {
            World world = new(input);

            int daysToSimulate = 256;
            for (int day = 0; day < daysToSimulate; day++)
            {
                world.IncrementDay();
            }

            return world.GetTotalLanterFishes().ToString();
        }

        public class World
        {
            public int CurrentDay { get; private set; } = 0;

            public long[] lanterFishesBucket = new long[9];

            public World(string input)
            {
                var lanterFishes = input
                    .Split(",")
                    .Select(o => int.Parse(o))
                    .GroupBy(o => o)
                    .ToList();

                foreach (var fish in lanterFishes)
                {
                    lanterFishesBucket[fish.Key] = fish.Count();
                }
            }

            public void IncrementDay()
            {
                CurrentDay++;

                long newFish = lanterFishesBucket[0];
                lanterFishesBucket[0] = lanterFishesBucket[1];
                lanterFishesBucket[1] = lanterFishesBucket[2];
                lanterFishesBucket[2] = lanterFishesBucket[3];
                lanterFishesBucket[3] = lanterFishesBucket[4];
                lanterFishesBucket[4] = lanterFishesBucket[5];
                lanterFishesBucket[5] = lanterFishesBucket[6];
                lanterFishesBucket[6] = lanterFishesBucket[7];
                lanterFishesBucket[7] = lanterFishesBucket[8];

                lanterFishesBucket[8] = newFish;
                lanterFishesBucket[6] += newFish;
            }

            internal long GetTotalLanterFishes()
            {
                return lanterFishesBucket.Sum();
            }
        }
    }
}
