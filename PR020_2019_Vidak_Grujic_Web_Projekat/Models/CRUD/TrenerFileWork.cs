using PR020_2019_Vidak_Grujic_Web_Projekat.Models.Enums;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class TrenerFileWork
    {
        public static List<Trener> ReadTrenere(string putanja)
        {
            List<Trener> treneri = new List<Trener>();


            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(putanja);
            string line = "";

            while ((line = sr.ReadLine()) != "END")
            {
                string[] fullTrener = line.Split(new string[] { ":" }, StringSplitOptions.None);

                string[] trenerPodaci = fullTrener[0].Split(new string[] { ";" }, StringSplitOptions.None);
                string[] spisakTreninga = fullTrener[1].Split(new string[] { ";" }, StringSplitOptions.None);

                Trener trener = new Trener()
                {
                    KorisnickoIme = trenerPodaci[0],
                    Lozinka = trenerPodaci[1],
                    Ime = trenerPodaci[2],
                    Prezime = trenerPodaci[3],
                    Pol = (Pol)Enum.Parse(typeof(Pol), trenerPodaci[4]),
                    Email = trenerPodaci[5],
                    DatumRodjenja = DateTime.Parse(trenerPodaci[6]),
                    Uloga = (Uloga)Enum.Parse(typeof(Uloga), trenerPodaci[7]),
                    IdTrenera = int.Parse(trenerPodaci[8]),
                    FitnesCentarAngazovanje = FitnesCentarCRUD.FindFitnesCentarById(int.Parse(trenerPodaci[9])),
                    JeBlokiran = bool.Parse(trenerPodaci[10]),
                    GrupniTreninziAngazovanje = new List<GrupniTrening>()
                };

                if (spisakTreninga[0] != "")
                {
                    foreach (string gt in spisakTreninga)
                    {
                        GrupniTrening grupniTrening = GrupniTreningCRUD.FindGrupniTreningById(int.Parse(gt));
                        if (grupniTrening != null)
                        {
                            trener.GrupniTreninziAngazovanje.Add(grupniTrening);
                        }
                    }
                }
                treneri.Add(trener);
            }

            sr.Close();
            stream.Close();
            return treneri;

        }


        public static void UpdateAndSaveTrenere()
        {
            string putanja = "~/App_Data/Treneri.txt";
            List<Trener> treneri = TrenerCRUD.ListaTrenera;
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            string linePodaci = "";
            string lineSpisakTreninga = "";


            foreach(Trener t in treneri)
            {
                linePodaci = "";
                lineSpisakTreninga = "";
                string grupniTreninzi = "";
                linePodaci = $"{t.KorisnickoIme};{t.Lozinka};{t.Ime};{t.Prezime};{t.Pol.ToString()};" +
                             $"{t.Email};{t.DatumRodjenja.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)};" +
                             $"{t.Uloga.ToString()};{t.IdTrenera};{t.FitnesCentarAngazovanje.IdFitnesCentra};{t.JeBlokiran.ToString()}";

                if (t.GrupniTreninziAngazovanje.Count != 0)
                {
                    foreach (GrupniTrening gt in t.GrupniTreninziAngazovanje)
                    {
                        lineSpisakTreninga += $"{gt.IdGrupnogTreninga};";
                    }
                    grupniTreninzi = lineSpisakTreninga.Remove(lineSpisakTreninga.Length - 1, 1);
                }
                sw.WriteLine(linePodaci + ":" + grupniTreninzi);
            }
            sw.WriteLine("END");
            sw.Close();
            stream.Close();
        }
    }
                
}