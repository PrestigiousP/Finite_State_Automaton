using System;
using System.Collections.Generic;
using TP1_Math.helpers;

namespace TP1_Math.automate
{
    class ExpressionReader
    {
        private readonly string _expression;
        private readonly string _initialState;
        private readonly Dictionary<string, StateTransition> _automate;

        public ExpressionReader(string expression, Dictionary<string, StateTransition> automate, string initialState)
        {
            _expression = expression;
            _automate = automate;
            _initialState = initialState;
        }

        private bool Validate()
        {
            char[] expression = _expression.ToCharArray();

            string depart = _initialState;
            StateTransition currentState = _automate[depart];

            //Vérifie si l'entrée est vide ou non.
            if (expression.Length == 0 && _automate[depart].IsFinalState)
                return true;

            for (int i = 0; i < _expression.Length; i++)
            {
                string input = expression[i].ToString();
                var value = int.Parse(input);
                currentState = _automate[depart];
                LinkedList<string> nextState = currentState.NextState[value];

                if (nextState.Count > 0)
                {
                    depart = StringHelper.ConvertListToString(nextState);
                    if (depart == "") return false;
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

        public void CheckExpression()
        {
            try
            {
                bool check = Validate();
                string msg = check ? "Valide!" : "Non valide!";
                Console.WriteLine(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}