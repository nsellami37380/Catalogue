using projetCatalogueProduit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI;

namespace projetCatalogueProduit.Controllers
{
    public class CategorieController : Controller
    {

      CATALOGUE_Entities db = new CATALOGUE_Entities();
        // GET: Categorie
        public ActionResult Index()
        {
            return View();
        }

      public ActionResult AjoutCategorie(string message = "")
      {
         try
         {
            ViewBag.Message = message;
            ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
            return View();
         }
         catch (Exception e)
         {

            return HttpNotFound();
         }
      }

      [HttpPost]
      public ActionResult AjoutCategorie(CAT_CATEGORIE categorie)
      {
         try
         {
            if (ModelState.IsValid)
            {
               categorie.DATE_SAISIE = DateTime.Now;
               db.CAT_CATEGORIE.Add(categorie);
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
            CAT_CATEGORIE categorie = db.CAT_CATEGORIE.Find(id);
            if (categorie != null)
            {
               ViewBag.Message = "Ma premiere alerte";
               var produit = db.CAT_PRODUIT.FirstOrDefault(p => p.CODE_CATEGORIE == categorie.CODE_CATEGORIE);
               if (produit == null)
               {
                  db.CAT_CATEGORIE.Remove(categorie);
                  db.SaveChanges();
               }
               else
               {

                  return RedirectToAction("AjoutCategorie", new { message="Suppression de la categorie " 
                     + categorie.LIBELLE_CATEGORIE + " car des produits luui sont assignes"
                  });
               }
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
            ViewBag.listeCategorie = db.CAT_CATEGORIE.ToList();
            CAT_CATEGORIE categorie = db.CAT_CATEGORIE.Find(id);
            if (categorie != null)
            {
               return View("AjoutCategorie",categorie);
            }
            return RedirectToAction("AjoutCategorie");
         }
         catch (Exception)
         {

            return HttpNotFound();
         }
      }

      [HttpPost]
      public ActionResult ModifierCategorie(CAT_CATEGORIE categorie)
      {
         try
         {            
            if (ModelState.IsValid)
            {
               categorie.DATE_SAISIE = DateTime.Now;
               db.Entry(categorie).State = EntityState.Modified;
               db.SaveChanges();
            }
            return RedirectToAction("AjoutCategorie");
         }
         catch (Exception)
         {

            return HttpNotFound();
         }
      }

   }
}