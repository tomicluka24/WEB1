using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class ProdavacController : Controller
    {
        public ActionResult Index()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            //manifestacije = (List<Manifestacija>)Session["manifestacije"];
            manifestacije = manifestacije.OrderByDescending(x => x.DatumIVremeOdrzavanja).ToList();
            return View(manifestacije);
        }

        public ActionResult PrikaziProfilProdavca(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
        }

        public ActionResult PrikaziManifestacijeProdavca(Korisnik k)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            k = (Korisnik)Session["korisnik"];

            return View(k);
        }

        public ActionResult ApdejtujManifestacijeProdavca(Korisnik k)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            k = (Korisnik)Session["korisnik"];

            //int brManifestacija = 0;
            List<Manifestacija> apdejtovaneManifestacije = new List<Manifestacija>();
            //foreach (Manifestacija m in k.Manifestacije)
            //{
            //    brManifestacija++;
            //}
            //for (int i = 0; i < brManifestacija; i++)
            //{
            //    apdejtovaneManifestacije[i] = manifestacije.Find(manifestacija => manifestacija.Naziv == k.Manifestacije[i].Naziv);
            //}
            //k.Manifestacije = apdejtovaneManifestacije;

            foreach (Manifestacija m in manifestacije)
            {
                foreach (Manifestacija m2 in k.Manifestacije)
                {
                    if(m.Naziv == m2.Naziv)
                    {
                        apdejtovaneManifestacije.Add(m);
                    }
                }
            }

            k.Manifestacije = apdejtovaneManifestacije;


            return RedirectToAction("PrikaziManifestacijeProdavca");
        }

        public ActionResult IzmeniPodatke(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
        }

        public ActionResult IzmeniPodatkeManifestacije()
        {
            Manifestacija manifestacija = (Manifestacija)Session["manifestacija"];
            return View(manifestacija);

        }

        [HttpPost]
        public ActionResult IzmeniPodatkeManifestacije(Manifestacija manifestacija)
        {
            // validacija
            #region null

            if (manifestacija.Naziv == null)
            {
                TempData["IzmeniManifestacijuGreska"] = "Polje Naziv ne sme ostati prazno.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }

            if (manifestacija.TipManifestacije == null)
            {
                TempData["IzmeniManifestacijuGreska"] = "Polje Tip ne sme ostati prazno.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }

            if (manifestacija?.BrojMesta == null)
            {
                TempData["IzmeniManifestacijuGreska"] = "Polje Broj mesta ne sme ostati prazno.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }

            if (manifestacija.DatumIVremeOdrzavanja == null)
            {
                TempData["IzmeniManifestacijuGreska"] = "Polje Datum i vreme odrzavanja ne sme ostati prazno.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }

            if (manifestacija?.CenaRegularneKarte == null)
            {
                TempData["IzmeniManifestacijuGreska"] = "Polje Cena regularne karte ne sme ostati prazno.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }

            if (manifestacija.MestoOdrzavanja.Mesto == null)
            {
                TempData["IzmeniManifestacijuGreska"] = "Polje Mesto ne sme ostati prazno.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }

            if (manifestacija.MestoOdrzavanja?.PostanskiBroj == null)
            {
                TempData["IzmeniManifestacijuGreska"] = "Polje Postanski broj ne sme ostati prazno.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }

            if (manifestacija.MestoOdrzavanja.Ulica == null)
            {
                TempData["IzmeniManifestacijuGreska"] = "Polje Ulica ne sme ostati prazno.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }

            if (manifestacija.MestoOdrzavanja.Broj == null)
            {
                TempData["IzmeniManifestacijuGreska"] = "Polje Broj ne sme ostati prazno.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }




            #endregion

            #region ostalo
            List<Manifestacija> manifestacije = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            Manifestacija m = manifestacije.Find(x => x.Naziv == manifestacija.Naziv);

            if (!Regex.IsMatch(manifestacija.MestoOdrzavanja.Mesto, @"^[a-zA-Z]+$"))
            {
                TempData["IzmeniManifestacijuGreska"] = "Za mesto mozete uneti samo slova.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }

            //if (!Regex.IsMatch(manifestacija.MestoOdrzavanja.Ulica, @"^[a-zA-Z]+$"))
            //{
            //    TempData["IzmeniManifestacijuGreska"] = "Za ulicu mozete uneti samo slova.";
            //    return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            //}

            if (!Regex.IsMatch(manifestacija.TipManifestacije, @"^[a-zA-Z]+$"))
            {
                TempData["IzmeniManifestacijuGreska"] = "Za tip mozete uneti samo slova.";
                return RedirectToAction("IzmeniPodatkeManifestacije", "Prodavac");
            }


            #endregion

            Podaci.IzmeniManifestaciju(manifestacija);
            HttpContext.Application["manifestacije"] = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            TempData["UspesnaIzmenaManifestacije"] = "Uspesno ste izmenili manifestaciju";
            return RedirectToAction("ApdejtujManifestacijeProdavca");
        }

        public ActionResult PrikaziKarteManifestacija(Korisnik k)
        {
            ////karte  = (List<Karta>)Session["karte"];
            ////Session["karte"] = karte;
            //k = (Korisnik)Session["korisnik"];
            //Dictionary<string, Karta> karte = Podaci.IscitajKarte("~/App_Data_karte.txt");
            //Podaci p = new Podaci();
            //List<Karta> karte = Podaci.PronadjiKarteZaManifestacije(k);

            //return View(karte);

            k = (Korisnik)Session["korisnik"];
            Dictionary<string, Karta> karte = Podaci.IscitajKarte("~/App_Data/karte.txt");
            List<Karta> karteZaPrikaz = new List<Karta>();

            foreach (Karta karta in karte.Values)
            {
                for (int i = 0; i < k.Manifestacije.Count(); i++)
                {
                    if(karta.Manifestacija.Naziv == k.Manifestacije[i].Naziv)
                    {
                        karteZaPrikaz.Add(karta);
                    }
                }
            }
            return View(karteZaPrikaz);
        }

        public ActionResult DodajManifestaciju()
        {
            Manifestacija manifestacija = new Manifestacija();
            Session["manifestacija"] = manifestacija;
            return View(manifestacija);
        }

        [HttpPost]
        public ActionResult DodajManifestaciju(Manifestacija manifestacija)
        {
            manifestacija.Poster = manifestacija.Naziv + ".jpg";
           // m.Slika = Path.GetFileName(file.FileName);
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];

            // validacija
            #region null

            if (manifestacija.Naziv == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Naziv ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }

            if (manifestacija.TipManifestacije == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Tip ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }

            if (manifestacija?.BrojMesta == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Broj mesta ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }

            if (manifestacija.DatumIVremeOdrzavanja == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Datum i vreme odrzavanja ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }

            if (manifestacija?.CenaRegularneKarte == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Cena regularne karte ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }

            if (manifestacija.MestoOdrzavanja.Mesto == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Mesto ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }

            if (manifestacija.MestoOdrzavanja?.PostanskiBroj == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Postanski broj ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }

            if (manifestacija.MestoOdrzavanja.Ulica == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Ulica ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }

            if (manifestacija.MestoOdrzavanja.Broj == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Broj ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }

            if(manifestacija.Poster == null)
            {
                TempData["DodajManifestacijuGreska"] = "Polje Poster ne sme ostati prazno.";
                return RedirectToAction("DodajManifestaciju");
            }



            #endregion

            #region ostalo

            Manifestacija m = manifestacije.Find(x => x.Naziv == manifestacija.Naziv);
            if (m != null && !m.IsIzbrisana)
            {
                TempData["DodajManifestacijuGreska"] = "Manifetacija sa unetim nazivom vec postoji.";
                return RedirectToAction("DodajManifestaciju");
            }

            Manifestacija mDM = manifestacije.Find(x => x.DatumIVremeOdrzavanja == manifestacija.DatumIVremeOdrzavanja && x.MestoOdrzavanja == manifestacija.MestoOdrzavanja);
            if (m != null && !m.IsIzbrisana)
            {
                TempData["DodajManifestacijuGreska"] = "Manifestacija vec postoji u zadato vreme na zadatom mestu.";
                return RedirectToAction("DodajManifestaciju");
            }



            //if (!Regex.IsMatch(manifestacija.MestoOdrzavanja.Mesto, @"^[a-zA-Z]+$"))
            //{
            //    TempData["DodajManifestacijuGreska"] = "Za mesto mozete uneti samo slova.";
            //    return RedirectToAction("DodajManifestaciju");
            //}

            //if (!Regex.IsMatch(manifestacija.MestoOdrzavanja.Ulica, @"^[a-zA-Z]+$"))
            //{
            //    TempData["DodajManifestacijuGreska"] = "Za ulicu mozete uneti samo slova.";
            //    return RedirectToAction("DodajManifestaciju");
            //}

            //if (!Regex.IsMatch(manifestacija.TipManifestacije, @"^[a-zA-Z]+$"))
            //{
            //    TempData["DodajManifestacijuGreska"] = "Za tip mozete uneti samo slova.";
            //    return RedirectToAction("DodajManifestaciju");
            //}


            #endregion

            manifestacija.IsAktivna = false;
            manifestacije.Add(manifestacija);
            Podaci.UpisiManifestaciju(manifestacija);
            Session["manifestacije"] = manifestacije;

            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult SortirajPoNazivu(string naziv)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> sortiraneManifestacije = new List<Manifestacija>();
            if (naziv == "a-z")
            {
                //moze i ovako ako se to trazi..
                //sortiraneManifestacije = manifestacije.OrderBy(o => o.Naziv).ThenBy(o => o.DatumIVremeOdrzavanja).ToList();
                sortiraneManifestacije = manifestacije.OrderBy(o => o.Naziv).ToList();
            }

            if (naziv == "z-a")
            {
                //sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ThenBy(o => o.DatumIVremeOdrzavanja).ToList();
                sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ToList();
            }
            HttpContext.Application["manifestacije"] = sortiraneManifestacije;
            //Session["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult SortirajPoDatumu(string datum)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> sortiraneManifestacije = new List<Manifestacija>();
            if (datum == "najskorije prvo")
            {
                sortiraneManifestacije = manifestacije.OrderBy(o => o.DatumIVremeOdrzavanja).ToList();
            }

            if (datum == "najskorije poslednje")
            {
                sortiraneManifestacije = manifestacije.OrderByDescending(o => o.DatumIVremeOdrzavanja).ToList();
            }
            HttpContext.Application["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult SortirajPoMestu(string mesto)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> sortiraneManifestacije = new List<Manifestacija>();
            if (mesto == "a-z")
            {
                sortiraneManifestacije = manifestacije.OrderBy(o => o.MestoOdrzavanja.Mesto).ToList();
            }

            if (mesto == "z-a")
            {
                sortiraneManifestacije = manifestacije.OrderByDescending(o => o.MestoOdrzavanja.Mesto).ToList();
            }
            HttpContext.Application["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult SortirajPoCeniKarte(string cenaKarte)
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> sortiraneManifestacije = new List<Manifestacija>();
            if (cenaKarte == "jeftinije prvo")
            {
                sortiraneManifestacije = manifestacije.OrderBy(o => o.CenaRegularneKarte).ToList();
            }

            if (cenaKarte == "skuplje prvo")
            {
                sortiraneManifestacije = manifestacije.OrderByDescending(o => o.CenaRegularneKarte).ToList();
            }
            HttpContext.Application["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult PrikaziManifestaciju(string Naziv)
        {
            Manifestacija mZaPrikaz = new Manifestacija();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];

            Korisnik korisnik = (Korisnik)Session["Korisnik"];


            foreach (var item in manifestacije)
            {
                if (item.Naziv == Naziv)
                {
                    mZaPrikaz = item;
                    break;
                }
            }

            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];
            List<Komentar> kZaPrikaz = new List<Komentar>();

            var man = korisnik.Manifestacije.Find(x => x.Naziv == Naziv);
            if (man != null)
            {
                foreach (var komentar in komentari)
                {
                    //i odobrene i neodobrene
                    if (komentar.Manifestacija == Naziv)
                    {
                        kZaPrikaz.Add(komentar);
                    }
                }
            }
            else
            {
                foreach (var komentar in komentari)
                {
                    //i odobrene i neodobrene
                    if (komentar.Manifestacija == Naziv && komentar.IsOdobren == true)
                    {
                        kZaPrikaz.Add(komentar);
                    }
                }
            }
          

            Tuple<Manifestacija, List<Komentar>> tuple = new Tuple<Manifestacija, List<Komentar>>(mZaPrikaz, kZaPrikaz);


            Session["manifestacija"] = mZaPrikaz;
            return View(tuple);
        }

        public ActionResult FiltrirajPoTipu(string tip)
        {
            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.TipManifestacije == tip)
                {
                    mZaPrikaz.Add(item);
                }
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult UkloniFilter()
        {
            HttpContext.Application["manifestacije"] = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult PretragaPoNazivu(string naziv)
        {
            if (naziv == "")
            {
                TempData["PretragaPoNazivuGreska"] = "Ostavili ste prazno polje za pretragu po nazivu.";
                return RedirectToAction("Index");
            }

            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.Naziv == naziv)
                {
                    mZaPrikaz.Add(item);
                }
            }

            if (!mZaPrikaz.Any())
            {
                TempData["PretragaPoNazivuGreska"] = "Trazena manifestacija ne postoji.";
                return RedirectToAction("Index");
            }


            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult PretragaPoMestu(string mesto)
        {
            if (mesto == "")
            {
                TempData["PretragaPoMestuGreska"] = "Ostavili ste prazno polje za pretragu po mestu.";
                return RedirectToAction("Index");
            }

            if (!Regex.IsMatch(mesto, @"^[a-zA-Z]+$"))
            {
                TempData["PretragaPoMestuGreska"] = "Mozete uneti samo slova za pretragu mesta.";
                return RedirectToAction("Index");
            }

            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.MestoOdrzavanja.Mesto == mesto)
                {
                    mZaPrikaz.Add(item);
                }
            }

            if (!mZaPrikaz.Any())
            {
                TempData["PretragaPoMestuGreska"] = "Manifestacije u trazenom mestu ne postoje.";
                return RedirectToAction("Index");
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult PretragaPoCeni(double? donjaGranica, double? gornjaGranica)
        {
            if (donjaGranica < 0 || gornjaGranica < 0)
            {
                TempData["PretragaPoCeniGreska"] = "Ne mozete uneti negativan broj za cenu.";
                return RedirectToAction("Index");
            }
            if (donjaGranica == null && gornjaGranica == null)
            {
                TempData["PretragaPoCeniGreska"] = "Ostavili ste prazno polje za pretragu po ceni.";
                return RedirectToAction("Index");
            }
            if (donjaGranica > gornjaGranica)
            {
                TempData["PretragaPoCeniGreska"] = "U levo polje upisite manju a u desno polje vecu cenu.";
                return RedirectToAction("Index");
            }

            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.CenaRegularneKarte >= donjaGranica && item.CenaRegularneKarte <= gornjaGranica)
                {
                    mZaPrikaz.Add(item);
                }
            }

            if (!mZaPrikaz.Any())
            {
                TempData["PretragaPoCeniGreska"] = "Manifestacije u trazenom opsegu cena ne postoje.";
                return RedirectToAction("Index");
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult PretragaPoDatumu(DateTime? donjaGranica, DateTime? gornjaGranica)
        {
            if (donjaGranica == null || gornjaGranica == null)
            {
                TempData["PretragaPoDatumuGreska"] = "Ostavili ste prazno polje za pretragu po datumu.";
                return RedirectToAction("Index");
            }

            if (DateTime.Compare((DateTime)donjaGranica, (DateTime)gornjaGranica) > 0)
            {
                TempData["PretragaPoDatumuGreska"] = "U levo polje upisite blizi a u desno polje dalji datum.";
                return RedirectToAction("Index");
            }

            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.DatumIVremeOdrzavanja >= donjaGranica && item.DatumIVremeOdrzavanja <= gornjaGranica)
                {
                    mZaPrikaz.Add(item);
                }
            }

            if (!mZaPrikaz.Any())
            {
                TempData["PretragaPoDatumuGreska"] = "Manifestacije u trazenom opsegu datume ne postoje.";
                return RedirectToAction("Index");
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Prodavac");
        }

        public ActionResult PrikaziKomentare(string naziv)
        {
            if (naziv == null)
            {
                naziv = TempData["naziv"].ToString();
            }

            List<Komentar> komentari = Podaci.IscitajKomentare("~/App_Data/komentari.txt");
            List<Komentar> kZaPrikaz = new List<Komentar>();

            foreach (Komentar komentar in komentari)
            {
                if (komentar.Manifestacija == naziv)
                    kZaPrikaz.Add(komentar);
            }

            if (kZaPrikaz.Count == 0)
            {
                TempData["NemaKomentara"] = "Nema komentara za izabranu manifestaciju.";
                return RedirectToAction("PrikaziManifestacijeProdavca");
            }

            return View(kZaPrikaz);
        }

        public ActionResult OdobriKomentar(double IdKomentara)
        {
            Komentar kZaOdobravanje = new Komentar();
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];

            foreach (var item in komentari)
            {
                if (item.IdKomentara == IdKomentara && item.IsIzbrisan == true)
                {
                    TempData["IzbrisanKomentar"] = "Ne mozete odobriti izbrisan komentar";
                    return RedirectToAction("PrikaziKomentare");
                }

                if (item.IdKomentara == IdKomentara && item.IsOdobren == true && item.IsIzbrisan == false)
                {
                    TempData["VecOdobren"] = "Komentar je vec odobren";
                    return RedirectToAction("PrikaziKomentare");
                }

                if (item.IdKomentara == IdKomentara && item.IsOdobren == false && item.IsIzbrisan == false)
                {
                    kZaOdobravanje = item;
                    kZaOdobravanje.IsOdobren = true;
                    break;
                }
            }

            Podaci.IzmeniKomentar(kZaOdobravanje);
            HttpContext.Application["komentari"] = Podaci.IscitajKomentare("~/App_Data/komentari.txt");

            TempData["KomentarOdobren"] = "Uspesno ste odobrili komentar";
            TempData["naziv"] = kZaOdobravanje.Manifestacija;
            return RedirectToAction("PrikaziKomentare");

        }
    }
}