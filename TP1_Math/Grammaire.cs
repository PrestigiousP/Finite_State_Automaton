using System;
using System.Collections.Generic;
using System.Text;

namespace TP1_Math
{
    public class Grammaire
    {
        public string Vocabulaire { get; set; }
        public int[] Terminaux { get; private set; } = { 0, 1 };
        public string SDepart { get; set; }
        public List<string> Regles { get; private set; }
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
            return "G = {V, T, S, R}" + "\nV = {" + Vocabulaire + "}\nT = {0, 1}\nS = {" + SDepart + "}\nR = {" + sb.ToString() + "}";
        }
    }
}
