using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiverSalaMVC.Models;

namespace RiverSalaMVC.Controllers
{
    public class HomeController : BaseController
    {

        private DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();

        //[OutputCache(Duration=20000)]
        public ActionResult Index(int? page)
        {
            //Cogemos todas las noticias si estamos autenticados o solo las publicas si no lo estamos.
            Noticia[] noticiasDB;
            if (Request.IsAuthenticated)
            {
                //Vemos todas.
                noticiasDB = db.Noticia.Include("Usuario").OrderBy(g => g.Fecha).ToArray();
            }
            else
            {
                //Solo las NO privadas.
                noticiasDB = db.Noticia.Include("Usuario").Where(g => g.EsPrivada == false).OrderBy(g => g.Fecha).ToArray();
            }

            int counter = 0;
            if (page != null)
            {
                counter = (page.Value * 10);
            }

            List<Noticia> noticias = new List<Noticia>();
            for (int i = counter; i < counter + 10; i++)
            {
                if (i <= noticiasDB.Count() - 1)
                {
                    noticias.Add(noticiasDB[i]);
                }
            }

            List<NoticiaModel> news = Utils.Utils.ConvertNoticiaToNoticiaModel(noticias);

            //Mandamos en un ViewBag la paginacion hecha, para el enlace.
            int pagePrev = (counter - 10) / 10;
            if (pagePrev < 0)
            {
                ViewBag.PagePrev = -1;
            }
            else
            {
                ViewBag.PagePrev = pagePrev;
            }
            int pageNext = (counter + 10) / 10;
            if (pageNext < 0)
            {
                ViewBag.pageNext = -1;
            }
            else
            {
                if (pageNext * 10 <= noticiasDB.Count())
                {
                    ViewBag.pageNext = pageNext;
                }
                else
                {
                    ViewBag.PageNext = -1;
                }
            }

            return View(news);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
