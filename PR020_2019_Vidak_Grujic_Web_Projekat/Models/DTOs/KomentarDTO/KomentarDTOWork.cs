using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.KomentarDTO
{
    public class KomentarDTOWork
    {
        public static KomentarDTO PrebaciKomentarUDTO(Komentar komentar)
        {
            KomentarDTO komentarDTO = new KomentarDTO
            {
                IdKomentara = komentar.IdKomentara,
                IdFitnesCentra = komentar.KomentarisanFitnesCentar.IdFitnesCentra,
                ImePosetiocaKomentatora = komentar.PosetilacKomentator.Ime,
                TekstKomentara = komentar.TekstKomentara,
                Ocena = komentar.Ocena,
                JeOdobren = komentar.JeOdobren,
                JeOdbijen = komentar.JeOdbijen
                
                
            };

            return komentarDTO;



        }
    }
}