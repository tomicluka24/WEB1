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
        public int? BrojMesta { get; set; }
        public DateTime? DatumIVremeOdrzavanja { get; set; }
        public double? CenaRegularneKarte { get; set; }
        public MestoOdrzavanja MestoOdrzavanja { get; set; }
        public bool IsAktivna { get; set; }
        public bool IsIzbrisana { get; set; }
        public string Poster { get; set; }

        
        public Manifestacija()
        {
            Naziv = "";
            TipManifestacije = "";
            BrojMesta = 1;
            DatumIVremeOdrzavanja = new DateTime();
            CenaRegularneKarte = 0;
            MestoOdrzavanja = new MestoOdrzavanja();
            IsAktivna = true;
            IsIzbrisana = false;
            Poster = "";
        }

        public Manifestacija(string naziv, string tipManifestacije, int brojMesta, DateTime datumIVremeOdrzavanja, double cenaRegularneKarte, MestoOdrzavanja mestoOdrzavanja, bool isAktivna, bool isIzbrisana, string poster)
        {
            Naziv = naziv;
            TipManifestacije = tipManifestacije;
            BrojMesta = brojMesta;
            DatumIVremeOdrzavanja = datumIVremeOdrzavanja;
            CenaRegularneKarte = cenaRegularneKarte;
            MestoOdrzavanja = mestoOdrzavanja;
            IsAktivna = isAktivna;
            IsIzbrisana = isIzbrisana;
            Poster = poster;
        }
    }
}