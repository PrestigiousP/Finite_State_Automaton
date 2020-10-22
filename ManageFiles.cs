using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using System.Diagnostics;


namespace TP1_Math
{
    public class ManageFiles
    {
        public string FilePath { get; set; }

        public ManageFiles()
        {
            // Menu();
        }

        public void LoadPath()
        {
            try
            {
                if(FilePath == null)
                {
                    Console.WriteLine("Entrez chemin d'accès vers le fichier (inclure également le nom du fichier et l'extension)");
                    FilePath = Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void Create_Rewrite_File(string grammaire)
        {
            try
            {
                Console.WriteLine("Entrez le nom du fichier (sans le nom d'extension du fichier).");
                string fileName = Console.ReadLine();
                Console.WriteLine("Entrez le nom du répertoire où vous voulez sauvegarder le fichier");
                FilePath = Console.ReadLine();
                FilePath += "\\" + fileName + ".txt";
                TextWriter tw = new StreamWriter(FilePath);
                tw.WriteLine(grammaire);
                tw.Close();
                // Console.WriteLine("ça marche");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public string GetFileData() 
        {
            StreamReader sr = new StreamReader(FilePath);
            StringBuilder sb = new StringBuilder();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sb.Append(line + "\n");
            }
            return sb.ToString();
        }
    }
}