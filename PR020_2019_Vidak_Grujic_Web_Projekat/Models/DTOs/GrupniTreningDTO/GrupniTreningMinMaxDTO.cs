using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.GrupniTreningDTO
{
    public class GrupniTreningMinMaxDTO
    {
        public string Naziv { get; set; }
        public string Tip { get; set; }
        public int GodinaOtvaranjaMin { get; set; }
        public int GodinaOtvaranjaMax { get; set; }
    }
}