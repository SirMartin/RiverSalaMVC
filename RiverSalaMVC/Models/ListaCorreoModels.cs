using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace RiverSalaMVC.Models
{
    public class ListaCorreoModel
    {
        [Display(Name = "Autor")]
        public String Autor { get; set; }

        [Display(Name = "Titulo")]
        public String Titulo { get; set; }

        [Display(Name = "Contenido")]
        public String Contenido { get; set; }
    }
}
