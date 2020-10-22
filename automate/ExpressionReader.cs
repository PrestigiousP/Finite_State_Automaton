using System;
using System.Collections.Generic;
using TP1_Math.helpers;

namespace TP1_Math.automate
{
    class ExpressionReader
    {
        private string _expression;
        private string _initialState;
        private Dictionary<string, StateTransition> _automate;

        public ExpressionReader(string expression, Dictionary<string, StateTransition> automate, string initialState)
        {
            _expression = expression;
            _automate = automate;
            _initialState = initialState;
        }

        public bool Validate()
        {
            char[] expression = _expression.ToCharArray();
            
            string depart = _initialState;
            StateTransition currentState = _automate[depart];
            LinkedList<string> nextState;

            //Vérifie si l'entrée est vide ou non.
            if (expression.Length == 0 && _automate[depart].IsFinalState)
                return true;

            for (int i = 0; i < _expression.Length; i++)
            {
                string input = expression[i].ToString();
                var value = Int32.Parse(input);
                currentState = _automate[depart];
                nextState = currentState.NextState[value];

                // //Vérifie si pour un seul input il y a plusieurs outputs (états).
                // if (nextState.Count > 1)
                // {
                //     //Si on est à la fin de l'expression et qu'un des deux états qu'on peut atteindre est final.
                //     if (i == _expression.Length - 1 && (nextState.ElementAt(0) == "SF" || nextState.ElementAt(1) == "SF"))
                //     {
                //         return true;
                //     }
                //     //Sinon prendre l'état non final.
                //     else
                //     {
                //         depart = (nextState.ElementAt(0) == "SF") ? nextState.ElementAt(1) : nextState.ElementAt(0);
                //
                //     }
                // }
                //Si le input ne mène qu'a un seul état.
                if (nextState.Count > 0)
                {
                    depart = StringHelper.ConvertListToString(nextState);
                    currentState = _automate[depart];
                }
                else
                {
                    return false;
                }
            }
            if (currentState.IsFinalState)
                return true;
            return false;
        }
    }
}
