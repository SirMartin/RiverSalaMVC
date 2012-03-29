using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RiverSalaMVC.Models;
using System.Web.Mvc;

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
        /// <param name="httpContext">El contexto para que cree las rutas correctas.</param>
        /// <returns>El HTML convertido.</returns>
        public static String TranslateBBCodeToHtml(String contenido, HttpContextBase httpContext)
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

            contenido = contenido.Replace(":arg", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/1.gif", httpContext) + "'/>");
            contenido = contenido.Replace(":)", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/2.gif", httpContext) + "'/>");

            contenido = contenido.Replace(":(", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/3.gif", httpContext) + "'/>");
            contenido = contenido.Replace(":D", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/4.gif", httpContext) + "'/>");

            contenido = contenido.Replace(":ninja:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/5.gif", httpContext) + "'/>");
            contenido = contenido.Replace(":ataque:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/6.gif", httpContext) + "'/>");

            contenido = contenido.Replace("xD", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/7.gif", httpContext) + "'/>");
            contenido = contenido.Replace(":9:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/8.gif", httpContext) + "'/>");

            contenido = contenido.Replace(":heavy:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/9.gif", httpContext) + "'/>");
            contenido = contenido.Replace(":banana:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/platano1.gif", httpContext) + "'/>");

            contenido = contenido.Replace(":banana2:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/platano2.gif", httpContext) + "'/>");
            contenido = contenido.Replace(":bailon:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/sparta1.gif", httpContext) + "'/>");

            contenido = contenido.Replace(":gato:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/sparta2.gif", httpContext) + "' width='30%' />");
            contenido = contenido.Replace(":ostia:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/sparta3.gif", httpContext) + "'/>");

            contenido = contenido.Replace(":panda:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/sparta4.gif", httpContext) + "' width='50%'/>");
            contenido = contenido.Replace(":stewie:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/stewie.gif", httpContext) + "' width='25%'/>");

            contenido = contenido.Replace(":river:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/river.gif", httpContext) + "' width='52'/>");
            contenido = contenido.Replace(":kitt:", "<img src='" + UrlHelper.GenerateContentUrl("/Content/images/emoticons/kitt.gif", httpContext) + "'/>");

            //Devolvemos el contenido cambiado.
            return contenido;
        }
    }

    /// <summary>
    /// Una clase con todas las constantes necesarias de forma global.
    /// </summary>
    public class Constantes
    {
        /// <summary>
        /// Separador para las identitys de la aplicación.
        /// </summary>
        public const String IdentitySeparator = "-*/*/-";
    }
}