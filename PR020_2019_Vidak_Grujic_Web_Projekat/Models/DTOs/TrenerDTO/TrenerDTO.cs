using PR020_2019_Vidak_Grujic_Web_Projekat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.TrenerDTO
{
    public class TrenerDTO
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pol { get; set; }
        public string Email { get; set; }
        public string DatumRodjenja { get; set; }
        public string Uloga { get; set; }
        public int IdTrenera { get; set; }
        public List<int> GrupniTreninziAngazovanje { get; set; }
        public string NazivFitnesCentra { get; set; }
        public bool JeBlokiran { get; set; }


        public TrenerDTO() { } 


    }
}