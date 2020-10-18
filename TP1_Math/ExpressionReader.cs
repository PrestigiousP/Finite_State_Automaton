using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP1_Math
{
    class ExpressionReader
    {
        public string Expression;
        public Dictionary<string, StateTransition> Regles;
        public ExpressionReader(string expression, StateTable state)
        {
            Expression = expression;
            Regles = state._tableStructure;
        }

        public bool Validate()
        {
            char[] expression = Expression.ToCharArray();
            
            //char index;
            string currentState = "S";
            string input;
            int inp;
            List<int> splitSection = new List<int>();
            List<string> etatSaved = new List<string>();
            //string firstState;
            //string secondState;
            //LinkedList<string>[] list;
            for (int i = 0; i < Expression.Length; i++)
            {
                input = expression[i].ToString();
                inp = Int32.Parse(input);

                if (Regles[currentState].IsFinalState || Expression.Length == 0)
                    return true;
                //int inputNum = Convert.ToInt32(input);
                //list = Regles[currentState].NextState;
                if (Regles[currentState].NextState[inp].Count > 1)
                {
                    if (i == expression.Length-1 && Regles[currentState].NextState[inp].ElementAt(0) == "SF" || 
                        Regles[currentState].NextState[inp].ElementAt(1) == "SF")
                    {
                        return true;
                    }
                    //save l'endroit ou il peut avoir plusieurs possibilités.

                }

                if(Regles[currentState].NextState[inp].Count > 0)
                {
                    if (Regles[currentState].NextState[inp].ElementAt(0) != "SF")
                        currentState = Regles[currentState].NextState[inp].ElementAt(0);
                    else
                    {
                        currentState = Regles[currentState].NextState[inp].ElementAt(1);
                    }
                }
                else
                {
                    return false;
                }
                //for(int j = 0; j < 2; j++)
                //{
                //    if (Regles[currentState].NextState[0].Count > 0)
                //        currentState = Regles[currentState].NextState[0].ToString();
                //    else if (Regles[currentState].NextState[1].Count > 0)
                //        currentState = Regles[currentState].NextState[1].ToString();
                //}

                    //currentState = Regles[currentState].NextState;


                    // etat1 = Regles.ElementAt(i).
            }
            if (Regles[currentState].IsFinalState)
                return true;
            return false;
        }
    }
}
