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
        private readonly Dictionary<string, LinkedList<string>[]> _dictionnary;

        public StateTable(Grammaire grammaire)
        {
            _grammaire = grammaire;
            _dictionnary = new Dictionary<string, LinkedList<string>[]>();
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
                    _dictionnary[split[0]][getTerminal].AddLast(nextState);
                }
                else
                {
                    LinkedList<string>[] terminal = new LinkedList<string>[2];
                    terminal[0] = new LinkedList<string>();
                    terminal[1] = new LinkedList<string>();
                    terminal[getTerminal].AddLast(nextState);
                    _dictionnary.Add(split[0], terminal);
                }
            });

            //Just for debugging
            foreach(KeyValuePair<string, LinkedList<string>[]> kvp in _dictionnary)
            {
                string zero = "", one = "";
                foreach (string s in kvp.Value[0])
                {
                    zero += s + "||";
                }

                foreach (string s in kvp.Value[1])
                {
                    one += s + "||";
                }

                Console.WriteLine("Key = {0}, terminal0 = {1}, terminal1 = {2}", kvp.Key, zero, one);
            }
        }
        //TODO
        public void CnvertToDFATable()
        {

        }
    }
}
