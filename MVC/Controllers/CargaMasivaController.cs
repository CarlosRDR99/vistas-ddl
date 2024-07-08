using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class CargaMasivaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CargaMasiva()
        {
            if (Session["pathExcel"]==null)
            {
                HttpPostedFileBase excel = Request.Files["Excel"];
                if (excel != null)
                {
                    string extension = Path.GetExtension(excel.FileName);
                    if (extension == ".xlsx")
                    {
                        string FilePath = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excel.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
                        if (!System.IO.File.Exists(FilePath))
                        {
                            excel.SaveAs(FilePath);
                            string context = ConfigurationManager.ConnectionStrings["OleDbConnection"].ToString() + FilePath;
                            ML.Result result = BL.Producto.LeerExcel(context);
                            if (result.Correct)
                            {
                                ML.Result validarExcel = BL.Producto.ValidarExcel(result.Objects);
                                if (validarExcel.Objects.Count == 0)
                                {
                                    Session["pathExcel"] = FilePath;

                                }
                                else
                                {
                                    ViewBag.Titulo = "Error de formato";
                                    ViewBag.Mensaje = "Tu archivo tiene errores:" + result.Objects;
                                }
                            }
                            else
                            {
                                ViewBag.Titulo = "Hubo un problema";
                                ViewBag.Mensaje = "Ocurrio un error al leer el archivo: " + result.ErrorMessage;
                            }
                        }
                        else
                        {
                            ViewBag.Titulo = "Archivo existente";
                            ViewBag.Mensaje = "El archivo que intentas mandar ya existe, cambia el nombre o intentalo más tarde";
                        }
                    }
                    else
                    {
                        ViewBag.Titulo = "Extensíon invalida";
                        ViewBag.Mensaje = "Asegurate de que el archivo sea .xlsx";
                    }
                }
                else
                {
                    ViewBag.Titulo = "VACÍO";
                    ViewBag.Mensaje = "No se mando ningun archivo";
                }
            }
            else
            {
                string context = ConfigurationManager.ConnectionStrings["OleDbConnection"].ToString() + Session["pathExcel"].ToString();
                ML.Result result = BL.Producto.LeerExcel(context);
                if (result.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = result.Objects;//??????????????????????????????????????????
                    foreach (ML.Producto producto in result.Objects)
                    {
                        ML.Result resultAdd = BL.Producto.Add(producto);
                        if (!resultAdd.Correct)
                        {
                            string error = "Error en el insert con nombre"+producto.Nombre+" "+resultAdd.ErrorMessage;
                            ViewBag.Titulo = "Error de insertado";
                            ViewBag.Mensaje += error;
                        }
                        ViewBag.Titulo = "Insertados";
                        ViewBag.Mensaje = "Registros insertados correctamente";
                    }
                }

                Session["pathExcel"] = null;
            }
            return PartialView("Modal");
        }
    }
}