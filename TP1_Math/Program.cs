
using System;

namespace TP1_Math
{
    public class Program
    {
        //Instance des objets utile au programme dans son intégralité
        public static ManageFiles manageFile = new ManageFiles();
        public static Grammaire grammaire;
        static void Main(string[] args)
        {
            //Maybe put it in a ressource...?
            manageFile.FilePath = "C:\\dev\\TP1_Math\\TP1_Math\\TextFile1.txt";
            Menu();
        }

        private static void Menu()
        {
            Console.WriteLine("---------------------Menu---------------------\n");
            Console.WriteLine("1- Gérer la grammaire");
            Console.WriteLine("2- ");
            Console.WriteLine("3- Quitter");
            int choix = ConsoleHelper.AskInteger("Entrez un nombre pour faire un choix: ", 1, 3);
            switch (choix)
            {
                case 1:
                    GererGrammaire();
                    //gérer grammaire
                    break;
                case 2:
                    //ManageFiles.Menu(grammaire);
                    //
                    //gérer règles de grammaire
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    break;
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
                    GrammarInitializer init = new GrammarInitializer();
                    grammaire = init.Initialize();
                    Console.WriteLine(grammaire.ToString());
                    manageFile.Create_Rewrite_File(grammaire.ToString());
                    break;
                case 2: //Charger une grammaire
                    string strGrammaire = manageFile.GetFileData();

                    GrammarInitializer preloadGrammar = new GrammarInitializer();
                    grammaire = preloadGrammar.Initialize(strGrammaire);
                    Console.WriteLine(grammaire.ToString());
                    //Seul problème c'est qu'on peut pas choisir quel fichier txt est utilisé pour chargé dans la grammaire
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Entrée non valide");
                    break;
            }
        }
    }
}