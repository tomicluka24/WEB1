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
            k = (Korisnik)Session["korisnik"];
            return View(k);
        }

        public ActionResult IzmeniPodatke(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
        }

        [HttpPost]
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

        [HttpPost]
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

        [HttpPost]
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

        [HttpPost]
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
    }
}