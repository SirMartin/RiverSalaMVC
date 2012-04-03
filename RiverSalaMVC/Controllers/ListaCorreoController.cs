using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiverSalaMVC.Models.Security;
using RiverSalaMVC.Models;
using RiverSalaMVC.Utils;

namespace RiverSalaMVC.Controllers
{
    public class ListaCorreoController : BaseController
    {
        //
        // GET: /ListaCorreo/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(ListaCorreoModel listaCorreo)
        {
            //Cogemos el usuario autenticado.
            String[] arrayIdentity = User.Identity.Name.Split(RiverSalaMVC.Utils.Constantes.IdentitySeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string username = arrayIdentity[1];
            listaCorreo.Autor = username;

            //Ponemos los saltos de linea.
            listaCorreo.Contenido = listaCorreo.Contenido.Replace("\r\n", "<br/>");

            //Enviamos el correo.
            MailService.SendEmailListaCorreo(listaCorreo);
            
            //Volvemos a la vista.
            return RedirectToAction("Index", "Home");
        }

    }
}
