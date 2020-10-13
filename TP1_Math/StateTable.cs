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
            ruleList.ForEach(r =>
            {
            int getTerminal = r.Contains("0") ? 0 : 1;
            string[] split = r.Split("->");
            string nextState = split[1];

            nextState = !rx.IsMatch(nextState) ? "FINAL" : nextState.Substring(1);

            if (_dictionnary.ContainsKey(split[0]))
            {
                    _dictionnary[split[0]].SetNextState(getTerminal, nextState);
                    if (nextState == "FINAL") _dictionnary[split[0]].IsFinalState = true;
                }
                else
                {
                    StateTransition stateTransition = new StateTransition();
                    stateTransition.SetNextState(getTerminal, nextState);

                    if (nextState == "FINAL") stateTransition.IsFinalState = true;
                    _dictionnary.Add(split[0], stateTransition);
                }
            });

            //Just for debugging
            foreach (KeyValuePair<string, StateTransition> kvp in _dictionnary)
            {
                string zero = "", one = "";
                foreach (string s in kvp.Value.NextState[0])
                {
                    zero += s + "||";
                }

                foreach (string s in kvp.Value.NextState[1])
                {
                    one += s + "||";
                }
                    
                bool final = kvp.Value.IsFinalState;
                

                Console.WriteLine("Key = {0}, terminal0 = {1}, terminal1 = {2}, final = {3}", kvp.Key, zero, one, final);
            }
        }
        //TODO
        public void CnvertToDFATable()
        {

        }
    }
}
