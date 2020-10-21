using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP1_Math
{
    class ExpressionReader
    {
        private string Expression;
        private Dictionary<string, StateTransition> Regles;
        private Grammaire Grammaire;

        public ExpressionReader(string expression, StateTable state, Grammaire grammaire)
        {
            Expression = expression;
            Regles = state._ndfaTableStructure;
            Grammaire = grammaire;
        }

        public bool Validate()
        {
            char[] expression = Expression.ToCharArray();

            //char index;
            string depart = Grammaire.SDepart;
            string input;
            int inp;
            List<int> inputIndex = new List<int>();
            List<string> embranchement = new List<string>();
            StateTransition currentState = Regles[depart];
            LinkedList<string> nextState;

            //Vérifie si l'entrée est vide ou non.
            if (expression.Length == 0 && Regles[depart].IsFinalState)
                return true;

            for (int i = 0; i < Expression.Length; i++)
            {
                input = expression[i].ToString();
                inp = Int32.Parse(input);
                string test = depart;
                currentState = Regles[depart];
                nextState = currentState.NextState[inp];

                //Vérifie si pour un seul input il y a plusieurs outputs (états).
                if (nextState.Count > 1)
                {
                    //Si on est à la fin de l'expression et qu'un des deux états qu'on peut atteindre est final.
                    if (i == Expression.Length - 1 && (nextState.ElementAt(0) == "SF" || nextState.ElementAt(1) == "SF"))
                    {
                        return true;
                    }
                    //Sinon prendre l'état non final.
                    else
                    {
                        depart = (nextState.ElementAt(0) == "SF") ? nextState.ElementAt(1) : nextState.ElementAt(0);

                    }
                }
                //Si le input ne mène qu'a un seul état.
                else if (nextState.Count > 0)
                {
                    depart = nextState.ElementAt(0);
                    currentState = Regles[depart];
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
