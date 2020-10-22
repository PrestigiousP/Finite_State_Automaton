using System.Collections.Generic;
using System.Text;

namespace TP1_Math.main
{
    public class Grammaire
    {
        private string Vocabulaire { get; }
        private int[] Terminaux { get; } = { 0, 1 };
        public string SDepart { get; }
        public List<string> Regles { get; }
        //Constructeur
        public Grammaire(string vocabulaire, string sDepart, List<string> regles)
        {
            Vocabulaire = vocabulaire;
            SDepart = sDepart;
            Regles = regles;
        }

        public void AddRules(string rule)
        {
            Regles.Add(rule);
        }
        public void RemoveRules(string rule)
        {
            Regles.Remove(rule);
        }
        //ToString de la classe
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Regles.ForEach(r =>
            {
                sb.Append(r + ", ");
            });

            sb.Remove(sb.Length - 2, 2);
            string terminal = $"{Terminaux[0]}, {Terminaux[1]}";
            return "G = {V, T, S, R}" + "\nV = {" + Vocabulaire + "}\nT = {" + terminal + "}\nS = {" + SDepart + "}\nR = {" + sb.ToString() + "}";
        }
    }
}
