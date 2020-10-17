using System;
using System.Collections.Generic;
using System.Text;

namespace TP1_Math
{
    class StateTransition
    {
        public LinkedList<string>[] NextState { get; private set; }
        public bool IsFinalState { get; set; }

        public StateTransition()
        {
            NextState = new LinkedList<string>[2];
            NextState[0] = new LinkedList<string>();
            NextState[1] = new LinkedList<string>();
            IsFinalState = false;
        }

        public void SetNextState(int terminalValue, string nextState)
        {
            NextState[terminalValue].AddLast(nextState);
        }
    }
}
