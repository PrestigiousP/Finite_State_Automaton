using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TP1_Math.helpers;
using TP1_Math.main;

namespace TP1_Math.automate
{
    /// <summary>Class <c>AutomateCreator</c> Model for the ndfa table and dfa table which create the
    /// model for the automate.</summary>
    ///
    class AutomateCreator
    {
        //Instance variable
        public Dictionary<string, StateTransition> Automate { get; private set; }
        private Grammar Grammar { get; }

        /// <summary>This constructor initializes a new automate based on the grammar.</summary>
        /// <param name="grammar">The grammar</param>
        public AutomateCreator(Grammar grammar)
        {
            //_grammaire = grammaire;
            Automate = new Dictionary<string, StateTransition>();
            Grammar = grammar;
        }

        /// <summary>This method create a NDFA table based on the grammar given to the constructor</summary>
        private void CreateNDFAStateTable()
        {
            Regex rx = new Regex("^[0-1]{1}[A-Z]{1}$");

            foreach (string r in Grammar.Rules)
            {
                int getTerminal = r.Contains("0") ? 0 : 1;
                string[] split = r.Split("->");
                string nextState = split[1];

                if (rx.IsMatch(nextState))
                {
                    nextState = nextState.Substring(1);
                }
                else
                {
                    if (nextState != "e") nextState = "SF";
                    if (!Automate.ContainsKey("SF") && nextState != "e")
                    {
                        StateTransition stateTransition = new StateTransition();
                        stateTransition.IsFinalState = true;
                        Automate.Add(nextState, stateTransition);
                    }
                }

                if (Automate.ContainsKey(split[0]))
                {
                    if (nextState == "e") Automate[split[0]].IsFinalState = true;
                    else Automate[split[0]].SetNextState(getTerminal, nextState);
                }
                else
                {
                    StateTransition stateTransition = new StateTransition();
                    if (nextState == "e")
                    {
                        Automate.Add(split[0], stateTransition);
                        Automate[split[0]].IsFinalState = true;
                    }
                    else
                    {
                        stateTransition.SetNextState(getTerminal, nextState);
                        Automate.Add(split[0], stateTransition);
                    }
                }
            }
        }

        /// <summary>This method create a DFA table based on the previously created NDFA table.
        /// It converts the NDFA table to a DFA table which will be usefull to read the
        /// user input more clearly</summary>
        private void ConvertToDFATable()
        {
            //Setting up the dfa table
            string initialSate = Grammar.InitialState;
            StateTransition state = Automate[initialSate];
            var listOfFinalState = GatherFinalState();
            var dfaTable = new Dictionary<string, StateTransition>();

            //Go add the key of the starting input with the next states
            dfaTable.Add(initialSate, state);

            //Take the terminal with the start state and then add a key
            string stateTerminalOne = StringHelper.ConvertListToString(dfaTable[initialSate].NextState[0]);
            string stateTerminalTwo = StringHelper.ConvertListToString(dfaTable[initialSate].NextState[1]);

            //If the list is not empty, then add it as a key to the dictionnary
            if (stateTerminalOne != "" && !dfaTable.ContainsKey(stateTerminalOne))
                dfaTable.Add(stateTerminalOne, new StateTransition());
            if (stateTerminalTwo != "" && !dfaTable.ContainsKey(stateTerminalTwo))
                dfaTable.Add(stateTerminalTwo, new StateTransition());

            //Go through all the dictionnary and while in it, add other keys
            for (int i = 0; i < dfaTable.Count; i++)
            {
                var kvp = dfaTable.ElementAt(i);
                if (kvp.Key == initialSate) continue;
                string[] splitState = kvp.Key.Split(",");

                //Initialize a list of string that will be converted to a full string afterward
                List<string> stateListTerminal0 = new List<string>();
                List<string> stateListTerminal1 = new List<string>();

                foreach (var key in splitState)
                {
                    if (key == "") continue;
                    //This allows to add every state in the list so that it will be easier to check for duplicate
                    stateListTerminal0.AddRange((List<string>) StringHelper.ConvertToList(Automate[key].NextState[0]));
                    stateListTerminal1.AddRange((List<string>) StringHelper.ConvertToList(Automate[key].NextState[1]));
                }

                //Remove the elements that are the same in the list
                StringHelper.ChekDuplicates(stateListTerminal0);
                StringHelper.ChekDuplicates(stateListTerminal1);

                //Reconvert the list to a string so that it can be added in the dictionnary
                string firstKeyToAdd = StringHelper.ConvertListToString(stateListTerminal0);
                string secondKeyToAdd = StringHelper.ConvertListToString(stateListTerminal1);

                //Add those value in the current key as the next state
                dfaTable[kvp.Key].SetNextState(0, firstKeyToAdd);
                dfaTable[kvp.Key].SetNextState(1, secondKeyToAdd);

                //Set the final state
                listOfFinalState.ForEach(element =>
                {
                    if (kvp.Key.Contains(element)) dfaTable[kvp.Key].IsFinalState = true;
                });

                //Look if there are similar keys, if not, add the string(firstKeyToAdd, secondKeyToAdd) to the dictionnary
                if (!dfaTable.ContainsKey(firstKeyToAdd) && stateListTerminal0.Count > 0)
                    dfaTable.Add(firstKeyToAdd, new StateTransition());
                if (!dfaTable.ContainsKey(secondKeyToAdd) && stateListTerminal1.Count > 0)
                    dfaTable.Add(secondKeyToAdd, new StateTransition());
            }

            Automate = dfaTable;
        }

        /// <summary>This method create a list of the state that are final in the NDFA table</summary>
        /// <returns>A list that contains the state that are finals</returns>>
        private List<string> GatherFinalState()
        {
            return (from kvp in Automate where kvp.Value.IsFinalState select kvp.Key).ToList();
        }

        /// <summary>This method execute all the tables to be able to give to the user the good automate</summary>
        public void Execute()
        {
            Console.WriteLine("Printing the non deterministic table");
            CreateNDFAStateTable();
            PrintTable();
            Console.WriteLine("-------------");
            Console.WriteLine("Printing the deterministic table");
            ConvertToDFATable();
            PrintTable();
        }

        /// <summary>This method print a table(NDFA or DFA)</summary>
        private void PrintTable()
        {
            Console.WriteLine("State\tInput(0)\tInput(1)\tIs it a finale state?");
            foreach (var kvp in Automate)
            {
                string terminal0 = "", terminal1 = "";
                terminal0 = kvp.Value.NextState[0].Aggregate(terminal0, (current, s) => current + (s + ","));
                terminal1 = kvp.Value.NextState[1].Aggregate(terminal1, (current, s) => current + (s + ","));

                if (!string.IsNullOrEmpty(terminal0))
                {
                    terminal0 = terminal0.Substring(0, terminal0.Length - 1);
                }

                if (!string.IsNullOrEmpty(terminal1))
                {
                    terminal1 = terminal1.Substring(0, terminal1.Length - 1);
                }

                bool final = kvp.Value.IsFinalState;

                Console.WriteLine($"{kvp.Key}\t{terminal0}\t\t{terminal1}\t\t{final}");
            }
        }
    }
}