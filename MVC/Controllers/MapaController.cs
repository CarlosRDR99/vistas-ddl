using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class MapaController : Controller
    {
        // GET: Mapa
        public ActionResult GetMapa()
        {
            ML.Result result = BL.Sucursal.GetAll();
            ML.Sucursal sucursal = new ML.Sucursal();
            sucursal.Sucursales = result.Objects;
            return View(sucursal);
        }
    }
}