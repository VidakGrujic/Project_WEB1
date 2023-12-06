using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class UpdateKorisnika
    {

        public static void UpdateKorisnik(Korisnik existingKorisnik, Korisnik updatedKorisnik)
        {
            existingKorisnik.KorisnickoIme = updatedKorisnik.KorisnickoIme;
            existingKorisnik.Lozinka = updatedKorisnik.Lozinka;
            existingKorisnik.Ime = updatedKorisnik.Ime;
            existingKorisnik.Prezime = updatedKorisnik.Prezime;
            existingKorisnik.Pol = updatedKorisnik.Pol;
            existingKorisnik.Email = updatedKorisnik.Email;
            existingKorisnik.DatumRodjenja = updatedKorisnik.DatumRodjenja;
            existingKorisnik.Uloga = updatedKorisnik.Uloga;
        }
    }
}