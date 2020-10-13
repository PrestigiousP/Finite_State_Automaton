using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace TP1_Math
{
    class StateTable
    {
        private Grammaire _grammaire;
        private Dictionary<string, StateTransition> _dictionnary;

        public StateTable(Grammaire grammaire)
        {
            _grammaire = grammaire;
            _dictionnary = new Dictionary<string, StateTransition>();
        }
        //CELA VA PERMETTRE DE EVENTUELLEMENT TRANSFORMER UN AUTOMATE NON-DETERMINISTE A UN DETERMINISTE
        public void CreateNDFAStateTable()
        {
            List<string> ruleList = _grammaire.Regles;
            Regex rx = new Regex("^[0-1]{1}[A-Z]{1}$");

            string[] charSet = new string[128];

            foreach (string r in ruleList)
            {
                int getTerminal = r.Contains("0") ? 0 : 1;
                string[] split = r.Split("->");
                char currentState = split[0][0];
                string nextState = split[1];
                if (rx.IsMatch(nextState)) nextState = nextState.Substring(1);
                else
                {
                    charSet[currentState] = charSet[currentState] == null ? nextState : (charSet[currentState] + $" {nextState}");
                    continue;
                }

                if (_dictionnary.ContainsKey(split[0]))
                {
                    _dictionnary[split[0]].SetNextState(getTerminal, nextState);
                }
                else
                {
                    StateTransition stateTransition = new StateTransition();
                    stateTransition.SetNextState(getTerminal, nextState);
                    _dictionnary.Add(split[0], stateTransition);
                }

            }
            AddFinalState(charSet);
            //Just for debugging
            foreach (var kvp in _dictionnary)
            {
                string zero = "", one = "";
                foreach (var s in kvp.Value.NextState[0])
                {
                    zero += s + "||";
                }

                foreach (var s in kvp.Value.NextState[1])
                {
                    one += s + "||";
                }

                bool final = kvp.Value.IsFinalState;


                Console.WriteLine("Key = {0}, terminal0 = {1}, terminal1 = {2}, final = {3}", kvp.Key, zero, one, final);
            }

        }

        private void AddFinalState(IReadOnlyList<string> charSet)
        {
            foreach (var kvp in _dictionnary)
            {
                //Chek if the key had a terminal state.(ex: A->1) Then mark the state that also has the terminal as final(Ex: A->1B: B will be mark as a final state)
                if (charSet[kvp.Key[0]] != null && charSet[kvp.Key[0]].Contains("0"))
                {
                    //Allow to set each 
                    foreach (var s in kvp.Value.NextState[0])
                    {
                        _dictionnary[s].IsFinalState = true;
                    }
                }
                if (charSet[kvp.Key[0]] != null && charSet[kvp.Key[0]].Contains("1"))
                {
                    //Allow to set each 
                    foreach (var s in kvp.Value.NextState[1])
                    {
                        _dictionnary[s].IsFinalState = true;
                    }
                }
                if (charSet[kvp.Key[0]] != null && charSet[kvp.Key[0]].Contains('e'))
                {
                    _dictionnary[kvp.Key].IsFinalState = true;
                }
            }
        }
        //TODO
        public void CnvertToDFATable()
        {

        }
    }
}
