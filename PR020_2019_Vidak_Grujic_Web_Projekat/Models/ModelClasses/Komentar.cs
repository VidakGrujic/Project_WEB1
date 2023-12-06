using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses
{
    public class Komentar
    {
        public int IdKomentara { get; set; }
        public Posetilac PosetilacKomentator { get; set; }
        public FitnesCentar KomentarisanFitnesCentar { get; set; }
        public string TekstKomentara { get; set; }
        public int Ocena { get; set; }
        public bool JeOdobren { get; set; }
        public bool JeOdbijen { get; set; }

        public Komentar() { }
    }
}