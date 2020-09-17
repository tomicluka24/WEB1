using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
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

            //kolekcije korisnika
            List<Korisnik> korisnici = Podaci.IscitajKorisnike("~/App_Data/korisnici.txt");
            HttpContext.Current.Application["korisnici"] = korisnici;

            //kolekcija manifestacija
            List<Manifestacija> manifestacije = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            HttpContext.Current.Application["manifestacije"] = manifestacije;

            //kolekcija karata
            Dictionary<string,Karta> karte = Podaci.IscitajKarte("~/App_Data/karte.txt");
            HttpContext.Current.Application["karte"] = karte;
        }
    }
}
