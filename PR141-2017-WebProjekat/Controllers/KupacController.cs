using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class KupacController : Controller
    {
        // GET: Kupac
        public ActionResult Index()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
          //  List<Manifestacija> sortiraneManifestacije = manifestacije.OrderBy(o => o.DatumIVremeOdrzavanja).ToList();
            return View(manifestacije);
        }

        public ActionResult PrikaziProfilKupca(Korisnik k)
        {
            //List<Karta> karte= (List<Karta>)HttpContext.Application["karte"];
            //Dictionary<string, Karta> karteRecnik = karte.ToDictionary(x => x.Manifestacija.Naziv, x => x);
           // Dictionary<string, Karta> karteRecnik = (Dictionary<string, Karta>)HttpContext.Application["karte"];
            k = (Korisnik)Session["korisnik"];
           // k.SveKarteBezObziraNaStatus = karteRecnik;
            return View(k);
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
            return RedirectToAction("Index", "Kupac");
        }
        public ActionResult SortirajKartePoNazivu(string naziv)
        {
            Dictionary<string, Karta> karte = (Dictionary<string, Karta>)HttpContext.Application["karte"];
            Dictionary<string, Karta> karteSortirane = new Dictionary<string, Karta>();
            if (naziv == "a-z")
            {
                 // karteSortirane = karte.OrderBy(o => o.Value.Manifestacija.Naziv);
                //var sortedDict = from entry in karte orderby entry.Value.Manifestacija.Naziv ascending select entry;
            }

            if (naziv == "z-a")
            {
                //var sortedDict = from entry in karte orderby entry.Value.Manifestacija.Naziv ascending select entry;
                //sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ThenBy(o => o.DatumIVremeOdrzavanja).ToList();
                //karteSortirane = (Dictionary<string, Karta>)karte.OrderByDescending(o => o.Value.Manifestacija.Naziv);
            }
            HttpContext.Application["karte"] = karteSortirane;
            //Session["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
        }

        public ActionResult UkloniFilter()
        {
            HttpContext.Application["manifestacije"] = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Kupac");
        }

        public ActionResult UkloniFilterKarata()
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            HttpContext.Application["karte"] = k.SveKarteBezObziraNaStatus;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
        }
        public ActionResult PretragaPoNazivuManifestacije(string naziv)
        {
            Dictionary<string,Karta> kZaPrikaz = new Dictionary<string, Karta>();
            Dictionary<string, Karta> karte = (Dictionary<string, Karta>)HttpContext.Application["karte"];
            foreach (var item in karte)
            {
                if (item.Value.Manifestacija.Naziv == naziv)
                {
                    kZaPrikaz.Add(item.Key,item.Value);
                }
            }

            HttpContext.Application["karte"] = kZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }
        public ActionResult PretragaPoCeniKarte(double cena)
        {
            Dictionary<string, Karta> kZaPrikaz = new Dictionary<string, Karta>();
            Dictionary<string, Karta> karte = (Dictionary<string, Karta>)HttpContext.Application["karte"];
            foreach (var item in karte)
            {
                if (item.Value.Cena == cena)
                {
                    kZaPrikaz.Add(item.Key, item.Value);
                }
            }

            HttpContext.Application["karte"] = kZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }
        public ActionResult PretragaKarataPoDatumu(DateTime donjaGranica, DateTime gornjaGranica)
        {
            Dictionary<string, Karta> kZaPrikaz = new Dictionary<string, Karta>();
            Dictionary<string, Karta> karte = (Dictionary<string, Karta>)HttpContext.Application["karte"];
            foreach (var item in karte)
            {
                if (item.Value.Manifestacija.DatumIVremeOdrzavanja >= donjaGranica && item.Value.Manifestacija.DatumIVremeOdrzavanja <= gornjaGranica)
                {
                    kZaPrikaz.Add(item.Key, item.Value);
                }
            }

            HttpContext.Application["karte"] = kZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }
    }
}