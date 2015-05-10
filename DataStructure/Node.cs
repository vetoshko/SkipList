using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{
    public class Node<TK, TV> 
        where TK : IComparable
        where TV : IComparable
    {
        public TK Key;
        public TV Value;
        public Node<TK, TV>[] Next;
        public Node(TK key,TV value, int level)
        {
            Key = key;
            Value = value;
            Next = new Node<TK, TV>[level];
        }
    }
}
