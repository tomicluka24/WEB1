using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            //List<Manifestacija> sortiraneManifestacije = manifestacije.OrderBy(o => o.DatumIVremeOdrzavanja).ToList();
            List<UploadedFile> files = (List<UploadedFile>)HttpContext.Application["files"];

            return View(manifestacije);
        }

        public ActionResult PrikaziManifestaciju(string Naziv)
        {
            //m = (Manifestacija)Session["manifestacija"];
            Manifestacija mZaPrikaz = new Manifestacija();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.Naziv == Naziv)
                {
                    mZaPrikaz = item;
                    break;
                }
            }

                return View(mZaPrikaz);
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
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "Home");
        }
    }
}