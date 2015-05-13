using System;
using System.Management.Instrumentation;

namespace DataStructure
{
    public class SkipList<TK, TV>
        where TK : IComparable
        where TV : IComparable
    {
        readonly double _probability;
        private Node<TK, TV> _header;
        private readonly MyRandom _random = new MyRandom();
        private const int MaxLevel = 32;
        private static readonly Node<TK, TV> EndNode = null;


        public SkipList(double prob)
        {
            _probability = prob;
            InitializeHeader();
        }


        private void InitializeHeader()
        {
            _header = new Node<TK, TV>(default(TK), default(TV), MaxLevel);
            for (var i = 0; i < MaxLevel; i++)
            {
                _header.Next[i] = EndNode;
            }
        }

     
        public void Insert(TK key, TV value)
        {
            var level = _random.ChooseElementLevel(_probability, MaxLevel);
            var update = new Node<TK, TV>[level+1];
            var currentNode = _header;
            for (var currentLevel = level; currentLevel > -1; currentLevel--)
            {
                while (!ReferenceEquals(currentNode.Next[currentLevel], EndNode) 
                    && currentNode.Key.CompareTo(key) < 0)
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
            var newNode = new Node<TK, TV>(key, value, level+1);
            for (var currentLevel = 0; currentLevel < level+1; currentLevel++)
            {
                //Верно ли?
                newNode.Next[currentLevel] = update[currentLevel].Next[currentLevel];
                update[currentLevel].Next[currentLevel] = newNode;
            }
        }


        public TV Search(TK key)
        {
            var currentNode = _header;
            for (var currentLevel = MaxLevel-1; currentLevel > -1; currentLevel--)
            {
                while (!ReferenceEquals(currentNode.Next[currentLevel], EndNode) 
                    && currentNode.Next[currentLevel].Key.CompareTo(key) <= 0)
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


        public TV Remove(TK key)
        {
            var update = new Node<TK, TV>[MaxLevel];
            var currentNode = _header;
            for (var currentLevel = MaxLevel - 1; currentLevel > -1; currentLevel--)
            {
                while (!ReferenceEquals(currentNode.Next[currentLevel], EndNode)
                    && currentNode.Next[currentLevel].Key.CompareTo(key) < 0)
                    currentNode = currentNode.Next[currentLevel];
                update[currentLevel] = currentNode;
            }
            currentNode = currentNode.Next[0];
            if (currentNode.Key.CompareTo(key) == 0)
            {
                for (var level = 0; level < MaxLevel; level++)
                {
                    if (!ReferenceEquals(update[level].Next[level], EndNode)
                        && update[level].Next[level].Key.CompareTo(key) != 0)
                        break;
                    update[level].Next[level] = currentNode.Next[level];
                }
                return currentNode.Value;
            }
            throw new InstanceNotFoundException(String.Format("There's no element to remove with key: {0}", key));
        }
    }
}
