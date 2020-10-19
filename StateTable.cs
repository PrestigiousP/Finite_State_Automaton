using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace TP1_Math
{
    class StateTable
    {
        private Grammaire _grammaire;
        public Dictionary<string, StateTransition> _tableStructure { set; get; }

        public StateTable()
        {
            //_grammaire = grammaire;
            _tableStructure = new Dictionary<string, StateTransition>();
        }

        public void SetGrammar(Grammaire gram)
        {
            _grammaire = gram;
        }

        public Grammaire GetGrammar()
        {
            return _grammaire;
        }
        //CELA VA PERMETTRE DE EVENTUELLEMENT TRANSFORMER UN AUTOMATE NON-DETERMINISTE A UN DETERMINISTE
        public void CreateNDFAStateTable()
        {
            List<string> ruleList = _grammaire.Regles;
            List<string> listeEtatsFusionnes = new List<string>();
            Regex rx = new Regex("^[0-1]{1}[A-Z]{1}$");

            foreach (string r in ruleList)
            {
                Console.WriteLine(r);

            }


            foreach (string r in ruleList)
            {
                int getTerminal = r.Contains("0") ? 0 : 1;
                string[] split = r.Split("->");
                string nextState = split[1];
                string lastState;

                if (rx.IsMatch(nextState))
                {
                    nextState = nextState.Substring(1);
                    Console.WriteLine(nextState);
                    Console.WriteLine(split[1]);
                    //Console.WriteLine(nextState.Substring(1));
                }
                else
                {
                    if (nextState != "e") nextState = "SF";
                    if (!_tableStructure.ContainsKey("SF") && nextState != "e")
                    {
                        StateTransition stateTransition = new StateTransition();
                        stateTransition.IsFinalState = true;
                        _tableStructure.Add(nextState, stateTransition);
                    }
                }

                if (_tableStructure.ContainsKey(split[0]))
                {
                    //Si le non-terminal existe déjà et que pour un même input il se rend vers un autre état différent,
                    //alors on le fait aller vers un état qui est la fusion des deux autres.
                    if(_tableStructure[split[0]].NextState[Int32.Parse(nextState.Substring(0))] != null)
                    {
                        lastState = _tableStructure[split[0]].GetNextState(Int32.Parse(nextState.Substring(0)));
                        StateTransition stateTransition = new StateTransition();
                        stateTransition.SetNextState(getTerminal, nextState + lastState);
                        _tableStructure[split[0]] = stateTransition;
                        listeEtatsFusionnes.Add(nextState + lastState);

                        //faire quelque chose
                    }

                    if (nextState == "e") _tableStructure[split[0]].IsFinalState = true;
                    else _tableStructure[split[0]].SetNextState(getTerminal, nextState);
                }
                else
                {
                    StateTransition stateTransition = new StateTransition();
                    if (nextState == "e")
                    {
                        _tableStructure.Add(split[0], stateTransition);
                        _tableStructure[split[0]].IsFinalState = true;
                    }
                    else
                    {
                        stateTransition.SetNextState(getTerminal, nextState);
                        _tableStructure.Add(split[0], stateTransition);
                    } 
                }

            }
            //Just for debugging
            printTable();
        }

        //TODO
        public void CnvertToDFATable()
        {
            string depart = _grammaire.SDepart;
            string suite = depart;
            string state1;
            string state2;
            StateTransition state = _tableStructure[depart];
            Dictionary<string, StateTransition> newTable = null;
            StateTransition newState = null;
            //LinkedList<string> nextState = state.NextState;
            for(int i = 0; i < _tableStructure.Count; i++)
            {
                int j = 0;
                while(j < 2)
                {
                    if (state.NextState[j].Count > 1)
                    {
                        state1 = state.NextState[j].ElementAt(0);
                        state2 = state.NextState[j].ElementAt(1);

                        if (_tableStructure[state1].IsFinalState || _tableStructure[state2].IsFinalState)
                            newState.IsFinalState = true;

                        newState.SetNextState(j, state1 + state2);
                        newTable[suite] = newState;
                    }
                    else if(state.NextState[j].Count == 1)
                    {
                        newState.SetNextState(j, state.NextState[j].ElementAt(0));
                        newTable[suite] = newState;
                    }

                    j++;
                }

                suite = _tableStructure[suite].NextState[0].ElementAt(0);

            }
        }

        public void printTable()
        {
            Console.WriteLine("State\tInput(0)\tInput(1)\tIs it a finale state?");
            foreach (var kvp in _tableStructure)
            {
                string zero = "", one = "";
                foreach (var s in kvp.Value.NextState[0])
                {
                    zero += s;
                }

                foreach (var s in kvp.Value.NextState[1])
                {
                    one += s;
                }

                bool final = kvp.Value.IsFinalState;

                Console.WriteLine($"{kvp.Key}\t{zero},\t\t{one},\t\t{final}");
            }
        }
    }
}
