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

        //Accesseurs et mutateurs
        public String Vocabulaire
        {
            get{ return vocabulaire; }
            set{ vocabulaire = value; }
        }

        public int[] Terminaux
        {
            get { return terminaux; }
        }

        public String SDepart
        {
            get { return sDepart; }
            set { sDepart = value; }
        }

        public String Regles
        {
            get { return regles; }
            set { regles = value; }
        }     

        //ToString de la classe
        public override string ToString()
        {
            return "G = {V, T, S, R}" + "\nV = {" + vocabulaire + "}\nT = {0, 1}\nS = {" + sDepart + "}\nR = {" + regles + "}";
        }
    }
}
