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

            //kolekcija korisnika
            List<Korisnik> korisnici = Podaci.IscitajKorisnike("~/App_Data/korisnici.txt");
            HttpContext.Current.Application["korisnici"] = korisnici;

            ////kolekcija admina
            //List<Korisnik> admini = Podaci.IscitajKorisnike("~/App_Data/admini.txt");
            //HttpContext.Current.Application["admini"] = admini;

            ////kolekcija kupaca
            //List<Korisnik> kupci = Podaci.IscitajKorisnike("~/App_Data/kupci.txt");
            //HttpContext.Current.Application["kupci"] = kupci;

            ////kolekcija prodavaca
            //List<Korisnik> prodavci = Podaci.IscitajKorisnike("~/App_Data/prodavci.txt");
            //HttpContext.Current.Application["prodavci"] = prodavci;

            //kolekcija manifestacija
            List<Manifestacija> manifestacije = Podaci.IscitajManifestacije("~/App_Data/manifestacije.txt");
            HttpContext.Current.Application["manifestacije"] = manifestacije;

            //kolekcija karata
            List<Manifestacija> karte = Podaci.IscitajManifestacije("~/App_Data/karte.txt");
            HttpContext.Current.Application["karte"] = karte;
        }
    }
}
