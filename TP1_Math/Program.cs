
using System;

namespace TP1_Math
{
    public class Program
    {
        //Instance des objets utile au programme dans son intégralité
        public static ManageFiles manageFile = new ManageFiles();
        public static Grammaire grammaire = new Grammaire("","","");
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            try
            {
                bool sortir = true;
                while (sortir)
                {
                    Console.WriteLine("---------------------Menu---------------------\n");
                    Console.WriteLine("Entrez un nombre pour faire un choix.");
                    Console.WriteLine("1- Gérer la grammaire");
                    Console.WriteLine("2- ");
                    Console.WriteLine("3- Quitter");
                    int choix = Int32.Parse(Console.ReadLine());
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
        public static void GererGrammaire()
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
                    case 1: //Créer ou réécrire une grammaire
                        Console.WriteLine("Veuillez rentrer le vocabulaire de la grammaire (Ex. : A, B, C, 1, 0)");
                        grammaire.Vocabulaire = Console.ReadLine();
                        Console.WriteLine("Veuillez rentrer l'état de départ de la grammaire (Ex. : S->A)");
                        grammaire.SDepart = Console.ReadLine();
                        Console.WriteLine("Veuillez rentrer les règles de transition de la grammaire (Ex. : A->1A, A->0B, B->0)");
                        grammaire.Regles = Console.ReadLine();
                        
                        manageFile.Create_Rewrite_File(grammaire.ToString());
                        
                        break;
                    case 2: //Charger une grammaire
                        String strGrammaire = manageFile.GetFileData();
                        string[] gramCharge = strGrammaire.Split('{', '}');

                        grammaire.Vocabulaire = gramCharge[3];
                        grammaire.SDepart = gramCharge[7];
                        grammaire.Regles = gramCharge[9];

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
}