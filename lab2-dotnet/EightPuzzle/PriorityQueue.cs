using System;
using System.Collections.Generic;

namespace EightPuzzle
{
    internal class PriorityQueue<T>
        where T : IComparable<T>
    {
        private readonly LinkedList<T> list;

        public PriorityQueue()
        {
            list = new LinkedList<T>();
        }

        public void Add(T entity)
        {
            if (list.Count == 0)
            {
                list.AddFirst(entity);
                return;
            }

            LinkedListNode<T> currentNode = list.First;

            while (currentNode != null)
            {
                if (entity.CompareTo(currentNode.Value) >= 0)
                {
                    list.AddBefore(currentNode, entity);
                    return;
                }

                currentNode = currentNode.Next;
            }

            list.AddLast(entity);
        }

        public T TakeFirst()
        {
            LinkedListNode<T> node = list.First;

            if (node == null)
            {
                throw new InvalidOperationException("Queue is empty!");
            }

            list.RemoveFirst();
            return node.Value;
        }

        public T TakeLast()
        {
            LinkedListNode<T> node = list.Last;

            if (node == null)
            {
                throw new InvalidOperationException("Queue is empty!");
            }

            list.RemoveLast();
            return node.Value;
        }
    }
}