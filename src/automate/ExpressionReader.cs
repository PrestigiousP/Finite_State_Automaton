using System;
using System.Collections.Generic;
using TP1_Math.helpers;

namespace TP1_Math.automate
{
    /// <summary>
    /// Class <c>ExpressionReader</c> model for an expression and how it will interact with the user.
    /// </summary>
    class ExpressionReader
    {
        //Instance variable
        private readonly string _expression;
        private readonly string _initialState;
        private readonly Dictionary<string, StateTransition> _automate;

        /// <summary>
        /// This constructor initialize a new expression to read.
        /// </summary>
        /// <param name="expression">the expression to evaluate.</param>
        /// <param name="automate">The automate that will be use to read the expression.</param>
        /// <param name="initialState">Set the initial state of the grammar.</param>
        public ExpressionReader(string expression, Dictionary<string, StateTransition> automate, string initialState)
        {
            _expression = expression;
            _automate = automate;
            _initialState = initialState;
        }

        /// <summary>
        /// This method allow to check if the expression is valide or not.
        /// </summary>
        /// <returns>A boolean that will return true if the expression is valid and false if not.</returns>
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

        /// <summary>
        /// This method execute the validation and write to the console if the automate is valid or not.
        /// </summary>
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