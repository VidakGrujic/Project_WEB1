using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.GrupniTreningDTO
{
    public class GrupniTreningDTOWork
    {
        public static GrupniTreningDetaljnoDTO PrebaciTreningUDTO(GrupniTrening gt)
        {
            GrupniTreningDetaljnoDTO gtdDTO = new GrupniTreningDetaljnoDTO()
            {
                DatumIVremeTreninga = gt.DatumIVremeTreninga.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                IdGrupnogTreninga = gt.IdGrupnogTreninga,
                Naziv = gt.Naziv,
                TipTreninga = gt.TipTreninga,
                FitnesCentarOdrzavanja = gt.FitnesCentarOdrzavanja.Naziv,
                TrajanjeTreninga = gt.TrajanjeTreninga,
                MaxBrojPosetilaca = gt.MaxBrojPosetilaca,
                UkupanBrojPosetilaca = gt.SpisakPosetilaca.Count
            };
            return gtdDTO;
        }

    }
}