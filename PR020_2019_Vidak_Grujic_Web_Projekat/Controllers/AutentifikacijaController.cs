using PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.PosetilacDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.TrenerDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.VlasnikDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.Enums;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PR020_2019_Vidak_Grujic_Web_Projekat.Controllers
{
    public class AutentifikacijaController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Login(Korisnik korisnik)
        {
            if (string.IsNullOrEmpty(korisnik.KorisnickoIme))
            {
                return BadRequest("Korisnicko ime ne sme biti prazno");
            }
            if (string.IsNullOrEmpty(korisnik.Lozinka))
            {
                return BadRequest("Lozinka ne sme biti prazna ");
            }

            Posetilac postojeciPosetilac = PosetilacCRUD.FindPosetilacByKorisnickoIme(korisnik.KorisnickoIme);
            if(postojeciPosetilac != null && postojeciPosetilac.Lozinka.Equals(korisnik.Lozinka))
            {
                HttpContext.Current.Session["posetilac"] = postojeciPosetilac;
                //Posetilac p = (Posetilac)HttpContext.Current.Session["posetilac"];
                return Ok(PosetilacDTOWork.PrebaciPosetiocaUDTO(postojeciPosetilac));
            }

            Trener postojeciTrener = TrenerCRUD.FindTrenerByKorisnickoIme(korisnik.KorisnickoIme);
            if(postojeciTrener != null && postojeciTrener.Lozinka.Equals(korisnik.Lozinka) && postojeciTrener.JeBlokiran != true)
            {
                HttpContext.Current.Session["trener"] = postojeciTrener;
                //Trener t = (Trener)HttpContext.Current.Session["trener"];
                return Ok(TrenerDTOWork.PrebaciTreneraUDTO(postojeciTrener));
            }
            else if(postojeciTrener != null && postojeciTrener.JeBlokiran)
            { 
                return BadRequest("Ako ste trener, blokirani ste, obratite se vlasnku fitnes centra");                
            }
           
            Vlasnik postojeciVlasnik = VlasnikCRUD.FindVlasnikByKorisnickoIme(korisnik.KorisnickoIme);
            if(postojeciVlasnik != null && postojeciVlasnik.Lozinka.Equals(korisnik.Lozinka))
            {
                HttpContext.Current.Session["vlasnik"] = postojeciVlasnik;
                //Vlasnik v = (Vlasnik)HttpContext.Current.Session["vlasnik"];
                return Ok(VlasnikDTOWork.PrebaciVlasnikaUDTO(postojeciVlasnik));
            }

            

            return BadRequest("Proverite da li su Korisnicko ime i lozinka ispravno uneti");
        }

        [HttpPost]
        public IHttpActionResult Register(Korisnik korisnik) 
        {
            if (string.IsNullOrEmpty(korisnik.KorisnickoIme))
                return BadRequest("Morate uneti korisnicko ime");
            if ((PosetilacCRUD.FindPosetilacByKorisnickoIme(korisnik.KorisnickoIme)) != null)
                return BadRequest("Vec postoji korisnik sa ovim korisnickim imenom");
            if (string.IsNullOrEmpty(korisnik.Lozinka))
                return BadRequest("Morate uneti lozinku");
            if (string.IsNullOrEmpty(korisnik.Ime))
                return BadRequest("Morate uneti ime");
            if (string.IsNullOrEmpty(korisnik.Prezime))
                return BadRequest("Morate uneti prezime");
            if (string.IsNullOrEmpty(korisnik.Email))
                return BadRequest("Morate uneti email");
            if (korisnik.DatumRodjenja == null)
                return BadRequest("Morate uneti datum rodjenja");
            if (PosetilacCRUD.FindPosetilacByKorisnickoIme(korisnik.KorisnickoIme) != null ||
                TrenerCRUD.FindTrenerByKorisnickoIme(korisnik.KorisnickoIme) != null ||
                VlasnikCRUD.FindVlasnikByKorisnickoIme(korisnik.KorisnickoIme) != null)
                return BadRequest("Vec postoji korisnik sa ovim korisnickim imenom");


            Posetilac p = new Posetilac()
            {
                KorisnickoIme = korisnik.KorisnickoIme,
                Lozinka = korisnik.Lozinka,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Pol = korisnik.Pol,
                Email = korisnik.Email,
                DatumRodjenja = DateTime.Parse(korisnik.DatumRodjenja.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)),
                Uloga = Uloga.Posetilac,
                PrijavljeniGrupniTreninzi = new List<GrupniTrening>()
            };

            Posetilac pVracen = PosetilacCRUD.AddPosetilac(p);
            HttpContext.Current.Session["posetilac"] = pVracen;
            PosetilacFileWork.UpdateAndSavePosetioce();

            PosetilacDTO posetilacDTO = PosetilacDTOWork.PrebaciPosetiocaUDTO(pVracen);

            return Ok(posetilacDTO);

        }

        //LOGOUT 
        [HttpGet]
        public IHttpActionResult Logout()
        {
            Posetilac p = (Posetilac)HttpContext.Current.Session["posetilac"];
            if(p != null)
            {
                HttpContext.Current.Session.Abandon();
                return Ok("Uspesno ste se odlogovali kao posetilac");
            }

            Trener t = (Trener)HttpContext.Current.Session["trener"];
            if(t != null)
            {
                HttpContext.Current.Session.Abandon();
                return Ok("Uspesno ste se odlogovali kao trener");
            }

            Vlasnik v = (Vlasnik)HttpContext.Current.Session["vlasnik"];
            if(v != null)
            {
                HttpContext.Current.Session.Abandon();
                return Ok("Uspesno ste se odlogovali kao vlasnik");
            }

            return BadRequest("Ne mozete se odlogovati ako niste prijavljeni");

        }

    }
}
