using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD
{
    public class FitnesCentarFileWork
    {
        public static List<FitnesCentar> ReadFitnesCentre(string putanja)
        {
            List<FitnesCentar> fitnesCentri = new List<FitnesCentar>();
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Read); 
            StreamReader sr = new StreamReader(stream);

            string line = "";
            while ((line = sr.ReadLine()) != "END")
            {
                string[] fitnesCentarLine = line.Split(new string[] { ";" }, StringSplitOptions.None);

                FitnesCentar fitnesCentar = new FitnesCentar()
                {
                    IdFitnesCentra = int.Parse(fitnesCentarLine[0]),
                    Naziv = fitnesCentarLine[1],
                    Adresa = fitnesCentarLine[2],
                    GodinaOtvaranja = int.Parse(fitnesCentarLine[3]),
                    VlasnikCentra = VlasnikCRUD.FindVlasnikByID(int.Parse(fitnesCentarLine[4])),
                    CenaMesecneClanarine = int.Parse(fitnesCentarLine[5]),
                    CenaGodisnjeClanarine = int.Parse(fitnesCentarLine[6]),
                    CenaJednogTreninga = int.Parse(fitnesCentarLine[7]),
                    CenaGrupnogTreninga = int.Parse(fitnesCentarLine[8]),
                    CenaTreningaSaPersonalnimTrenerom = int.Parse(fitnesCentarLine[9]),
                    JeObrisan = bool.Parse(fitnesCentarLine[10])

                };

                //Vlasnik vlasnik = VlasnikCRUD.FindVlasnikByID(fitnesCentar.IdFitnesCentra);
                //vlasnik.FitnesCentriVlasnika.Add(fitnesCentar);
                fitnesCentri.Add(fitnesCentar);
            }
            sr.Close();
            stream.Close();
            return fitnesCentri;
        }


        public static void UpdateAndSaveGrupneCentre()
        {
            string putanja = "~/App_Data/FitnesCentri.txt";
            List<FitnesCentar> fitnesCentri = FitnesCentarCRUD.ListaFintesCentara;
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            string line = "";

            foreach (FitnesCentar ft in fitnesCentri)
            {
                line = $"{ft.IdFitnesCentra};{ft.Naziv};{ft.Adresa};{ft.GodinaOtvaranja};" +
                       $"{ft.VlasnikCentra.IdVlasnika};{ft.CenaMesecneClanarine};{ft.CenaGodisnjeClanarine};" +
                       $"{ft.CenaJednogTreninga};{ft.CenaGrupnogTreninga};{ft.CenaTreningaSaPersonalnimTrenerom};" +
                       $"{ft.JeObrisan}";
                sw.WriteLine(line);
                line = "";
            }
            sw.WriteLine("END");
            sw.Close();
            stream.Close();
        }

    }
}