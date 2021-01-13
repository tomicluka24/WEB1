using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class AutentifikacijaController : Controller
    {
        public ActionResult Prijavljivanje(string korisnickoIme, string lozinka)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            Korisnik korisnik = korisnici.Find(u => u.KorisnickoIme.Equals(korisnickoIme) && u.Lozinka.Equals(lozinka));

            if (korisnik == null)
            {
                return View("Prijavljivanje");
            }

            Session["korisnik"] = korisnik;

            if (korisnik.Uloga == "administrator")
                return RedirectToAction("Index", "Administrator");
            else if (korisnik.Uloga == "prodavac")
                return RedirectToAction("Index", "Prodavac");
            else
                return RedirectToAction("Index", "Kupac");
        }

        public ActionResult Registracija()
        {
            Korisnik korisnik = new Korisnik();
            Session["korisnik"] = korisnik;
            return View(korisnik);
        }

        public ActionResult Registracija(Korisnik korisnik)
        {
            korisnik.Uloga = "kupac";
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            korisnici.Add(korisnik);
            Podaci.UpisiKorisnika(korisnik);
            Session["korisnik"] = korisnik;

            return RedirectToAction("Prijavljivanje", "Autentifikacija");
        }

        public ActionResult IzmeniPodatke(Korisnik korisnik)
        {
            Session["korisnik"] = korisnik;
            //korisnik = (Korisnik)Session["korisnik"];
            

            Podaci.IzmeniKorisnika(korisnik);
            if(korisnik.Uloga == "administrator")
            {
                return RedirectToAction("PrikaziProfilAdministratora", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("PrikaziProfilKupca", "Kupac");
            }
            else
            {
                return RedirectToAction("PrikaziProfilProdavca", "Prodavac");
            }
        }

        public ActionResult IzmeniPodatkeManifestacije(Manifestacija m)
        {
            Session["manifestacija"] = m;
            //korisnik = (Korisnik)Session["korisnik"];


            Podaci.IzmeniManifestaciju(m);

            HttpContext.Application["manifestacije"] = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            return RedirectToAction("PrikaziManifestacijeProdavca", "Prodavac");
        }

        public ActionResult Odjava()
        {
            Session["korisnik"] = null;

            ViewBag.odjava = $"Odjavili ste se";
             return View("Prijavljivanje");
            //return RedirectToAction("Index", "Home");
        }
    }
}