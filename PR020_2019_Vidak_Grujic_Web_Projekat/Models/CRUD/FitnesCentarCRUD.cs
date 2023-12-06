using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class FitnesCentarCRUD
    {
        public static List<FitnesCentar> ListaFintesCentara = new List<FitnesCentar>();


        public static FitnesCentar FindFitnesCentarById(int id)
        {
            foreach(FitnesCentar fc in ListaFintesCentara)
            {
                if(fc.IdFitnesCentra == id)
                {
                    return fc;
                }
            }
            return null;
        }

        public static FitnesCentar AddFitnesCentar(FitnesCentar fitnesCentar)
        {
            fitnesCentar.IdFitnesCentra = GenerateId.GenerateID();
            ListaFintesCentara.Add(fitnesCentar);
            return fitnesCentar;
        }

        public static void RemoveFitnesCentar(FitnesCentar fitnesCentar)
        {
            ListaFintesCentara.Remove(fitnesCentar);
        }

        public static FitnesCentar UpdateFitnesCentar(FitnesCentar fitnesCentar)
        {
            FitnesCentar existingFitnesCentar = FindFitnesCentarById(fitnesCentar.IdFitnesCentra);

            existingFitnesCentar.Naziv = fitnesCentar.Naziv;
            existingFitnesCentar.Adresa = fitnesCentar.Adresa;
            existingFitnesCentar.GodinaOtvaranja = fitnesCentar.GodinaOtvaranja;
            existingFitnesCentar.VlasnikCentra = fitnesCentar.VlasnikCentra;
            existingFitnesCentar.CenaMesecneClanarine = fitnesCentar.CenaMesecneClanarine;
            existingFitnesCentar.CenaGodisnjeClanarine = fitnesCentar.CenaGodisnjeClanarine;
            existingFitnesCentar.CenaJednogTreninga = fitnesCentar.CenaJednogTreninga;
            existingFitnesCentar.CenaGrupnogTreninga = fitnesCentar.CenaGrupnogTreninga;
            existingFitnesCentar.CenaTreningaSaPersonalnimTrenerom = fitnesCentar.CenaTreningaSaPersonalnimTrenerom;
            existingFitnesCentar.JeObrisan = fitnesCentar.JeObrisan;

            return existingFitnesCentar;

        }

        public static FitnesCentar FindFitnesCentarByName(string naziv)
        {
            foreach(FitnesCentar ft in ListaFintesCentara)
            {
                if (ft.Naziv.Equals(naziv))
                {
                    return ft;
                }
            }
            return null; 
        }
     
       

    

    }
}