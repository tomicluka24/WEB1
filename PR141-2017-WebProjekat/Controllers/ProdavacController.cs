using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class ProdavacController : Controller
    {
        // GET: Prodavac
        public ActionResult Index()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> sortiraneManifestacije = manifestacije.OrderBy(o => o.DatumIVremeOdrzavanja).ToList();
            return View(sortiraneManifestacije);
        }

        public ActionResult PrikaziProfilProdavca(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
        }

        public ActionResult PrikaziManifestacijeProdavca(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
        }

        public ActionResult IzmeniPodatke(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
        }

        public ActionResult PrikaziKarteManifestacija(Korisnik k)
        {
            ////karte  = (List<Karta>)Session["karte"];
            ////Session["karte"] = karte;
            //k = (Korisnik)Session["korisnik"];
            //Dictionary<string, Karta> karte = Podaci.IscitajKarte("~/App_Data_karte.txt");
            //Podaci p = new Podaci();
            //List<Karta> karte = Podaci.PronadjiKarteZaManifestacije(k);

            //return View(karte);

            k = (Korisnik)Session["korisnik"];
            Dictionary<string, Karta> karte = Podaci.IscitajKarte("~/App_Data/karte.txt");
            List<Karta> karteZaPrikaz = new List<Karta>();

            foreach (Karta karta in karte.Values)
            {
                for (int i = 0; i < k.Manifestacije.Count(); i++)
                {
                    if(karta.Manifestacija.Naziv == k.Manifestacije[i].Naziv)
                    {
                        karteZaPrikaz.Add(karta);
                    }
                }
            }
            return View(karteZaPrikaz);
        }
    }
}