using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TP1_Math.main
{
    /// <summary>
    /// This method will initialize the given type for a grammar.
    /// Author: Frédérick Simard
    /// </summary>
    class GrammarInitializer
    {
        /// <summary>
        /// Initialize a grammar with the user input.
        /// </summary>
        /// <returns>A grammar wich has the same model as the grammar class.</returns>
        public static Grammar Initialize()
        {
            string vocabulary = EnterVocabulary();
            string startState = EnterInitialState(vocabulary);
            List<string> rules = EnterRules(vocabulary, startState[0]);
            var grammaire = new Grammar(vocabulary, startState, rules);
            return grammaire;
        }

        /// <summary>
        /// Initialize a Grammar object with a given predifined grammar string.
        /// </summary>
        /// <param name="input">The grammar string that need to be converted to a grammar object.</param>
        /// <returns>A Grammar object.</returns>
        public static Grammar Initialize(string input)
        {
            string[] lineSplitter = input.Split("\n");
            int[] start = new int[lineSplitter.Length];
            int[] end = new int[lineSplitter.Length];

            for (var i = 1; i < start.Length; i++)
            {
                start[i] = lineSplitter[i].IndexOf("{") + 1;
                end[i] = lineSplitter[i].IndexOf("}") - start[i];
            }

            string voc = lineSplitter[1].Substring(start[1], end[1]);
            string startState = lineSplitter[3].Substring(start[3], end[3]);
            string rules = lineSplitter[4].Substring(start[4], end[4]);

            string[] commaSplitter = rules.Split(", ");
            List<string> ruleList = commaSplitter.ToList();

            Grammar grammar = new Grammar(voc, startState, ruleList);
            return grammar;
        }

        /// <summary>
        /// This method allows to enter a vocabulary.
        /// </summary>
        /// <returns>The string that corresponds to the vocabulary.</returns>
        private static string EnterVocabulary()
        {
            int valid = 0;
            string voc = "";
            Regex rx = new Regex("^[A-Z0-1]{1}$", RegexOptions.Compiled);

            while (valid == 0)
            {
                Console.Write("Veuillez rentrer le vocabulaire de la grammaire (Ex. : A, B, C, 1, 0) ");
                voc = Console.ReadLine();
                string[] splitter = voc.Split(", ");
                int matchCount = splitter.Count(s => rx.IsMatch(s));
                if (matchCount == splitter.Length)
                {
                    valid = (!voc.Contains("0") || !voc.Contains("1")) ? 0 : 1;
                    if (valid == 0) Console.WriteLine("Les symboles d'entrer n'ont pas ete specifie");
                }
                else
                {
                    Console.WriteLine("Veuillez entrer le format correctement");
                }
            }

            Console.WriteLine("Bonne entrée");
            return voc;
        }

        /// <summary>
        /// This method allow to user to enter the initial state.
        /// </summary>
        /// <param name="vocabulary">A vocabulary that will be use to check if the user input is correct.</param>
        /// <returns>Return the string corresponding to the initial state </returns>
        private static string EnterInitialState(string vocabulary)
        {
            int valid = 0;
            string state = "";
            Regex rx = new Regex("^[A-Z]{1}$", RegexOptions.Compiled);
            while (valid == 0)
            {
                Console.Write("Veuillez rentrer l'état de départ de la grammaire (Ex. : A) ");
                state = Console.ReadLine();
                if (rx.IsMatch(state))
                {
                    valid = (vocabulary.Contains(state)) ? 1 : 0;
                    if (valid == 0) Console.WriteLine("L'etat specifier ne fais pas partie du vocabulaire...");
                }
                else
                {
                    Console.WriteLine("Mauvais format... Veuillez reesayer.");
                }
            }

            Console.WriteLine("Bonne entrée");
            return state;
        }

        /// <summary>
        /// This method allows to enter the list of rules for the grammar.
        /// </summary>
        /// <param name="vocabulary">A vocabulary that will be be use to check if the user input is correct.</param>
        /// <param name="initialState">The character corresponding to the initial state.</param>
        /// <returns>A list of rules that will be use by the grammar</returns>
        private static List<string> EnterRules(string vocabulary, char initialState)
        {
            string str = null;
            List<string> ruleList = new List<string>();
            Regex rx = new Regex("^[A-Z]{1}->?([0-1]{1}|[0-1]{1}[A-Z]{1}|[e]{1})$", RegexOptions.Compiled);

            while (true)
            {
                int valid = 0;
                while (valid == 0)
                {
                    Console.Write(
                        "Veuillez rentrer une règle de transition de la grammaire. Si vous avez termine, ne rien entrer. (Ex. : A->1A) ");
                    str = Console.ReadLine();
                    //Au moins une rgle doit etre entrer
                    if (str == "" && ruleList.Count > 0) break;
                    if (rx.IsMatch(str))
                    {
                        //Checking if the letters entered are part of the vocabulary
                        bool isVocabulary = true;
                        foreach (char c in str)
                        {
                            if (c.Equals('-') || c.Equals('>')) continue;
                            if (!vocabulary.Contains(c.ToString()) && c != 'e')
                            {
                                isVocabulary = false;
                                break;
                            }

                            if (c == 'e' && str[0] != initialState)
                            {
                                isVocabulary = false;
                                break;
                            }
                        }

                        valid = isVocabulary ? 1 : 0;
                        if (valid == 0)
                            Console.WriteLine(
                                "La regle est invalide puisqu'un ou plusieurs de ses etats ne font pas partie du vocabulaire ou n'est pas une grammaire reguliaire.");
                    }
                    else
                    {
                        Console.WriteLine("Le format de la regle n'est pas valide...");
                    }
                }

                if (str == "")
                {
                    Console.WriteLine("L'ajout de regle est termine");
                    break;
                }

                ruleList.Add(str);
            }

            return ruleList;
        }
    }
}