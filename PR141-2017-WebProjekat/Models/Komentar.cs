using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class Komentar
    {
        public string Kupac { get; set; }
        public Manifestacija Manifestacija { get; set; }
        public string Tekst { get; set; }
        public int Ocena { get; set; }
        public bool IsIzbrisan { get; set; }
        public Komentar()
        {
            Kupac = "";
            Manifestacija = new Manifestacija();
            Tekst = "";
            Ocena = 0;
            IsIzbrisan = false;
        }

        public Komentar(string kupac, Manifestacija manifestacija, string tekst, int ocena, bool isIzbrisan)
        {
            Kupac = kupac;
            Manifestacija = manifestacija;
            Tekst = tekst;
            Ocena = ocena;
            IsIzbrisan = isIzbrisan;
        }
    }
}