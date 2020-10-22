using System.Collections.Generic;

namespace TP1_Math.automate
{
    class StateTransition
    {
        public LinkedList<string>[] NextState { get; }
        public bool IsFinalState { get; set; }

        public StateTransition()
        {
            NextState = new[] {new LinkedList<string>(), new LinkedList<string>()};
            IsFinalState = false;
        }

        public void SetNextState(int terminalValue, string nextState)
        {
            NextState[terminalValue].AddLast(nextState);
        }
        
    }
}
