using System;
using System.Linq;
using System.Management.Instrumentation;

namespace DataStructure
{
    public class SkipList<TK, TV>
        where TK : IComparable
        where TV : IComparable
    {
        readonly double _probability;
        private static Node<TK, TV> _header;
        private const int MaxLevel = 32;
        readonly Random _random = new Random();
        private static readonly Node<TK, TV> EndNode = null;


        public SkipList(double prob)
        {
            _probability = prob;
            InitializeHeader();
        }


        private static void InitializeHeader()
        {
            _header = new Node<TK, TV>(default(TK), default(TV), 32);
            for (int i = 0; i < 32; i++)
            {
                _header.Next[i] = EndNode;
            }
        }


        public int ChooseElementLevel()
        {
            var level = 0;
            var rand = _random.NextDouble();
            while (rand < _probability && level < MaxLevel)
            {
                level++;
                rand = _random.NextDouble();
            }
            return level;
        }

     
        public void Insert(TK key, TV value)
        {
            var level = ChooseElementLevel();
            var update = new Node<TK, TV>[level+1];
            var currentNode = _header;
            for (var currentLevel = level; currentLevel > -1; currentLevel--)
            {
                while (currentNode.Key.CompareTo(key) <= 0 && currentNode.Next[currentLevel] != EndNode)
                {
                    if (currentNode.Next[currentLevel].Key.CompareTo(key) == 0)
                    {
                        currentNode.Next[currentLevel].Value = value;
                        return;
                    }
                    currentNode = currentNode.Next[currentLevel];
                }
                update[currentLevel] = currentNode;
            }
            var newNode = new Node<TK, TV>(key, value, level);
            for (var currentLevel = 0; currentLevel < level; currentLevel++)
            {
                newNode.Next[currentLevel] = update[currentLevel];
                update[currentLevel].Next[currentLevel] = newNode;
            }
        }


        public TV Search(TK key)
        {
            var currentNode = _header;
            for (var currentLevel = MaxLevel-1; currentLevel > -1; currentLevel--)
            {
                while (currentNode.Next[currentLevel] != EndNode && currentNode.Next[currentLevel].Key.CompareTo(key) < 0)
                {
                    currentNode = currentNode.Next[currentLevel];
                }
                if (currentNode.Key.CompareTo(key) == 0)
                    return currentNode.Value;
            }
            if (currentNode.Key.CompareTo(key) == 0)
                return currentNode.Value;
            throw new InstanceNotFoundException(string.Format("There's no node with key: {0}", key));
        }
    }
}
