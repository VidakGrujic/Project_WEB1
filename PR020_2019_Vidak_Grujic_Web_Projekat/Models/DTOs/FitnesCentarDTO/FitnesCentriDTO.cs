using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.FitnesCentarDTO
{
    public class FitnesCentriDTO
    {
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public int GodinaOtvaranja { get; set; }
        public int IdFitnesCentra { get; set; }

        public FitnesCentriDTO(int idFitnesCentra, string naziv, string adresa, int godinaOtvaranja)
        {
            IdFitnesCentra = idFitnesCentra;
            Naziv = naziv;
            Adresa = adresa;
            GodinaOtvaranja = godinaOtvaranja;
        }

        public FitnesCentriDTO() { }
    }
}