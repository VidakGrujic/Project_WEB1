using PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.FitnesCentarDTO
{
    public class FitnesCentriDTOWork
    {
        //Rad sa DTO-ovima
        public static List<FitnesCentriDTO> FindFitnesCentarByNaziv(string naziv)
        {
            List<FitnesCentar> fitnesCentri = FitnesCentarCRUD.ListaFintesCentara;
            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();

            foreach (FitnesCentar ft in fitnesCentri)
            {
                if (ft.Naziv == naziv && ft.JeObrisan != true)
                {
                    fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }
            }
            return fitnesCentriDTO;
        }
        public static List<FitnesCentriDTO> FindFitnesCentarByAdresa(string adresa)
        {
            List<FitnesCentar> fitnesCentri = FitnesCentarCRUD.ListaFintesCentara;
            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();

            foreach (FitnesCentar ft in fitnesCentri)
            {
                if (ft.Adresa.Equals(adresa) && ft.JeObrisan != true)
                {
                    fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }
            }
            return fitnesCentriDTO;
        }
        public static List<FitnesCentriDTO> FindFitnesCentarByGodinaOtvaranja(int godinaOtvaranja)
        {
            List<FitnesCentar> fitnesCentri = FitnesCentarCRUD.ListaFintesCentara;
            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();

            foreach (FitnesCentar ft in fitnesCentri)
            {
                if (ft.GodinaOtvaranja == godinaOtvaranja && ft.JeObrisan != true)
                {
                    fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }
            }
            return fitnesCentriDTO;
        }
        public static List<FitnesCentriDTO> ConvertFitnesCentarToDTO()
        {
            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();
            foreach (FitnesCentar ft in FitnesCentarCRUD.ListaFintesCentara)
            {
                fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
            }
            return fitnesCentriDTO;
        }
        public static FitnesCentarDetaljnoDTO PrebaciUFCDetaljnoDTO(FitnesCentar ft)
        {
                FitnesCentarDetaljnoDTO ftDTO = new FitnesCentarDetaljnoDTO()
                {
                    IdFitnesCentra = ft.IdFitnesCentra,
                    Naziv = ft.Naziv,
                    Adresa = ft.Adresa,
                    GodinaOtvaranja = ft.GodinaOtvaranja,
                    VlasnikCentra = ft.VlasnikCentra.Ime,
                    CenaGodisnjeClanarine = ft.CenaGodisnjeClanarine,
                    CenaMesecneClanarine = ft.CenaMesecneClanarine,
                    CenaGrupnogTreninga = ft.CenaGrupnogTreninga,
                    CenaJednogTreninga = ft.CenaJednogTreninga,
                    CenaTreningaSaPersonalnimTrenerom = ft.CenaTreningaSaPersonalnimTrenerom
                };

            return ftDTO;


        }
    }
}