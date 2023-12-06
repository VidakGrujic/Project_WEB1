using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class TrenerCRUD
    {
        public static List<Trener> ListaTrenera = new List<Trener>();

        public static Trener FindTrenerByID(int id)
        {
            foreach (Trener t in ListaTrenera)
            {
                if (t.IdTrenera == id)
                {
                    return t;
                }
            }
            return null;
        }

        public static Trener FindTrenerByKorisnickoIme(string korisnickoIme)
        {

            foreach (Trener t in ListaTrenera)
            {
                if (t.KorisnickoIme == korisnickoIme)
                {
                    return t;
                }
            }
            return null;

        }

        public static Trener AddTrener(Trener trener)
        {
            trener.IdTrenera = GenerateId.GenerateID();
            ListaTrenera.Add(trener);
            return trener;
        }

        public static void RemoveTrener(Trener trener)
        {
            ListaTrenera.Remove(trener);
        }

        public static Trener UpdateTrener(Trener updatedTrener)
        {
            Trener existingTrener = FindTrenerByID(updatedTrener.IdTrenera);

            UpdateKorisnika.UpdateKorisnik(existingTrener, updatedTrener);

            existingTrener.GrupniTreninziAngazovanje = updatedTrener.GrupniTreninziAngazovanje;

            return existingTrener;

        }





    }
}