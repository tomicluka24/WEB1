using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class Karta
    {
        public string ID { get; set; }
        public Manifestacija Manifestacija { get; set; }
        public DateTime DatumIVremeManifestacijce { get; set; }
        public double Cena { get; set; }
        public string Kupac { get; set; }
        public bool IsRezervisana { get; set; }
        public TipKarte TipKarte { get; set; }
        public bool IsIzbrisana { get; set; }

        public Karta()
        {
            ID = "";
            Manifestacija = new Manifestacija();
            DatumIVremeManifestacijce = new DateTime();
            Cena = 0;
            Kupac = "";
            IsRezervisana = false;
            TipKarte = 0;
            IsIzbrisana = false;
        }

        public Karta(string iD, Manifestacija manifestacija, DateTime datumIVremeManifestacijce, double cena, string kupac, bool isRezervisana, TipKarte tipKarte, bool isIzbrisana)
        {
            ID = iD;
            Manifestacija = manifestacija;
            DatumIVremeManifestacijce = datumIVremeManifestacijce;
            Cena = cena;
            Kupac = kupac;
            IsRezervisana = isRezervisana;
            TipKarte = tipKarte;
            IsIzbrisana = isIzbrisana;
        }
    }
}