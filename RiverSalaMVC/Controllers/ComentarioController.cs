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
    public class ComentarioController : Controller
    {
        private DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();

        //
        // GET: /Comentario/

        public ViewResult Index()
        {
            var comentario = db.Comentario.Include("Usuario");
            return View(comentario.ToList());
        }

        //
        // GET: /Comentario/Details/5

        public ViewResult Details(int id)
        {
            Comentario comentario = db.Comentario.Single(c => c.ID == id);
            return View(comentario);
        }

        //
        // GET: /Comentario/Create

        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.Usuario, "ID", "Nombre");
            return View();
        } 

        //
        // POST: /Comentario/Create

        [HttpPost]
        public ActionResult Create(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Comentario.AddObject(comentario);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdUsuario = new SelectList(db.Usuario, "ID", "Nombre", comentario.IdUsuario);
            return View(comentario);
        }
        
        //
        // GET: /Comentario/Edit/5
 
        public ActionResult Edit(int id)
        {
            Comentario comentario = db.Comentario.Single(c => c.ID == id);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "ID", "Nombre", comentario.IdUsuario);
            return View(comentario);
        }

        //
        // POST: /Comentario/Edit/5

        [HttpPost]
        public ActionResult Edit(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Comentario.Attach(comentario);
                db.ObjectStateManager.ChangeObjectState(comentario, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "ID", "Nombre", comentario.IdUsuario);
            return View(comentario);
        }

        //
        // GET: /Comentario/Delete/5
 
        public ActionResult Delete(int id)
        {
            Comentario comentario = db.Comentario.Single(c => c.ID == id);
            return View(comentario);
        }

        //
        // POST: /Comentario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Comentario comentario = db.Comentario.Single(c => c.ID == id);
            db.Comentario.DeleteObject(comentario);
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