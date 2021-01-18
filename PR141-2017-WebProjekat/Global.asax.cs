using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PR141_2017_WebProjekat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //kolekcija korisnika
            List<Korisnik> korisnici = Podaci.IscitajKorisnike("~/App_Data/korisnici.txt");
            HttpContext.Current.Application["korisnici"] = korisnici;

            //kolekcija manifestacija
            List<Manifestacija> manifestacije = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            //HttpContext.Current.Application["manifestacije"] = manifestacije.OrderBy(o => o.DatumIVremeOdrzavanja).ToList();
            HttpContext.Current.Application["manifestacije"] = manifestacije.OrderByDescending(x => x.DatumIVremeOdrzavanja).ToList();
            //kolekcija karata
            Dictionary<string,Karta> karte = Podaci.IscitajKarte("~/App_Data/karte.txt");
            HttpContext.Current.Application["karte"] = karte;

            //kolekcija komentara
            List<Komentar> komentari = Podaci.IscitajKomentare("~/App_Data/komentari.txt");
            HttpContext.Current.Application["komentari"] = komentari;

            //fotografije
            string path = Path.Combine(Server.MapPath("~/Files/"));
            List<UploadedFile> files = new List<UploadedFile>();
            foreach (var file in Directory.GetFiles(path))
            {
                files.Add(new UploadedFile(Path.GetFileName(file), file));
            }
            //HttpContext.Current.Application["Files"] = files;
        }
    }
}
