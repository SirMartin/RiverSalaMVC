using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiverSalaMVC.Models.Security;

namespace RiverSalaMVC.Controllers
{
    public class BaseController : Controller
    {
        private DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check if this action has Authorization attribute
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            bool permisosOk = true;

            //Recogemos al usuario logueado.
            string email = HttpContext.User.Identity.Name;
            Usuario usuario = db.Usuario.Where(g => g.Email.Equals(email)).FirstOrDefault();

            //importante, hay que ir comparando entrando primero en los roles más poderosos            
            if (attributes.Any(a => a is RiverSalaMVC.Models.Security.AuthorizationAttributes.UserAuthorize))
            {
                if (usuario == null)
                    permisosOk = false;
                else
                {
                    permisosOk = !usuario.IsAdmin;
                }
            }
            else if (attributes.Any(a => a is RiverSalaMVC.Models.Security.AuthorizationAttributes.AdminAuthorize))
            {

                if (usuario == null)
                {
                    permisosOk = false;
                }
                else
                {
                    permisosOk = usuario.IsAdmin;
                }
            }

            if (permisosOk)
            {
                return;
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }


            base.OnActionExecuting(filterContext);
        }
    }
}
