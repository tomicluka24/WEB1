using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class AutentifikacijaController : Controller
    {
        public ActionResult Prijavljivanje()
        {
            Korisnik korisnik = new Korisnik();
            Session["korisnik"] = korisnik;
            return View(korisnik);
        }

        [HttpPost]
        public ActionResult Prijavljivanje(string korisnickoIme, string lozinka)
        {
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];
            Korisnik korisnik = korisnici.Find(u => u.KorisnickoIme.Equals(korisnickoIme) && u.Lozinka.Equals(lozinka));

            if(korisnickoIme == "")
            {
                TempData["PrijavljivanjeGreska"] = "Ne mozete ostaviti polje Korisnicko ime prazno";
                return RedirectToAction("Prijavljivanje");
            }

            if (lozinka == "")
            {
                TempData["PrijavljivanjeGreska"] = "Ne mozete ostaviti polje Lozinka prazno";
                return RedirectToAction("Prijavljivanje");
            }


            if (korisnik == null || korisnik.IsIzbrisan)
            {
                TempData["PrijavljivanjeGreska"] = "Ne postoji korisnik sa unetim korisnickim imenom i lozinkom";
                return RedirectToAction("Prijavljivanje");
            }

            Session["korisnik"] = korisnik;

            TempData["UspesnaPrijava"] = "Uspesno ste se prijavili";

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

        [HttpPost]
        public ActionResult Registracija(Korisnik korisnik)
        {
            korisnik.Uloga = "kupac";
            
            List<Korisnik> korisnici = (List<Korisnik>)HttpContext.Application["korisnici"];

            #region validacija
            Korisnik k = korisnici.Find(x => x.KorisnickoIme == korisnik.KorisnickoIme);
            if(k != null && !k.IsIzbrisan)
            {
                TempData["RegistracijaGreska"] = "Korisnik sa unetim korisnickim imenom vec postoji.";
                return RedirectToAction("Registracija");
            }

            if(korisnik.Ime == null)
            {
                TempData["RegistracijaGreska"] = "Polje ime ne sme ostati prazno.";
                return RedirectToAction("Registracija");
            }

            if (korisnik.Prezime == null)
            {
                TempData["RegistracijaGreska"] = "Polje Prezime ne sme ostati prazno.";
                return RedirectToAction("Registracija");
            }

            if (korisnik.KorisnickoIme == null)
            {
                TempData["RegistracijaGreska"] = "Polje Korisnicko ime ne sme ostati prazno.";
                return RedirectToAction("Registracija");
            }


            if (korisnik.Lozinka == null)
            {
                TempData["RegistracijaGreska"] = "Polje Lozinka ne sme ostati prazno.";
                return RedirectToAction("Registracija");
            }

            if (korisnik.Pol == null)
            {
                TempData["RegistracijaGreska"] = "Polje Pol ne sme ostati prazno.";
                return RedirectToAction("Registracija");
            }

            if (korisnik.DatumRodjenja == null)
            {
                TempData["RegistracijaGreska"] = "Polje Datum rodjenja ne sme ostati prazno.";
                return RedirectToAction("Registracija");
            }

            if (!Regex.IsMatch(korisnik.Ime, @"^[a-zA-Z]+$"))
            {
                TempData["RegistracijaGreska"] = "Za ime mozete uneti samo slova.";
                return RedirectToAction("Registracija");
            }

            if (!Regex.IsMatch(korisnik.Prezime, @"^[a-zA-Z]+$"))
            {
                TempData["RegistracijaGreska"] = "Za prezime mozete uneti samo slova.";
                return RedirectToAction("Registracija");
            }

            if(korisnik.Lozinka.Length < 5)
            {
                TempData["RegistracijaGreska"] = "Lozinka prekratka, minimum je 5 karaktera.";
                return RedirectToAction("Registracija");
            }

            if(korisnik.Pol != "muski" && korisnik.Pol != "zenski")
            {
                TempData["RegistracijaGreska"] = "U polje pol unesite ili \"muski\" ili \"zenski\".";
                return RedirectToAction("Registracija");
            }

            #endregion

            korisnici.Add(korisnik);
            Podaci.UpisiKorisnika(korisnik);
            Session["korisnik"] = korisnik;

            TempData["UspesnaRegistracija"] = "Uspesno ste se registrovali";
            return RedirectToAction("Prijavljivanje");

        }

        public ActionResult IzmeniPodatke()
        {
            Korisnik korisnik = new Korisnik();
            Session["korisnik"] = korisnik;
            return View(korisnik);
        }

        [HttpPost]
        public ActionResult IzmeniPodatke(Korisnik korisnik)
        {

           //validacija
            #region null

        if (korisnik.Ime == null)
        {
            TempData["IzmenaGreska"] = "Polje ime ne sme ostati prazno.";
            if (korisnik.Uloga == "administrator")
            {
                return RedirectToAction("IzmeniPodatke", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("IzmeniPodatke", "Kupac");
            }
            else
            {
                return RedirectToAction("IzmeniPodatke", "Prodavac");
            }
        }

        if (korisnik.Prezime == null)
        {
            TempData["IzmenaGreska"] = "Polje Prezime ne sme ostati prazno.";
            if (korisnik.Uloga == "administrator")
            {
                return RedirectToAction("IzmeniPodatke", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("IzmeniPodatke", "Kupac");
            }
            else
            {
                return RedirectToAction("IzmeniPodatke", "Prodavac");
            }
        }

        if (korisnik.Lozinka == null)
        {
            TempData["IzmenaGreska"] = "Polje Lozinka ne sme ostati prazno.";
            if (korisnik.Uloga == "administrator")
            {
                return RedirectToAction("IzmeniPodatke", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("IzmeniPodatke", "Kupac");
            }
            else
            {
                return RedirectToAction("IzmeniPodatke", "Prodavac");
            }
        }

        if (korisnik.Pol == null)
        {
            TempData["IzmenaGreska"] = "Polje Pol ne sme ostati prazno.";
            if (korisnik.Uloga == "administrator")
            {
                return RedirectToAction("IzmeniPodatke", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("IzmeniPodatke", "Kupac");
            }
            else
            {
                return RedirectToAction("IzmeniPodatke", "Prodavac");
            }
        }

        if (korisnik.DatumRodjenja == null)
        {
            TempData["IzmenaGreska"] = "Polje Datum rodjenja ne sme ostati prazno.";
            if (korisnik.Uloga == "administrator")
            {
                return RedirectToAction("IzmeniPodatke", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("IzmeniPodatke", "Kupac");
            }
            else
            {
                return RedirectToAction("IzmeniPodatke", "Prodavac");
            }
        }
        #endregion null
            #region ostalo
        if (!Regex.IsMatch(korisnik.Ime, @"^[a-zA-Z]+$"))
        {
            TempData["IzmenaGreska"] = "Za ime mozete uneti samo slova.";
            if (korisnik.Uloga == "administrator")
            {
                return RedirectToAction("IzmeniPodatke", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("IzmeniPodatke", "Kupac");
            }
            else
            {
                return RedirectToAction("IzmeniPodatke", "Prodavac");
            }
        }

        if (!Regex.IsMatch(korisnik.Prezime, @"^[a-zA-Z]+$"))
        {
            TempData["IzmenaGreska"] = "Za prezime mozete uneti samo slova.";
            if (korisnik.Uloga == "administrator")
            {
                return RedirectToAction("IzmeniPodatke", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("IzmeniPodatke", "Kupac");
            }
            else
            {
                return RedirectToAction("IzmeniPodatke", "Prodavac");
            }
        }

        if (korisnik.Lozinka.Length < 5)
        {
            TempData["IzmenaGreska"] = "Lozinka prekratka, minimum je 5 karaktera.";
            if (korisnik.Uloga == "administrator")
            {
                return RedirectToAction("IzmeniPodatke", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("IzmeniPodatke", "Kupac");
            }
            else
            {
                return RedirectToAction("IzmeniPodatke", "Prodavac");
            }
        }

        if (korisnik.Pol != "muski" && korisnik.Pol != "zenski")
        {
            TempData["IzmenaGreska"] = "U polje pol unesite ili \"muski\" ili \"zenski\".";
            if (korisnik.Uloga == "administrator")
            {
                return RedirectToAction("IzmeniPodatke", "Administrator");
            }
            else if (korisnik.Uloga == "kupac")
            {
                return RedirectToAction("IzmeniPodatke", "Kupac");
            }
            else
            {
                return RedirectToAction("IzmeniPodatke", "Prodavac");
            }
        }
            #endregion

            // Session["korisnik"] = korisnik;

            Korisnik kZaIzmenu = (Korisnik)Session["Korisnik"];
            kZaIzmenu.DatumRodjenja = korisnik.DatumRodjenja;
            kZaIzmenu.Ime = korisnik.Ime;
            kZaIzmenu.Prezime = korisnik.Prezime;
            kZaIzmenu.Pol = korisnik.Pol;
            kZaIzmenu.Lozinka = korisnik.Lozinka;
            Podaci.IzmeniKorisnika(kZaIzmenu);

            TempData["IzmenaPodataka"] = "Podaci uspesno izmenjeni";
            if (kZaIzmenu.Uloga == "administrator")
            {
                return RedirectToAction("PrikaziProfilAdministratora", "Administrator");
            }
            else if (kZaIzmenu.Uloga == "kupac")
            {
                return RedirectToAction("PrikaziProfilKupca", "Kupac");
            }
            else
            {
                return RedirectToAction("PrikaziProfilProdavca", "Prodavac");
            }
        }

        public ActionResult Odjava()
        {
            Session["korisnik"] = null;

            ViewBag.Odjava = $"Odjavili ste se";
            return View("Prijavljivanje");
            //return RedirectToAction("Prijavljivanje", "Autentifikacija");
        }
    }
}