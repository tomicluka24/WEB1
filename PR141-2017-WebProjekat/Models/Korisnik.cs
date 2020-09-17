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
        public DateTime DatumRodjenja { get; set; }
        public string Uloga { get; set; }
        public Dictionary<string, Karta> SveKarteBezObziraNaStatus { get; set; } //ako je korisnik kupac
        public List<Manifestacija> Manifestacije { get; set; } //ako je korisnik prodavac
        public double BrojSakupljenihBodova { get; set; } //ako je korisnik prodavac
        public TipKorisnika TipKorisnika { get; set; }
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

        public Korisnik(string korisnickoIme, string lozinka, string ime, string prezime, string pol, DateTime datumRodjenja, string uloga, double brojSakupljenihBodova, TipKorisnika tipKorisnika, bool isIzbrisan)
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
            BrojSakupljenihBodova = brojSakupljenihBodova;
            TipKorisnika = tipKorisnika;
            IsIzbrisan = isIzbrisan;
        }

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
    }
}