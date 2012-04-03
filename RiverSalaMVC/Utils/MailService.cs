using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Net.Mail;
using System.Net;
using RiverSalaMVC.Models;

namespace RiverSalaMVC.Utils
{
    /// <summary>
    /// Provides a method for sending email.
    /// </summary>
    public static class MailService
    {

        #region Envio de email sin archivo adjunto.

        /// <summary>
        /// Envio de email con desde la cuenta de operador.
        /// </summary>
        /// <param name="to">Dirección de email del destinatario.</param>
        /// <param name="subject">El asunto del email.</param>
        /// <param name="body">El cuerpo del mensaje.</param>
        /// <param name="isHtml">Si tiene o no formato html.</param>
        public static void Send(MailAddress to, string subject, string body, bool isHtml)
        {
            //create the mail message 
            MailMessage mail = new MailMessage();

            //set the addresses 
            mail.From = new MailAddress("noticias@riversala.com");
            mail.To.Add(to);

            //set the content 
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isHtml;

            //send the message 
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587; //465
            smtp.EnableSsl = true;

            NetworkCredential Credentials = new NetworkCredential("noticias@riversala.com", "noticiasriver12");
            smtp.Credentials = Credentials;
            smtp.Send(mail);
        }

        #endregion

        #region Email de Nueva noticia.

        /// <summary>
        /// Enviamos el mail como que hay una nueva noticia.
        /// </summary>
        /// <param name="noticia">La noticia a enviar.</param>
        internal static void SendEmailNuevaNoticia(Noticia noticia)
        {
            String body = "<div align='center'>";
            body += "<img src='http://www.riversala.com/Content/images/style/escudo.gif' width='104px' height='139px' />";
            body += "<br />";
            body += "</div>";
            body += "<h1>" + noticia.Titulo + "</h1> ";
            body += "<br /><br />";
            body += "<p>" + noticia.Contenido + "</p>";
            body += "<br /><br />";
            body += "<p>";
            body += "<a href='http://www.riversala.com/Noticia/Comentarios/"
                + noticia.ID + "'><b>Pulsa aquí para ver la noticia en la web y comentarla.</b>.</a>";
            body += "<br /><br /><br />";
            body += "<b>VISITANOS!!!</b>";
            body += "<br />";
            body += "</p>";

            //Recuperamos todos los usuarios.
            DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();
            List<Usuario> usuarios = db.Usuario.Where(g => g.Activo == true).ToList();

            foreach (Usuario user in usuarios)
            {
                //Enviamos el correo.
                MailAddress to = new MailAddress(user.Email, user.Nombre + user.Apellidos);
                MailService.Send(to, "Nueva noticia en RiverSala.", body, true);
            }
        }

        #endregion

        #region Email de lista de correo.

        /// <summary>
        /// Envia un mensaje desde la web de RiverSala.
        /// </summary>
        /// <param name="listaCorreo">El mensaje de lista de correo.</param>
        internal static void SendEmailListaCorreo(ListaCorreoModel listaCorreo)
        {
            String body = "<div align='center'>";
            body += "<img src='http://www.riversala.com/Content/images/style/escudo.gif' width='104px' height='139px' />";
            body += "<br />";
            body += "</div>";
            body += "<h1>" + listaCorreo.Titulo + "</h1>";
            body += "<p>";
            body += "<b>" + listaCorreo.Contenido + "</b>";
            body += "<br /><br />";
            body += "<a href='http://www.riversala.com'><b>VISITANOS!!!</b></a>";
            body += "<br /><br />";
            body += "Mensaje enviado por: " + listaCorreo.Autor;
            body += "</p>";

            //Recuperamos todos los usuarios.
            DB_38969_riversalaEntities db = new DB_38969_riversalaEntities();
            List<Usuario> usuarios = db.Usuario.Where(g => g.Activo == true).ToList();

            foreach (Usuario user in usuarios)
            {
                //Enviamos el correo.
                MailAddress to = new MailAddress(user.Email, user.Nombre + user.Apellidos);
                String asunto = listaCorreo.Titulo + " ::: Mensaje de RiverSala Web"; 
                MailService.Send(to, asunto, body, true);
            }
        }

        #endregion

    }
}