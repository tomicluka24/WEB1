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
        public DateTime DatumIVremeManifestacije { get; set; }
        public double Cena { get; set; }
        public string Kupac { get; set; }
        public bool IsRezervisana { get; set; }
        public string Tip { get; set; }

    }
}