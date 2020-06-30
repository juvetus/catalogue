using ProjetCatalogueProduit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetCatalogueProduit.Controllers
{
    public class ProduitController : Controller
    {
        Catalogue_Entities db = new Catalogue_Entities();
        // GET: Produit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjoutProduit()
        {
            try
            {
                ViewBag.listeProduit = db.Cat_Produit.ToList();
                ViewBag.listeCategorie = db.Cat_Categorie.ToList();
                return View();
            }
            catch (Exception e)
            {
                return HttpNotFound();
               
            }
        }
        [HttpPost]
        public ActionResult AjoutProduit(Cat_Produit _Produit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0]; // le nom de notre fichier
                        if (file !=null && file.ContentLength > 0) // si notre fichier est !null et > 0 octect 
                        {
                            var fileName = Path.GetFileName(file.FileName); // recuperer le nom du fichier 
                            var path = Path.Combine(Server.MapPath("~/Fichier"), fileName); // Recuperer le chemin d'acces ou sera ùis notre fichier
                            file.SaveAs(path); // Enregistrement sur le serveur

                            _Produit.Image_Produit = fileName;
                            _Produit.Url_Image_Produit = "/Fichier";
                        }
                    }
                    _Produit.Date_Saisie = DateTime.Now;
                    db.Cat_Produit.Add(_Produit);
                    db.SaveChanges();

                }
                return RedirectToAction("AjoutProduit");
            }
            catch (Exception e)
            {
                return HttpNotFound();

            }
        }

        public ActionResult SupprimerProduit(int id)
        {
            try
            {
                Cat_Produit produit = db.Cat_Produit.Find(id);
                if (produit != null)
                {
                    db.Cat_Produit.Remove(produit); // Supprimer produit
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutProduit");
            }
            catch (Exception e)
            {

                return HttpNotFound();
            }
        }
        public ActionResult ModifierProduit(int id)
        {
            try
            {
                ViewBag.listeCategorie = db.Cat_Categorie.ToList();
                ViewBag.listeProduit = db.Cat_Produit.ToList();
                Cat_Produit _Produit = db.Cat_Produit.Find(id);
                if (_Produit != null)
                {
                    return View("AjoutProduit", _Produit);
                }
                return RedirectToAction("AjoutProduit");
            }

            catch (Exception e)
            {

                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult ModifierProduit(Cat_Produit _Produit)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Entry(_Produit).State = EntityState.Modified; // Modification de notre categorie
                    db.SaveChanges();
                }
                return RedirectToAction("AjoutProduit");
            }

            catch (Exception e)
            {

                return HttpNotFound();
            }
        }
    }
}