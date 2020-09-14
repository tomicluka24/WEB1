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
        public bool IsAktivno { get; set; }
        public MestoOdrzavanja MestoOdrzavanja { get; set; }

        //dodaj za sliku
        public Manifestacija()
        {
            Naziv = "";
            TipManifestacije = "";
            BrojMesta = 0;
            DatumIVremeOdrzavanja = new DateTime();
            CenaRegularneKarte = 0;
            IsAktivno = false;
            MestoOdrzavanja = new MestoOdrzavanja();
        }

        public Manifestacija(string naziv, string tipManifestacije, int brojMesta, DateTime datumIVremeOdrzavanja, double cenaRegularneKarte,  MestoOdrzavanja mestoOdrzavanja)
        {
            Naziv = naziv;
            TipManifestacije = tipManifestacije;
            BrojMesta = brojMesta;
            DatumIVremeOdrzavanja = datumIVremeOdrzavanja;
            CenaRegularneKarte = cenaRegularneKarte;
            IsAktivno = false;
            MestoOdrzavanja = mestoOdrzavanja;
        }
    }
}