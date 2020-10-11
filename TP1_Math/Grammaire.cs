using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace TP1_Math
{
    public class Grammaire
    {
        public string Vocabulaire { get; set; }
        public int[] Terminaux { get; private set; }
        public string SDepart { get; set; }
        public string Regles { get; set; }
        //Constructeur
        public Grammaire(string vocabulaire, String sDepart, String regles)
        {
            Vocabulaire = vocabulaire;
            Terminaux[0] = 0;
            Terminaux[1] = 1;
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
