using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class GrupniTreningCRUD
    {
        public static List<GrupniTrening> ListaGrupnihTreninga = new List<GrupniTrening>();

        public static GrupniTrening FindGrupniTreningById(int id)
        {
            foreach(GrupniTrening gt in ListaGrupnihTreninga)
            {
                if(gt.IdGrupnogTreninga == id)
                {
                    return gt;
                }
            }
            return null;
        }

        public static GrupniTrening AddGrupniTrening(GrupniTrening grupniTrening)
        {
            grupniTrening.IdGrupnogTreninga = GenerateId.GenerateID();
            ListaGrupnihTreninga.Add(grupniTrening);
            return grupniTrening;

        }

        public static void RemoveGrupniTrening(GrupniTrening grupniTrening)
        {
            ListaGrupnihTreninga.Remove(grupniTrening);

        }

        public static GrupniTrening UpdateGrupniTrening(GrupniTrening updatedGrupniTrening)
        {
            GrupniTrening existingGrupniTrening = FindGrupniTreningById(updatedGrupniTrening.IdGrupnogTreninga);

            existingGrupniTrening.Naziv = updatedGrupniTrening.Naziv;
            existingGrupniTrening.TipTreninga = updatedGrupniTrening.TipTreninga;
            existingGrupniTrening.FitnesCentarOdrzavanja = updatedGrupniTrening.FitnesCentarOdrzavanja;
            existingGrupniTrening.TrajanjeTreninga = updatedGrupniTrening.TrajanjeTreninga;
            existingGrupniTrening.DatumIVremeTreninga = updatedGrupniTrening.DatumIVremeTreninga;
            existingGrupniTrening.MaxBrojPosetilaca = updatedGrupniTrening.MaxBrojPosetilaca;
            existingGrupniTrening.SpisakPosetilaca = updatedGrupniTrening.SpisakPosetilaca;
            existingGrupniTrening.JeObrisan = updatedGrupniTrening.JeObrisan;

            return existingGrupniTrening;

        }

        public static List<GrupniTrening> FindTreningeInOneFitnesCentar(int idFitnesCentra)
        {
            List<GrupniTrening> treninziUCentru = new List<GrupniTrening>();

            foreach(GrupniTrening gt in ListaGrupnihTreninga)
            {
                if(gt.FitnesCentarOdrzavanja.IdFitnesCentra == idFitnesCentra)
                {
                    treninziUCentru.Add(gt);
                }
            }

            return treninziUCentru;
        }
    }
}