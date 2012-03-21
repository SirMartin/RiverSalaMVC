using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace RiverSalaMVC.Models
{

    public class NoticiaModel
    {
        [Display(Name = "Titulo")]
        [DataType(DataType.Text)]
        public string Titulo { get; set; }

        [Display(Name = "Contenido")]
        public string Contenido { get; set; }

        [Display(Name = "Autor")]
        public string Autor { get; set; }

        [Display(Name = "IdNoticia")]
        public int IdNoticia { get; set; }

        [Display(Name = "IdAutor")]
        public int IdAutor { get; set; }

        [Display(Name = "Comentarios")]
        public int Comentarios { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }
    }
}
