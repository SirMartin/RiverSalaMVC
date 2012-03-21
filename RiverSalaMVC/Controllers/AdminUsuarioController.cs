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
    public class AdminUsuarioController : Controller
    {
        private DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();

        //
        // GET: /AdminUsuario/

        public ViewResult Index()
        {
            return View(db.Usuario.ToList());
        }

        //
        // GET: /AdminUsuario/Details/5

        public ViewResult Details(int id)
        {
            Usuario usuario = db.Usuario.Single(u => u.ID == id);
            return View(usuario);
        }

        //
        // GET: /AdminUsuario/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AdminUsuario/Create

        [HttpPost]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.AddObject(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(usuario);
        }
        
        //
        // GET: /AdminUsuario/Edit/5
 
        public ActionResult Edit(int id)
        {
            Usuario usuario = db.Usuario.Single(u => u.ID == id);
            return View(usuario);
        }

        //
        // POST: /AdminUsuario/Edit/5

        [HttpPost]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Attach(usuario);
                db.ObjectStateManager.ChangeObjectState(usuario, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        //
        // GET: /AdminUsuario/Delete/5
 
        public ActionResult Delete(int id)
        {
            Usuario usuario = db.Usuario.Single(u => u.ID == id);
            return View(usuario);
        }

        //
        // POST: /AdminUsuario/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Usuario usuario = db.Usuario.Single(u => u.ID == id);
            db.Usuario.DeleteObject(usuario);
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