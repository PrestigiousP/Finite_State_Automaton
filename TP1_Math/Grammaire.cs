using System;

namespace TP1_Math
{
    public class Grammaire
    {
        public string Vocabulaire { get; set; }
        public int[] Terminaux { get; private set; } = { 0, 1 };
        public string SDepart { get; set; }
        public string Regles { get; set; }
        //Constructeur
        public Grammaire(string vocabulaire, String sDepart, String regles)
        {
            Vocabulaire = vocabulaire;
            SDepart = sDepart;
            Regles = regles;
        }
        //ToString de la classe
        public override string ToString()
        {
            return "G = {V, T, S, R}" + "\nV = {" + Vocabulaire + "}\nT = {0, 1}\nS = {" + SDepart + "}\nR = {" + Regles + "}";
        }
    }
}
