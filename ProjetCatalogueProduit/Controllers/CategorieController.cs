using ProjetCatalogueProduit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetCatalogueProduit.Controllers

{
    
    public class CategorieController : Controller
    {
        Catalogue_Entities db = new Catalogue_Entities();
        // GET: Categorie
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AjoutCategorie()
        {
            try
            {
                ViewBag.listeCategorie = db.Cat_Categorie.ToList();
                return View();
            }
            catch (Exception e)
            {

                return HttpNotFound();
            }

        }
        [HttpPost]
        public ActionResult AjoutCategorie(Cat_Categorie _categorie) // Enregistrement 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categorie.Date_Saisie = DateTime.Now;
                    db.Cat_Categorie.Add(_categorie);
                    db.SaveChanges();

                }
                return RedirectToAction("AjoutCategorie");
            }
            catch (Exception e)
            {

                return HttpNotFound();
            }

        }
        public ActionResult SupprimerCategorie(int id)
        {
            try
            {
                Cat_Categorie categorie = db.Cat_Categorie.Find(id);
                if (categorie !=null)
                {
                    db.Cat_Categorie.Remove(categorie); // Supprimer categorie
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutCategorie");
            }
            catch (Exception e)
            {

                return HttpNotFound();
            }
        }
        public ActionResult ModifierCategorie(int id)
        {
            try
            {
                ViewBag.listeCategorie = db.Cat_Categorie.ToList();
                Cat_Categorie categorie = db.Cat_Categorie.Find(id);
                if (categorie!=null)
                {
                    return View("AjoutCategorie",categorie);
                }
                return RedirectToAction("AjoutCategorie");
            }
           
            catch (Exception e)
            {

                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult ModifierCategorie(Cat_Categorie categorie)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    categorie.Date_Saisie = DateTime.Now;
                    db.Entry(categorie).State = EntityState.Modified; // Modification de notre categorie
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutCategorie");
            }

            catch (Exception e)
            {

                return HttpNotFound();
            }
        }
    }
}