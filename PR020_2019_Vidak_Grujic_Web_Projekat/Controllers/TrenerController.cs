using PR020_2019_Vidak_Grujic_Web_Projekat.Models.CRUD;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.GrupniTreningDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.PosetilacDTO;
using PR020_2019_Vidak_Grujic_Web_Projekat.Models.DTOs.TrenerDTO;
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
    public class TrenerController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AddTrening(GrupniTreningDetaljnoDTO grupniTreningDTO)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            DateTime sadasnjeVreme = DateTime.Now;
            DateTime vremeOdrzavanjaTreninga = DateTime.Parse(grupniTreningDTO.DatumIVremeTreninga);

            TimeSpan razlika = vremeOdrzavanjaTreninga - sadasnjeVreme;

            //ako je razlika dana 3, onda moze da se doda novi trening
            //ako je trening u trenerovom fitnes centru
            if (razlika.Days > 3 && trener.FitnesCentarAngazovanje.Naziv == grupniTreningDTO.FitnesCentarOdrzavanja)
            {
                GrupniTrening gt = new GrupniTrening()
                {
                    Naziv = grupniTreningDTO.Naziv,
                    TipTreninga = grupniTreningDTO.TipTreninga,
                    FitnesCentarOdrzavanja = FitnesCentarCRUD.FindFitnesCentarByName(grupniTreningDTO.FitnesCentarOdrzavanja),
                    TrajanjeTreninga = grupniTreningDTO.TrajanjeTreninga,
                    DatumIVremeTreninga = vremeOdrzavanjaTreninga,
                    MaxBrojPosetilaca = grupniTreningDTO.MaxBrojPosetilaca,
                    JeObrisan = false,
                    SpisakPosetilaca = new List<Posetilac>()
                };

                GrupniTrening newGrupniTrening = GrupniTreningCRUD.AddGrupniTrening(gt);
                trener.GrupniTreninziAngazovanje.Add(newGrupniTrening);

                GrupniTreninziFileWork.UpdateAndSaveGrupneTreninge();
                TrenerFileWork.UpdateAndSaveTrenere();
                return Ok();
            }

            return BadRequest("Trening mora biti 3 dana unapred");
        }

        [HttpDelete]
        public IHttpActionResult DeleteTrening(int idTreninga)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }


            GrupniTrening gt = GrupniTreningCRUD.FindGrupniTreningById(idTreninga);

            //da li je ovaj trening trenerov i da li se odrzava u trenerovom fitnes centru
            //i da li nema korisnika i da li je u budcnosti
            //ako je sve ovo ispunjeno, tada moze da ga brise, logicki
            if (trener.GrupniTreninziAngazovanje.Contains(gt)
                && gt.FitnesCentarOdrzavanja.IdFitnesCentra == trener.FitnesCentarAngazovanje.IdFitnesCentra
                && gt.SpisakPosetilaca.Count == 0 && gt.DatumIVremeTreninga > DateTime.Now)
            {
                gt.JeObrisan = true;


                //trener.GrupniTreninziAngazovanje.Remove(gt);
                //trener moze u sebi da ima i obrisane treninge, on ce ih videti,
                //samo korisnik ne moze da ih vidi

                GrupniTreninziFileWork.UpdateAndSaveGrupneTreninge();
                //TrenerFileWork.UpdateAndSaveTrenere();

                return Ok();

            }

            return BadRequest("Nije moguce obrisati trening.");
        }

        [HttpPut]
        public IHttpActionResult ModificateTrening(GrupniTreningDetaljnoDTO gtDTO)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            GrupniTrening gt = GrupniTreningCRUD.FindGrupniTreningById(gtDTO.IdGrupnogTreninga);

            //moze da izmeni trening iako ima prijavljenih korisnika
            //trening mora da bude u buducnosti, i moze se promeniti samo za buducnost
            //tj. ne moze da se menja trening a da mu se stavi datum u proslosti
            if (trener.GrupniTreninziAngazovanje.Contains(gt)
                && gt.FitnesCentarOdrzavanja.IdFitnesCentra == trener.FitnesCentarAngazovanje.IdFitnesCentra
                && gt.DatumIVremeTreninga > DateTime.Now && DateTime.Parse(gtDTO.DatumIVremeTreninga) > DateTime.Now)
            {
                gt.Naziv = gtDTO.Naziv;
                gt.TipTreninga = gtDTO.TipTreninga;
                gt.TrajanjeTreninga = gtDTO.TrajanjeTreninga;
                gt.DatumIVremeTreninga = DateTime.Parse(gtDTO.DatumIVremeTreninga);
                gt.MaxBrojPosetilaca = gtDTO.MaxBrojPosetilaca;

                GrupniTreninziFileWork.UpdateAndSaveGrupneTreninge();

                List<GrupniTrening> grup = GrupniTreningCRUD.ListaGrupnihTreninga;


                return Ok();

            }
            return BadRequest("Ne mozete izmeniti trening");


        }

        [HttpGet]
        public IHttpActionResult GetTreninge()
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            List<GrupniTreningDetaljnoDTO> grupniTreningDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach (GrupniTrening gt in trener.GrupniTreninziAngazovanje)
            {
                if (gt.JeObrisan != true)
                {
                    grupniTreningDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }


            return Ok(grupniTreningDTO);
        }
        [HttpGet]
        public IHttpActionResult GetPreviousTreninge()
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            List<GrupniTreningDetaljnoDTO> grupniTreningDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach (GrupniTrening gt in trener.GrupniTreninziAngazovanje)
            {
                if (gt.JeObrisan != true && gt.DatumIVremeTreninga < DateTime.Now)
                {
                    grupniTreningDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreningDTO);
        }


        [HttpGet]
        public IHttpActionResult GetPosetioceByTrening(int IdTreninga)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            GrupniTrening gt = GrupniTreningCRUD.FindGrupniTreningById(IdTreninga);
            List<PosetilacDTO> posetiociDTO = new List<PosetilacDTO>();
            if (trener.GrupniTreninziAngazovanje.Contains(gt))
            {
                foreach (Posetilac p in gt.SpisakPosetilaca)
                {
                    posetiociDTO.Add(PosetilacDTOWork.PrebaciPosetiocaUDTO(p));
                }
                return Ok(posetiociDTO);
            }

            return BadRequest("Niste angazovani na grupnom treningu");
        }

        [HttpGet]
        public IHttpActionResult GetTrening(int idGrupnogTreninga)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }
            GrupniTrening gt = GrupniTreningCRUD.FindGrupniTreningById(idGrupnogTreninga);
            if(gt != null)
            {
                GrupniTreningDetaljnoDTO gtDTO = GrupniTreningDTOWork.PrebaciTreningUDTO(gt);
                return Ok(gtDTO);
            }
            return BadRequest("Ne postoji trening");
        }


        [HttpGet]
        public IHttpActionResult FindGTByNaziv(string naziv)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach (GrupniTrening gt in trener.GrupniTreninziAngazovanje)
            {
                if (gt.Naziv.Equals(naziv) && gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
                {
                    grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);
        }

        [HttpGet]
        public IHttpActionResult FindGTByTip(string tipTreninga)
        {
            Trener trener = (Trener) HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }
            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

            foreach (GrupniTrening gt in trener.GrupniTreninziAngazovanje)
            {
                if (gt.TipTreninga.Equals(tipTreninga) && gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
                {
                    grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);
                
        }

        //ovde mozda bude post zbog evropskog vremena
        [HttpGet]
        public IHttpActionResult FindByDate(int godinaOtvaranjaMin, int godinaOtvaranjaMax)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();

          
            foreach(GrupniTrening gt in trener.GrupniTreninziAngazovanje)
            {
                if(godinaOtvaranjaMin <= gt.DatumIVremeTreninga.Year && godinaOtvaranjaMax >= gt.DatumIVremeTreninga.Year && gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
                {
                    grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);
        }


        //ovde je problem algoritam, mora lepo da se testira
        //mora da se ispravi, to cu vcrs
        [HttpPost]
        public IHttpActionResult FindByAllCategories(GrupniTreningMinMaxDTO grupniTreningDTO)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            /*DateTime datum = DateTime.Now;
            if (grupniTreningDTO.DatumIVremeTreninga != null)
                datum = DateTime.Parse(grupniTreningDTO.DatumIVremeTreninga);*/

            List<GrupniTreningDetaljnoDTO> grupniTreninziDTO = new List<GrupniTreningDetaljnoDTO>();
            
            foreach (GrupniTrening gt in trener.GrupniTreninziAngazovanje)
            {
                if (gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
                {
                    //pretrazi po sva tri
                    if (grupniTreningDTO.Naziv != null && grupniTreningDTO.Tip != null && grupniTreningDTO.GodinaOtvaranjaMax != 0 && grupniTreningDTO.GodinaOtvaranjaMin != 0)
                    {
                        if (gt.Naziv.Equals(grupniTreningDTO.Naziv) && gt.TipTreninga.Equals(grupniTreningDTO.Tip)
                            && (gt.DatumIVremeTreninga.Year >= grupniTreningDTO.GodinaOtvaranjaMin && gt.DatumIVremeTreninga.Year <= grupniTreningDTO.GodinaOtvaranjaMax))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po nazivu 
                    else if (grupniTreningDTO.Naziv != null && grupniTreningDTO.Tip == null && grupniTreningDTO.GodinaOtvaranjaMax == 0 && grupniTreningDTO.GodinaOtvaranjaMin == 0)
                    {
                        if (gt.Naziv.Equals(grupniTreningDTO.Naziv))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po tipu treninga
                    else if (grupniTreningDTO.Naziv == null && grupniTreningDTO.Tip != null && grupniTreningDTO.GodinaOtvaranjaMax == 0 && grupniTreningDTO.GodinaOtvaranjaMin == 0)
                    {
                        if (gt.TipTreninga.Equals(grupniTreningDTO.Tip))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po datumu
                    else if (grupniTreningDTO.Naziv == null && grupniTreningDTO.Tip == null && grupniTreningDTO.GodinaOtvaranjaMax != 0 && grupniTreningDTO.GodinaOtvaranjaMin != 0)
                    {
                        if (gt.DatumIVremeTreninga.Year >= grupniTreningDTO.GodinaOtvaranjaMin && gt.DatumIVremeTreninga.Year <= grupniTreningDTO.GodinaOtvaranjaMax)
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po nazivu i tipu treninga
                    else if (grupniTreningDTO.Naziv != null && grupniTreningDTO.Tip != null && grupniTreningDTO.GodinaOtvaranjaMax == 0 && grupniTreningDTO.GodinaOtvaranjaMin == 0)
                    {
                        if (gt.Naziv.Equals(grupniTreningDTO.Naziv) && gt.TipTreninga.Equals(grupniTreningDTO.Tip))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }

                    }
                    //pretrazi po nazivu i datumu odrzavanja
                    else if (grupniTreningDTO.Naziv != null && grupniTreningDTO.Tip == null && grupniTreningDTO.GodinaOtvaranjaMax != 0 && grupniTreningDTO.GodinaOtvaranjaMin != 0)
                    {
                        if (gt.Naziv.Equals(grupniTreningDTO.Naziv) && (gt.DatumIVremeTreninga.Year >= grupniTreningDTO.GodinaOtvaranjaMin && gt.DatumIVremeTreninga.Year <= grupniTreningDTO.GodinaOtvaranjaMax))
                        {
                            grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                        }
                    }
                    //pretrazi po tipu treninga i datumu odrzavanja
                    else if (grupniTreningDTO.Naziv == null && grupniTreningDTO.Tip != null && grupniTreningDTO.GodinaOtvaranjaMax != 0 && grupniTreningDTO.GodinaOtvaranjaMin != 0)
                    {
                        if (gt.TipTreninga.Equals(grupniTreningDTO.Tip) && (gt.DatumIVremeTreninga.Year >= grupniTreningDTO.GodinaOtvaranjaMin && gt.DatumIVremeTreninga.Year <= grupniTreningDTO.GodinaOtvaranjaMax))
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
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            GrupniTrening[] grupniTreninzi = trener.GrupniTreninziAngazovanje.ToArray();
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
                    if (gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
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
                    if (gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
                        grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);


        }

        [HttpGet]
        public IHttpActionResult SortGTByTip(string tipSortiranja)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            GrupniTrening[] grupniTreninzi = trener.GrupniTreninziAngazovanje.ToArray();
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
                    if (gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
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
                    if (gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
                        grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);

        }


        [HttpGet] 
        public IHttpActionResult SortGTByDatum(string tipSortiranja)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            GrupniTrening[] grupniTreninzi = trener.GrupniTreninziAngazovanje.ToArray();
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
                    if (gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
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
                    if (gt.DatumIVremeTreninga < DateTime.Now && gt.JeObrisan != true)
                        grupniTreninziDTO.Add(GrupniTreningDTOWork.PrebaciTreningUDTO(gt));
                }
            }
            return Ok(grupniTreninziDTO);

        }

        [HttpPut]
        public IHttpActionResult ModificateTrener(TrenerDTO trenerDTO)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }

            if (string.IsNullOrEmpty(trenerDTO.KorisnickoIme))
                return BadRequest("Morate uneti korisnicko ime");
            if (string.IsNullOrEmpty(trenerDTO.Lozinka))
                return BadRequest("Morate uneti lozinku");
            if (string.IsNullOrEmpty(trenerDTO.Ime))
                return BadRequest("Morate uneti ime");
            if (string.IsNullOrEmpty(trenerDTO.Prezime))
                return BadRequest("Morate uneti prezime");
            if (string.IsNullOrEmpty(trenerDTO.Email))
                return BadRequest("Morate uneti email");
            if (trenerDTO.DatumRodjenja == null)
                return BadRequest("Morate uneti datum rodjenja");
            if (PosetilacCRUD.FindPosetilacByKorisnickoIme(trenerDTO.KorisnickoIme) != null ||
                TrenerCRUD.FindTrenerByKorisnickoIme(trenerDTO.KorisnickoIme) != null ||
                VlasnikCRUD.FindVlasnikByKorisnickoIme(trenerDTO.KorisnickoIme) != null)
                return BadRequest("Vec postoji korisnik sa ovim korisnickim imenom");


            trener.KorisnickoIme = trenerDTO.KorisnickoIme;
            trener.Lozinka = trenerDTO.Lozinka;
            trener.Ime = trenerDTO.Ime;
            trener.Prezime = trenerDTO.Prezime;
            trener.Pol = (Pol)Enum.Parse(typeof(Pol), trenerDTO.Pol);
            trener.Email = trenerDTO.Email;



            TrenerFileWork.UpdateAndSaveTrenere();
            return Ok("Uspesno ste azurirali svoje podatke");
        }

        [HttpGet]
        public IHttpActionResult GetTrener(int idTrenera)
        {
            Trener trener = (Trener)HttpContext.Current.Session["trener"];
            if (trener == null)
            {
                return BadRequest("Niste logovani");
            }




            Trener t = TrenerCRUD.FindTrenerByID(idTrenera);
            TrenerDTO tDTO = TrenerDTOWork.PrebaciTreneraUDTO(t);
            return Ok(tDTO);

        }
    }
}
