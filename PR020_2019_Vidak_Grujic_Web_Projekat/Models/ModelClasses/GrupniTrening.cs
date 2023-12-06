using PR020_2019_Vidak_Grujic_Web_Projekat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses
{
    public class GrupniTrening
    {
        public int IdGrupnogTreninga { get; set; }
        public string Naziv { get; set; }
        public string TipTreninga { get; set; }
        public FitnesCentar FitnesCentarOdrzavanja { get; set; }
        public int TrajanjeTreninga { get; set; }
        public DateTime DatumIVremeTreninga { get; set; }
        public int MaxBrojPosetilaca { get; set; }
        public List<Posetilac> SpisakPosetilaca { get; set; }
        public bool JeObrisan { get; set; }

        public GrupniTrening() { }

    }
}