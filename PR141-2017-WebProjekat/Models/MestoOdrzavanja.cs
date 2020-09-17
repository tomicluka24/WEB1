using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class MestoOdrzavanja
    {
        public string Mesto { get; set; }
        public double PostanskiBroj { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public bool IsIzbrisano { get; set; }
        public MestoOdrzavanja()
        {
            Mesto = "";
            PostanskiBroj = 0;
            Ulica = "";
            Broj = "";
            IsIzbrisano = false;
        }

        public MestoOdrzavanja(string mesto, double postanskiBroj, string ulica, string broj, bool isIzbrisano)
        {
            Mesto = mesto;
            PostanskiBroj = postanskiBroj;
            Ulica = ulica;
            Broj = broj;
            IsIzbrisano = isIzbrisano;
        }
    }
}