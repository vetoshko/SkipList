using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DataStructure
{
    [TestFixture]
    class SkipListShould
    {
        [Test]
        public void AddElement()
        {
            var myList = new SkipList<int, int>(0.5);
            myList.Insert(1, 2);
            Assert.AreEqual(2, myList.Search(1));
        }

        [Test]
        public void ChooseElem()
        {
            var ml = new SkipList<int,int>(0.5);
            var res = new List<int>();
            for (int i = 0; i < 10000; i++)
                res.Add(ml.ChooseElementLevel());
            Assert.True(res.All(i => i <= 31));
        }
    }
}
