using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.PosetilacDTO
{
    public class PosetilacDTOWork
    {
        public static PosetilacDTO PrebaciPosetiocaUDTO(Posetilac posetilac)
        {
            PosetilacDTO newPosetilacDTO = new PosetilacDTO()
            {
                KorisnickoIme = posetilac.KorisnickoIme,
                Lozinka = posetilac.Lozinka,
                Ime = posetilac.Ime,
                Prezime = posetilac.Prezime,
                Pol = posetilac.Pol.ToString(),
                Email = posetilac.Email,
                DatumRodjenja = posetilac.DatumRodjenja.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Uloga = posetilac.Uloga.ToString(),
                IdPosetioca = posetilac.IdPosetioca
            };
            return newPosetilacDTO;
        }


    }
}