
using System;

namespace TP1_Math
{
    public class Program
    {
        //Instance des objets utile au programme dans son intégralité
        private static readonly ManageFiles manageFile = new ManageFiles();
        private static Grammaire _grammaire;
        static void Main(string[] args)
        {
            //Maybe put it in a ressource...?
            manageFile.FilePath = "C:\\Users\\Phili\\source\\repos\\math2\\TP1_Math\\TP1_Math\\test.txt";
            StateTable st = new StateTable();
            Menu(st);

        }

        private static void Menu(StateTable st)
        {
            bool sortir = true;
            while (sortir)
            {
                Console.WriteLine("---------------------Menu---------------------\n");
                Console.WriteLine("1- Gérer la grammaire");
                Console.WriteLine("2- ");
                Console.WriteLine("3- Quitter");
                int choix = ConsoleHelper.AskInteger("Entrez un nombre pour faire un choix: ", 1, 3);
                switch (choix)
                {
                    case 1:
                        GererGrammaire(st);
                        //gérer grammaire
                        break;
                    case 2:
                        st.CreateNDFAStateTable();
                        //st.CnvertToDFATable();
                        CheckExpression(st);
                        break;
                    default:
                        break;
                }
            }
            
        } 
        private static void CheckExpression(StateTable st)
        {
            bool sortir = true;
            while (sortir)
            {
                Console.WriteLine("Veuillez entrer une expression (Ex.: 10001110). Pour quitter, entrez \"q\": ");
                try
                {
                    // StateTable st = new StateTable(_grammaire);
                    //// st.CnvertToDFATable();
                    // var dict = st.GetState();
                    string expression = Console.ReadLine();
                    if(expression != "q")
                    {
                        ExpressionReader exp = new ExpressionReader(expression, st, st.GetGrammar());
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

        private static void GererGrammaire(StateTable st)
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
                    _grammaire = init.Initialize();
                    Console.WriteLine(_grammaire.ToString());
                    manageFile.Create_Rewrite_File(_grammaire.ToString());
                    break;
                case 2: //Charger une grammaire
                    string strGrammaire = manageFile.GetFileData();

                    GrammarInitializer preloadGrammar = new GrammarInitializer();
                    _grammaire = GrammarInitializer.Initialize(strGrammaire);
                    Console.WriteLine(_grammaire.ToString());
                    st.SetGrammar(_grammaire);
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