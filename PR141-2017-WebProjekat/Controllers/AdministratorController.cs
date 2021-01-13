using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class AdministratorController : Controller
    {
       
        public ActionResult Index()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
           // List<Manifestacija> sortiraneManifestacije = manifestacije.OrderBy(o => o.DatumIVremeOdrzavanja).ToList();
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
        }

        public ActionResult KreirajProdavca(Korisnik korisnik)
        {
            korisnik.Uloga = "prodavac";
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            korisnici.Add(korisnik);
            Podaci.UpisiKorisnika(korisnik);

            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult IzlistajSveKorisnike()
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];

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
            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.Naziv == naziv)
                {
                    mZaPrikaz.Add(item);
                }
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult PretragaPoMestu(string mesto)
        {
            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.MestoOdrzavanja.Mesto == mesto)
                {
                    mZaPrikaz.Add(item);
                }
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult PretragaPoCeni(double donjaGranica, double gornjaGranica)
        {
            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.CenaRegularneKarte >= donjaGranica && item.CenaRegularneKarte <= gornjaGranica)
                {
                    mZaPrikaz.Add(item);
                }
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult PretragaPoDatumu(DateTime donjaGranica, DateTime gornjaGranica)
        {
            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.DatumIVremeOdrzavanja >= donjaGranica && item.DatumIVremeOdrzavanja <= gornjaGranica)
                {
                    mZaPrikaz.Add(item);
                }
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Administrator");
        }

        public ActionResult ObrisiManifestaciju(string naziv)
        {
            Manifestacija mZaBrisanje = new Manifestacija();
            List<Manifestacija> manifestacije = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");

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
            return RedirectToAction("IzlistajSveKorisnike", "Administrator");
        }
    }
}