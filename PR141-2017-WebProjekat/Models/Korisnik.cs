using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class Korisnik
    {
        public string KorisnickoIme { get; set; } //jedinstveno
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pol { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public string Uloga { get; set; }
        public Dictionary<string, Karta> SveKarteBezObziraNaStatus { get; set; } //ako je korisnik kupac
        public List<Manifestacija> Manifestacije { get; set; } //ako je korisnik prodavac
        public double? BrojSakupljenihBodova { get; set; } //ako je korisnik kupac
        public TipKorisnika TipKorisnika { get; set; }  //ako je korisnik kupac
        public bool IsIzbrisan { get; set; }

        public Korisnik()
        {
            KorisnickoIme = "";
            Lozinka = "";
            Ime = "";
            Prezime = "";
            Pol = "";
            DatumRodjenja = new DateTime();
            Uloga = "";
            SveKarteBezObziraNaStatus = new Dictionary<string, Karta>();
            Manifestacije = new List<Manifestacija>();
            BrojSakupljenihBodova = 0;
            TipKorisnika = new TipKorisnika();
            IsIzbrisan = false;
        }

        //za ucitavanje admina
        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, string pol, DateTime datumRodjenja, string uloga, bool isIzbrisan)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            DatumRodjenja = datumRodjenja;
            Uloga = uloga;
            SveKarteBezObziraNaStatus = new Dictionary<string, Karta>();
            Manifestacije = new List<Manifestacija>();
            BrojSakupljenihBodova = 0;
            TipKorisnika = new TipKorisnika();
            IsIzbrisan = isIzbrisan;
        }

        //za ucitavanje prodavca
        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, string pol, DateTime datumRodjenja, string uloga, List<Manifestacija> manifestacije, bool isIzbrisan)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            DatumRodjenja = datumRodjenja;
            Uloga = uloga;
            SveKarteBezObziraNaStatus = new Dictionary<string, Karta>();
            Manifestacije = manifestacije;
            BrojSakupljenihBodova = 0;
            TipKorisnika = new TipKorisnika();
            IsIzbrisan = isIzbrisan;
        }

        //za ucitavanje kupca
        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, string pol, DateTime datumRodjenja, string uloga, Dictionary<string, Karta> sveKarteBezObziraNaStatus, double brojSakupljenihBodova, TipKorisnika tipKorisnika, bool isIzbrisan)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            DatumRodjenja = datumRodjenja;
            Uloga = uloga;
            SveKarteBezObziraNaStatus = sveKarteBezObziraNaStatus;
            Manifestacije = new List<Manifestacija>();
            BrojSakupljenihBodova = brojSakupljenihBodova;
            TipKorisnika = tipKorisnika;
            IsIzbrisan = isIzbrisan;
        }
    }
}