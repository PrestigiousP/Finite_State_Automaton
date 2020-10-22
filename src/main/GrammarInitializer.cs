using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TP1_Math.main
{
    class GrammarInitializer
    {

        public static Grammaire Initialize()
        {
            string vocabulary = EnterVocabulary("0", "1");
            string startState = EnterDepartState(vocabulary);
            List<string> rules = EnterRules(vocabulary, startState[0]);
            var grammaire = new Grammaire(vocabulary, startState, rules);
            return grammaire;
        }

        public static Grammaire Initialize(string input)
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
            List<string> ruleList = new List<string>();

            foreach (var s in commaSplitter)
            {
                ruleList.Add(s);
            }

            Grammaire grammaire = new Grammaire(voc, startState, ruleList);
            return grammaire;
        }

        //Méthode s'assurant que l'input de l'utilisateur respecte le format
        private static string EnterVocabulary(string inputSymbol1, string inputSymbol2)
        {
            int valid = 0;
            string voc = "";
            Regex rx = new Regex("^[A-Z0-1]{1}$", RegexOptions.Compiled);

            while (valid == 0)
            {
                Console.Write("Veuillez rentrer le vocabulaire de la grammaire (Ex. : A, B, C, 1, 0) ");
                voc = Console.ReadLine();
                string[] splitter = voc.Split(", ");
                int matchCount = 0;
                foreach (string s in splitter)
                {
                    if (rx.IsMatch(s)) matchCount++;
                }
                if (matchCount == splitter.Length)
                {
                    valid = (!voc.Contains(inputSymbol1) || !voc.Contains(inputSymbol2)) ? 0 : 1;
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

        private static string EnterDepartState(string vocabulary)
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

        //Méthode s'assurant que l'input de l'utilisateur respecte le format
        private static List<string> EnterRules(string vocabulary, char startState)
        {
            string str = null;
            List<string> ruleList = new List<string>();
            Regex rx = new Regex("^[A-Z]{1}->?([0-1]{1}|[0-1]{1}[A-Z]{1}|[e]{1})$", RegexOptions.Compiled);

            while (true)
            {
                int valid = 0;
                while (valid == 0)
                {
                    Console.Write("Veuillez rentrer une règle de transition de la grammaire. Si vous avez termine, ne rien entrer. (Ex. : A->1A) ");
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
                            if (c == 'e' && str[0] != startState)
                            {
                                isVocabulary = false;
                                break;
                            }
                        }
                        valid = isVocabulary ? 1 : 0;
                        if (valid == 0) Console.WriteLine("La regle est invalide puisqu'un ou plusieurs de ses etats ne font pas partie du vocabulaire ou n'est pas une grammaire reguliaire.");
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
