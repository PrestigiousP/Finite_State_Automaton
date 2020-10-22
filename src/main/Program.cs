using System;
using System.Collections.Generic;
using System.Text;
using TP1_Math.automate;
using TP1_Math.helpers;

namespace TP1_Math.main
{
    /// <summary>
    /// This class contains the main workflow.
    /// Author: Philippe Baillargeon, Frédérick Simard, Julien Turcotte
    /// </summary>
    public class Program
    {
        //Instance variables
        private static readonly ManageFiles _manageFile = new ManageFiles();
        private static Grammar _grammar;

        /// <summary>
        /// The main entry method
        /// </summary>
        /// <param name="args">An argument that can be use when starting the program.</param>
        static void Main(string[] args)
        {
            Menu();
        }

        /// <summary>
        /// this method is a menu displaying the main information.
        /// </summary>
        private static void Menu()
        {
            while (true)
            {
                Console.WriteLine("---------------------Menu---------------------\n");
                Console.WriteLine("1- Gérer la grammaire");
                Console.WriteLine("2- Exécuter les règles et la grammaire");
                Console.WriteLine("3- POUR CORRECTEUR: EXÉCUTER AUTOMATIQUEMENT UNE GRAMMAIRE ET UN AUTOMATE PRÉFAITE");
                Console.WriteLine("4- Lister la grammaire");
                Console.WriteLine("5- Quitter");
                int choix = ConsoleHelper.AskInteger("Entrez un nombre pour faire un choix: ", 1, 5);
                switch (choix)
                {
                    case 1:
                        ManageGrammar();
                        break;
                    case 2:
                        if (_grammar == null)
                        {
                            Console.WriteLine("No grammar has been initialize.");
                            break;
                        }

                        AutomateCreator automate = new AutomateCreator(_grammar);
                        automate.Execute();
                        string expression = "";
                        while (expression != "q")
                        {
                            expression =
                                ConsoleHelper.AskString(
                                    "Veuillez entrer une expression (Ex.: 10001110). Pour quitter, entrez \"q\": ");
                            ExpressionReader expressionReader =
                                new ExpressionReader(expression, automate.Automate, _grammar.InitialState);
                            expressionReader.CheckExpression();
                        }

                        break;
                    case 3:
                        AutomaticInput();
                        break;
                    case 4:
                        if (_grammar == null)
                        {
                            Console.WriteLine("La grammaire n'existe pas.");
                            break;
                        }
                        ConsoleHelper.PrintString(_grammar.ToString(), 10000);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        /// <summary>
        /// This method is a submenu that will allows to do operation on the grammar.
        /// </summary>
        private static void ManageGrammar()
        {
            Console.WriteLine("---------------------Menu Gérer une grammaire---------------------\n");
            Console.WriteLine("1- Créer ou réécrire une grammaire");
            Console.WriteLine("2- Charger une grammaire");
            Console.WriteLine("3- Ajouter une règle à la grammaire");
            Console.WriteLine("4- Retirer une règle de la grammaire");
            Console.WriteLine("5- Retour au menu principal");
            int choix = ConsoleHelper.AskInteger("Entrez un nombre pour faire un choix: ", 1, 5);
            switch (choix)
            {
                case 1: //Create or rewrite a grammar and put it in a file text
                    _grammar = GrammarInitializer.Initialize();
                    Console.WriteLine(_grammar.ToString());
                    _manageFile.Create_Rewrite_File(_grammar.ToString());
                    break;
                case 2: //Load a grammar from a txt file.
                    _manageFile.FilePath =
                        ConsoleHelper.AskString(
                            "Entrez le chemin d'accès sous la forme suivante (C:\\Utilisateurs:\\etc...) : \n");
                    string strGrammaire = _manageFile.GetFileData();
                    _grammar = GrammarInitializer.Initialize(strGrammaire);
                    Console.WriteLine(_grammar.ToString());
                    break;
                case 3:
                    if(_grammar == null) break;
                    List<string> list = new List<string>();
                    list.AddRange(_grammar.Rules);
                    list.AddRange(GrammarInitializer.EnterRules(_grammar.Vocabulary, _grammar.InitialState[0]));
                    _grammar.Rules = list;
                    break;
                case 4:
                    string ruleToRmv = null;
                    while (ruleToRmv != "")
                    {
                        ruleToRmv = ConsoleHelper.AskString("Please enter the rule to remove: ");
                        _grammar.RemoveRules(ruleToRmv);
                    }
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("Entrée non valide");
                    break;
            }
        }

        /// <summary>
        /// This method allows to simulate a program where the user has to input everything.
        /// A predifined grammar has been added to allow everything to be execute automatically.
        /// In brief, it plays everything without the user having to touch it.
        /// </summary>
        private static void AutomaticInput()
        {
            const string predefinedGrammar = "G = {V, T, S, R}\n" +
                                             "V = {0, 1, A, S, B, C, D, E}\n" +
                                             "T = {0, 1}\n" +
                                             "S = {S}\n" +
                                             "R = {S->e, S->0A, S->0, S->0D, A->1B, B->1C, C->0A, C->0, C->0D, D->0, D->0E, D->1, E->0, E->0E}";

            _grammar = GrammarInitializer.Initialize(predefinedGrammar);
            ConsoleHelper.PrintString("Voici la grammaire--v", 500);
            ConsoleHelper.PrintString(_grammar.ToString(), 5000);
            ConsoleHelper.PrintString("L'automate va se créer...", 500);
            AutomateCreator automate = new AutomateCreator(_grammar);
            automate.Execute();
            ConsoleHelper.PrintString("L'automate a été créé avec les éléments ci-dessus!", 5000);
            ConsoleHelper.PrintString("10 input aléatoires seront effectués", 500);
            for (int i = 0; i < 11; i++)
            {
                string expression = RandomInputGenerator(11, 2);
                ConsoleHelper.PrintString($"Input {i}: {expression}", 2000);
                ExpressionReader expressionReader =
                    new ExpressionReader(expression, automate.Automate, _grammar.InitialState);
                expressionReader.CheckExpression();
                ConsoleHelper.PrintString("------------------------------", 2000);
            }

            ConsoleHelper.PrintString("Fini!", 1000);
        }

        /// <summary>
        /// This method allows to randomize an input.
        /// </summary>
        /// <param name="maxLenght">The max length that an input can have.</param>
        /// <param name="limit">The limit of the input. (Can generate a number from 0 to the limit set).</param>
        /// <returns>The string corresponding to the "fake" user input.</returns>
        private static string RandomInputGenerator(int maxLenght, int limit)
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            int inputLength = random.Next(0, maxLenght);
            for (int i = 0; i < inputLength; i++)
            {
                int terminalInput = random.Next(0, limit);
                sb.Append(terminalInput);
            }

            return sb.ToString();
        }
    }
}