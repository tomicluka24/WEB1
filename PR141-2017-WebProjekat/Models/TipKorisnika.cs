using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR141_2017_WebProjekat.Models
{
    public class TipKorisnika
    {
        public string ImeTipa { get; set; }
        public double Popust { get; set; }
        public double TrazeniBrojBodova { get; set; }

        public TipKorisnika()
        {
            ImeTipa = "";
            Popust = 0;
            TrazeniBrojBodova = 0;
        }

        public TipKorisnika(string imeTipa, double popust, double trazeniBrojBodova)
        {
            ImeTipa = imeTipa;
            Popust = popust;
            TrazeniBrojBodova = trazeniBrojBodova;
        }

    }
}