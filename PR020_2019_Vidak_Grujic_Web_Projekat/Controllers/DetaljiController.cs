using PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.FitnesCentarDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.GrupniTreningDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.KomentarDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Controllers
{
    public class DetaljiController : ApiController
    {

        //napravicu 2 akcije, jedna koja dobavlja informacije o fitnes centru
        //a druga dobavlja informacije o treninzima u tom fitnes centru
        [HttpGet]
        public FitnesCentarDetaljnoDTO GetFCDetaljno(int idFitnesCentra)
        {
            FitnesCentar fc = FitnesCentarCRUD.FindFitnesCentarById(idFitnesCentra);

            if (fc == null)
                return new FitnesCentarDetaljnoDTO();

            FitnesCentarDetaljnoDTO fcDetaljnoDTO = new FitnesCentarDetaljnoDTO()
            {
                IdFitnesCentra = fc.IdFitnesCentra,
                Naziv = fc.Naziv,
                Adresa = fc.Adresa,
                GodinaOtvaranja = fc.GodinaOtvaranja,
                VlasnikCentra = fc.VlasnikCentra.Ime + ' ' + fc.VlasnikCentra.Prezime,
                CenaMesecneClanarine = fc.CenaMesecneClanarine,
                CenaGodisnjeClanarine = fc.CenaGodisnjeClanarine,
                CenaJednogTreninga = fc.CenaJednogTreninga,
                CenaGrupnogTreninga = fc.CenaGrupnogTreninga,
                CenaTreningaSaPersonalnimTrenerom = fc.CenaTreningaSaPersonalnimTrenerom
            };

            return fcDetaljnoDTO;
        }


        [HttpGet]
        public List<GrupniTreningDetaljnoDTO> GetGrupneTreningeInFitnesCentar(int idFitnesCentra)
        {
            List<GrupniTrening> grupniTreninziUCentru = GrupniTreningCRUD.FindTreningeInOneFitnesCentar(idFitnesCentra);
            List<GrupniTreningDetaljnoDTO> grupniDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach(GrupniTrening gt in grupniTreninziUCentru)
            {
                if (gt.DatumIVremeTreninga > DateTime.Now && gt.JeObrisan != true)
                {
                    GrupniTreningDetaljnoDTO gtDTO = new GrupniTreningDetaljnoDTO()
                    {
                        IdGrupnogTreninga = gt.IdGrupnogTreninga,
                        Naziv = gt.Naziv,
                        TipTreninga = gt.TipTreninga,
                        FitnesCentarOdrzavanja = gt.FitnesCentarOdrzavanja.Naziv,
                        TrajanjeTreninga = gt.TrajanjeTreninga,
                        DatumIVremeTreninga = gt.DatumIVremeTreninga.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                        MaxBrojPosetilaca = gt.MaxBrojPosetilaca,
                        UkupanBrojPosetilaca = gt.SpisakPosetilaca.Count
                    };

                    grupniDTO.Add(gtDTO);
                }
            }
            return grupniDTO;

        }

        [HttpGet]
        public List<KomentarDTO> GetKomentareForFitnesCentar(int idFitnesCentra)
        {
            List<Komentar> komentari = KomentarCRUD.FindKomentareByFitnesCentar(idFitnesCentra);
            List<KomentarDTO> komentariDTO = new List<KomentarDTO>();

            foreach(Komentar k in komentari)
            {
                if (k.JeOdobren)
                {
                    KomentarDTO kDTO = KomentarDTOWork.PrebaciKomentarUDTO(k);
                    komentariDTO.Add(kDTO);
                }
            }
            return komentariDTO;
        }



    }
}
