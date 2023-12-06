using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class KomentarCRUD
    {
        public static List<Komentar> ListaKomentara = new List<Komentar>();

        public static Komentar FindKomentarByID(int id)
        {
            foreach(Komentar k in ListaKomentara)
            {
                if(k.IdKomentara == id)
                {
                    return k;
                }
            }
            return null;
        }

        public static Komentar AddKomentar(Komentar komentar)
        {
            komentar.IdKomentara = GenerateId.GenerateID();
            ListaKomentara.Add(komentar);
            return komentar;
        }

        public static void RemoveKomentar(Komentar komentar)
        {
            ListaKomentara.Remove(komentar);
        }
        
        public static List<Komentar> FindKomentareByFitnesCentar(int idFitnesCentra)
        {
            List<Komentar> komentari = new List<Komentar>();
            foreach(Komentar k in ListaKomentara)
            {
                if(k.KomentarisanFitnesCentar.IdFitnesCentra == idFitnesCentra)
                {
                    komentari.Add(k);
                }
            }

            return komentari;
        }




    }
}