using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_testing
{
    public class State
    {        
        public int fValue
        {
            get
            {
                return  hValue + gValue;
            }
            set
            {
                fValue = hValue + gValue;
            }

        }
        public int gValue = -1;
        public int hValue = -1;

        public int rowIndex = -1;
        public int colIndex = -1;
        public bool isBlocked = false;
        public int search = 0;
        public State previousState;
        public int cost = 1;

        public State()
        { }

        public State(int i, int j, int value =0)
        {
            rowIndex = i;
            colIndex = j;
            gValue = 0;
            if (value == 0)
                isBlocked = true;
            previousState = new State();
        }

        public int getPriority()
        {
            fValue = gValue + hValue;
            return fValue;
        }

        public State tieBreaker(State a, State b)
        {
            if (a.gValue > b.gValue)
                return a;
            else
                return b;
        }

        public override bool Equals(object obj)
        {
            State cur = (State)obj;
            if (rowIndex == cur.rowIndex && colIndex == cur.colIndex)
                return true;
            else
                return false;

        }
        



    }
}
