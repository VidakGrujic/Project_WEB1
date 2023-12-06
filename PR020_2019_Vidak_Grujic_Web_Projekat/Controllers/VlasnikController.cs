using PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.FitnesCentarDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.KomentarDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.TrenerDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.VlasnikDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.Enums;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Controllers
{
    public class VlasnikController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddNewTrener(TrenerDTO trenerDTO)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }

            if (PosetilacCRUD.FindPosetilacByKorisnickoIme(trenerDTO.KorisnickoIme) != null ||
                TrenerCRUD.FindTrenerByKorisnickoIme(trenerDTO.KorisnickoIme) != null ||
                VlasnikCRUD.FindVlasnikByKorisnickoIme(trenerDTO.KorisnickoIme) != null)
                return BadRequest("Vec postoji korisnik sa ovim korisnickim imenom");

            FitnesCentar vlasnikovCentar = FitnesCentarCRUD.FindFitnesCentarByName(trenerDTO.NazivFitnesCentra);

            Trener trener = new Trener()
            {
                KorisnickoIme = trenerDTO.KorisnickoIme,
                Lozinka = trenerDTO.Lozinka,
                Ime = trenerDTO.Ime,
                Prezime = trenerDTO.Prezime,
                Pol = (Pol)Enum.Parse(typeof(Pol), trenerDTO.Pol),
                Email = trenerDTO.Email,
                DatumRodjenja = DateTime.Parse(trenerDTO.DatumRodjenja),
                Uloga = Uloga.Trener,
                JeBlokiran = false,
                FitnesCentarAngazovanje = vlasnikovCentar,
                GrupniTreninziAngazovanje = new List<GrupniTrening>()
            };

            Trener dodatTrener = TrenerCRUD.AddTrener(trener);

            TrenerFileWork.UpdateAndSaveTrenere();

            return Ok("Trener uspesno dodat");
        }

        [HttpPut]
        public IHttpActionResult BlockTrener(string korisnickoIme)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }

            Trener blokiraniTrener = TrenerCRUD.FindTrenerByKorisnickoIme(korisnickoIme);
            if (blokiraniTrener == null)
            {
                return BadRequest("Trener sa korisnickim imenom ne postoji");
            }
            if (!vlasnik.FitnesCentriVlasnika.Contains(blokiraniTrener.FitnesCentarAngazovanje))
            {
                return BadRequest("Ne mozete blokirati trenera koji ne radi u vasem centru");
            }

            blokiraniTrener.JeBlokiran = true;
            TrenerFileWork.UpdateAndSaveTrenere();
            return Ok($"Trener {korisnickoIme} je blokiran");
        }

        [HttpPost]//Nije testirano
        public IHttpActionResult AddNewFitnesCentar(FitnesCentarDetaljnoDTO fitnesCentarDTO)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }

            if (FitnesCentarCRUD.FindFitnesCentarByName(fitnesCentarDTO.Naziv) != null)
            {
                return BadRequest("Fitnes centar sa ovim imeno vec postoji");

            }

            FitnesCentar ft = new FitnesCentar()
            {
                Naziv = fitnesCentarDTO.Naziv,
                Adresa = fitnesCentarDTO.Adresa,
                GodinaOtvaranja = fitnesCentarDTO.GodinaOtvaranja,
                VlasnikCentra = vlasnik,
                CenaMesecneClanarine = fitnesCentarDTO.CenaMesecneClanarine,
                CenaGodisnjeClanarine = fitnesCentarDTO.CenaGodisnjeClanarine,
                CenaGrupnogTreninga = fitnesCentarDTO.CenaGrupnogTreninga,
                CenaJednogTreninga = fitnesCentarDTO.CenaJednogTreninga,
                CenaTreningaSaPersonalnimTrenerom = fitnesCentarDTO.CenaTreningaSaPersonalnimTrenerom,
                JeObrisan = false
            };

            FitnesCentar newFitnesCentar = FitnesCentarCRUD.AddFitnesCentar(ft);
            vlasnik.FitnesCentriVlasnika.Add(newFitnesCentar);

            FitnesCentarFileWork.UpdateAndSaveGrupneCentre();
            VlasnikFileWork.UpdateAndSaveVlasnike();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetFitnesCentreVlasnika()
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }

            List<FitnesCentarDetaljnoDTO> fitnesCentriDTO = new List<FitnesCentarDetaljnoDTO>();

            foreach(FitnesCentar ft in vlasnik.FitnesCentriVlasnika)
            {
                if(ft.JeObrisan != true)
                    fitnesCentriDTO.Add(FitnesCentriDTOWork.PrebaciUFCDetaljnoDTO(ft));
            }
            return Ok(fitnesCentriDTO);
        }

        [HttpDelete]
        public IHttpActionResult DeleteFitnesCentar(int idFitnesCentra)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }

            FitnesCentar obrisanFC = FitnesCentarCRUD.FindFitnesCentarById(idFitnesCentra);

            foreach(GrupniTrening gt in GrupniTreningCRUD.ListaGrupnihTreninga)
            {
               //ako se grupni trening odrzava u tom fitnes centru
               //ako se grupni trening odrzava u buducnosti
               //ako trening nije obrisan
               if (gt.FitnesCentarOdrzavanja.IdFitnesCentra == obrisanFC.IdFitnesCentra && gt.DatumIVremeTreninga > DateTime.Now && gt.JeObrisan == false)
               {
                    return BadRequest("Ne mozete obrisati fitnes centar, postoje treninzi u njemu");
               }
            }


            obrisanFC.JeObrisan = true;
            
            foreach(Trener t in TrenerCRUD.ListaTrenera)
            {
                if(t.FitnesCentarAngazovanje.IdFitnesCentra == obrisanFC.IdFitnesCentra)
                {
                    t.JeBlokiran = true;
                }
            }

            FitnesCentarFileWork.UpdateAndSaveGrupneCentre();
            TrenerFileWork.UpdateAndSaveTrenere();

            return Ok("Fitnes centar je uspesno obrisan");




        }

        [HttpGet]
        public IHttpActionResult GetKomentareForFitnesCentar(int idFitnesCentra)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }

            List<KomentarDTO> komentariDTO = new List<KomentarDTO>();
            FitnesCentar fc = FitnesCentarCRUD.FindFitnesCentarById(idFitnesCentra);

            foreach(Komentar k in KomentarCRUD.ListaKomentara)
            {
                if (vlasnik.FitnesCentriVlasnika.Contains(fc) && k.KomentarisanFitnesCentar.IdFitnesCentra == fc.IdFitnesCentra)
                {
                    komentariDTO.Add(KomentarDTOWork.PrebaciKomentarUDTO(k));
                }
            }


            return Ok(komentariDTO);

        }

        [HttpPut]
        public IHttpActionResult ApproveKomentar(int idKomentara)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }


            Komentar k = KomentarCRUD.FindKomentarByID(idKomentara);

            if (!vlasnik.FitnesCentriVlasnika.Contains(k.KomentarisanFitnesCentar))
            {
                return BadRequest("Ne mozete odobriti komentar na tudj fitnes centar");
            }

            k.JeOdobren = true;
            k.JeOdbijen = false;

            KomentarFileWork.UpdateAndSaveKomentare();
            return Ok("Komentar je odobren");


            

        }

        [HttpPut]
        public IHttpActionResult RefuseKomentar(int idKomentara)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }

            Komentar k = KomentarCRUD.FindKomentarByID(idKomentara);

            if (!vlasnik.FitnesCentriVlasnika.Contains(k.KomentarisanFitnesCentar))
            {
                return BadRequest("Ne mozete odbiti komentar na tudj fitnes centar");
            }

            k.JeOdobren = false;
            k.JeOdbijen = true;
            KomentarFileWork.UpdateAndSaveKomentare();
            return Ok("Komentar je odbijen");

        }


        [HttpPut]
        public IHttpActionResult ModificateFitnesCentar(int idFitnesCentra, [FromBody]FitnesCentarDetaljnoDTO fcDTO)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }


            FitnesCentar fc = FitnesCentarCRUD.FindFitnesCentarById(idFitnesCentra);

            fc.Naziv = fcDTO.Naziv;
            fc.Adresa = fcDTO.Adresa;
            fc.CenaMesecneClanarine = fcDTO.CenaMesecneClanarine;
            fc.CenaGodisnjeClanarine = fcDTO.CenaGodisnjeClanarine;
            fc.CenaJednogTreninga = fcDTO.CenaJednogTreninga;
            fc.CenaGrupnogTreninga = fcDTO.CenaGrupnogTreninga;
            fc.CenaTreningaSaPersonalnimTrenerom = fcDTO.CenaTreningaSaPersonalnimTrenerom;

            FitnesCentarFileWork.UpdateAndSaveGrupneCentre();
            return Ok("Fitnes centar je uspesno modifikovan");


        }


        [HttpPut]
        public IHttpActionResult ModificateVlasnik(VlasnikDTO vlasnikDTO)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }


            if (string.IsNullOrEmpty(vlasnikDTO.KorisnickoIme))
                return BadRequest("Morate uneti korisnicko ime");
            if (string.IsNullOrEmpty(vlasnikDTO.Lozinka))
                return BadRequest("Morate uneti lozinku");
            if (string.IsNullOrEmpty(vlasnikDTO.Ime))
                return BadRequest("Morate uneti ime");
            if (string.IsNullOrEmpty(vlasnikDTO.Prezime))
                return BadRequest("Morate uneti prezime");
            if (string.IsNullOrEmpty(vlasnikDTO.Email))
                return BadRequest("Morate uneti email");
            if (vlasnikDTO.DatumRodjenja == null)
                return BadRequest("Morate uneti datum rodjenja");
            if (PosetilacCRUD.FindPosetilacByKorisnickoIme(vlasnikDTO.KorisnickoIme) != null ||
                TrenerCRUD.FindTrenerByKorisnickoIme(vlasnikDTO.KorisnickoIme) != null ||
                VlasnikCRUD.FindVlasnikByKorisnickoIme(vlasnikDTO.KorisnickoIme) != null)
                return BadRequest("Vec postoji korisnik sa ovim korisnickim imenom");


            vlasnik.KorisnickoIme = vlasnikDTO.KorisnickoIme;
            vlasnik.Lozinka = vlasnikDTO.Lozinka;
            vlasnik.Ime = vlasnikDTO.Ime;
            vlasnik.Prezime = vlasnikDTO.Prezime;
            vlasnik.Pol = (Pol)Enum.Parse(typeof(Pol), vlasnikDTO.Pol);
            vlasnik.Email = vlasnikDTO.Email;


            VlasnikFileWork.UpdateAndSaveVlasnike();
            return Ok("Uspesno ste azurirali svoje podatke");
        }

        [HttpGet]
        public IHttpActionResult GetVlasnik(int idVlasnika)
        {
            Vlasnik vlasnik = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if (vlasnik == null)
            {
                return BadRequest("Niste se logovali");
            }

            Vlasnik v = VlasnikCRUD.FindVlasnikByID(idVlasnika);
            VlasnikDTO vlasnikDTO = VlasnikDTOWork.PrebaciVlasnikaUDTO(v);
            return Ok(vlasnikDTO);

        }

    }
}
