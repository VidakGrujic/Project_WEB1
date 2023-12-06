using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class KomentarFileWork
    {
        public static List<Komentar> ReadKomentare(string putanja)
        {
            List<Komentar> komentari = new List<Komentar>();

            string line = "";
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(putanja);

            while ((line = sr.ReadLine()) != "END")
            {
                string[] komentarString = line.Split(new string[] { ";" }, StringSplitOptions.None);

                Komentar komentar = new Komentar()
                {
                    IdKomentara = int.Parse(komentarString[0]),
                    PosetilacKomentator = PosetilacCRUD.FindPosetilacByID(int.Parse(komentarString[1])),
                    KomentarisanFitnesCentar = FitnesCentarCRUD.FindFitnesCentarById(int.Parse(komentarString[2])),
                    TekstKomentara = komentarString[3],
                    Ocena = int.Parse(komentarString[4]),
                    JeOdobren = bool.Parse(komentarString[5]),
                    JeOdbijen = bool.Parse(komentarString[6])
                };


                komentari.Add(komentar);
            }
            sr.Close();
            stream.Close();
            return komentari;
        }
        

        public static void UpdateAndSaveKomentare()
        {
            string putanja = "~/App_Data/Komentari.txt";
            List<Komentar> komentari = KomentarCRUD.ListaKomentara;
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            string linePodaci = "";

            foreach(Komentar k in komentari)
            {
                linePodaci = $"{k.IdKomentara};{k.PosetilacKomentator.IdPosetioca};{k.KomentarisanFitnesCentar.IdFitnesCentra};" +
                             $"{k.TekstKomentara};{k.Ocena};{k.JeOdobren};{k.JeOdbijen}";

                sw.WriteLine(linePodaci);
                linePodaci = "";
            }
            sw.WriteLine("END");
            sw.Close();
            stream.Close();




        }



    }
}