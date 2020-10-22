
using System;
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
            _manageFile.FilePath = "C:\\dev\\TP1_Math\\test.txt";
            Menu();
        }

        private static void Menu()
        {
            bool sortir = true;
            while (sortir)
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
                        CheckExpression(automate);
                        break;
                    case 3:
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
        
        private static void CheckExpression(AutomateCreator automate)
        {
            bool sortir = true;
            while (sortir)
            {
                Console.WriteLine("Veuillez entrer une expression (Ex.: 10001110). Pour quitter, entrez \"q\": ");
                try
                {
                    string expression = Console.ReadLine();
                    if(expression != "q")
                    {
                        ExpressionReader exp = new ExpressionReader(expression, automate.Automate, _grammaire.SDepart);
                        bool check = exp.Validate();
                        Console.WriteLine(check);
                    }
                    else
                    {
                        sortir = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}