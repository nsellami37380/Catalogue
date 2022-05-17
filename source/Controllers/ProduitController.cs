using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using projetCatalogueProduit.Models;
using System.IO;

namespace projetCatalogueProduit.Controllers
{
    public class ProduitController : Controller
    {
        // GET: Produit
        public ActionResult Index()
        {
            return View();
        }

      CATALOGUE_Entities db = new CATALOGUE_Entities();
      public ActionResult AjoutProduit()
      {
         try
         {
            ViewBag.ListeProduit = db.CAT_PRODUIT.ToList();
            ViewBag.ListeCategorie = db.CAT_CATEGORIE.ToList();
            return View();
         }
         catch (Exception)
         {

            return HttpNotFound();
         }
      }

      [HttpPost]
      public ActionResult  AjoutProduit(CAT_PRODUIT produit)
      {
         try
         {
            if (ModelState.IsValid)
            {
               if (Request.Files.Count > 0)
               {
                  var file = Request.Files[0];
                  if (file != null && file.ContentLength > 0)
                  {
                     var fileName = Path.GetFileName(file.FileName);
                     var path = Path.Combine(Server.MapPath("~/Fichier"), fileName);
                     file.SaveAs(path);
                     produit.IMAGE_PRODUIT = fileName;
                     produit.URL_IMAGE_PRODUIT = "/Fichier";
                  }
               }
               produit.DATE_SAISIE = DateTime.Now;
               //if (produit.CODE_CATEGORIE == null) { produit.CODE_CATEGORIE = 1; }
               db.CAT_PRODUIT.Add(produit);
               db.SaveChanges();
            }
            return RedirectToAction("AjoutProduit");
         }
         catch (Exception)
         {

            return HttpNotFound();
         }
      }

      public ActionResult SupprimerProduit(int id)
      {
         try
         {
            CAT_PRODUIT produit = db.CAT_PRODUIT.Find(id);
            if (produit != null)
            {
               db.CAT_PRODUIT.Remove(produit);
               db.SaveChanges();
            }
            return RedirectToAction("AjoutProduit");
         }
         catch (Exception)
         {

            return HttpNotFound();
         }
      }

      public ActionResult ModifierProduit(int id)
      {
         try
         {
            ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
            ViewBag.listeProduit = db.CAT_PRODUIT.ToList();
            CAT_PRODUIT produit = db.CAT_PRODUIT.Find(id);
            if (produit != null)
            {
               return View("AjoutProduit", produit);
            }
            return RedirectToAction("AjoutProduit");
         }
         catch (Exception)
         {

            return HttpNotFound();
         }
      }

      [HttpPost]
      public ActionResult ModifierProduit(CAT_PRODUIT produit)
      {
         try
         {
            if (ModelState.IsValid)
            {
               db.Entry(produit).State = EntityState.Modified;


               if (Request.Files.Count > 0)
               {
                  var file = Request.Files[0];
                  if (file != null && file.ContentLength > 0)
                  {
                     var fileName = Path.GetFileName(file.FileName);
                     var path = Path.Combine(Server.MapPath("~/Fichier"), fileName);
                     file.SaveAs(path);
                     produit.IMAGE_PRODUIT = fileName;
                     produit.URL_IMAGE_PRODUIT = "/Fichier";
                  }
               }

               db.SaveChanges();
            }
            return RedirectToAction("AjoutProduit");
         }
         catch (Exception)
         {

            return HttpNotFound();
         }
      }

   }
}