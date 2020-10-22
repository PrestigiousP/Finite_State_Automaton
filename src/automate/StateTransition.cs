using System.Collections.Generic;

namespace TP1_Math.automate
{
    /// <summary>
    /// Class <c>StateTransition</c> This class is a model for what consist a state transition.
    /// </summary>
    class StateTransition
    {
        //Instance variables
        public LinkedList<string>[] NextState { get; }
        public bool IsFinalState { get; set; }

        /// <summary>
        /// This constructor initialize a new state transition. It contains an array of linked list which contains the two
        /// terminal/input(0 or 1) and if the transition is a final state.
        /// </summary>
        public StateTransition()
        {
            NextState = new[] {new LinkedList<string>(), new LinkedList<string>()};
            IsFinalState = false;
        }

        /// <summary>
        /// This method allow to set the next state to the linklist with a terminal and a state.
        /// </summary>
        /// <param name="terminalValue">the input/terminal(0 and 1).</param>
        /// <param name="nextState">The next marked state(A || B, ect..).</param>
        public void SetNextState(int terminalValue, string nextState)
        {
            NextState[terminalValue].AddLast(nextState);
        }
    }
}