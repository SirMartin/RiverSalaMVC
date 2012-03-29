using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RiverSalaMVC;
using RiverSalaMVC.Controllers;

namespace RiverSalaMVC.Tests.Controllers
{
    [TestClass]
    public class OtherTests
    {
        [TestMethod]
        public void SplitSeparator()
        {
            String identity = "sirmartin@gmail.com-*/*/-Eduardo-*/*/-True";

            String[] arrayIdentity = identity.Split(Utils.Constantes.IdentitySeparator.ToCharArray());

            String[] arrayIdentity2 = identity.Split(Utils.Constantes.IdentitySeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            
        }
    }
}
