using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ProductoController : Controller
    {
        public ActionResult GetAllCards()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetAll();
            producto.Productos = result.Objects;
            return View(producto);
        }
        // GET: Producto
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetAll();
            ML.Result resultCategorias = BL.Categoria.GetAll();
            producto.SubCategoria = new ML.SubCategoria();
            producto.SubCategoria.Categoria = new ML.Categoria();
            producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
            producto.Productos = result.Objects;
            return View(producto);
        }
        [HttpPost]
        public ActionResult Form(ML.Producto producto, HttpPostedFileBase file)
        {
            ML.Result resultCategorias = BL.Categoria.GetAll();
            ML.Result resultSubCategorias = BL.SubCategoria.GetByIdCategoria(producto.SubCategoria.Categoria.Id);
            ML.Result result = new ML.Result();
            if (file != null)
            {
                producto.Imagen = ConvertToBase64(file);
            }
            if (ModelState.IsValid)
            {
                if (producto.Id > 0)
                {
                    result = BL.Producto.Update(producto);
                }
                else
                {
                    result = BL.Producto.Add(producto);
                }
                if (result.Correct)
                {
                    if (producto.Id > 0)
                    {
                        ViewBag.Titulo = "ACTUALIZADO";
                        ViewBag.Mensaje = "Producto actualizado correctamente";
                    }
                    else
                    {
                        ViewBag.Titulo = "INSERTADO";
                        ViewBag.Mensaje = "Producto insertado correctamente";
                    }
                }
                else
                {
                    if (producto.Id > 0)
                    {
                        ViewBag.Titulo = "ERROR";
                        ViewBag.Mensaje = "No se pudo actualizar el producto: " + result.ErrorMessage;
                    }
                    else
                    {
                        ViewBag.Titulo = "ERROR";
                        ViewBag.Mensaje = "No se pudo insertar el producto: " + result.ErrorMessage;
                    }

                }
                return PartialView("Modal");
            }
            else
            {
                producto.SubCategoria.SubCategorias = resultSubCategorias.Objects;
                producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
                return View(producto);
            }

        }

        //GET: Producto formulario
        [HttpGet]
        public ActionResult Form(int Id)
        {
            ML.Result resultCategorias = BL.Categoria.GetAll();
            ML.Producto producto = new ML.Producto();
            producto.Id = Id;
                if (Id > 0)
                {
                    ML.Result result = BL.Producto.GetByID(producto.Id);
                    producto.SubCategoria = new ML.SubCategoria();
                    producto.SubCategoria.Categoria = new ML.Categoria();
                    producto = (ML.Producto)result.Object;
                    producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
                    ML.Result resultSubCategorias = BL.SubCategoria.GetByIdCategoria(producto.SubCategoria.Categoria.Id);
                    producto.SubCategoria.SubCategorias = resultSubCategorias.Objects;
                    // PENDIENTE DE SUBCATEGORIA usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    return View(producto);
                }
                else
                {
                    producto.SubCategoria = new ML.SubCategoria();
                    producto.SubCategoria.Categoria = new ML.Categoria();
                    producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
                    return View(producto);
                }      
        }
        //POST Delete producto
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            ML.Producto producto = new ML.Producto();
            producto.Id = Id;
            ML.Result result = BL.Producto.Delete(producto);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Usuario Eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "No se pudo eliminar al usuario: " + result.ErrorMessage;
            }
            //servicio.Close();
            return PartialView("Modal");
        }

        public string ConvertToBase64(HttpPostedFileBase archivo)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(archivo.InputStream);
            data = reader.ReadBytes((int)archivo.ContentLength);
            return Convert.ToBase64String(data);
        }
        [HttpGet]
        public JsonResult GetSubCategoriasByIdCategoria(int Id)
        {
            ML.Result result = BL.SubCategoria.GetByIdCategoria(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Busqueda(ML.Producto producto)
        {
            producto.SubCategoria.Categoria.Id = producto.SubCategoria.Categoria.Id > 0 ? producto.SubCategoria.Categoria.Id : 0;
             producto.SubCategoria.Id = producto.SubCategoria.Id > 0 ? producto.SubCategoria.Id : 0;
            ML.Result result = BL.Producto.GetAllBusqueda(producto);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}