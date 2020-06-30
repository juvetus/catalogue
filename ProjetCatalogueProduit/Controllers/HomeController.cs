using ProjetCatalogueProduit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetCatalogueProduit.Controllers
{
    public class HomeController : Controller
    {
        Catalogue_Entities db = new Catalogue_Entities();
        // GET: Home
        public ActionResult Index()
        {
          ViewBag.listeCategorie = db.Cat_Categorie.ToList().OrderBy(x => x.Libelle_Categorie); // Liste des categories rangée
            return View();
        }
    }
}