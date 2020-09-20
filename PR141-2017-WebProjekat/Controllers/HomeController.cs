using PR141_2017_WebProjekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PR141_2017_WebProjekat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            List<Manifestacija> sortiraneManifestacije = manifestacije.OrderBy(o => o.DatumIVremeOdrzavanja).ToList();
            List<UploadedFile> files = (List<UploadedFile>)HttpContext.Application["files"];

            return View(sortiraneManifestacije);
        }
        public ActionResult PrikaziManifestaciju(string Naziv)
        {
            //m = (Manifestacija)Session["manifestacija"];
            Manifestacija mZaPrikaz = new Manifestacija();
            List<Manifestacija> manifestacije = (List<Manifestacija>)HttpContext.Application["manifestacije"];
            foreach (var item in manifestacije)
            {
                if (item.Naziv == Naziv)
                {
                    mZaPrikaz = item;
                    break;
                }
            }

                return View(mZaPrikaz);
        }

    }
}