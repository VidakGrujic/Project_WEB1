using PR020_2019_Vidak_Grujic_Web_Projekat.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses
{
    public class Korisnik
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Pol Pol { get; set; }
        public string Email { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public Uloga Uloga { get; set; }

        public Korisnik() { }

        public Korisnik(string korisnickoIme, string lozinka)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
        }


        public override string ToString()
        {
            string s = "";

            s += $"Korisnicko ime: {KorisnickoIme}\n" +
                $"Ime: {Ime}\n" +
                $"Prezime: {Prezime}\n" +
                $"Pol: {Pol.ToString()}\n" +
                $"Email: {Email}\n" +
                $"Datum rodjenja: {DatumRodjenja.ToString("dd/MM/yyyy")}\n" +
                $"Uloga: {Uloga.ToString()}";

            return s;
        }
    }
}