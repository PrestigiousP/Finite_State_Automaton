using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Text;


namespace TP1_Math
{
    class ManageFiles
    {
        public string filePath { get; set; }

        public ManageFiles()
        {
            // Menu();
        }

        public void Create_Rewrite_File(string grammaire)
        {
            try
            {
                string fileName;
                Console.WriteLine("Entrez le nom du fichier (sans le nom d'extension).");
                fileName = Console.ReadLine();
                Console.WriteLine("Entrez le nom du répertoire où vous voulez sauvegarder le fichier");
                filePath = Console.ReadLine();
                filePath += "\\" + fileName + ".txt";
                TextWriter tw = new StreamWriter(filePath);
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
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                string result = "";
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result += line;
                }
                return result;
            }
        }
    }
}