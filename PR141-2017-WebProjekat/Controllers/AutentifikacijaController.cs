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
        // GET: Autentifikacija
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registracija(Korisnik korisnik)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            korisnici.Add(korisnik);
           //    Podaci.SaveUser(korisnik);
            Session["korisnik"] = korisnik;

            return RedirectToAction("Logovanje", "Autentifikacija"); //return to login when registration is completed
        }

        [HttpPost]
        public ActionResult Prijavljivanje(string korisnickoIme, string lozinka)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            Korisnik korisnik = korisnici.Find(u => u.KorisnickoIme.Equals(korisnickoIme) && u.Lozinka.Equals(lozinka));

            if (korisnik == null)
            {
                ViewBag.Message = $"Korisnik ne postoji!";
                return View("Index");
            }

            Session["korisnik"] = korisnik;

            if (korisnik.Uloga == "administrator")
                return RedirectToAction("Index", "Administrator");
            else if (korisnik.Uloga == "prodavac")
                return RedirectToAction("Index", "Prodavac");
            else
                return RedirectToAction("Index", "Kupac");


        }
    }
}