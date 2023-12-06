using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.VlasnikDTO
{
    public class VlasnikDTO
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pol { get; set; }
        public string Email { get; set; }
        public string DatumRodjenja { get; set; }
        public string Uloga { get; set; }
        public int IdVlasnika { get; set; }
        public List<int> FitnesCentriVlasnika { get; set; }

        public VlasnikDTO() { }



    }
}