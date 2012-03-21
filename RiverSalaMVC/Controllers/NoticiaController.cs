using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiverSalaMVC;

namespace RiverSalaMVC.Controllers
{ 
    public class NoticiaController : Controller
    {
        private DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();

        //
        // GET: /Noticia/

        public ViewResult Index()
        {
            var noticia = db.Noticia.Include("Usuario");
            return View(noticia.ToList());
        }

        //
        // GET: /Noticia/Details/5

        public ViewResult Details(int id)
        {
            Noticia noticia = db.Noticia.Single(n => n.ID == id);
            return View(noticia);
        }

        //
        // GET: /Noticia/Create

        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.Usuario, "ID", "Nombre");
            return View();
        } 

        //
        // POST: /Noticia/Create

        [HttpPost]
        public ActionResult Create(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                db.Noticia.AddObject(noticia);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdUsuario = new SelectList(db.Usuario, "ID", "Nombre", noticia.IdUsuario);
            return View(noticia);
        }
        
        //
        // GET: /Noticia/Edit/5
 
        public ActionResult Edit(int id)
        {
            Noticia noticia = db.Noticia.Single(n => n.ID == id);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "ID", "Nombre", noticia.IdUsuario);
            return View(noticia);
        }

        //
        // POST: /Noticia/Edit/5

        [HttpPost]
        public ActionResult Edit(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                db.Noticia.Attach(noticia);
                db.ObjectStateManager.ChangeObjectState(noticia, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "ID", "Nombre", noticia.IdUsuario);
            return View(noticia);
        }

        //
        // GET: /Noticia/Delete/5
 
        public ActionResult Delete(int id)
        {
            Noticia noticia = db.Noticia.Single(n => n.ID == id);
            return View(noticia);
        }

        //
        // POST: /Noticia/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Noticia noticia = db.Noticia.Single(n => n.ID == id);
            db.Noticia.DeleteObject(noticia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}