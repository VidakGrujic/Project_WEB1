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
    public class GrupniTreninziFileWork
    {
        public static List<GrupniTrening> ReadGrupneTreninge(string putanja)
        {
            List<GrupniTrening> grupniTreninzi = new List<GrupniTrening>();

            string line = "";
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(putanja);

            while ((line = sr.ReadLine()) != "END")
            {
                string[] fullGrupniTrening = line.Split(new string[] { "-" }, StringSplitOptions.None);

                string[] grupniTreningPodaci = fullGrupniTrening[0].Split(new string[] { ";" }, StringSplitOptions.None);
                string[] prijavljeniPosetioci = fullGrupniTrening[1].Split(new string[] { ";" }, StringSplitOptions.None);

                GrupniTrening grupniTrening = new GrupniTrening()
                {
                    IdGrupnogTreninga = int.Parse(grupniTreningPodaci[0]),
                    Naziv = grupniTreningPodaci[1],
                    TipTreninga = grupniTreningPodaci[2],
                    FitnesCentarOdrzavanja = FitnesCentarCRUD.FindFitnesCentarById(int.Parse(grupniTreningPodaci[3])),
                    TrajanjeTreninga = int.Parse(grupniTreningPodaci[4]),
                    DatumIVremeTreninga = DateTime.Parse(grupniTreningPodaci[5]),
                    MaxBrojPosetilaca = int.Parse(grupniTreningPodaci[6]),
                    JeObrisan = bool.Parse(grupniTreningPodaci[7]),
                    SpisakPosetilaca = new List<Posetilac>()
                };

                if (prijavljeniPosetioci[0] != "")
                {
                    foreach (string pk in prijavljeniPosetioci)
                    {
                        Posetilac posetilac = PosetilacCRUD.FindPosetilacByID(int.Parse(pk));
                        if (posetilac != null)
                        {
                            //kad pronadjemo korisnika, moramo da mu dodelimo ovaj trening
                            //jer se on prijavio na njega
                            grupniTrening.SpisakPosetilaca.Add(posetilac);
                        }
                    }
                }
                grupniTreninzi.Add(grupniTrening);
            }
            sr.Close();
            stream.Close();
            return grupniTreninzi;
        }


        public static void UpdateAndSaveGrupneTreninge()
        {
            string putanja = "~/App_Data/GrupniTreninzi.txt";
            List<GrupniTrening> grupniTreninzi = GrupniTreningCRUD.ListaGrupnihTreninga;
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            string linePodaci = "";
            string lineSpisakPosetilaca = "";

            foreach(GrupniTrening gt in grupniTreninzi)
            {
                linePodaci = "";
                lineSpisakPosetilaca = "";
                string spisakPosetilaca = "";

                linePodaci = $"{gt.IdGrupnogTreninga};{gt.Naziv};{gt.TipTreninga};{gt.FitnesCentarOdrzavanja.IdFitnesCentra};" +
                             $"{gt.TrajanjeTreninga};{gt.DatumIVremeTreninga.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)};" +
                             $"{gt.MaxBrojPosetilaca};{gt.JeObrisan}";

                if (gt.SpisakPosetilaca.Count != 0)
                {
                    foreach (Posetilac p in gt.SpisakPosetilaca)
                    {
                        lineSpisakPosetilaca += $"{p.IdPosetioca};";
                    }
                    spisakPosetilaca = lineSpisakPosetilaca.Remove(lineSpisakPosetilaca.Length - 1, 1); 
                }
                sw.WriteLine(linePodaci + "-" + spisakPosetilaca);
            }
            //Sto se tice StreamWritera, desava se neki problem da on upisuje nesto na kraju prilikom zatvaranja
            //ne znam zasto, pa sam dodao flag "END"
            sw.WriteLine("END");
            sw.Close();
            stream.Close();
        }
    }
}