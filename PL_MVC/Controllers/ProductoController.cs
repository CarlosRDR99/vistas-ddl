using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto

        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            //ML.Result resultCategoria = BL.Categoria.GetAll();
            //producto.SubCategoria.Categoria = new ML.Categoria();
            ////UsuarioServices.UsuarioServicesClient servicio = new UsuarioServices.UsuarioServicesClient();
            ////UsuarioServices.Result resultServicio = servicio.GetAll(usuario);
            //producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;
            ML.Result result = BL.Producto.GetAll();
            producto.Productos = result.Objects;
            return View();
        }
        [HttpPost]
        public ActionResult Form(ML.Producto producto, HttpPostedFileBase file)
        {
            ML.Result result = new ML.Result();
            if (file != null)
            {
                producto.Imagen = ConvertToBase64(file);
            }

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

        //GET: Usuario formulario
        [HttpGet]
        public ActionResult Form(int Id)
        {
            ML.Result resultCategorias = BL.Categoria.GetAll();//PENDIENTEEEE
            ML.Producto producto = new ML.Producto();
            //ML.Result result = new ML.Result();//obj result
            ML.Categoria categoria = new ML.Categoria();//obj de rol
            categoria.Categorias = resultCategorias.Objects;//damos a nuestra list Roles los roles que trajo resultRol.Objects

            producto.Id = Id;//damos el valor del id usuario siempre para comparar

            if (Id > 0)//si el usuario si trae ID es modificar
            {
                ML.Result result = BL.Producto.GetByID(producto.Id);
                producto = (ML.Producto)result.Object;
                producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;
                // PENDIENTE DE SUBCATEGORIA usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                return View(producto);
            }
            else
            {
                //siempre cuando se instancie por primera vez una prop de nav se debe inicializar RECUERDALOOOOOOOOOOOOOOO
                producto.SubCategoria = new ML.SubCategoria();
                producto.SubCategoria.Categoria = new ML.Categoria();
                return View(producto);
            }

        }
        public string ConvertToBase64(HttpPostedFileBase archivo)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(archivo.InputStream);
            data = reader.ReadBytes((int)archivo.ContentLength);
            return Convert.ToBase64String(data);
        }
    }
}