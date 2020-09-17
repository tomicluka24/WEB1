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
            List<Manifestacija> sortiraneManifestacije = manifestacije.OrderBy(o => o.DatumIVremeOdrzavanja).ToList();
            return View(sortiraneManifestacije);
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

        public ActionResult IzmeniPodatke()
        {
            return View();
        }

        public ActionResult IzlistajSveKorisnike()
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];

            return View(korisnici);
        }

        public ActionResult IzlistajSveKarte()
        {
            List<Karta> karte = (List<Karta>)HttpContext.Application["karte"];

            return View(karte);
        }
    }
}