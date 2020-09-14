using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR141_2017_WebProjekat.Models
{
    public class Podaci
    {
        //citaj korisnike iz .txt
        public static List<Korisnik> IscitajKorisnike(string putanja)
        {
            List<Korisnik> korisnici = new List<Korisnik>();
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                string[] tokeni = red.Split(';');
                Korisnik k = new Korisnik(tokeni[0], tokeni[1], tokeni[2], tokeni[3], tokeni[4], DateTime.Parse(tokeni[5]), tokeni[6]); 
                korisnici.Add(k);
            }

            sr.Close();
            stream.Close();
            return korisnici;
        }
    }
}