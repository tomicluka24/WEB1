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
                Korisnik k = new Korisnik(tokeni[0], tokeni[1], tokeni[2], tokeni[3], tokeni[4], DateTime.ParseExact(tokeni[5], "MM/dd/yyyy", null), tokeni[6], false); 
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

            #region
            string mesec = korisnik.DatumRodjenja.Month.ToString();
            string dan = korisnik.DatumRodjenja.Day.ToString();
            string godina = korisnik.DatumRodjenja.Year.ToString();
            if (korisnik.DatumRodjenja.Month <= 9)
            {
                mesec = '0' + mesec;
            }
            if (korisnik.DatumRodjenja.Day <= 9)
            {
                dan = '0' + dan;
            }
            if (korisnik.DatumRodjenja.Year < 1000 && korisnik.DatumRodjenja.Year > 99)
            {
                godina = '0' + godina;
            }
            if (korisnik.DatumRodjenja.Year < 100 && korisnik.DatumRodjenja.Year > 9)
            {
                godina = "00" + godina;
            }
            if (korisnik.DatumRodjenja.Year < 10)
            {
                godina = "000" + godina;
            }
            #endregion

            string objectToWrite = korisnik.KorisnickoIme + ";" + korisnik.Lozinka + ";" + korisnik.Ime + ";" + korisnik.Prezime + ";"
            + korisnik.Pol + ";" + mesec + "/" + dan + "/"  + godina + ";" + korisnik.Uloga;

            
            sw.WriteLine(objectToWrite);

            sw.Close();
            stream.Close();
        }
        public static List<Manifestacija> IscitajManifestacije(string putanja)
        {
            List<Manifestacija> manifestacije = new List<Manifestacija>();
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                string[] tokeni = red.Split(';');

                MestoOdrzavanja mO = new MestoOdrzavanja(tokeni[5], double.Parse(tokeni[6]), tokeni[7], tokeni[8], 
                bool.Parse(tokeni[10])); //moze tokeni[10] jer ako je manifestacija izbrisana onda je i mesto izbrisano?
                                         //ako ne moze, dodati u .txt poslednji parametar da bude isto boolean al za mesto
                Manifestacija m = new Manifestacija(tokeni[0], tokeni[1], int.Parse(tokeni[2]), DateTime.ParseExact(tokeni[3], "MM/dd/yyyy HH:mm", null), 
                double.Parse(tokeni[4]), mO, bool.Parse(tokeni[9]), bool.Parse(tokeni[10]));

                if(!m.IsIzbrisana)
                manifestacije.Add(m);
            }

            sr.Close();
            stream.Close();
            return manifestacije;
        }
    }
}