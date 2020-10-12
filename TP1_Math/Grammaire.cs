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
        private List<string> _regles;
        //Constructeur
        public Grammaire(string vocabulaire, string sDepart, List<string> regles)
        {
            Vocabulaire = vocabulaire;
            SDepart = sDepart;
            _regles = regles;
        }

        public void AddRules(string rule)
        {
            _regles.Add(rule);
        }
        public void RemoveRules(string rule)
        {
            _regles.Remove(rule);
        }
        //ToString de la classe
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            _regles.ForEach(r =>
            {
                sb.Append(r + ", ");
            });
            
            sb.Remove(sb.Length - 2, 2);
            return "G = {V, T, S, R}" + "\nV = {" + Vocabulaire + "}\nT = {0, 1}\nS = {" + SDepart + "}\nR = {" + sb.ToString() + "}";
        }
    }
}
