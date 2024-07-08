using Microsoft.Ajax.Utilities;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class PedidoController : Controller
    {
        private void ModificarCantidad(ML.DetallePedido item, int? Op)
        {
            if (Op == 1)
            {
                item.Cantidad -= 1;
                if (item.Cantidad == 0)
                {
                    ((ML.DetallePedido)Session["Carrito"]).DetallePedidos.Remove(item);
                }
            }
            else if (Op == 2 | Op == null)
            {
                item.Cantidad += 1;
            }
            else
            {
                ((ML.DetallePedido)Session["Carrito"]).DetallePedidos.Remove(item);
            }
        }
        [HttpGet]
        [Route("Pedido/Carrito/{Id:int}")]
        public ActionResult Carrito(int Id, int? Op)
        {
            if (Session["Carrito"] == null)
            {
                ML.DetallePedido detallePedido = new ML.DetallePedido();
                detallePedido.DetallePedidos = new List<object>();
                ML.DetallePedido dpItem = new ML.DetallePedido();
                ML.Result result = BL.Producto.GetByID(Id);
                dpItem.Producto = new ML.Producto();
                dpItem.Producto = (ML.Producto)result.Object;
                dpItem.Cantidad = 1;
                detallePedido.DetallePedidos.Add(dpItem);
                Session["Carrito"] = detallePedido;
                return View(detallePedido);

            }
            else
            {
                ML.DetallePedido detallePedido = (ML.DetallePedido)Session["Carrito"];
                bool Exist = false;

                foreach (ML.DetallePedido item in detallePedido.DetallePedidos)
                {
                    if (item.Producto.Id == Id)
                    {
                        ModificarCantidad(item,Op);       
                        Exist = true;
                        break;
                    }
                }
                if (!Exist)
                {
                    ML.DetallePedido dpItem = new ML.DetallePedido();
                    ML.Result result = BL.Producto.GetByID(Id);
                    dpItem.Producto = new ML.Producto();
                    dpItem.Producto = (ML.Producto)result.Object;
                    dpItem.Cantidad = 1;
                    detallePedido.DetallePedidos.Add(dpItem);
                }
                Session["Carrito"] = detallePedido;
                return View(detallePedido);
            }
        }    
    }
}