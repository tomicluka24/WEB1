using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class Manifestacija
    {
        public string Naziv { get; set; }
        public string TipManifestacije { get; set; }
        public int BrojMesta { get; set; }
        public DateTime DatumIVremeOdrzavanja { get; set; }
        public double CenaRegularneKarte { get; set; }
        public MestoOdrzavanja MestoOdrzavanja { get; set; }
        public bool IsAktivna { get; set; }
        public bool IsIzbrisana { get; set; }
        public string Slika { get; set; }

        //dodaj za sliku
        public Manifestacija()
        {
            Naziv = "";
            TipManifestacije = "";
            BrojMesta = 0;
            DatumIVremeOdrzavanja = new DateTime();
            CenaRegularneKarte = 0;
            MestoOdrzavanja = new MestoOdrzavanja();
            IsAktivna = true;
            IsIzbrisana = false;
            Slika = "";
        }

        public Manifestacija(string naziv, string tipManifestacije, int brojMesta, DateTime datumIVremeOdrzavanja, double cenaRegularneKarte, MestoOdrzavanja mestoOdrzavanja, bool isAktivna, bool isIzbrisana, string slika)
        {
            Naziv = naziv;
            TipManifestacije = tipManifestacije;
            BrojMesta = brojMesta;
            DatumIVremeOdrzavanja = datumIVremeOdrzavanja;
            CenaRegularneKarte = cenaRegularneKarte;
            MestoOdrzavanja = mestoOdrzavanja;
            IsAktivna = isAktivna;
            IsIzbrisana = isIzbrisana;
            Slika = slika;
        }
    }
}