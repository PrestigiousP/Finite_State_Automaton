using System;
using System.Collections.Generic;
using System.Text;

namespace TP1_Math
{
    public class Grammaire
    {
        private String vocabulaire;
        private int[] terminaux;
        private String sDepart;
        private String regles;


        //Constructeur
        public Grammaire(String vocabulaire, String sDepart, String regles)
        {
            this.vocabulaire = vocabulaire;
            this.terminaux[0] = 0;
            this.terminaux[1] = 1;
            this.sDepart = sDepart;
            this.regles = regles;
        }

        //Accesseurs
        public String GetVocabulaire()
        {
            return vocabulaire;
        }

        public int[] GetTerminaux()
        {
            return terminaux;
        }

        public String GetSDepart()
        {
            return sDepart;
        }

        public String GetRegles()
        {
            return regles;
        }

        //Mutateurs
        public void SetVocabulaire(String vocabulaire)
        {
            this.vocabulaire = vocabulaire;
        }

        public void SetSDepart(String sDepart)
        {
            this.sDepart = sDepart;
        }

        public void SetRegles(String regles)
        {
            this.regles = regles;
        }

        public override string ToString()
        {
            return "G = {V, T, S, R}" + "\nV = {" + vocabulaire + "}\nT = {0, 1}\nS = {" + sDepart + "}\nR = {" + regles + "}";
        }
    }
}
