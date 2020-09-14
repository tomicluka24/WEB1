using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class Korisnik
    {
        public string KorisnickoIme { get; set; } //jedinstveno
        public double Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pol { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Uloga { get; set; }
        //public Dictionary<string, Karta> SveKarteBezObziraNaStatus { get; set; } //ako je korisnik kupac
        //public List<Manifestacija> Manifestacije { get; set; } //ako je korisnik prodavac
        public double BrojSakupljenihBodova { get; set; } //ako je korisnik prodavac
        public string TipKorisnika { get; set; } // koja je razlika izmedju uloge i tipa?

    }
}