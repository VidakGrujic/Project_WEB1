using PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR020_2019_Vidak_Grujic_Web_Projekat
{
    public class InitializeDataFromTXTFiles
    {
        public static void InitializeData()
        {
            string putanjaCentri = "~/App_Data/FitnesCentri.txt";
            List<FitnesCentar> fitnesCentri = FitnesCentarFileWork.ReadFitnesCentre(putanjaCentri);
            FitnesCentarCRUD.ListaFintesCentara = fitnesCentri;

            string putanjaVlasnici = "~/App_Data/Vlasnici.txt";
            List<Vlasnik> vlasnici = VlasnikFileWork.ReadVlasnike(putanjaVlasnici);
            VlasnikCRUD.listaVlasnika = vlasnici;

            string putanjaTreninzi = "~/App_Data/GrupniTreninzi.txt";
            List<GrupniTrening> treninzi = GrupniTreninziFileWork.ReadGrupneTreninge(putanjaTreninzi);
            GrupniTreningCRUD.ListaGrupnihTreninga = treninzi;

            string putanjaTreneri = "~/App_Data/Treneri.txt";
            List<Trener> treneri = TrenerFileWork.ReadTrenere(putanjaTreneri);
            TrenerCRUD.ListaTrenera = treneri;

            string putanjaPosetilac = "~/App_Data/Posetioci.txt";
            List<Posetilac> posetioci = PosetilacFileWork.ReadPosetioce(putanjaPosetilac);
            PosetilacCRUD.ListaPosetilaca = posetioci;

            string putanjaKomentar = "~/App_Data/Komentari.txt";
            List<Komentar> komentari = KomentarFileWork.ReadKomentare(putanjaKomentar);
            KomentarCRUD.ListaKomentara = komentari;
        }
    }
}