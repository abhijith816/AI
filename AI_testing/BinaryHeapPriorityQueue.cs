
using System;
using System.Collections;
using System.Collections.Generic;

namespace AI_testing
{
    public class MyComparerNew :IComparer<State>
    {
        bool selectHighestGValue = true;
        public int Compare(State curState, State targetState)
        {
            if (curState.fValue != targetState.fValue)
                return curState.fValue - targetState.fValue;
            else if (curState.fValue == targetState.fValue && curState.gValue != targetState.gValue)
            {
                //Chosing the one with highest gValue
                if (selectHighestGValue)
                    return (curState.fValue * 202) - curState.gValue - (targetState.fValue * 202) - targetState.gValue;
                else
                    return curState.gValue - targetState.gValue;

            }
            else
                return curState.hValue - targetState.hValue;
        }

    }

   
    public class BinaryHeap<TPriority, TState> : ICollection<Tuple<TPriority, TState>>
    {
        private List<Tuple<TPriority, TState>> binaryheappq;
        private IComparer<State> minComparernew = new MyComparerNew();


        public BinaryHeap()
        {
            binaryheappq = new List<Tuple<TPriority, TState>>();
        }

        public BinaryHeap(int capacity)            
        {
            binaryheappq = new List<Tuple<TPriority, TState>>(capacity);
        }

        public BinaryHeap(IEnumerable<Tuple<TPriority, TState>> data)
        {
            binaryheappq = new List<Tuple<TPriority, TState>>(data);
            for (int pos = binaryheappq.Count / 2 - 1; pos >= 0; pos--)
                HeapifyDown(pos);
        }

       
        public void Enqueue(TPriority priority, TState value)
        {
            Insert(priority, value);
        }

        public Tuple<TPriority, TState> Remove()
        {
            if (!IsEmpty)
            {
                Tuple<TPriority, TState> result = binaryheappq[0];
                DeleteMin();
                return result;
            }
            else
                throw new InvalidOperationException("Priority queue is empty");
        }
        public TState RemoveAndReturnValue()
        {
            return Remove().Item2;
        }

        public Tuple<TPriority, TState> Peek()
        {
            if (!IsEmpty)
                return binaryheappq[0];
            else
                throw new InvalidOperationException("Priority queue is empty");
        }

        public TPriority PeekValue()
        {
            return Peek().Item1;
        }

        public bool IsEmpty
        {
            get { return binaryheappq.Count == 0; }
        }

        private void Swap(int pos1, int pos2)
        {
            Tuple<TPriority, TState> val = binaryheappq[pos1];
            binaryheappq[pos1] = binaryheappq[pos2];
            binaryheappq[pos2] = val;
        }

        private void Insert(TPriority priority, TState value)
        {
            Tuple<TPriority, TState> val = new Tuple<TPriority, TState>(priority, value);
            binaryheappq.Add(val);
            HeapifyUp(binaryheappq.Count - 1);
        }


        private int HeapifyUp(int pos)
        {
            if (pos >= binaryheappq.Count) return -1;

            while (pos > 0)
            {
                int parentPos = (pos - 1) / 2;

                //State curState = new State();
                //State targetState = new State();
                //object o = binaryheappq[parentPos].Item2;
                //curState = (State)o;                
                //object c = binaryheappq[pos].Item2;
                //targetState = (State)c;

                //if (minComparer.Compare(Convert.ToInt32(binaryheappq[parentPos].Item1), Convert.ToInt32(binaryheappq[pos].Item1)) > 0)
                if (minComparernew.Compare((State)((object)(binaryheappq[parentPos].Item2)), (State)((object)(binaryheappq[pos].Item2))) > 0)
                {
                    Swap(parentPos, pos);
                    pos = parentPos;
                }
                else break;
            }
            return pos;
        }


        private void DeleteMin()
        {
            if (binaryheappq.Count <= 1)
            {
                binaryheappq.Clear();
                return;
            }

            binaryheappq[0] = binaryheappq[binaryheappq.Count - 1];
            binaryheappq.RemoveAt(binaryheappq.Count - 1);
            HeapifyDown(0);
        }

        private void HeapifyDown(int pos)
        {
            if (pos >= binaryheappq.Count) return;
            while (true)
            {
                int smallest = pos;
                int left = 2 * pos + 1;
                int right = 2 * pos + 2;

                //State smallestState = new State();
                //State leftState = new State();
                //State rightState = new State();
                //object o = binaryheappq[smallest].Item2;
                //smallestState = (State)o;
                //object c = binaryheappq[left].Item2;
                //leftState = (State)c;
                //object rightObject = binaryheappq[right].Item2;
                //rightState = (State)rightObject;


                if (left < binaryheappq.Count && minComparernew.Compare((State)((object)(binaryheappq[smallest].Item2)), (State)((object)(binaryheappq[left].Item2))) > 0)
                    smallest = left;
                if (right < binaryheappq.Count && minComparernew.Compare((State)((object)(binaryheappq[smallest].Item2)), (State)((object)(binaryheappq[right].Item2))) > 0)
                    smallest = right;

                if (smallest != pos)
                {
                    Swap(smallest, pos);
                    pos = smallest;
                }
                else break;
            }
        }
        public void Add(Tuple<TPriority, TState> item)
        {
            Enqueue(item.Item1, item.Item2);
        }

        public void Clear()
        {
            binaryheappq.Clear();
        }

        public bool Contains(Tuple<TPriority, TState> item)
        {
            return binaryheappq.Contains(item);
        }

        public int Count
        {
            get { return binaryheappq.Count; }
        }
        public void CopyTo(Tuple<TPriority, TState>[] array, int arrayIndex)
        {
            binaryheappq.CopyTo(array, arrayIndex);
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
        public bool Remove(Tuple<TPriority, TState> item)
        {
            int elementIdx = binaryheappq.IndexOf(item);
            if (elementIdx < 0) return false;

            binaryheappq[elementIdx] = binaryheappq[binaryheappq.Count - 1];
            binaryheappq.RemoveAt(binaryheappq.Count - 1);
            int newPos = HeapifyUp(elementIdx);
            if (newPos == elementIdx)
                HeapifyDown(elementIdx);

            return true;
        }

        public IEnumerator<Tuple<TPriority, TState>> GetEnumerator()
        {
            return binaryheappq.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public List<TState> printHeap()
        {
            List<TState> output = new List<TState>();

            foreach (Tuple<TPriority, TState> kvp in binaryheappq)            
                output.Add(kvp.Item2);            

            return output;
        }

    }
}
