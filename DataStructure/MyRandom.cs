using System;

namespace DataStructure
{
    class MyRandom : IRandomGenerator
    {

        public int ChooseElementLevel(int level)
        {
            return level;
        }

        public int ChooseElementLevel(double probability, int maxLevel)
        {
            var random = new Random();
            var level = 0;
            var rand = random.NextDouble();
            while (rand < probability && level < maxLevel - 1)
            {
                level++;
                rand = random.NextDouble();
            }
            return level;
        }
    }
}
