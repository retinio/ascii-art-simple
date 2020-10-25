using System;
using System.Collections.Generic;
using System.Threading;

namespace AsciiArtSimple.Pipelines.MultiThreaded.Structs
{
    public class PriorityQueue : IDisposable
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly List<Node> _queue = new List<Node>();
        private readonly bool _isMinPriorityQueue;
        private int _heapSize = -1;
        
        private class Node
        {
            public int Priority { get; set; }
            public string Line { get; set; }
        }

        public int Count => _queue.Count;

        /// <summary>
        /// If min queue or max queue
        /// </summary>
        /// <param name="isMinPriorityQueue"></param>
        public PriorityQueue(bool isMinPriorityQueue = false)
        {
            _isMinPriorityQueue = isMinPriorityQueue;
        }

        /// <summary>
        /// Adds an line with prioroty to the queue
        /// </summary>
        /// <param name="priority">Prority of line</param>
        /// <param name="line">Converted line</param>
        public void Enqueue(int priority, string line)
        {
            _lock.EnterWriteLock();

            try
            {
                var node = new Node { Priority = priority, Line = line };
                _queue.Add(node);
                _heapSize++;

                if (_isMinPriorityQueue)
                    BuildHeapMin(_heapSize);
                else
                    BuildHeapMax(_heapSize);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Get priority of peek element of queue
        /// </summary>
        /// <returns></returns>
        public int PeakPriority()
        {
            _lock.EnterReadLock();

            try
            {
                if (_heapSize <= -1) return -1;
                var returnVal = _queue[0];
                return returnVal.Priority;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        /// <summary>
        ///  Remove and return the line from the queue with max (min) priority
        /// </summary>
        /// <returns></returns>
        public string Dequeue()
        {
            _lock.EnterWriteLock();

            try
            {
                if (_heapSize <= -1) return null;

                var returnVal = _queue[0].Line;
                _queue[0] = _queue[_heapSize];
                _queue.RemoveAt(_heapSize);
                _heapSize--;
                    
                if (_isMinPriorityQueue)
                    MinHeapify(0);
                else
                    MaxHeapify(0);

                return returnVal;

            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        private void MinHeapify(int i)
        {
            var left = ChildLeft(i);
            var right = ChildRight(i);

            var lowest = i;

            if (left <= _heapSize && _queue[lowest].Priority > _queue[left].Priority)
                lowest = left;
            if (right <= _heapSize && _queue[lowest].Priority > _queue[right].Priority)
                lowest = right;

            if (lowest == i) return;

            Swap(lowest, i);
            MinHeapify(lowest);
        }

        private void MaxHeapify(int i)
        {
            var left = ChildLeft(i);
            var right = ChildRight(i);

            var heighst = i;

            if (left <= _heapSize && _queue[heighst].Priority < _queue[left].Priority)
                heighst = left;
            if (right <= _heapSize && _queue[heighst].Priority < _queue[right].Priority)
                heighst = right;

            if (heighst == i) return;

            Swap(heighst, i);
            MaxHeapify(heighst);
        }

        private void BuildHeapMax(int i)
        {
            while (i >= 0 && _queue[(i - 1) / 2].Priority < _queue[i].Priority)
            {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        private void BuildHeapMin(int i)
        {
            while (i >= 0 && _queue[(i - 1) / 2].Priority > _queue[i].Priority)
            {
                Swap(i, (i - 1) / 2);
                i = (i - 1) / 2;
            }
        }

        private void Swap(int i, int j)
        {
            var temp = _queue[i];
            _queue[i] = _queue[j];
            _queue[j] = temp;
        }
        private int ChildLeft(int i)
        {
            return i * 2 + 1;
        }
        private int ChildRight(int i)
        {
            return i * 2 + 2;
        }

        public void Dispose()
        {
            ((IDisposable)_lock).Dispose();
        }
    }
}