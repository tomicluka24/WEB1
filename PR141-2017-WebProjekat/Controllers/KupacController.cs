using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class KupacController : Controller
    {
        // GET: Kupac
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrikaziProfilKupca(Korisnik k)
        {
            k = (Korisnik)Session["korisnik"];
            return View(k);
        }
    }
}