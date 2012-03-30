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
    public class UsuarioController : BaseController
    {
        private DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();

        //
        // GET: /Usuario/

        public ViewResult Index()
        {
            return View(db.Usuario.ToList());
        }

        //
        // GET: /Usuario/Details/5

        public ViewResult Details(int id)
        {
            Usuario usuario = db.Usuario.Single(u => u.ID == id);
            return View(usuario);
        }
    }
}