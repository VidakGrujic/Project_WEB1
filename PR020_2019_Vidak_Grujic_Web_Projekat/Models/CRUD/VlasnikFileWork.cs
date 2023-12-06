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
    public class VlasnikFileWork
    {

        public static List<Vlasnik> ReadVlasnike(string putanja)
        {
           
            List<Vlasnik> vlasnici = new List<Vlasnik>();

            
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(putanja);
            string line = "";

            while ((line = sr.ReadLine()) != "END")
            {
                string[] fullVlasnik = line.Split(new string[] { ":" }, StringSplitOptions.None);

                string[] vlasnikPodaci = fullVlasnik[0].Split(new string[] { ";" }, StringSplitOptions.None);
                string[] vlasnikoviCentri = fullVlasnik[1].Split(new string[] { ";" }, StringSplitOptions.None);

                Vlasnik vlasnik = new Vlasnik()
                {
                    KorisnickoIme = vlasnikPodaci[0],
                    Lozinka = vlasnikPodaci[1],
                    Ime = vlasnikPodaci[2],
                    Prezime = vlasnikPodaci[3],
                    Pol = (Pol)Enum.Parse(typeof(Pol), vlasnikPodaci[4]),
                    Email = vlasnikPodaci[5],
                    DatumRodjenja = DateTime.Parse(vlasnikPodaci[6]),
                    Uloga = (Uloga)Enum.Parse(typeof(Uloga), vlasnikPodaci[7]),
                    IdVlasnika = int.Parse(vlasnikPodaci[8]),
                    FitnesCentriVlasnika = new List<FitnesCentar>()
                    
                };

              
                foreach (string idCentra in vlasnikoviCentri)
                {
                    FitnesCentar fitnesCentar = FitnesCentarCRUD.FindFitnesCentarById(int.Parse(idCentra));
                    if (fitnesCentar != null)
                    {
                        vlasnik.FitnesCentriVlasnika.Add(fitnesCentar);

                        //ovde je moralo da se uradi ovo, jer se prvo inicijalizuju fitnes centri pa tek vlasnici
                        //pa moramo odgovarajucem fitnes centru da dodelimo vlasnika
                        fitnesCentar.VlasnikCentra = vlasnik;
                    }
                }

                vlasnici.Add(vlasnik);
            }
            sr.Close();
            stream.Close();
            return vlasnici;
        }



        public static void UpdateAndSaveVlasnike()
        {
            string putanja = "~/App_Data/Vlasnici.txt";
            List<Vlasnik> vlasnici = VlasnikCRUD.listaVlasnika;
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            string linePodaci = "";
            string lineCentriVlasnika = "";


            foreach(Vlasnik v in vlasnici)
            {
                linePodaci = $"{v.KorisnickoIme};{v.Lozinka};{v.Ime};{v.Prezime};{v.Pol.ToString()};" +
                             $"{v.Email};{v.DatumRodjenja.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)};" +
                             $"{v.Uloga.ToString()};{v.IdVlasnika}";

               
                foreach(FitnesCentar fc in v.FitnesCentriVlasnika)
                {
                    lineCentriVlasnika += $"{fc.IdFitnesCentra};";
                }
                string centriVlasnika = lineCentriVlasnika.Remove(lineCentriVlasnika.Length - 1, 1);

                sw.WriteLine(linePodaci + ":" + centriVlasnika);
                linePodaci = "";
                lineCentriVlasnika = "";
            }
            sw.WriteLine("END");
            sw.Close();
            stream.Close();

        }

    }

}          






