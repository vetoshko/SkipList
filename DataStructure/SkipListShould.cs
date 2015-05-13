using System.Management.Instrumentation;
using NUnit.Framework;

namespace DataStructure
{
    [TestFixture]
    class SkipListShould
    {
        [Test]
        public void AddOneElement()
        {
            var myList = new SkipList<int, int>(0.5);
            myList.Insert(1, 2);
            Assert.AreEqual(2, myList.Search(1));
        }


        [Test]
        public void ReplaceValue()
        {
            var myList = new SkipList<int, int>(0.5);
            myList.Insert(3, 4);
            myList.Insert(3, 5);
            Assert.AreEqual(5, myList.Search(3));
        }

        [Test]
        public void RemoveElement()
        {
            var myList = new SkipList<int, int>(0.5);
            myList.Insert(1, 2);
            myList.Insert(3, 4);
            Assert.AreEqual(2, myList.Remove(1));
        }

        [Test]
        [ExpectedException]
        public void ThrowExceptionNothingToRemove()
        {
            var myList = new SkipList<int, int>(0.5);
            myList.Insert(1, 2);
            myList.Insert(3, 4);
            Assert.AreEqual(typeof(InstanceNotFoundException), myList.Search(1));
        }
    }
}
