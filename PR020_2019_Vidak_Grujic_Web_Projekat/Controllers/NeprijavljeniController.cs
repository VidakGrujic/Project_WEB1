using PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.FitnesCentarDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Controllers
{
    public class NeprijavljeniController : ApiController
    {
        [HttpGet]
        public List<FitnesCentriDTO> GetSortedFitnesCentreByNaziv()
        {
            FitnesCentar[] fitnesCentars = FitnesCentarCRUD.ListaFintesCentara.ToArray();

            for(int i = 1; i < fitnesCentars.Length; i++)
            {
                for(int j = 0; j < fitnesCentars.Length - 1; j++)
                {
                    if(string.Compare(fitnesCentars[j].Naziv, fitnesCentars[j + 1].Naziv) > 0)
                    {
                        var temp = fitnesCentars[j];
                        fitnesCentars[j] = fitnesCentars[j + 1];
                        fitnesCentars[j + 1] = temp;
                    }
                }
            }

            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();

            foreach(FitnesCentar ft in fitnesCentars)
            {
                if(ft.JeObrisan != true) 
                    fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
            }

            return fitnesCentriDTO;
        }

        [HttpGet]
        public List<FitnesCentriDTO> FindFitnesCentreByNaziv(string naziv)
        {
            return FitnesCentriDTOWork.FindFitnesCentarByNaziv(naziv);
        }
        
        [HttpGet]
        public List<FitnesCentriDTO> FindFitnesCentreByGodinaOtvaranja(int godinaOtvaranjaMin, int godinaOtvaranjaMax)
        {
            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();

            foreach(FitnesCentar ft in FitnesCentarCRUD.ListaFintesCentara)
            {
                if(ft.GodinaOtvaranja >= godinaOtvaranjaMin && ft.GodinaOtvaranja <= godinaOtvaranjaMax && ft.JeObrisan != true)
                {
                    fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }
            }
            return fitnesCentriDTO;
        }

        [HttpGet]
        public List<FitnesCentriDTO> FindFitnesCentreByAdresa(string adresa)
        {
            return FitnesCentriDTOWork.FindFitnesCentarByAdresa(adresa);
        }

        [HttpPost]
        //[Route("api/Neprijavljeni/FindByAllCategories/{naziv}/{adresa}/{godinaOtvaranja}")]
        public List<FitnesCentriDTO> FindByAllCategories(FitnesCentarDTOMinMax ftDTO)
        {
            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();
            List<FitnesCentar> fitnesCentri = FitnesCentarCRUD.ListaFintesCentara;

            foreach (FitnesCentar ft in fitnesCentri)
            {
                if (ft.JeObrisan != true)
                {
                    //pretrazi po sva tri
                    if (ftDTO.Naziv != null && ftDTO.GodinaOtvaranjaMin != 0 && ftDTO.GodinaOtvaranjaMax != 0 && ftDTO.Adresa != null)
                    {
                        if (ft.Naziv.Equals(ftDTO.Naziv) && ft.Adresa.Equals(ftDTO.Adresa)
                            && (ft.GodinaOtvaranja <= ftDTO.GodinaOtvaranjaMax && ft.GodinaOtvaranja >= ftDTO.GodinaOtvaranjaMin))
                        {
                            fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                        }
                    }
                    //pretrazi po nazivu 
                    else if (ftDTO.Naziv != null && ftDTO.GodinaOtvaranjaMin == 0 && ftDTO.GodinaOtvaranjaMax == 0 && ftDTO.Adresa == null)
                    {
                        if (ft.Naziv.Equals(ftDTO.Naziv))
                        {
                            fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                        }
                    }
                    //pretrazi po godini
                    else if (ftDTO.Naziv == null && ftDTO.GodinaOtvaranjaMax != 0 && ftDTO.GodinaOtvaranjaMin != 0 && ftDTO.Adresa == null)
                    {
                        if (ft.GodinaOtvaranja >= ftDTO.GodinaOtvaranjaMin && ft.GodinaOtvaranja <= ftDTO.GodinaOtvaranjaMax)
                        {
                            fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                        }
                    }
                    //pretrazi po adr
                    else if (ftDTO.Naziv == null && ftDTO.GodinaOtvaranjaMin == 0 && ftDTO.GodinaOtvaranjaMax == 0 && ftDTO.Adresa != null)
                    {
                        if (ft.Adresa.Equals(ftDTO.Adresa))
                        {
                            fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                        }
                    }
                    //pretrazi po nazivu i god
                    else if (ftDTO.Naziv != null && ftDTO.GodinaOtvaranjaMin != 0 && ftDTO.GodinaOtvaranjaMax != 0 && ftDTO.Adresa == null)
                    {
                        if (ft.Naziv.Equals(ftDTO.Naziv) && (ft.GodinaOtvaranja >= ftDTO.GodinaOtvaranjaMin && ft.GodinaOtvaranja <= ftDTO.GodinaOtvaranjaMax))
                        {
                            fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                        }

                    }
                    //pretrazi po nazivu i adr
                    else if (ftDTO.Naziv != null && ftDTO.GodinaOtvaranjaMax == 0 && ftDTO.GodinaOtvaranjaMin == 0 && ftDTO.Adresa != null)
                    {
                        if (ft.Naziv.Equals(ftDTO.Naziv) && ft.Adresa.Equals(ftDTO.Adresa))
                        {
                            fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                        }
                    }
                    //pretrazi po godini i adr
                    else if (ftDTO.Naziv == null && ftDTO.GodinaOtvaranjaMin != 0 && ftDTO.GodinaOtvaranjaMax != 0 && ftDTO.Adresa != null)
                    {
                        if ((ft.GodinaOtvaranja >= ftDTO.GodinaOtvaranjaMin && ft.GodinaOtvaranja <= ftDTO.GodinaOtvaranjaMax) && ft.Adresa.Equals(ftDTO.Adresa))
                        {
                            fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                        }
                    }
                }

            }

            return fitnesCentriDTO;
        }

        //sortiranje po nazivu
        [HttpGet]
        public List<FitnesCentriDTO> SortFCByNaziv(string tipSortiranja)
        {
            FitnesCentar[] fitnesCentars = FitnesCentarCRUD.ListaFintesCentara.ToArray();
            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();

            if (tipSortiranja.Equals("rastuce"))
            {
                for (int i = 1; i < fitnesCentars.Length; i++)
                {
                    for (int j = 0; j < fitnesCentars.Length - 1; j++)
                    {
                        if (string.Compare(fitnesCentars[j].Naziv, fitnesCentars[j + 1].Naziv) > 0)
                        {
                            var temp = fitnesCentars[j];
                            fitnesCentars[j] = fitnesCentars[j + 1];
                            fitnesCentars[j + 1] = temp;
                        }
                    }
                }
                foreach (FitnesCentar ft in fitnesCentars)
                {
                    if (ft.JeObrisan != true)
                        fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }

            }
            else if (tipSortiranja.Equals("opadajuce"))
            {
                for (int i = 1; i < fitnesCentars.Length; i++)
                {
                    for (int j = 0; j < fitnesCentars.Length - 1; j++)
                    {
                        if (string.Compare(fitnesCentars[j].Naziv, fitnesCentars[j + 1].Naziv) < 0)
                        {
                            var temp = fitnesCentars[j];
                            fitnesCentars[j] = fitnesCentars[j + 1];
                            fitnesCentars[j + 1] = temp;
                        }
                    }
                }
                foreach (FitnesCentar ft in fitnesCentars)
                {
                    if (ft.JeObrisan != true)
                        fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }
            }
            return fitnesCentriDTO;

        }

        //sortiranje po adresi
        [HttpGet]
        public List<FitnesCentriDTO> SortFCByAdresa(string tipSortiranja)
        {
            FitnesCentar[] fitnesCentars = FitnesCentarCRUD.ListaFintesCentara.ToArray();
            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();

            if (tipSortiranja.Equals("rastuce"))
            {
                for (int i = 1; i < fitnesCentars.Length; i++)
                {
                    for (int j = 0; j < fitnesCentars.Length - 1; j++)
                    {
                        if (string.Compare(fitnesCentars[j].Adresa, fitnesCentars[j + 1].Adresa) > 0)
                        {
                            var temp = fitnesCentars[j];
                            fitnesCentars[j] = fitnesCentars[j + 1];
                            fitnesCentars[j + 1] = temp;
                        }
                    }
                }
                foreach (FitnesCentar ft in fitnesCentars)
                {
                    if (ft.JeObrisan != true)
                        fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }

            }
            else if (tipSortiranja.Equals("opadajuce"))
            {
                for (int i = 1; i < fitnesCentars.Length; i++)
                {
                    for (int j = 0; j < fitnesCentars.Length - 1; j++)
                    {
                        if (string.Compare(fitnesCentars[j].Adresa, fitnesCentars[j + 1].Adresa) < 0)
                        {
                            var temp = fitnesCentars[j];
                            fitnesCentars[j] = fitnesCentars[j + 1];
                            fitnesCentars[j + 1] = temp;
                        }
                    }
                }
                foreach (FitnesCentar ft in fitnesCentars)
                {
                    if (ft.JeObrisan != true)
                        fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }
            }
            return fitnesCentriDTO;
        }

        //sortiranje po godini otvaranja
        [HttpGet]
        public List<FitnesCentriDTO> SortFCByGodinaOtvaranja(string tipSortiranja)
        {
            FitnesCentar[] fitnesCentars = FitnesCentarCRUD.ListaFintesCentara.ToArray();
            List<FitnesCentriDTO> fitnesCentriDTO = new List<FitnesCentriDTO>();

            if (tipSortiranja.Equals("rastuce"))
            {
                for (int i = 1; i < fitnesCentars.Length; i++)
                {
                    for (int j = 0; j < fitnesCentars.Length - 1; j++)
                    {
                        if (fitnesCentars[j].GodinaOtvaranja > fitnesCentars[j + 1].GodinaOtvaranja)
                        {
                            var temp = fitnesCentars[j];
                            fitnesCentars[j] = fitnesCentars[j + 1];
                            fitnesCentars[j + 1] = temp;
                        }
                    }
                }
                foreach (FitnesCentar ft in fitnesCentars)
                {
                    if (ft.JeObrisan != true)
                        fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }

            }
            else if (tipSortiranja.Equals("opadajuce"))
            {
                for (int i = 1; i < fitnesCentars.Length; i++)
                {
                    for (int j = 0; j < fitnesCentars.Length - 1; j++)
                    {
                        if (fitnesCentars[j].GodinaOtvaranja < fitnesCentars[j + 1].GodinaOtvaranja)
                        {
                            var temp = fitnesCentars[j];
                            fitnesCentars[j] = fitnesCentars[j + 1];
                            fitnesCentars[j + 1] = temp;
                        }
                    }
                }
                foreach (FitnesCentar ft in fitnesCentars)
                {
                    if (ft.JeObrisan != true)
                        fitnesCentriDTO.Add(new FitnesCentriDTO(ft.IdFitnesCentra, ft.Naziv, ft.Adresa, ft.GodinaOtvaranja));
                }
            }
            return fitnesCentriDTO;
        }

    }
}
