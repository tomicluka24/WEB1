using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class AdministratorController : Controller
    {
       
        public ActionResult Index()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            //manifestacije = manifestacije.OrderByDescending(x => x.DatumIVremeOdrzavanja).ToList();
            return View(manifestacije);
        }

        public ActionResult PrikaziProfilAdministratora(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
        }

        public ActionResult KreirajProdavca()
        {
            Korisnik korisnik = new Korisnik();
            Session["korisnik"] = korisnik;
            return View(korisnik);

            //return View();
        }

        [HttpPost]
        public ActionResult KreirajProdavca(Korisnik korisnik)
        {
            korisnik.Uloga = "prodavac";
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];

            #region validacija
            Korisnik k = korisnici.Find(x => x.KorisnickoIme == korisnik.KorisnickoIme);
            if (k != null && !k.IsIzbrisan)
            {
                TempData["KreiranjeProdavcaGreska"] = "Korisnik sa unetim korisnickim imenom vec postoji.";
                return RedirectToAction("KreirajProdavca");
            }

            if (korisnik.Ime == null)
            {
                TempData["KreiranjeProdavcaGreska"] = "Polje ime ne sme ostati prazno.";
                return RedirectToAction("KreirajProdavca");
            }

            if (korisnik.Prezime == null)
            {
                TempData["KreiranjeProdavcaGreska"] = "Polje Prezime ne sme ostati prazno.";
                return RedirectToAction("KreirajProdavca");
            }

            if (korisnik.KorisnickoIme == null)
            {
                TempData["KreiranjeProdavcaGreska"] = "Polje Korisnicko ime ne sme ostati prazno.";
                return RedirectToAction("KreirajProdavca");
            }


            if (korisnik.Lozinka == null)
            {
                TempData["KreiranjeProdavcaGreska"] = "Polje Lozinka ne sme ostati prazno.";
                return RedirectToAction("KreirajProdavca");
            }

            if (korisnik.Pol == null)
            {
                TempData["KreiranjeProdavcaGreska"] = "Polje Pol ne sme ostati prazno.";
                return RedirectToAction("KreirajProdavca");
            }

            if (korisnik.DatumRodjenja == null)
            {
                TempData["KreiranjeProdavcaGreska"] = "Polje Datum rodjenja ne sme ostati prazno.";
                return RedirectToAction("KreirajProdavca");
            }

            if (!Regex.IsMatch(korisnik.Ime, @"^[a-zA-Z]+$"))
            {
                TempData["KreiranjeProdavcaGreska"] = "Za ime mozete uneti samo slova.";
                return RedirectToAction("KreirajProdavca");
            }

            if (!Regex.IsMatch(korisnik.Prezime, @"^[a-zA-Z]+$"))
            {
                TempData["KreiranjeProdavcaGreska"] = "Za prezime mozete uneti samo slova.";
                return RedirectToAction("KreirajProdavca");
            }

            if (korisnik.Lozinka.Length < 5)
            {
                TempData["KreiranjeProdavcaGreska"] = "Lozinka prekratka, minimum je 5 karaktera.";
                return RedirectToAction("KreirajProdavca");
            }

            if (korisnik.Pol != "muski" && korisnik.Pol != "zenski")
            {
                TempData["KreiranjeProdavcaGreska"] = "U polje pol unesite ili \"muski\" ili \"zenski\".";
                return RedirectToAction("KreirajProdavca");
            }

            #endregion
            korisnici.Add(korisnik);
            Podaci.UpisiKorisnika(korisnik);

            //return RedirectToAction("KreirajProdavca", "Administrator");
            //ViewBag.ProdavacDodan = "Uspesno ste dodali prodavca";
            TempData["DodanProdavac"] = "Uspesno ste dodali prodavca";
            return RedirectToAction("IzlistajSveKorisnike");
        }

        public ActionResult IzlistajSveKorisnike()
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            Korisnik korisnik = (Korisnik)Session["Korisnik"];
            return View(korisnici);
        }

        public ActionResult IzlistajSveKarte()
        {
            //List<Karta> karte = (List<Karta>)HttpContext.Application["karte"];
            Dictionary<string, Karta> karte = Podaci.IscitajKarte("~/App_Data/karte.txt");
            List<Karta> listaKarta = karte.Values.ToList();

          

            return View(listaKarta);
        }

        public ActionResult IzmeniPodatke(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
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
            return RedirectToAction("Index", "Administrator");
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
            return RedirectToAction("Index", "Administrator");
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
            return RedirectToAction("Index", "Administrator");
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
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult SortirajPoImenu(string ime)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> sortiraniKorisnici = new List<Korisnik>();
            if (ime == "a-z")
            {
                sortiraniKorisnici = korisnici.OrderBy(o => o.Ime).ToList();
            }

            if (ime == "z-a")
            {
                //sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ThenBy(o => o.DatumIVremeOdrzavanja).ToList();
                sortiraniKorisnici = korisnici.OrderByDescending(o => o.Ime).ToList();
            }
            HttpContext.Application["korisnici"] = sortiraniKorisnici;
            //Session["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("IzlistajSveKorisnike", "Administrator");
        }

        public ActionResult SortirajPoPrezimenu(string prezime)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> sortiraniKorisnici = new List<Korisnik>();
            if (prezime == "a-z")
            {
                sortiraniKorisnici = korisnici.OrderBy(o => o.Prezime).ToList();
            }

            if (prezime == "z-a")
            {
                //sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ThenBy(o => o.DatumIVremeOdrzavanja).ToList();
                sortiraniKorisnici = korisnici.OrderByDescending(o => o.Prezime).ToList();
            }
            HttpContext.Application["korisnici"] = sortiraniKorisnici;
            //Session["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("IzlistajSveKorisnike", "Administrator");
        }

        public ActionResult SortirajPoBrojuBodova(string brojBodova)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> sortiraniKorisnici = new List<Korisnik>();
            if (brojBodova == "uzlazno")
            {
                sortiraniKorisnici = korisnici.OrderBy(o => o.BrojSakupljenihBodova).ToList();
            }

            if (brojBodova == "silazno")
            {
                //sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ThenBy(o => o.DatumIVremeOdrzavanja).ToList();
                sortiraniKorisnici = korisnici.OrderByDescending(o => o.BrojSakupljenihBodova).ToList();
            }
            HttpContext.Application["korisnici"] = sortiraniKorisnici;
            //Session["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("IzlistajSveKorisnike", "Administrator");
        }

        public ActionResult SortirajPoKorisnickomImenu(string korisnickoIme)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> sortiraniKorisnici = new List<Korisnik>();
            if (korisnickoIme == "a-z")
            {
                sortiraniKorisnici = korisnici.OrderBy(o => o.KorisnickoIme).ToList();
            }

            if (korisnickoIme == "z-a")
            {
                //sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ThenBy(o => o.DatumIVremeOdrzavanja).ToList();
                sortiraniKorisnici = korisnici.OrderByDescending(o => o.KorisnickoIme).ToList();
            }
            HttpContext.Application["korisnici"] = sortiraniKorisnici;
            //Session["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("IzlistajSveKorisnike", "Administrator");
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
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult UkloniFilter()
        {
            HttpContext.Application["manifestacije"] = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult FiltrirajPoTipuKupca(string tip)
        {
            List<Korisnik> kZaPrikaz = new List<Korisnik>();
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            foreach (var item in korisnici)
            {
                if (item.TipKorisnika.ImeTipa == tip)
                {
                    kZaPrikaz.Add(item);
                }
            }

            HttpContext.Application["korisnici"] = kZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("IzlistajSveKorisnike", "Administrator");
        }

        public ActionResult UkloniFilterZaKupce()
        {
            HttpContext.Application["korisnici"] = Podaci.IscitajKorisnike("~/App_Data/korisnici.txt");
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("IzlistajSveKorisnike", "Administrator");
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
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult PretragaPoMestu(string mesto)
        {
            if (mesto == "")
            {
                TempData["PretragaPoMestuGreska"] = "Ostavili ste prazno polje za pretragu po mestu.";
                return RedirectToAction("Index");
            }

            //if (!Regex.IsMatch(mesto, @"^[a-zA-Z]+$"))
            //{
            //    TempData["PretragaPoMestuGreska"] = "Mozete uneti samo slova za pretragu mesta.";
            //    return RedirectToAction("Index");
            //}

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
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult PretragaPoCeni(double? donjaGranica, double? gornjaGranica)
        {
            if(donjaGranica == null)
            {
                TempData["PretragaPoCeniGreska"] = "Ostavili ste prazno polje Od prilikom pretrage po ceni.";
                return RedirectToAction("Index");
            }

            if (gornjaGranica == null)
            {
                TempData["PretragaPoCeniGreska"] = "Ostavili ste prazno polje Do prilikom pretrage po ceni.";
                return RedirectToAction("Index");
            }


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
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult PretragaPoDatumu(DateTime? donjaGranica, DateTime? gornjaGranica)
        {
            if (donjaGranica == null)
            {
                TempData["PretragaPoDatumuGreska"] = "Ostavili ste prazno polje Od prilikom pretrage po datumu.";
                return RedirectToAction("Index");
            }

            if (gornjaGranica == null)
            {
                TempData["PretragaPoDatumuGreska"] = "Ostavili ste prazno polje Do prilikom pretrage po datumu.";
                return RedirectToAction("Index");
            }

            if (donjaGranica == null || gornjaGranica == null)
            {
                TempData["PretragaPoDatumuGreska"] = "Ostavili ste prazno polje Do prilikom pretrage po datumu.";
                return RedirectToAction("Index");
            }

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
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult ObrisiManifestaciju(string naziv)
        {
            Manifestacija mZaBrisanje = new Manifestacija();
            //List<Manifestacija> manifestacije = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            //List<Manifestacija> manifestacije = (List<Manifestacija>)Session["manifestacije"];
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];

            foreach (var item in manifestacije)
            {
                if (item.Naziv == naziv && item.IsIzbrisana != true)
                {
                    mZaBrisanje = item;
                    mZaBrisanje.IsIzbrisana = true;
                    break;
                }
            }

            Podaci.IzmeniManifestaciju(mZaBrisanje);
            HttpContext.Application["manifestacije"] = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            TempData["UspesnoObrisanaManifestacija"] = "Uspesno ste obrisali manifestaciju";
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult OdobriManifestaciju(string naziv)
        {
            Manifestacija mZaOdobravanje = new Manifestacija();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];

            foreach (var item in manifestacije)
            {
                if (item.Naziv == naziv && item.IsIzbrisana == true)
                {
                    TempData["IzbrisanaManifestacija"] = "Ne mozete odobriti izbrisanu manifestaciju";
                    return RedirectToAction("Index", "Administrator");
                }

                if (item.Naziv == naziv && item.IsAktivna && !item.IsIzbrisana)
                {
                    TempData["VecOdobrena"] = "Manifestacija je vec odobrena";
                    return RedirectToAction("Index", "Administrator");
                }

                if (item.Naziv == naziv && !item.IsAktivna && item.IsIzbrisana != true)
                {
                    mZaOdobravanje = item;
                    mZaOdobravanje.IsAktivna = true;
                    break;
                }
            }

            Podaci.IzmeniManifestaciju(mZaOdobravanje);
            HttpContext.Application["manifestacije"] = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            TempData["UspesnoOdobrena"] = "Uspesno ste odobrili manifestaciju";
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult ObrisiKorisnika(string korisnickoIme)
        {
            Korisnik kZaBrisanje = new Korisnik();
            List<Korisnik> korisnici = Podaci.IscitajKorisnike("~/App_Data/korisnici.txt");

            foreach (var item in korisnici)
            {
                if (item.KorisnickoIme == korisnickoIme && item.IsIzbrisan!=true)
                {
                    kZaBrisanje = item;
                    kZaBrisanje.IsIzbrisan = true;
                    break;
                }
            }

            Podaci.IzmeniKorisnika(kZaBrisanje);
            HttpContext.Application["korisnici"] = Podaci.IscitajKorisnike("~/App_Data/korisnici.txt");
            TempData["UspesnoObrisanKorisnik"] = "Uspesno ste obrisali korisnika";
            return RedirectToAction("IzlistajSveKorisnike", "Administrator");
        }

        public ActionResult ObrisiKomentar(double idKomentara)
        {
            Komentar kZaBrisanje = new Komentar();
            List<Komentar> komentari = Podaci.IscitajKomentare("~/App_Data/komentari.txt");

            foreach (var item in komentari)
            {
                if (item.IdKomentara == idKomentara && item.IsIzbrisan != true)
                {
                    kZaBrisanje = item;
                    kZaBrisanje.IsIzbrisan = true;
                    break;
                }
            }

            Podaci.IzmeniKomentar(kZaBrisanje);
            HttpContext.Application["komentari"] = Podaci.IscitajKomentare("~/App_Data/komentari.txt");
            TempData["UspesnoObrisanKomentar"] = "Uspesno ste obrisali komentar";
            return RedirectToAction("Index", "Administrator");
        }
    }
}