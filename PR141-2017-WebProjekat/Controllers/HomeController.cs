using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            List<UploadedFile> files = (List<UploadedFile>)HttpContext.Application["files"];
            manifestacije = manifestacije.OrderByDescending(x => x.DatumIVremeOdrzavanja).ToList();
            return View(manifestacije);
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

            if (korisnik.Uloga == "administrator")
            {
                foreach (var komentar in komentari)
                {
                    // i odobrene i neodobrene
                    if (komentar.Manifestacija == Naziv)
                    {
                        kZaPrikaz.Add(komentar);
                    }
                }
            }

            if (korisnik.Uloga == "kupac")
            {
                foreach (var komentar in komentari)
                {
                    // samo odobrene
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

            if(naziv == "z-a")
            {
                //sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ThenBy(o => o.DatumIVremeOdrzavanja).ToList();
                sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ToList();
            }
            HttpContext.Application["manifestacije"] = sortiraneManifestacije;
            //Session["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("Index","Home");
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
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }

        public ActionResult FiltrirajPoTipu(string tip)
        {
            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List <Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.TipManifestacije == tip)
                {
                    mZaPrikaz.Add(item);
                }
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UkloniFilter()
        {
            HttpContext.Application["manifestacije"] = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Home");
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
            

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PretragaPoMestu(string mesto)
        {
            if (mesto == "")
            {
                TempData["PretragaPoMestuGreska"] = "Ostavili ste prazno polje za pretragu po mestu.";
                return RedirectToAction("Index");
            }

            //if(!Regex.IsMatch(mesto, @"^[a-zA-Z]+$"))
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
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PretragaPoCeni(double? donjaGranica, double? gornjaGranica)
        {
            if(donjaGranica < 0 || gornjaGranica < 0)
            {
                TempData["PretragaPoCeniGreska"] = "Ne mozete uneti negativan broj za cenu.";
                return RedirectToAction("Index");
            }
            if (donjaGranica == null && gornjaGranica == null)
            {
                TempData["PretragaPoCeniGreska"] = "Ostavili ste prazno polje za pretragu po ceni.";
                return RedirectToAction("Index");
            }
            if(donjaGranica > gornjaGranica)
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
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }
    }
}