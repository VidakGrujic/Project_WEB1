using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.VlasnikDTO
{
    public class VlasnikDTOWork
    {
        public static VlasnikDTO PrebaciVlasnikaUDTO(Vlasnik vlasnik)
        {
            VlasnikDTO newVlasnikDTO = new VlasnikDTO()
            {
                KorisnickoIme = vlasnik.KorisnickoIme,
                Lozinka = vlasnik.Lozinka,
                Ime = vlasnik.Ime,
                Prezime = vlasnik.Prezime,
                Pol = vlasnik.Pol.ToString(),
                Email = vlasnik.Email,
                DatumRodjenja = vlasnik.DatumRodjenja.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Uloga = vlasnik.Uloga.ToString(),
                IdVlasnika = vlasnik.IdVlasnika,
                FitnesCentriVlasnika = new List<int>()
            };

            foreach (FitnesCentar fc in vlasnik.FitnesCentriVlasnika)
            {
                newVlasnikDTO.FitnesCentriVlasnika.Add(fc.IdFitnesCentra);
            }
            return newVlasnikDTO;
        }

    }
}