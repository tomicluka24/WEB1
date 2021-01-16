using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class Komentar
    {
        public double IdKomentara { get; set; }
        public string Kupac { get; set; }
        public string Manifestacija { get; set; }
        public string Tekst { get; set; }
        public int? Ocena { get; set; }
        public bool IsOdobren { get; set; }
        public bool IsIzbrisan { get; set; }

        public Komentar()
        {
            IdKomentara = 0;
            Kupac = "";
            Manifestacija = "";
            Tekst = "";
            Ocena = 0;
            IsOdobren = false;
            IsIzbrisan = false;
        }

        public Komentar(double idKomentara, string kupac, string manifestacija, string tekst, int ocena, bool isOdobren, bool isIzbrisan)
        {
            IdKomentara = idKomentara;
            Kupac = kupac;
            Manifestacija = manifestacija;
            Tekst = tekst;
            Ocena = ocena;
            IsOdobren = isOdobren;
            IsIzbrisan = isIzbrisan;
        }
    }
}