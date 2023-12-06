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
    public class PosetilacFileWork
    {

        public static List<Posetilac> ReadPosetioce(string putanja)
        {
            List<Posetilac> posetioci = new List<Posetilac>();

            
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(putanja);
            string line = "";

            while ((line = sr.ReadLine()) != "END")
            {
                string[] fullPosetilac = line.Split(new string[] { ":" }, StringSplitOptions.None);

                string[] posetilacPodaci = fullPosetilac[0].Split(new string[] { ";" }, StringSplitOptions.None);
                string[] treninziPosetioca = fullPosetilac[1].Split(new string[] { ";" }, StringSplitOptions.None);

                Posetilac posetilac = new Posetilac()
                {
                    KorisnickoIme = posetilacPodaci[0],
                    Lozinka = posetilacPodaci[1],
                    Ime = posetilacPodaci[2],
                    Prezime = posetilacPodaci[3],
                    Pol = (Pol)Enum.Parse(typeof(Pol), posetilacPodaci[4]),
                    Email = posetilacPodaci[5],
                    DatumRodjenja = DateTime.Parse(posetilacPodaci[6]),
                    Uloga = (Uloga)Enum.Parse(typeof(Uloga), posetilacPodaci[7]),
                    IdPosetioca = int.Parse(posetilacPodaci[8]),
                    PrijavljeniGrupniTreninzi = new List<GrupniTrening>()
                };

                if (treninziPosetioca[0] != "")
                {
                    foreach (string idGrupnogTreninga in treninziPosetioca)
                    {
                        GrupniTrening trening = GrupniTreningCRUD.FindGrupniTreningById(int.Parse(idGrupnogTreninga));
                        if (trening != null)
                        {
                            posetilac.PrijavljeniGrupniTreninzi.Add(trening);

                            //ovo sam dodao ovde ipak, jer neki grupni trenizni ce postojati, neki nece
                            //kada se doda novi grupni trening, nece biti posetioca
                            //medjutim, kad korisnik doda nov trening, treba da se azurira i grupni treninzi
                            //da mu se doda ovaj korisnik

                            trening.SpisakPosetilaca.Add(posetilac);
                        }
                    }
                }
                posetioci.Add(posetilac);
            }
            sr.Close();
            stream.Close();
            return posetioci;
        }


        public static void UpdateAndSavePosetioce()
        {
            string putanja = "~/App_Data/Posetioci.txt";
            List<Posetilac> posetioci = PosetilacCRUD.ListaPosetilaca;
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(stream);

            string linePodaci = "";
            string lineSpisakTrening = "";

            foreach(Posetilac p in posetioci)
            {
                linePodaci = "";
                lineSpisakTrening = "";
                string prijavljeniGrupniTreninzi = "";
                linePodaci = $"{p.KorisnickoIme};{p.Lozinka};{p.Ime};{p.Prezime};{p.Pol.ToString()};" +
                             $"{p.Email};{p.DatumRodjenja.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)};" +
                             $"{p.Uloga.ToString()};{p.IdPosetioca}";

                //moramo jer moze da se desi da porisnik nema prijavljenje treninge
                if (p.PrijavljeniGrupniTreninzi.Count != 0)
                {
                    foreach (GrupniTrening gt in p.PrijavljeniGrupniTreninzi)
                    {
                        lineSpisakTrening += $"{gt.IdGrupnogTreninga};";
                    }
                    prijavljeniGrupniTreninzi = lineSpisakTrening.Remove(lineSpisakTrening.Length - 1, 1);

                }
                sw.WriteLine(linePodaci + ":" + prijavljeniGrupniTreninzi);

            }
            sw.WriteLine("END");
            sw.Close();
            stream.Close();
        }











    }
}