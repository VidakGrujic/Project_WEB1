using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.GrupniTreningDTO
{
    public class GrupniTreningDetaljnoDTO
    {
        public int IdGrupnogTreninga { get; set; }
        public string Naziv { get; set; }
        public string TipTreninga { get; set; }
        public string FitnesCentarOdrzavanja { get; set; }
        public int TrajanjeTreninga { get; set; }
        public string DatumIVremeTreninga { get; set; }
        public int MaxBrojPosetilaca { get; set; }
        public int UkupanBrojPosetilaca { get; set; }
    }
}