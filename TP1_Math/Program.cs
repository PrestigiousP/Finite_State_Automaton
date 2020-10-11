using System;

namespace TP1_Math
{
    class Program
    {

        static void Main(string[] args)
        {
            //string infos;
            //ManageFiles file = new ManageFiles();
            //string grammaire = "Je fais un test voir si ça marche.";
            //file.CreateResetFile(grammaire);
            //infos = file.GetFileData();
            //Console.ReadLine();
            ManageFiles manageFile = new ManageFiles();
            Menu(manageFile);
        }

        public static void Menu(ManageFiles manageFile)
        {
            try
            {
                string grammaire = "Ceci est un test pour l'instant";
                bool sortir = true;
                while (sortir)
                {
                    Console.WriteLine("---------------------Menu---------------------\n");
                    Console.WriteLine("Entrez un nombre pour faire un choix.");
                    Console.WriteLine("1- Créer une grammaire");
                    Console.WriteLine("2- Écrire une grammaire");
                    Console.WriteLine("3- Quitter");
                    int choix = Int32.Parse(Console.ReadLine());
                    switch (choix)
                    {
                        case 1:
                            CreerGrammaire(grammaire, manageFile);
                            //gérer grammaire
                            break;
                        case 2:
                            //ManageFiles.Menu(grammaire);
                            EcrireGrammaire(grammaire);
                            //gérer règles de grammaire
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void CreerGrammaire(string grammaire, ManageFiles manageFile)
        {
            bool sortir = true;
            while (sortir)
            {
                Console.WriteLine("---------------------Menu Gérer une grammaire---------------------\n");
                Console.WriteLine("Entrez un nombre pour faire un choix.");
                Console.WriteLine("1- Créer ou réécrire une grammaire");
                Console.WriteLine("2- Charger une grammaire");
                Console.WriteLine("3- Retour au menu principal");
                int choix = Int32.Parse(Console.ReadLine());
                switch (choix)
                {
                    case 1:

                        manageFile.Create_Rewrite_File(grammaire);
                        // ManageFiles manageFile = new ManageFiles();
                        //gérer grammaire
                        break;
                    case 2:
                        grammaire = manageFile.GetFileData();
                        //gérer règles de grammaire
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
        public static void EcrireGrammaire(string grammaire)
        {

        }
    }
}
