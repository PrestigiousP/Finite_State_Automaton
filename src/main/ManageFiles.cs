using System;
using System.IO;
using System.Text;

namespace TP1_Math.main
{
    /// <summary>
    /// This class is a model that will be use when reading and writing to a file
    /// Author: Philippe Baillargeon
    /// </summary>
    public class ManageFiles
    {
        //Instance variables
        public string FilePath { get; set; }

        /// <summary>
        /// This method allows to write a string to a file.
        /// </summary>
        /// <param name="grammaire">A string that correspond to the grammar.</param>
        public void Create_Rewrite_File(string grammaire)
        {
            try
            {
                Console.WriteLine("Entrez le nom du fichier (sans le nom d'extension).");
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

        /// <summary>
        /// This method allows to read the data from a file.
        /// </summary>
        /// <returns>All the lines from the file.</returns>
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