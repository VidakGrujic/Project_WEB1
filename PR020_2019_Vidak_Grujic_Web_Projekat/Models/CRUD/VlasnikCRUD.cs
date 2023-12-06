using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class VlasnikCRUD
    {
        public static List<Vlasnik> listaVlasnika = new List<Vlasnik>();

        public static Vlasnik FindVlasnikByID(int id)
        {
            foreach (Vlasnik v in listaVlasnika)
            {
                if (v.IdVlasnika == id)
                {
                    return v;
                }
            }
            return null;
        }

        public static Vlasnik FindVlasnikByKorisnickoIme(string korisnickoIme)
        {
            foreach (Vlasnik v in listaVlasnika)
            {
                if (v.KorisnickoIme == korisnickoIme)
                {
                    return v;
                }
            }
            return null;
        }
    }
}