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
        public Dictionary<string, StateTransition> _ndfaTableStructure { set; get; }

        public StateTable()
        {
            //_grammaire = grammaire;
            _ndfaTableStructure = new Dictionary<string, StateTransition>();
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
                    if (!_ndfaTableStructure.ContainsKey("SF") && nextState != "e")
                    {
                        StateTransition stateTransition = new StateTransition();
                        stateTransition.IsFinalState = true;
                        _ndfaTableStructure.Add(nextState, stateTransition);
                    }
                }

                if (_ndfaTableStructure.ContainsKey(split[0]))
                {
                    // //Si le non-terminal existe déjà et que pour un même input il se rend vers un autre état différent,
                    // //alors on le fait aller vers un état qui est la fusion des deux autres.
                    // if(_ndfaTableStructure[split[0]].NextState[Int32.Parse(nextState.Substring(0))] != null)
                    // {
                    //     lastState = _ndfaTableStructure[split[0]].GetNextState(Int32.Parse(nextState.Substring(0)));
                    //     StateTransition stateTransition = new StateTransition();
                    //     stateTransition.SetNextState(getTerminal, nextState + lastState);
                    //     _ndfaTableStructure[split[0]] = stateTransition;
                    //     listeEtatsFusionnes.Add(nextState + lastState);
                    //
                    //     //faire quelque chose
                    // }
                    if (nextState == "e") _ndfaTableStructure[split[0]].IsFinalState = true;
                    else _ndfaTableStructure[split[0]].SetNextState(getTerminal, nextState);
                }
                else
                {
                    StateTransition stateTransition = new StateTransition();
                    if (nextState == "e")
                    {
                        _ndfaTableStructure.Add(split[0], stateTransition);
                        _ndfaTableStructure[split[0]].IsFinalState = true;
                    }
                    else
                    {
                        stateTransition.SetNextState(getTerminal, nextState);
                        _ndfaTableStructure.Add(split[0], stateTransition);
                    } 
                }

            }
            //Just for debugging
            PrintTable();
        }

        //TODO
        public void ConvertToDFATable()
        {
            //Setting up the dfa table
            string depart = _grammaire.SDepart;
            StateTransition state = _ndfaTableStructure[depart];
            var listOfFinalState = GatherFinalState();
            var dfaTableStructure = new Dictionary<string, StateTransition>();

            //Go add the key of the starting input with the next states
            dfaTableStructure.Add(depart, state);
            
            //Take the terminal with the start state and then add a key
            string stateTerminalOne = Helper.ConvertListToString(dfaTableStructure[depart].NextState[0]);
            string stateTerminalTwo = Helper.ConvertListToString(dfaTableStructure[depart].NextState[1]);
            
            //If the list is not empty, then add it as a key to the dictionnary
            if (stateTerminalOne != "") dfaTableStructure.Add(stateTerminalOne, new StateTransition());
            if (stateTerminalTwo != "") dfaTableStructure.Add(stateTerminalTwo, new StateTransition());

            //Go through all the dictionnary and while in it, add other keys
            for (int i = 0; i < dfaTableStructure.Count; i++)
            {
                var kvp = dfaTableStructure.ElementAt(i);
                if (kvp.Key == depart) continue;
                string[] splitNonTerminal = kvp.Key.Split(",");
                
                //Initialize a list of string that will be converted to a full string afterward
                List<string> l0 = new List<string>();
                List<string> l1 = new List<string>();

                foreach (var key in splitNonTerminal)
                {
                    if (key == "") continue;
                    //This allows to add every state in the list so that it will be easier to check for duplicate
                    l0.AddRange((List<string>)Helper.ConvertStringToList(_ndfaTableStructure[key].NextState[0]));
                    l1.AddRange((List<string>)Helper.ConvertStringToList(_ndfaTableStructure[key].NextState[1]));
                }
                //Remove the elements that are the same in the list
                Helper.ChekDuplicates(l0);
                Helper.ChekDuplicates(l1);

                //Reconvert the list to a string so that it can be added in the dictionnary
                string firstKeyToAdd = Helper.ConvertListToString(l0);
                string secondKeyToAdd = Helper.ConvertListToString(l1);
                
                //Add those value in the current key as the next state
                dfaTableStructure[kvp.Key].SetNextState(0, firstKeyToAdd);
                dfaTableStructure[kvp.Key].SetNextState(1, secondKeyToAdd);
                
                //Set the final state
                listOfFinalState.ForEach(element =>
                {
                    if (kvp.Key.Contains(element)) dfaTableStructure[kvp.Key].IsFinalState = true;
                });  
                
                //Look if there are similar keys, if not, add the string(firstKeyToAdd, secondKeyToAdd) to the dictionnary
                if (!dfaTableStructure.ContainsKey(firstKeyToAdd) && l0.Count > 0)
                {
                    dfaTableStructure.Add(firstKeyToAdd, new StateTransition());
                }
                if (!dfaTableStructure.ContainsKey(secondKeyToAdd) && l1.Count > 0)
                {
                    dfaTableStructure.Add(secondKeyToAdd, new StateTransition());
                }
            }
            PrintTable(dfaTableStructure);
        }

        private List<string> GatherFinalState()
        {
            List<string> list = new List<string>();
            foreach (var kvp in _ndfaTableStructure)
            {
                if (kvp.Value.IsFinalState) list.Add(kvp.Key); 
            }

            return list;
        }

        private void PrintTable()
        {
            Console.WriteLine("State\tInput(0)\tInput(1)\tIs it a finale state?");
            foreach (var kvp in _ndfaTableStructure)
            {
                string zero = "", one = "";
                foreach (var s in kvp.Value.NextState[0])
                {
                    zero += s + ",";
                }

                foreach (var s in kvp.Value.NextState[1])
                {
                    one += s + ",";
                }

                bool final = kvp.Value.IsFinalState;

                Console.WriteLine($"{kvp.Key}\t{zero}\t\t{one}\t\t{final}");
            }
        }
        
        private void PrintTable(Dictionary<string, StateTransition> dic)
        {
            Console.WriteLine("State\tInput(0)\tInput(1)\tIs it a finale state?");
            foreach (var kvp in dic)
            {
                string zero = "", one = "";
                foreach (var s in kvp.Value.NextState[0])
                {
                    zero += s + ",";
                }

                foreach (var s in kvp.Value.NextState[1])
                {
                    one += s + ",";
                }

                bool final = kvp.Value.IsFinalState;

                Console.WriteLine($"{kvp.Key}\t{zero}\t\t{one}\t\t{final}");
            }
        }
    }
}
