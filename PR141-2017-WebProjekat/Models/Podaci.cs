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
                //Korisnik k = new Korisnik(tokeni[0], tokeni[1], tokeni[2], tokeni[3], tokeni[4], DateTime.ParseExact(tokeni[5], "MM/dd/yyyy", null), tokeni[6], false); 
                //korisnici.Add(k);

            if(tokeni[6] == "administrator")
                {
                    Korisnik k = new Korisnik(tokeni[0], tokeni[1], tokeni[2], tokeni[3], tokeni[4], DateTime.ParseExact(tokeni[5], "MM/dd/yyyy", null), tokeni[6], bool.Parse(tokeni[7]));
                    korisnici.Add(k);
                }
            else if(tokeni[6] == "kupac")
                {
                    Dictionary<string, Karta> karte = IscitajKarte("~/App_Data/karte.txt");
                    Dictionary<string, Karta> sveKarteKupca = new Dictionary<string, Karta>();

                    string[] IDevi = tokeni[8].Split(',');

                    for (int i = 0; i < IDevi.Length; i++)
                    {
                        if(karte.ContainsKey(IDevi[i]))
                            {
                            sveKarteKupca.Add(IDevi[i], karte[IDevi[i]]); 
                            }
                    }

                    TipKorisnika tK = new TipKorisnika();
                    tK.ImeTipa = tokeni[10];
                    if(tK.ImeTipa == "zlatni")
                    {
                        tK.Popust = 100;
                        tK.TrazeniBrojBodova = 3001;
                    }
                    else if(tK.ImeTipa == "srebrni")
                    {
                        tK.Popust = 50;
                        tK.TrazeniBrojBodova = 2000;
                    }
                    else
                    {
                        tK.Popust = 20;
                        tK.TrazeniBrojBodova = 500;
                    }

                    Korisnik k = new Korisnik(tokeni[0], tokeni[1], tokeni[2], tokeni[3], tokeni[4], DateTime.ParseExact(tokeni[5], "MM/dd/yyyy", null), tokeni[6], sveKarteKupca, double.Parse(tokeni[9]), tK, bool.Parse(tokeni[7]));
                    korisnici.Add(k);
                }
            else
                {
                    List<Manifestacija> manifestacije = IscitajManifestacije("~/App_Data/manifestacije.txt");
                    List<Manifestacija> manifestacijeProdavca = new List<Manifestacija>();

                    string[] nazivi = tokeni[8].Split(',');
                    for (int i = 0; i < nazivi.Length; i++)
                    {
                        foreach (Manifestacija m in manifestacije)
                        {
                            if(m.Naziv == nazivi[i])
                            {
                                manifestacijeProdavca.Add(m);
                            }
                        }
                    }

                    Korisnik k = new Korisnik(tokeni[0], tokeni[1], tokeni[2], tokeni[3], tokeni[4], DateTime.ParseExact(tokeni[5], "MM/dd/yyyy", null), tokeni[6], manifestacijeProdavca, bool.Parse(tokeni[7]));
                    korisnici.Add(k);

                }
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
            + korisnik.Pol + ";" + mesec + "/" + dan + "/"  + godina + ";" + korisnik.Uloga + ";" + "false";

            if(korisnik.Uloga == "kupac")
            {
                objectToWrite = objectToWrite + ";" + " , " + ";" + "0" + ";" + "bronzani";
            }

            if(korisnik.Uloga == "prodavac")
            {
                objectToWrite = objectToWrite + ";" + " , ";
            }
            
            sw.WriteLine(objectToWrite);

            sw.Close();
            stream.Close();
        }
        public static List<Manifestacija> IscitajManifestacije(string putanja)
        {
            MestoOdrzavanja mO = new MestoOdrzavanja();
            Manifestacija m = new Manifestacija();
            List<Manifestacija> manifestacije = new List<Manifestacija>();
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                string[] tokeni = red.Split(';');

                try
                {

                mO = new MestoOdrzavanja(tokeni[5], double.Parse(tokeni[6]), tokeni[7], tokeni[8], bool.Parse(tokeni[9])); 
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }

                try
                {
                m = new Manifestacija(tokeni[0], tokeni[1], int.Parse(tokeni[2]), DateTime.ParseExact(tokeni[3], "MM/dd/yyyy HH:mm", null), 
                double.Parse(tokeni[4]), mO, bool.Parse(tokeni[9]), bool.Parse(tokeni[10]), tokeni[11]);

                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }

                if(!m.IsIzbrisana)
                manifestacije.Add(m);
            }

            sr.Close();
            stream.Close();
            return manifestacije;
        }
        public static Dictionary<string, Karta> IscitajKarte(string putanja)
        {
            List<Manifestacija> manifestacije = IscitajManifestacije("~/App_Data/manifestacije.txt");
            Manifestacija man = new Manifestacija();

            Dictionary<string, Karta> karte = new Dictionary<string, Karta>();
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                string[] tokeni = red.Split(';');

                foreach (var m in manifestacije)
                {
                    if (tokeni[1] == m.Naziv)
                    {
                        man = m;
                    }
                }
                Karta k = new Karta(tokeni[0], man, DateTime.ParseExact(tokeni[2], "MM/dd/yyyy HH:mm", null), double.Parse(tokeni[3]), tokeni[4], bool.Parse(tokeni[5]), (TipKarte)Enum.Parse(typeof(TipKarte), tokeni[6]), bool.Parse(tokeni[7]));
                if (!k.IsIzbrisana)
                    karte.Add(k.ID, k);
            }

            sr.Close();
            stream.Close();
            return karte;
        }
    }

}