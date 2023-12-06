using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses
{
    public class Posetilac : Korisnik
    {
        public int IdPosetioca { get; set; }
        public List<GrupniTrening> PrijavljeniGrupniTreninzi { get; set; }

        public override string ToString()
        {
            string s = base.ToString();

            s += "Prijavljeni grupni treninzi: \n";
            foreach (GrupniTrening gt in PrijavljeniGrupniTreninzi)
            {
                s += gt.ToString();
            }
            return s;
        }

    }
}