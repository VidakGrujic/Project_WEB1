using PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.Enums;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace PR020_2019_Vidak_Grujic_Web_Projekat
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        void MyPostAuthenticateRequest(object sender, EventArgs e)
        {
            //moze bez ovoga i na taj nacin dobijemo sesiju
            //if (HttpContext.Current.Request.Url.AbsolutePath.StartsWith("/rest/"))
            //{
                System.Web.HttpContext.Current.SetSessionStateBehavior(
                SessionStateBehavior.Required);
            //}
        }
        public override void Init()
        {
            this.PostAuthenticateRequest += MyPostAuthenticateRequest;
            base.Init();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeDataFromTXTFiles.InitializeData();



            /*FitnesCentar fitnesCentar = new FitnesCentar()
            {
                IdFitnesCentra = 12121212,
                Naziv = "Total gym",
                Adresa = new Adresa("Bulevar revolucije", 43, "Novi Sad", 21000),
                GodinaOtvaranja = 2015,
                CenaMesecneClanarine = 3500,
                CenaGodisnjeClanarine = 15000,
                CenaGrupnogTreninga = 1000,
                CenaJednogTreninga = 243,
                CenaTreningaSaPersonalnimTrenerom = 1200,
                VlasnikCentra = VlasnikCRUD.listaVlasnika[0]
            };
            FitnesCentarCRUD.ListaFintesCentara.Add(fitnesCentar);

            //dodavanje fitnes centra vlasniku
            VlasnikCRUD.listaVlasnika[0].FitnesCentriVlasnika.Add(fitnesCentar);

            FitnesCentarFileWork.SaveGrupneCentre();
            VlasnikFileWork.SaveVlasnika();
            */
            /*List<Posetilac> posets = new List<Posetilac>()
            {
                PosetilacCRUD.FindPosetilacByID(10101010),
                PosetilacCRUD.FindPosetilacByID(60606060),
            };

            GrupniTrening gt = new GrupniTrening
            {
                IdGrupnogTreninga = 33339999,
                Naziv = "Trening snage",
                TipTreninga = "powerlifting",
                FitnesCentarOdrzavanja = FitnesCentarCRUD.FindFitnesCentarById(11122233),
                TrajanjeTreninga = 100,
                DatumIVremeTreninga = new DateTime(2022, 12, 04, 12, 50, 00),
                MaxBrojPosetilaca = 15,
                SpisakPosetilaca = posets
            };

            GrupniTreningCRUD.ListaGrupnihTreninga.Add(gt);

            GrupniTreninziFileWork.SaveGrupneTreninge();

            PosetilacCRUD.FindPosetilacByID(10101010).PrijavljeniGrupniTreninzi.Add(gt);
            PosetilacCRUD.FindPosetilacByID(60606060).PrijavljeniGrupniTreninzi.Add(gt);

            PosetilacFileWork.SavePosetioce();*/

            //TrenerCRUD.FindTrenerByID(11110000).GrupniTreninziAngazovanje.Add(GrupniTreningCRUD.FindGrupniTreningById(33339999));
            //TrenerFileWork.SaveTrenere();

            /*Posetilac p = new Posetilac()
            {
                IdPosetioca = 42424242,
                KorisnickoIme = "SlobaBre",
                Lozinka = "slobodance",
                Ime = "Slobodan",
                Prezime = "Zivkovic",
                Pol = Pol.Muski,
                Email = "polucovek@gmail.com",
                DatumRodjenja = new DateTime(1961, 01, 22),
                Uloga = Uloga.Posetilac,
                PrijavljeniGrupniTreninzi = new List<GrupniTrening>()
            };
            PosetilacCRUD.ListaPosetilaca.Add(p);
            PosetilacFileWork.SavePosetioce();*/

            /*GrupniTrening gt = new GrupniTrening
            {
                IdGrupnogTreninga = 77779999,
                Naziv = "Trening brzine",
                TipTreninga = "cardio",
                FitnesCentarOdrzavanja = FitnesCentarCRUD.FindFitnesCentarById(11122233),
                TrajanjeTreninga = 100,
                DatumIVremeTreninga = new DateTime(2022, 12, 04, 12, 50, 00),
                MaxBrojPosetilaca = 15,
                SpisakPosetilaca = new List<Posetilac>()
            };

            GrupniTreningCRUD.ListaGrupnihTreninga.Add(gt);
            GrupniTreninziFileWork.SaveGrupneTreninge();*/


            /*Trener t = new Trener()
            {
                IdTrenera = 87878787,
                KorisnickoIme = "zilence",
                Lozinka = "zile2222",
                Ime = "Zivorad",
                Prezime = "Stojkovic",
                Pol = Pol.Ostalo,
                Email = "zileppp@gmail.com",
                DatumRodjenja = new DateTime(1995, 03, 01),
                Uloga = Uloga.Trener,
                FitnesCentarAngazovanje = FitnesCentarCRUD.FindFitnesCentarById(12121212),
                GrupniTreninziAngazovanje = new List<GrupniTrening>(),
                JeBlokiran = false
            };

            TrenerCRUD.ListaTrenera.Add(t);
            TrenerFileWork.SaveTrenere();*/


            /*Komentar k = new Komentar()
            {
                IdKomentara = 66665555,
                PosetilacKomentator = PosetilacCRUD.FindPosetilacByID(60606060),
                KomentarisanFitnesCentar = FitnesCentarCRUD.FindFitnesCentarById(11222333),
                TekstKomentara = "Sasvim zadovoljavajuce, sve proporuke",
                Ocena = 4,
                JeOdobren = true
            };

            KomentarCRUD.AddKomentar(k);
            KomentarFileWork.SaveKomentare();*/

        }
    }
}
