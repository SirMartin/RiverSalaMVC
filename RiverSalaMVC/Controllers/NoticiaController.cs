using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiverSalaMVC;
using RiverSalaMVC.Models;
using RiverSalaMVC.Models.Security;

namespace RiverSalaMVC.Controllers
{
    public class NoticiaController : BaseController
    {
        private DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();

        #region Index
        //
        // GET: /Noticia/

        public ActionResult Index()
        {
            //Vamos a la acción de HOME, que es la principal.
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Comentarios
        //
        // GET: /Noticia/Comentarios/5

        public ActionResult Comentarios(int id)
        {
            //Recuperamos la noticia.
            Noticia noticia = db.Noticia.Include("Usuario").Single(n => n.ID == id);

            //Si es privada y no estamos logueados no se puede ver.
            if ((!Request.IsAuthenticated) && (noticia.EsPrivada))
            {
                return RedirectToAction("Index", "Home");
            }

            //Convertimos el contenido de la noticia a HTML.
            noticia.Contenido = Utils.Utils.TranslateBBCodeToHtml(noticia.Contenido, HttpContext);

            //La convertimos al modelo para mostrar.
            NoticiaModel notMod = Utils.Utils.ConvertNoticiaToNoticiaModel(noticia);

            //Recuperamos los comentarios de la noticia y los pasamos en un ViewBag.
            List<Comentario> comentarios = db.Comentario.Include("Usuario").Where(c => c.IdNoticia == noticia.ID).ToList();

            //Convertimos el contenido de los comentarios a HTML.
            comentarios.ForEach(g => g.Texto = Utils.Utils.TranslateBBCodeToHtml(g.Texto, HttpContext));

            //Los pasamos en un ViewBag.
            ViewBag.Comentarios = comentarios;

            //Cogemos los usuarios con su número de posteos.
            List<Usuario> userPosts = db.Usuario.Include("Comentario").ToList();
            List<PosteosModel> posteos = new List<PosteosModel>();
            foreach (Usuario user in userPosts)
            {
                PosteosModel post = new PosteosModel()
                {
                    IdUsuario = user.ID,
                    TotalPosts = user.Comentario.Count
                };
                posteos.Add(post);
            }
            //Lo metemos en un ViewBag.
            ViewBag.Posteos = posteos;

            return View(notMod);
        }

        [HttpPost]
        [AuthorizationAttributes.UserAuthorize]
        public ActionResult Comentarios(int id, FormCollection collection)
        {
            //Insertamos el comentario.
            Comentario comentario = new Comentario();
            comentario.Texto = collection["contenido"].ToString().Replace("\r\n", "<br/>");
            comentario.IdUsuario = UsuarioLogueado.ID;
            comentario.Fecha = DateTime.Now;
            comentario.IdNoticia = id;

            //Guardamos el comentario.
            db.Comentario.AddObject(comentario);
            db.SaveChanges();

            //Recuperamos la noticia.
            Noticia noticia = db.Noticia.Include("Usuario").Single(n => n.ID == id);
            NoticiaModel notMod = Utils.Utils.ConvertNoticiaToNoticiaModel(noticia);

            //Recuperamos los comentarios de la noticia y los pasamos en un ViewBag.
            List<Comentario> comentarios = db.Comentario.Include("Usuario").Where(c => c.IdNoticia == noticia.ID).ToList();
            ViewBag.Comentarios = comentarios;

            //Cogemos los usuarios con su número de posteos.
            List<Usuario> userPosts = db.Usuario.Include("Comentario").ToList();
            List<PosteosModel> posteos = new List<PosteosModel>();
            foreach (Usuario user in userPosts)
            {
                PosteosModel post = new PosteosModel()
                {
                    IdUsuario = user.ID,
                    TotalPosts = user.Comentario.Count
                };
                posteos.Add(post);
            }
            //Lo metemos en un ViewBag.
            ViewBag.Posteos = posteos;

            return View(notMod);
        }

        #endregion

        #region Crear

        //
        // GET: /Noticia/Create

        [AuthorizationAttributes.AdminAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Noticia/Create

        [HttpPost]
        [AuthorizationAttributes.AdminAuthorize]
        public ActionResult Create(Noticia noticia)
        {
            //Ponemos los datos que faltan.
            noticia.IdUsuario = UsuarioLogueado.ID;
            noticia.Fecha = DateTime.Now;

            //Ponemos los saltos de linea.
            noticia.Contenido = noticia.Contenido.Replace("\r\n", "<br/>");

            if (ModelState.IsValid)
            {
                db.Noticia.AddObject(noticia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(noticia);
        }

        #endregion

        #region Editar

        //
        // GET: /Noticia/Edit/5

        [AuthorizationAttributes.AdminAuthorize]
        public ActionResult Edit(int id)
        {
            Noticia noticia = db.Noticia.Single(n => n.ID == id);
            noticia.Contenido = noticia.Contenido.Replace("<br/>", "\r\n");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "ID", "Nombre", noticia.IdUsuario);
            return View(noticia);
        }

        //
        // POST: /Noticia/Edit/5

        [HttpPost]
        [AuthorizationAttributes.AdminAuthorize]
        public ActionResult Edit(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                Noticia not = db.Noticia.Where(g => g.ID == noticia.ID).FirstOrDefault();
                not.Contenido = noticia.Contenido.Replace("\r\n", "<br/>");
                not.Titulo = noticia.Titulo;
                not.EsPrivada = noticia.EsPrivada;
                //Continuamos con la modificación.
                db.SaveChanges();
                return RedirectToAction("Comentarios", new { id = noticia.ID });
            }
            return View(noticia);
        }

        #endregion

        #region Eliminar

        //
        // GET: /Noticia/Delete/5

        [AuthorizationAttributes.AdminAuthorize]
        public ActionResult Delete(int id)
        {
            Noticia noticia = db.Noticia.Single(n => n.ID == id);
            return View(noticia);
        }

        //
        // POST: /Noticia/Delete/5

        [HttpPost, ActionName("Delete")]
        [AuthorizationAttributes.AdminAuthorize]
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

        #endregion

        #region Archivo

        public ActionResult Historico(int? mes, int? year)
        {
            //Miramos si es un mes en concreto o son simplemente el indice.
            if ((mes == null) && (year == null))
            {
                //Es indice.
                List<Noticia> noticias = db.Noticia.ToList();
                List<DateTime> fechas = new List<DateTime>();
                //Recorremos las noticias y cogemos los meses y años distintos.
                foreach (Noticia noticia in noticias)
                {
                    DateTime fecha = new DateTime(noticia.Fecha.Year, noticia.Fecha.Month, 1);
                    if (!fechas.Contains(fecha))
                    {
                        //No contiene la fecha, la añadimos.
                        fechas.Add(fecha);
                    }
                }

                //Pasamos la lista a la vista, para mostrarlo.
                ViewBag.FechasHistorico = fechas;

                //Vamos a la vista.
                List<NoticiaModel> news = null;
                return View(news);
            }
            else
            {
                //Es un mes en concreto.
                DateTime diaInicial = new DateTime(year.Value, mes.Value, 1, 0, 0, 0);
                DateTime diaFinal = new DateTime(year.Value, mes.Value, DateTime.DaysInMonth(year.Value, mes.Value), 23, 59, 59);
                List<Noticia> noticias = db.Noticia.Include("Usuario").Where(not => not.Fecha >= diaInicial && not.Fecha <= diaFinal).OrderByDescending(g => g.Fecha).ToList();

                //Convertimos el contenido a HTML.
                noticias.ForEach(g => g.Contenido = Utils.Utils.TranslateBBCodeToHtml(g.Contenido, HttpContext));

                List<NoticiaModel> news = Utils.Utils.ConvertNoticiaToNoticiaModel(noticias);

                //Vamos a la vista.
                return View(news);
            }
        }

        #endregion

        #region Administración

        [AuthorizationAttributes.AdminAuthorize]
        public ActionResult Administracion()
        {
            List<Noticia> noticias = db.Noticia.Include("Usuario").OrderByDescending(g => g.Fecha).ToList();

            return View(noticias);
        }

        #endregion
    }
}