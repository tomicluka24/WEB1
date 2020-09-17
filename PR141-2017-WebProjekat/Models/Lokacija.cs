using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class Lokacija
    {
        public double GeografskaDuzina { get; set; }
        public double GeografskaSirina { get; set; }
        public MestoOdrzavanja MestoOdrzavanja { get; set; }
        public bool IsIzbrisana { get; set; }

        public Lokacija()
        {
            GeografskaDuzina = 0;
            GeografskaSirina = 0;
            MestoOdrzavanja = new MestoOdrzavanja();
            IsIzbrisana = false;
        }

        public Lokacija(double geografskaDuzina, double geografskaSirina, MestoOdrzavanja mestoOdrzavanja, bool isIzbrisana)
        {
            GeografskaDuzina = geografskaDuzina;
            GeografskaSirina = geografskaSirina;
            MestoOdrzavanja = mestoOdrzavanja;
            IsIzbrisana = isIzbrisana;
        }
    }
}