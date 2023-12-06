using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class PosetilacCRUD
    {
        public static List<Posetilac> ListaPosetilaca = new List<Posetilac>();

        public static Posetilac FindPosetilacByID(int id)
        {
            foreach(Posetilac p in ListaPosetilaca)
            {
                if(p.IdPosetioca == id)
                {
                    return p;
                }
            }
            return null;
        }

        public static Posetilac FindPosetilacByKorisnickoIme(string korisnickoIme)
        {
            foreach(Posetilac p in ListaPosetilaca)
            {
                if (p.KorisnickoIme.Equals(korisnickoIme))
                {
                    return p;
                }
            }
            return null;
        }

        public static Posetilac AddPosetilac(Posetilac posetilac)
        {
            posetilac.IdPosetioca = GenerateId.GenerateID();
            ListaPosetilaca.Add(posetilac);
            return posetilac;
        }

        public static void RemovePosetilac(Posetilac posetilac)
        {
            ListaPosetilaca.Remove(posetilac);
        }

        public static Posetilac UpdatePosetilac(Posetilac updatedPosetilac)
        {
            Posetilac existingPosetilac = FindPosetilacByID(updatedPosetilac.IdPosetioca);

            UpdateKorisnika.UpdateKorisnik(existingPosetilac, updatedPosetilac);

            existingPosetilac.PrijavljeniGrupniTreninzi = updatedPosetilac.PrijavljeniGrupniTreninzi;

            return existingPosetilac;

        }
    }
}