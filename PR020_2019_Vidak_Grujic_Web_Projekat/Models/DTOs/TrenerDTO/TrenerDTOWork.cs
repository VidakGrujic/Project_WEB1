using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.TrenerDTO
{
    public class TrenerDTOWork
    {
        public static TrenerDTO PrebaciTreneraUDTO(Trener trener)
        {
            TrenerDTO newTrenerDTO = new TrenerDTO()
            {
                KorisnickoIme = trener.KorisnickoIme,
                Lozinka = trener.Lozinka,
                Ime = trener.Ime,
                Prezime = trener.Prezime,
                Pol = trener.Pol.ToString(),
                Email = trener.Email,
                DatumRodjenja = trener.DatumRodjenja.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                Uloga = trener.Uloga.ToString(),
                IdTrenera = trener.IdTrenera,
                NazivFitnesCentra = trener.FitnesCentarAngazovanje.Naziv,
                JeBlokiran = trener.JeBlokiran,
                GrupniTreninziAngazovanje = new List<int>()
            };
            
            foreach(GrupniTrening gt in trener.GrupniTreninziAngazovanje)
            {
                newTrenerDTO.GrupniTreninziAngazovanje.Add(gt.IdGrupnogTreninga);
            }
            return newTrenerDTO;
        }


    }
}