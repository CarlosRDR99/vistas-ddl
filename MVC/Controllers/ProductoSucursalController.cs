using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ProductoSucursalController : Controller
    {
        // GET: ProductoSucursal
        [HttpGet]
        public ActionResult Index()
        {
            ML.Result result = BL.Sucursal.GetAll();
            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
            productoSucursal.Sucursal = new ML.Sucursal();
            productoSucursal.Sucursal.Sucursales = result.Objects;
            return View(productoSucursal);
        }
        [HttpPost]
        public ActionResult Index(ML.ProductoSucursal productoSucursal)
        {

            if (productoSucursal.Sucursal.Id > 0)
            {
                ML.Result result = BL.ProductoSucursal.GetByIdSucursal(productoSucursal.Sucursal.Id);
                ML.Result resultSucursal = BL.Sucursal.GetAll();
                productoSucursal.Sucursal.Sucursales = resultSucursal.Objects;
                productoSucursal.ProductoSucursales = result.Objects;

                return View(productoSucursal);
            }
            else
            {
                ViewBag.Titulo = "Sucursal inválida";
                ViewBag.Mensaje = "Debes seleccionar una sucursal primero";
                return PartialView("Modal");
            }


        }
        [HttpGet]
        public JsonResult UpdateStock(int Id,int Stock)
        {
            ML.Result result = BL.ProductoSucursal.UpdateStock(Id,Stock);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}