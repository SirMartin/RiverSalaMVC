using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiverSalaMVC.Models.Security;
using System.Web.Security;

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
                {
                    permisosOk = false;
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
                filterContext.Result = new HttpUnauthorizedResult("No tienes permiso");
            }


            base.OnActionExecuting(filterContext);
        }

        //private static Usuario _usuario;
        /// <summary>
        /// Devuelve el usuario "logado" en el sistema.
        /// Si no tenemos (no está autenticado), devuelve null
        /// </summary>
        protected Usuario UsuarioLogueado
        {
            get
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    //_usuario = null;
                    //Session["Usuario"] = null;
                    return null;
                }
                else
                {
                    //por ahora en cada llamada vamos a la base de datos.
                    //podríamos cachear por request.
                    //ojo, no dejarlo como variable estática ya que es compartida por TODOS los usuarios de la aplicación.
                    //el session tampoco sirve por qeu actua a modo de cache y cambios realizados en el estado del usuario por otras aplicaciones no lo actualizan

                    string identity = HttpContext.User.Identity.Name;
                    String[] arrayIdentity = HttpContext.User.Identity.Name.Split(RiverSalaMVC.Utils.Constantes.IdentitySeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    String username = arrayIdentity[0];
                    if (HttpContext != null)
                    {
                        string ocKey = "usr_" + HttpContext.GetHashCode().ToString("x");
                        if (!HttpContext.Items.Contains(ocKey))
                        {
                            using (DB_38969_riversalaEntities db = new DB_38969_riversalaEntities())
                            {
                                //no lo tengo cacheado                                
                                Usuario usuario = db.Usuario.Where(g => g.Email.Equals(username)).FirstOrDefault();
                                if (usuario != null)
                                {
                                    HttpContext.Items.Add(ocKey, usuario); //cacheo
                                    return usuario;
                                }
                                else return null;
                            }

                        }
                        else
                        {
                            return HttpContext.Items[ocKey] as Usuario;
                        }
                    }
                    else
                    {
                        using (DB_38969_riversalaEntities db = new DB_38969_riversalaEntities())
                        {
                            Usuario usuario = db.Usuario.Where(g => g.Email.Equals(username)).FirstOrDefault();
                            if (usuario == null)
                            {
                                FormsAuthentication.SignOut();
                                return null;
                            }
                            else
                            {
                                return usuario;
                            }
                        }
                    }
                }
            }
        }
    }
}
