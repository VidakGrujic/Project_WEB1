using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses
{
    public class FitnesCentar
    {
        public int IdFitnesCentra { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public int GodinaOtvaranja { get; set; }
        public Vlasnik VlasnikCentra { get; set; }
        public double CenaMesecneClanarine { get; set; }
        public double CenaGodisnjeClanarine { get; set; }
        public double CenaJednogTreninga { get; set; }
        public double CenaGrupnogTreninga { get; set; }
        public double CenaTreningaSaPersonalnimTrenerom { get; set; }
        public bool JeObrisan { get; set; }


        public FitnesCentar() { }

    }
}