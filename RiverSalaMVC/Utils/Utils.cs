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

        /// <summary>
        /// Convierte los códigos de BB métidos en las noticias en BBDD y demás, por HTML.
        /// </summary>
        /// <param name="contenido">El contenido con los códigos de imagenes y demás.</param>
        /// <returns>El HTML convertido.</returns>
        public static String TranslateBBCodeToHtml(String contenido)
        {
            // Ponemos cada BBCode y su reemplazo

            contenido = contenido.Replace("[i]", "<em>");
            contenido = contenido.Replace("[/i]", "</em>");

            contenido = contenido.Replace("[b]", "<strong>");
            contenido = contenido.Replace("[/b]", "</strong>");

            contenido = contenido.Replace("[u]", "<u>");
            contenido = contenido.Replace("[/u]", "</u>");

            contenido = contenido.Replace("[img]", "<img src='");
            contenido = contenido.Replace("[/img]", "' />");

            contenido = contenido.Replace("[center]", "<p align='center'>");
            contenido = contenido.Replace("[/center]", "</p>");

            contenido = contenido.Replace("[left]", "<p align='left'>");
            contenido = contenido.Replace("[/left]", "</p>");

            contenido = contenido.Replace("[right]", "<p align='right'>");
            contenido = contenido.Replace("[/right]", "</p>");

            contenido = contenido.Replace("[justify]", "<p align='justify'>");
            contenido = contenido.Replace("[/justify]", "</p>");

            //EMOTICONS

            contenido = contenido.Replace(":arg", "<img src='../img/emoticons/1.gif'/>");
            contenido = contenido.Replace(":)", "<img src='../img/emoticons/2.gif'/>");

            contenido = contenido.Replace(":(", "<img src='../img/emoticons/3.gif'/>");
            contenido = contenido.Replace(":D", "<img src='../img/emoticons/4.gif'/>");

            contenido = contenido.Replace(":ninja:", "<img src='../img/emoticons/5.gif'/>");
            contenido = contenido.Replace(":ataque:", "<img src='../img/emoticons/6.gif'/>");

            contenido = contenido.Replace("xD", "<img src='../img/emoticons/7.gif'/>");
            contenido = contenido.Replace(":9:", "<img src='../img/emoticons/8.gif'/>");

            contenido = contenido.Replace(":heavy:", "<img src='../img/emoticons/9.gif'/>");
            contenido = contenido.Replace(":banana:", "<img src='../img/emoticons/platano1.gif'/>");

            contenido = contenido.Replace(":banana2:", "<img src='../img/emoticons/platano2.gif'/>");
            contenido = contenido.Replace(":bailon:", "<img src='../img/emoticons/sparta1.gif'/>");

            contenido = contenido.Replace(":gato:", "<img src='../img/emoticons/sparta2.gif' width='30%' />");
            contenido = contenido.Replace(":ostia:", "<img src='../img/emoticons/sparta3.gif'/>");

            contenido = contenido.Replace(":panda:", "<img src='../img/emoticons/sparta4.gif'  width='50%'/>");
            contenido = contenido.Replace(":stewie:", "<img src='../img/emoticons/stewie.gif' width='25%'/>");

            contenido = contenido.Replace(":river:", "<img src='../img/emoticons/river.gif'  width='52'/>");
            contenido = contenido.Replace(":kitt:", "<img src='../img/emoticons/kitt.gif'/>");

            //Devolvemos el contenido cambiado.
            return contenido;
        }
    }
}