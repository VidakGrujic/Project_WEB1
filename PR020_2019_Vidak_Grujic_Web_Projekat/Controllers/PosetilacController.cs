using PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.GrupniTreningDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.KomentarDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.PosetilacDTO;
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
    public class PosetilacController : ApiController
    {
        [HttpGet]
        public IHttpActionResult SeePreviousTreninge()
        {
            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if(posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            List<GrupniTreningDetaljnoDTO> grupniTreningDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach(GrupniTrening gt in posetilac.PrijavljeniGrupniTreninzi)
            {
                if(gt.DatumIVremeTreninga < DateTime.Now) 
                    grupniTreningDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
            }
            return Ok(grupniTreningDTO);
        }

        [HttpPut]
        public IHttpActionResult AddNewTreningToKorisnik(int idGrupnogTreninga)
        {
            if(idGrupnogTreninga == 0)
            {
                return BadRequest("Poslat je pogresan podatak");
            }

            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            GrupniTrening gt = GrupniTreningCRUD.FindGrupniTreningById(idGrupnogTreninga);

            if (gt.SpisakPosetilaca.Contains(posetilac))
            {
                return BadRequest("Vec ste se prijavili na ovaj trening");
            }
            if(gt.SpisakPosetilaca.Count >= gt.MaxBrojPosetilaca)
            {
                return BadRequest("Kapaciteti ovog treninga su popunjeni");
            }
            //uspesno izvrsene promene
            posetilac.PrijavljeniGrupniTreninzi.Add(gt);
            gt.SpisakPosetilaca.Add(posetilac);

            PosetilacFileWork.UpdateAndSavePosetioce();
            GrupniTreninziFileWork.UpdateAndSaveGrupneTreninge();

            return Ok("Uspesno ste prijavljeni na trening");


        }

        [HttpGet]
        public IHttpActionResult FindGrupniTreningByNaziv(string naziv)
        {
            if (naziv == null)
                naziv = "";

            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach(GrupniTrening gt in posetilac.PrijavljeniGrupniTreninzi)
            {
                if (gt.Naziv.Equals(naziv) && gt.DatumIVremeTreninga < DateTime.Now)
                {
                    grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);
        }

        [HttpGet]
        public IHttpActionResult FindGrupniTreningByTip(string tipTreninga)
        {
            if (tipTreninga == null)
                tipTreninga = "";


            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach (GrupniTrening gt in posetilac.PrijavljeniGrupniTreninzi)
            {
                if (gt.TipTreninga.Equals(tipTreninga) && gt.DatumIVremeTreninga < DateTime.Now)
                {
                    grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);
        }

        [HttpGet]
        public IHttpActionResult FindGrupniTreningByNazivFitnesCentra(string nazivFitnesCentra)
        {
            if (nazivFitnesCentra == null)
                nazivFitnesCentra = "";


            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach (GrupniTrening gt in posetilac.PrijavljeniGrupniTreninzi)
            {
                if (gt.FitnesCentarOdrzavanja.Naziv.Equals(nazivFitnesCentra) && gt.DatumIVremeTreninga < DateTime.Now)
                {
                    grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);
        }

        [HttpPost]
        public IHttpActionResult FindByAllCategories(GrupniTreningDetaljnoDTO gtDetaljnoDTO)
        {
            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach (GrupniTrening gt in posetilac.PrijavljeniGrupniTreninzi)
            {
                if (gt.DatumIVremeTreninga < DateTime.Now)
                {
                    //pretrazi po sva tri
                    if (gtDetaljnoDTO.Naziv != null && gtDetaljnoDTO.TipTreninga != null && gtDetaljnoDTO.FitnesCentarOdrzavanja != null)
                    {
                        if (gt.Naziv.Equals(gtDetaljnoDTO.Naziv) && gt.TipTreninga.Equals(gtDetaljnoDTO.TipTreninga)
                            && gt.FitnesCentarOdrzavanja.Naziv.Equals(gtDetaljnoDTO.FitnesCentarOdrzavanja))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po nazivu 
                    else if (gtDetaljnoDTO.Naziv != null && gtDetaljnoDTO.TipTreninga == null && gtDetaljnoDTO.FitnesCentarOdrzavanja == null)
                    {
                        if (gt.Naziv.Equals(gtDetaljnoDTO.Naziv))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po tipu treninga
                    else if (gtDetaljnoDTO.Naziv == null && gtDetaljnoDTO.TipTreninga != null && gtDetaljnoDTO.FitnesCentarOdrzavanja == null)
                    {
                        if (gt.TipTreninga.Equals(gtDetaljnoDTO.TipTreninga))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po nazivu fitnes centra
                    else if (gtDetaljnoDTO.Naziv == null && gtDetaljnoDTO.TipTreninga == null && gtDetaljnoDTO.FitnesCentarOdrzavanja != null)
                    {
                        if (gt.FitnesCentarOdrzavanja.Naziv.Equals(gtDetaljnoDTO.FitnesCentarOdrzavanja))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po nazivu i tipu treninga
                    else if (gtDetaljnoDTO.Naziv != null && gtDetaljnoDTO.TipTreninga != null && gtDetaljnoDTO.FitnesCentarOdrzavanja == null)
                    {
                        if (gt.Naziv.Equals(gtDetaljnoDTO.Naziv) && gt.TipTreninga.Equals(gtDetaljnoDTO.TipTreninga))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }

                    }
                    //pretrazi po nazivu i nazivu fitnes centra
                    else if (gtDetaljnoDTO.Naziv != null && gtDetaljnoDTO.TipTreninga == null && gtDetaljnoDTO.FitnesCentarOdrzavanja != null)
                    {
                        if (gt.Naziv.Equals(gtDetaljnoDTO.Naziv) && gt.FitnesCentarOdrzavanja.Naziv.Equals(gtDetaljnoDTO.FitnesCentarOdrzavanja))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po tipu treninga i nazivu fitnes centra
                    else if (gtDetaljnoDTO.Naziv == null && gtDetaljnoDTO.TipTreninga != null && gtDetaljnoDTO.FitnesCentarOdrzavanja != null)
                    {
                        if (gt.TipTreninga.Equals(gtDetaljnoDTO.TipTreninga) && gt.FitnesCentarOdrzavanja.Naziv.Equals(gtDetaljnoDTO.FitnesCentarOdrzavanja))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                }

            }

            return Ok(grupniTreninziDTO);
        }
       
        [HttpGet]
        public IHttpActionResult SortGTByNaziv(string tipSortiranja)
        {
            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            GrupniTrening[] grupniTreninzi = posetilac.PrijavljeniGrupniTreninzi.ToArray();
            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

            if (tipSortiranja.Equals("rastuce"))
            {
                for (int i = 1; i < grupniTreninzi.Length; i++)
                {
                    for (int j = 0; j < grupniTreninzi.Length - 1; j++)
                    {
                        if (string.Compare(grupniTreninzi[j].Naziv, grupniTreninzi[j + 1].Naziv) > 0)
                        {
                            var temp = grupniTreninzi[j];
                            grupniTreninzi[j] = grupniTreninzi[j + 1];
                            grupniTreninzi[j + 1] = temp;
                        }
                    }
                }
                foreach (GrupniTrening gt in grupniTreninzi)
                {
                    if(gt.DatumIVremeTreninga < DateTime.Now)
                        grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }

            }
            else if (tipSortiranja.Equals("opadajuce"))
            {
                for (int i = 1; i < grupniTreninzi.Length; i++)
                {
                    for (int j = 0; j < grupniTreninzi.Length - 1; j++)
                    {
                        if (string.Compare(grupniTreninzi[j].Naziv, grupniTreninzi[j + 1].Naziv) < 0)
                        {
                            var temp = grupniTreninzi[j];
                            grupniTreninzi[j] = grupniTreninzi[j + 1];
                            grupniTreninzi[j + 1] = temp;
                        }
                    }
                }
                foreach (GrupniTrening gt in grupniTreninzi)
                {
                    if (gt.DatumIVremeTreninga < DateTime.Now)
                        grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);
        }

        [HttpGet]
        public IHttpActionResult SortGTByTipTreninga(string tipSortiranja)
        {
            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            GrupniTrening[] grupniTreninzi = posetilac.PrijavljeniGrupniTreninzi.ToArray();
            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

            if (tipSortiranja.Equals("rastuce"))
            {
                for (int i = 1; i < grupniTreninzi.Length; i++)
                {
                    for (int j = 0; j < grupniTreninzi.Length - 1; j++)
                    {
                        if (string.Compare(grupniTreninzi[j].TipTreninga, grupniTreninzi[j + 1].TipTreninga) > 0)
                        {
                            var temp = grupniTreninzi[j];
                            grupniTreninzi[j] = grupniTreninzi[j + 1];
                            grupniTreninzi[j + 1] = temp;
                        }
                    }
                }
                foreach (GrupniTrening gt in grupniTreninzi)
                {
                    if (gt.DatumIVremeTreninga < DateTime.Now)
                        grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }

            }
            else if (tipSortiranja.Equals("opadajuce"))
            {
                for (int i = 1; i < grupniTreninzi.Length; i++)
                {
                    for (int j = 0; j < grupniTreninzi.Length - 1; j++)
                    {
                        if (string.Compare(grupniTreninzi[j].TipTreninga, grupniTreninzi[j + 1].TipTreninga) < 0)
                        {
                            var temp = grupniTreninzi[j];
                            grupniTreninzi[j] = grupniTreninzi[j + 1];
                            grupniTreninzi[j + 1] = temp;
                        }
                    }
                }
                foreach (GrupniTrening gt in grupniTreninzi)
                {
                    if (gt.DatumIVremeTreninga < DateTime.Now)
                        grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);
        }
        
        [HttpGet]
        public IHttpActionResult SortGTByDatum(string tipSortiranja)
        {
            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            GrupniTrening[] grupniTreninzi = posetilac.PrijavljeniGrupniTreninzi.ToArray();
            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

            if (tipSortiranja.Equals("rastuce"))
            {
                for (int i = 1; i < grupniTreninzi.Length; i++)
                {
                    for (int j = 0; j < grupniTreninzi.Length - 1; j++)
                    {
                        if (DateTime.Compare(grupniTreninzi[j].DatumIVremeTreninga, grupniTreninzi[j + 1].DatumIVremeTreninga) > 0)
                        {
                            var temp = grupniTreninzi[j];
                            grupniTreninzi[j] = grupniTreninzi[j + 1];
                            grupniTreninzi[j + 1] = temp;
                        }
                    }
                }
                foreach (GrupniTrening gt in grupniTreninzi)
                {
                    if (gt.DatumIVremeTreninga < DateTime.Now)
                        grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }

            }
            else if (tipSortiranja.Equals("opadajuce"))
            {
                for (int i = 1; i < grupniTreninzi.Length; i++)
                {
                    for (int j = 0; j < grupniTreninzi.Length - 1; j++)
                    {
                        if (DateTime.Compare(grupniTreninzi[j].DatumIVremeTreninga, grupniTreninzi[j + 1].DatumIVremeTreninga) < 0)
                        {
                            var temp = grupniTreninzi[j];
                            grupniTreninzi[j] = grupniTreninzi[j + 1];
                            grupniTreninzi[j + 1] = temp;
                        }
                    }
                }
                foreach (GrupniTrening gt in grupniTreninzi)
                {
                    if (gt.DatumIVremeTreninga < DateTime.Now)
                        grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);
        }

        [HttpPost]//ne secam se da je testirano
        public IHttpActionResult AddKomentar(KomentarDTO komentarDTO)
        {
            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            foreach(GrupniTrening gt in posetilac.PrijavljeniGrupniTreninzi)
            {
                //ako posetilac ima grupni trening koji se odrzao u odgovarajucem fitnes centru
                //tada moze da ostavi komentar
                if(gt.FitnesCentarOdrzavanja.IdFitnesCentra == komentarDTO.IdFitnesCentra && gt.DatumIVremeTreninga < DateTime.Now)
                {
                    Komentar Komentar = new Komentar()
                    {
                        KomentarisanFitnesCentar = FitnesCentarCRUD.FindFitnesCentarById(komentarDTO.IdFitnesCentra),
                        PosetilacKomentator = posetilac,
                        TekstKomentara = komentarDTO.TekstKomentara,
                        Ocena = komentarDTO.Ocena,
                        JeOdobren = false,
                        JeOdbijen = false
                    };

                    KomentarCRUD.AddKomentar(Komentar);
                    KomentarFileWork.UpdateAndSaveKomentare();
                    return Ok("Komentar je ostavljen");
                }
                else if(gt.DatumIVremeTreninga > DateTime.Now && gt.FitnesCentarOdrzavanja.IdFitnesCentra == komentarDTO.IdFitnesCentra)
                {
                    return BadRequest("Da biste komentarisali, morate imati trening koji se ovde odrzao u proslosti");
                }
            }

            return BadRequest("Ne mozete ostavljati komentar jer niste bili u ovom fitnes centru");


        }

        [HttpPut]
        public IHttpActionResult ModificatePosetilac(PosetilacDTO posetilacDTO)
        {
            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            if (string.IsNullOrEmpty(posetilacDTO.KorisnickoIme))
                return BadRequest("Morate uneti korisnicko ime");
            if (string.IsNullOrEmpty(posetilacDTO.Lozinka))
                return BadRequest("Morate uneti lozinku");
            if (string.IsNullOrEmpty(posetilacDTO.Ime))
                return BadRequest("Morate uneti ime");
            if (string.IsNullOrEmpty(posetilacDTO.Prezime))
                return BadRequest("Morate uneti prezime");
            if (string.IsNullOrEmpty(posetilacDTO.Email))
                return BadRequest("Morate uneti email");
            if (posetilacDTO.DatumRodjenja == null)
                return BadRequest("Morate uneti datum rodjenja");
            if (PosetilacCRUD.FindPosetilacByKorisnickoIme(posetilacDTO.KorisnickoIme) != null ||
                TrenerCRUD.FindTrenerByKorisnickoIme(posetilacDTO.KorisnickoIme) != null ||
                VlasnikCRUD.FindVlasnikByKorisnickoIme(posetilacDTO.KorisnickoIme) != null)
                return BadRequest("Vec postoji korisnik sa ovim korisnickim imenom");



            posetilac.KorisnickoIme = posetilacDTO.KorisnickoIme;
            posetilac.Lozinka = posetilacDTO.Lozinka;
            posetilac.Ime = posetilacDTO.Ime;
            posetilac.Prezime = posetilacDTO.Prezime;
            posetilac.Pol = (Pol)Enum.Parse(typeof(Pol), posetilacDTO.Pol);
            posetilac.Email = posetilacDTO.Email;

            PosetilacFileWork.UpdateAndSavePosetioce();
            return Ok("Uspesno ste azurirali svoje podatke");

        }

        [HttpGet]
        public IHttpActionResult GetPosetilac(int idPosetioca)
        {
            Posetilac posetilac = (Posetilac)HttpContext.Current.Session["posetilac"];
            if (posetilac == null)
            {
                return BadRequest("Niste logovani");
            }

            Posetilac p = PosetilacCRUD.FindPosetilacByID(idPosetioca);
            PosetilacDTO pDTO = PosetilacDTOWork.PrebaciPosetiocaUDTO(p);

            return Ok(pDTO);

        }


    }
}
