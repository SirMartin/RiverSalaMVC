using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RiverSalaMVC.Models;

namespace RiverSalaMVC.Utils
{
    public class Utils
    {
        private static DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();

        public static NoticiaModel ConvertNoticiaToNoticiaModel(Noticia noticia)
        {
            int numComentarios = db.Comentario.Where(g => g.IdNoticia == noticia.ID).Count();

            NoticiaModel notMod = new NoticiaModel()
            {
                Autor = noticia.Usuario.Nombre,
                Contenido = noticia.Contenido,
                Fecha = noticia.Fecha,
                IdAutor = noticia.IdUsuario,
                IdNoticia = noticia.ID,
                Titulo = noticia.Titulo,
                Comentarios = numComentarios
            };

            return notMod;
        }

        public static List<NoticiaModel> ConvertNoticiaToNoticiaModel(List<Noticia> noticias)
        {
            List<NoticiaModel> news = new List<NoticiaModel>();
            foreach (Noticia noticia in noticias)
            {
                NoticiaModel notMod = ConvertNoticiaToNoticiaModel(noticia);

                //Añadimos a la lista.
                news.Add(notMod);
            }

            return news;
        }
    }
}