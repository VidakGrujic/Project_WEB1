using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses
{
    public class Trener : Korisnik
    {
        public int IdTrenera { get; set; }
        public List<GrupniTrening> GrupniTreninziAngazovanje { get; set; }
        public FitnesCentar FitnesCentarAngazovanje { get; set; }
        public bool JeBlokiran { get; set; }

        public override string ToString()
        {
            string s = base.ToString();
            s += $"Fitnes centar gde je angazovan: {FitnesCentarAngazovanje.ToString()}\n";

            foreach (GrupniTrening gt in GrupniTreninziAngazovanje)
            {
                s += gt.ToString();
            }
            return s;
        }
    }
}