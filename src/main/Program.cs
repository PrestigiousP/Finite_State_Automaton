using System;
using System.Collections.Generic;
using System.Text;
using TP1_Math.automate;
using TP1_Math.helpers;

namespace TP1_Math.main
{
    public class Program
    {
        //Instance des objets utile au programme dans son intégralité
        private static readonly ManageFiles _manageFile = new ManageFiles();
        private static Grammaire _grammaire = null;
        static void Main(string[] args)
        {
            _manageFile.FilePath = "..\\..\\..\\test.txt";
            Menu();
        }

        private static void Menu()
        {
            while (true)
            {
                Console.WriteLine("---------------------Menu---------------------\n");
                Console.WriteLine("1- Gérer la grammaire");
                Console.WriteLine("2- Exécuter les règles et la grammaire");
                Console.WriteLine("3- POUR CORRECTEUR: EXÉCUTER AUTOMATIQUEMENT UNE GRAMMAIRE ET UN AUTOMATE PRÉFAITE");
                Console.WriteLine("4- Quitter");
                int choix = ConsoleHelper.AskInteger("Entrez un nombre pour faire un choix: ", 1, 4);
                switch (choix)
                {
                    case 1:
                        GererGrammaire();
                        break;
                    case 2:
                        if (_grammaire == null)
                        {
                            Console.WriteLine("No grammar has been initialize.");
                            break;
                        }
                        AutomateCreator automate = new AutomateCreator(_grammaire);
                        automate.Execute();
                        string expression = "";
                        while (expression != "q")
                        {
                            expression =
                                ConsoleHelper.AskString(
                                    "Veuillez entrer une expression (Ex.: 10001110). Pour quitter, entrez \"q\": ");
                            ExpressionReader expressionReader = new ExpressionReader(expression, automate.Automate, _grammaire.SDepart);
                            expressionReader.CheckExpression();
                        }
                        break;
                    case 3:
                        AutomaticInput();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
            
        }

        private static void GererGrammaire()
        {
            Console.WriteLine("---------------------Menu Gérer une grammaire---------------------\n");
            Console.WriteLine("1- Créer ou réécrire une grammaire");
            Console.WriteLine("2- Charger une grammaire");
            Console.WriteLine("3- Retour au menu principal");
            int choix = ConsoleHelper.AskInteger("Entrez un nombre pour faire un choix: ", 1, 3);
            switch (choix)
            {
                case 1: //Créer ou réécrire une grammaire
                    _grammaire = GrammarInitializer.Initialize();
                    Console.WriteLine(_grammaire.ToString());
                    _manageFile.Create_Rewrite_File(_grammaire.ToString());
                    break;
                case 2: //Charger une grammaire
                    string strGrammaire = _manageFile.GetFileData();
                    _grammaire = GrammarInitializer.Initialize(strGrammaire);
                    Console.WriteLine(_grammaire.ToString());
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Entrée non valide");
                    break;
            }
        }

        private static void AutomaticInput()
        {
            string s = "G = {V, T, S, R}\n" +
                       "V = {0, 1, A, S, B, C, D, E}\n" +
                       "T = {0, 1}\n" +
                       "S = {S}\n" +
                       "R = {S->e, S->0A, S->0, S->0D, A->1B, B->1C, C->0A, C->0, C->0D, D->0, D->0E, D->1, E->0, E->0E}";

            // ConsoleHelper.PrintString("Les informations sont en train d'être traité par le système...", 3000);
            // string strGrammaire = _manageFile.
            // ConsoleHelper.PrintString("Succès!", 500);
            // ConsoleHelper.PrintString("Les données receuillis seront transferé dans un grammaire.", 1000);
            _grammaire = GrammarInitializer.Initialize(s);
            ConsoleHelper.PrintString("Les données ont bels et bien été transféré dans une grammaire. La voici:", 500);
            ConsoleHelper.PrintString(_grammaire.ToString(), 5000);
            ConsoleHelper.PrintString("L'automate va se créer...", 500);
            AutomateCreator automate = new AutomateCreator(_grammaire);
            automate.Execute();
            ConsoleHelper.PrintString("L'automate a été créé avec les éléments ci-dessus!", 5000);
            ConsoleHelper.PrintString("5 input aléatoires seront effectués", 500);
            for (int i = 0; i < 6; i++)
            {
                string expression = RandomInputGenerator();
                ConsoleHelper.PrintString($"Input {i}: {expression}", 2000);
                ExpressionReader expressionReader = new ExpressionReader(expression, automate.Automate, _grammaire.SDepart);
                expressionReader.CheckExpression();
                ConsoleHelper.PrintString("------------------------------", 2000);
            }
            ConsoleHelper.PrintString("Fini!", 1000);
            
        }

        private static string RandomInputGenerator()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            int inputLength = random.Next(0, 11);
            for (int i = 0; i < inputLength; i++)
            {
                int terminalInput = random.Next(0, 2);
                sb.Append(terminalInput);
            }

            return sb.ToString();
        }
    }
}