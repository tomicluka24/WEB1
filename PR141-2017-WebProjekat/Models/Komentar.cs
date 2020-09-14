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


    }
}