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
        // GET: Administrator
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

        [HttpPost]
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
            return RedirectToAction("Index", "Administrator");
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
            return RedirectToAction("Index", "Administrator");
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
            return RedirectToAction("Index", "Administrator");
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
            return RedirectToAction("Index", "Administrator");
        }

        [HttpPost]
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

        [HttpPost]
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

        [HttpPost]
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
    }
}