using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class KupacController : Controller
    {
        public ActionResult Index()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            manifestacije = manifestacije.OrderByDescending(x => x.DatumIVremeOdrzavanja).ToList();
            return View(manifestacije);
        }

        public ActionResult PrikaziProfilKupca(Korisnik k)
        {
            //List<Karta> karte= (List<Karta>)HttpContext.Application["karte"];
            //Dictionary<string, Karta> karteRecnik = karte.ToDictionary(x => x.Manifestacija.Naziv, x => x);
           // Dictionary<string, Karta> karteRecnik = (Dictionary<string, Karta>)HttpContext.Application["karte"];
            k = (Korisnik)Session["korisnik"];
           // k.SveKarteBezObziraNaStatus = karteRecnik;
            return View(k);
        }

        public ActionResult IzmeniPodatke(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
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

            if (naziv == "z-a")
            {
                //sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ThenBy(o => o.DatumIVremeOdrzavanja).ToList();
                sortiraneManifestacije = manifestacije.OrderByDescending(o => o.Naziv).ToList();
            }
            HttpContext.Application["manifestacije"] = sortiraneManifestacije;
            //Session["manifestacije"] = sortiraneManifestacije;
            return RedirectToAction("Index", "Kupac");
        }

        public ActionResult SortirajKartePoNazivu(string naziv)
        {
            Korisnik k = (Korisnik)Session["korisnik"];

            if (naziv == "a-z")

            {
                // k.SveKarteBezObziraNaStatus = (Dictionary<string, Karta>)(from entry in k.SveKarteBezObziraNaStatus orderby entry.Value.Manifestacija.Naziv ascending select entry);
                k.SveKarteBezObziraNaStatus = k.SveKarteBezObziraNaStatus.OrderBy(x => x.Value.Manifestacija.Naziv).ToDictionary(x => x.Key, x => x.Value);
            }

            if (naziv == "z-a")
            {
                //  k.SveKarteBezObziraNaStatus = (Dictionary<string, Karta>)(from entry in k.SveKarteBezObziraNaStatus orderby entry.Value.Manifestacija.Naziv descending select entry);
                k.SveKarteBezObziraNaStatus = k.SveKarteBezObziraNaStatus.OrderByDescending(x => x.Value.Manifestacija.Naziv).ToDictionary(x => x.Key, x => x.Value);
            }

            Session["korisnik"] = k;
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }

        public ActionResult SortirajKartePoDatumu(string datum)
        {
            Korisnik k = (Korisnik)Session["korisnik"];

            if (datum == "najskorije prvo")

            {
                k.SveKarteBezObziraNaStatus = k.SveKarteBezObziraNaStatus.OrderBy(x => x.Value.DatumIVremeManifestacijce).ToDictionary(x => x.Key, x => x.Value);
            }

            if (datum == "najskorije poslednje")
            {
                k.SveKarteBezObziraNaStatus = k.SveKarteBezObziraNaStatus.OrderByDescending(x => x.Value.DatumIVremeManifestacijce).ToDictionary(x => x.Key, x => x.Value);
            }

            Session["korisnik"] = k;
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }

        public ActionResult SortirajKartePoCeni(string cena)
        {
            Korisnik k = (Korisnik)Session["korisnik"];

            if (cena == "jeftinije prvo")

            {
                k.SveKarteBezObziraNaStatus = k.SveKarteBezObziraNaStatus.OrderBy(x => x.Value.Cena).ToDictionary(x => x.Key, x => x.Value);
            }

            if (cena == "skuplje prvo")
            {
                k.SveKarteBezObziraNaStatus = k.SveKarteBezObziraNaStatus.OrderByDescending(x => x.Value.Cena).ToDictionary(x => x.Key, x => x.Value);
            }

            Session["korisnik"] = k;
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
        }

        public ActionResult FiltrirajPoTipu(string tip)
        {
            List<Manifestacija> mZaPrikaz = new List<Manifestacija>();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.TipManifestacije == tip)
                {
                    mZaPrikaz.Add(item);
                }
            }

            HttpContext.Application["manifestacije"] = mZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Kupac");
        }

        public ActionResult FiltrirajPoTipuKarte(string tip)
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            Dictionary<string, Karta> kZaPrikaz = new Dictionary<string, Karta>();
            Dictionary<string, Karta> karte = (Dictionary<string, Karta>)HttpContext.Application["karte"];
            foreach (var item in k.SveKarteBezObziraNaStatus)
            {
                if (item.Value.TipKarte.ToString() == tip)
                {
                    kZaPrikaz.Add(item.Key,item.Value);
                }
            }
            k.SveKarteBezObziraNaStatus = kZaPrikaz;
            Session["korisnik"] = k;
            //HttpContext.Application["karte"] = kZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }

        public ActionResult FiltrirajPoStatusu(string status)
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            Dictionary<string, Karta> kZaPrikaz = new Dictionary<string, Karta>();
            Dictionary<string, Karta> karte = (Dictionary<string, Karta>)HttpContext.Application["karte"];

            bool isRezervisana = false;
            if (status == "rezervisana")
                isRezervisana = true;
          

            foreach (var item in karte)
            {
                if (item.Value.IsRezervisana == isRezervisana)
                {
                    kZaPrikaz.Add(item.Key,item.Value);
                }
            }

            k.SveKarteBezObziraNaStatus = kZaPrikaz;
            Session["korisnik"] = k;
            //HttpContext.Application["karte"] = kZaPrikaz;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }

        public ActionResult UkloniFilter()
        {
            HttpContext.Application["manifestacije"] = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("Index", "Kupac");
        }

        public ActionResult UkloniFilterKarata()
        {
            List<Korisnik> korisnici = Podaci.IscitajKorisnike("~/App_Data/korisnici.txt");

            Korisnik k = (Korisnik)Session["korisnik"];

            foreach (var item in korisnici)
           {
                if(k.Ime == item.Ime)
                {
                    k = item;
                }
            }


            HttpContext.Application["karte"] = k.SveKarteBezObziraNaStatus;
            HttpContext.Application["korisnik"] = k;
            Session["korisnik"] = k;
            //Session["manifestacije"] = mZaPrikaz;        
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
        }

        public ActionResult PretragaPoMestu(string mesto)
        {
            if (mesto == "")
            {
                TempData["PretragaPoMestuGreska"] = "Ostavili ste prazno polje za pretragu po mestu.";
                return RedirectToAction("Index");
            }

            if (!Regex.IsMatch(mesto, @"^[a-zA-Z]+$"))
            {
                TempData["PretragaPoMestuGreska"] = "Mozete uneti samo slova za pretragu mesta.";
                return RedirectToAction("Index");
            }

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
            return RedirectToAction("Index", "Kupac");
        }

        public ActionResult PretragaPoCeni(double? donjaGranica, double? gornjaGranica)
        {
            if (donjaGranica < 0 || gornjaGranica < 0)
            {
                TempData["PretragaPoCeniGreska"] = "Ne mozete uneti negativan broj za cenu.";
                return RedirectToAction("Index");
            }
            if (donjaGranica == null && gornjaGranica == null)
            {
                TempData["PretragaPoCeniGreska"] = "Ostavili ste prazno polje za pretragu po ceni.";
                return RedirectToAction("Index");
            }
            if (donjaGranica > gornjaGranica)
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
            return RedirectToAction("Index", "Kupac");
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
            return RedirectToAction("Index", "Kupac");
        }

        public ActionResult PretragaPoNazivuManifestacije(string naziv)
        {

            if (naziv == "")
            {
                TempData["PretragaKarataPoNazivuGreska"] = "Ostavili ste prazno polje za pretragu po nazivu.";
                return RedirectToAction("PrikaziProfilKupca");
            }

            Korisnik k = (Korisnik)Session["korisnik"];
            Dictionary<string, Karta> kZaPrikaz = new Dictionary<string, Karta>();

            string key = k.SveKarteBezObziraNaStatus.FirstOrDefault(x => x.Value.Manifestacija.Naziv == naziv).Key;

            if (key == null)
            {
                TempData["PretragaKarataPoNazivuGreska"] = "Karta sa unesenim nazivom ne postoji.";
                return RedirectToAction("PrikaziProfilKupca");
            }

            Karta karta = k.SveKarteBezObziraNaStatus[key];


            kZaPrikaz.Add(key, karta);

            k.SveKarteBezObziraNaStatus = kZaPrikaz;
            Session["korisnik"] = k;
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }

        public ActionResult PretragaPoCeniKarte(double? cena)
        {
            if (cena == null)
            {
                TempData["PretragaKarataPoCeniGreska"] = "Ostavili ste prazno polje za pretragu po ceni.";
                return RedirectToAction("PrikaziProfilKupca");
            }
            Korisnik k = (Korisnik)Session["korisnik"];
            Dictionary<string, Karta> kZaPrikaz = new Dictionary<string, Karta>();

            string key = k.SveKarteBezObziraNaStatus.FirstOrDefault(x => x.Value.Cena == cena).Key;


            if (key == null)
            {
                TempData["PretragaKarataPoCeniGreska"] = "Karta sa unesenom cenom ne postoji.";
                return RedirectToAction("PrikaziProfilKupca");
            }
            Karta karta = k.SveKarteBezObziraNaStatus[key];


            kZaPrikaz.Add(key, karta);

            k.SveKarteBezObziraNaStatus = kZaPrikaz;
            Session["korisnik"] = k;
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }

        public ActionResult PretragaKarataPoDatumu(DateTime? datumIVreme)
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            Dictionary<string, Karta> kZaPrikaz = new Dictionary<string, Karta>();

            if (datumIVreme == null)
            {
                TempData["PretragaKarataPoDatumuGreska"] = "Ostavili ste prazno polje za pretragu po datumu.";
                return RedirectToAction("PrikaziProfilKupca");
            }


            string key = k.SveKarteBezObziraNaStatus.FirstOrDefault(x => x.Value.DatumIVremeManifestacijce == datumIVreme).Key;
            if (key == null)
            {
                TempData["PretragaKarataPoDatumuGreska"] = "Karte za trazeni datum i vreme ne postoje.";
                return RedirectToAction("PrikaziProfilKupca");
            }

            Karta karta = k.SveKarteBezObziraNaStatus[key];


            kZaPrikaz.Add(key, karta);

            
            k.SveKarteBezObziraNaStatus = kZaPrikaz;
            Session["korisnik"] = k;
            return RedirectToAction("PrikaziProfilKupca", "Kupac");
        }

        public ActionResult OstaviKomentar(string naziv)
        {
            Komentar komentar = new Komentar();
            komentar.Manifestacija = naziv;
            Random random = new Random();
            komentar.IdKomentara = random.Next(0, 99999);
            Session["Komentar"] = komentar;

            return View("OstaviKomentar");
        }

        [HttpPost]
        public ActionResult OstaviKomentar(Komentar k)
        {

            if (k?.Ocena == null)
            {
                TempData["KomentarGreska"] = "Ne mozete ostaviti polje Ocena prazno";
                return RedirectToAction("OstaviKomentar");
            }

            if (k.Tekst == null)
            {
                TempData["KomentarGreska"] = "Ne mozete ostaviti polje Tekst prazno";
                return RedirectToAction("OstaviKomentar");
            }

            Komentar komentar = (Komentar)Session["Komentar"];
            Korisnik korisnik = (Korisnik)Session["Korisnik"];
            komentar.IsIzbrisan = false;
            komentar.IsOdobren = false;
            komentar.Kupac = korisnik.KorisnickoIme;
            komentar.Ocena = k.Ocena;
            komentar.Tekst = k.Tekst;

            //provera da ne postoji id fali
            Podaci.UpisiKomentar(komentar);

            TempData["KomentarUspesnoOstavljen"] = "Uspesno ste ostavili komentar, bice objavljen nakon sto ga administrator odobri";
            return RedirectToAction("PrikaziProfilKupca");
        }
    }
}