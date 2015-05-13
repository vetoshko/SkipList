

namespace DataStructure
{
    interface IRandomGenerator
    {
        int ChooseElementLevel(int level);
        int ChooseElementLevel(double probability, int maxLevel);
    }
}
