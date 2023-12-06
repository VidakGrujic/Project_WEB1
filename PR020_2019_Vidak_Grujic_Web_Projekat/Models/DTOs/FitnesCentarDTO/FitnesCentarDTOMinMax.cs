using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.FitnesCentarDTO
{
    public class FitnesCentarDTOMinMax
    {
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public int GodinaOtvaranjaMin { get; set; }
        public int GodinaOtvaranjaMax { get; set; }

        public FitnesCentarDTOMinMax() { }
    }
}