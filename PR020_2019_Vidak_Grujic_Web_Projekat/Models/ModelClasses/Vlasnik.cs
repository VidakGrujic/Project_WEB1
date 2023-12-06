using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses
{
    public class Vlasnik : Korisnik
    {
        public int IdVlasnika { get; set; }
        public List<FitnesCentar> FitnesCentriVlasnika { get; set; }

        public Vlasnik()
        {

        }

        public override string ToString()
        {
            string s = base.ToString();
            s += $"Uloga: {Uloga.ToString()}\n";

            foreach (FitnesCentar fc in FitnesCentriVlasnika)
            {
                s += fc.ToString();
            }

            return s;

        }
    }
}