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
                Korisnik k = new Korisnik(tokeni[0], tokeni[1], tokeni[2], tokeni[3], tokeni[4], DateTime.ParseExact(tokeni[5], "dd/MM/yyyy", null), tokeni[6]); 
                korisnici.Add(k);
            }

            sr.Close();
            stream.Close();
            return korisnici;
        }
        public static void UpisiKorisnika(Korisnik korisnik)
        {
            string path = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            FileStream stream = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(stream);

            string objectToWrite = korisnik.KorisnickoIme + ";" + korisnik.Lozinka + ";" + korisnik.Ime + ";" + korisnik.Prezime + ";" + korisnik.Pol + ";" + korisnik.DatumRodjenja.ToString() + ";" + korisnik.Uloga.ToString();
            sw.WriteLine(objectToWrite);

            sw.Close();
            stream.Close();
        }
    }
}