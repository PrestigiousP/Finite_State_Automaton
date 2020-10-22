using System.Collections.Generic;
using System.Text;

namespace TP1_Math.main
{
    /// <summary>
    /// Class <c>Grammaire</c> This class is a model for a Grammar.
    /// Author: Julien Turcotte
    /// </summary>
    public class Grammar
    {
        //Instance variables
        public string Vocabulary { get; }
        private int[] Terminals { get; } = {0, 1};
        public string InitialState { get; }
        public List<string> Rules { get; set; }

        /// <summary>
        /// This constructor initialize the attributes of the grammar.
        /// </summary>
        /// <param name="vocabulary">The vocabulaire that will be given to the grammaire.</param>
        /// <param name="initialState">The </param>
        /// <param name="rules"></param>
        public Grammar(string vocabulary, string initialState, List<string> rules)
        {
            Vocabulary = vocabulary;
            InitialState = initialState;
            Rules = rules;
        }

        /// <summary>
        /// Delete a rule from the list of rules.
        /// </summary>
        /// <param name="rule">The rule to delete from the list.</param>
        public void RemoveRules(string rule)
        {
            Rules.Remove(rule);
        }

        /// <summary>
        /// Override the ToString method.
        /// </summary>
        /// <returns>The string returned while calling this method</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Rules.ForEach(r => { sb.Append(r + ", "); });

            sb.Remove(sb.Length - 2, 2);
            string terminal = $"{Terminals[0]}, {Terminals[1]}";
            return "G = {V, T, S, R}" + "\nV = {" + Vocabulary + "}\nT = {" + terminal + "}\nS = {" + InitialState +
                   "}\nR = {" + sb.ToString() + "}";
        }
    }
}