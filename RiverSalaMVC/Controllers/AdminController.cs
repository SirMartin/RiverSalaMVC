using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RiverSalaMVC.Models.Security;

namespace RiverSalaMVC.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/
        [AuthorizationAttributes.AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }       
    }
}
