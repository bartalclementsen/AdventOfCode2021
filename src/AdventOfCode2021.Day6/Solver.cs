using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2021.Day6
{
    internal class Solver
    {
        public string SolveDayStar1(string input)
        {
            //creates a new lanternfish once every 7 days
            World world = new(input);

            int daysToSimulate = 80;
            for(int day = 0; day < daysToSimulate; day++)
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

            public BlockingCollection<LanterFish> LanterFishes { get; }

            public World(string input)
            {
                var lanterFishes = input
                    .Split(",")
                    .Select(o => new LanterFish(int.Parse(o)))
                    .ToList();

                LanterFishes = new BlockingCollection<LanterFish>();
                foreach (var fish in lanterFishes)
                {
                    LanterFishes.Add(fish);
                }
            }

            public void IncrementDay()
            {
                CurrentDay++;

                Parallel.ForEach(LanterFishes, (lanterFish) =>
                {
                    if(lanterFish.TickAndSpawnIfPossibleAndReset(out var spawnedFish))
                    {
                        LanterFishes.Add(spawnedFish!);
                    }
                });
            }

            internal int GetTotalLanterFishes()
            {
                return LanterFishes.Count();
            }

            public override string ToString()
            {
                return $"After {CurrentDay.ToString("00")} days: {string.Join(",", LanterFishes.Select(lf => lf.CurrentRefractoryPeriod))}";
            }
        }

        public class LanterFish
        {
            public const int RefractoryPeriod = 6;

            public int CurrentRefractoryPeriod { get; private set; }

            public LanterFish(int currentRefractoryPeriod)
            {
                CurrentRefractoryPeriod = currentRefractoryPeriod;
            }

            public bool TickAndSpawnIfPossibleAndReset(out LanterFish? lanterFish)
            {
                CurrentRefractoryPeriod--;

                if (CurrentRefractoryPeriod < 0)
                {
                    CurrentRefractoryPeriod = RefractoryPeriod;
                    lanterFish = new LanterFish(8);
                    return true;
                }

                lanterFish = null;
                return false;
            }
        }

    }
}
