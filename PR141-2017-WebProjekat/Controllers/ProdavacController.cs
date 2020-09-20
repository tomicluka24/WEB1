using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult DodajManifestaciju()
        {
            Manifestacija manifestacija = new Manifestacija();
            Session["manifestacija"] = manifestacija;
            return View(manifestacija);
        }

        [HttpPost]
        public ActionResult DodajManifestaciju(Manifestacija m)
        {
            bool datumIVremeIsti = false;
            bool mestoIsto = false;
           // m.Slika = Path.GetFileName(file.FileName);
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];

            foreach (var item in manifestacije)
            {
                if (item.DatumIVremeOdrzavanja == m.DatumIVremeOdrzavanja)
                    datumIVremeIsti = true;
                if (item.MestoOdrzavanja == m.MestoOdrzavanja)
                    mestoIsto = true;
            }

            if (mestoIsto == false || datumIVremeIsti == false)
            {
                manifestacije.Add(m);
                Podaci.UpisiManifestaciju(m);
            }
            else
                ViewBag.Message = "Vreme i mesto su zauzeti za drugu manifestaciju";

            //List<UploadedFile> files = (List<UploadedFile>)HttpContext.Application["files"];
            //try
            //{
            //    if (file.ContentLength > 0)
            //    {
            //        string fileName = Path.GetFileName(file.FileName);
            //        string path = Path.Combine(Server.MapPath("~/Files/")),
            //    }
                
            //}
            //catch 
            //{

            //    ViewBag.Message = "File upload failed!";
            //}

            return RedirectToAction("Index", "Prodavac");
        }

        //public ActionResult DodajManifestaciju(Manifestacija m)
        //{
        //    //m.IsAktivna = false;
        //    //m.IsIzbrisana = false;
        //   List<Manifestacija> manifestacije = (List<Manifestacija>)Session["manifestacije"];
            
        //    bool datumIVremeIsti = false;
        //    bool mestoIsto = false;

        //    foreach (var item in manifestacije)
        //    {
        //        if (item.DatumIVremeOdrzavanja == m.DatumIVremeOdrzavanja)
        //            datumIVremeIsti = true;
        //        if (item.MestoOdrzavanja == m.MestoOdrzavanja)
        //            mestoIsto = true;
        //    }

        //    if (mestoIsto == false || datumIVremeIsti == false)
        //        Podaci.UpisiManifestaciju(m);
        //    else
        //        ViewBag.Message = "Vreme i mesto su zauzeti za drugu manifestaciju";

        //    return View(m);
        //}
    }
}