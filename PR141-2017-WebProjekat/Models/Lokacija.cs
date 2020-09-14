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

    }
}