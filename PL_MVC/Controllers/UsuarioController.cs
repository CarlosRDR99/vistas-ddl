using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        public ML.Result Add(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultado.Correct = true;
                   
                }
                else
                {
                    resultado.Correct = false;
                }
            }

            return resultado;
        }
        public ML.Result Update(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/api/");

                //HTTP PUT
                var postTask = client.PutAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultado.Correct = true;

                }
                else
                {
                    resultado.Correct = false;
                }
            }

            return resultado;
        }
        public ML.Result DeleteAPI(int IdUsuario, int IdDireccion)
        {
            ML.Result resultado = new ML.Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/api/");

                //HTTP DELETE
                var postTask = client.DeleteAsync("Usuario/Delete?IdUsuario="+IdUsuario+"&IdDireccion="+IdDireccion);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultado.Correct = true;

                }
                else
                {
                    resultado.Correct = false;
                }
            }

            return resultado;
        }
        public ML.Result GetAllAPI(ML.Usuario usuario)
        {
            ML.Result resultado = new ML.Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("Usuario/GetAll",usuario);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultado.Correct = true;
                    var read = result.Content.ReadAsAsync<ML.Result>();
                    read.Wait();
                    resultado.Objects = new List<object>();
                    foreach (var fila in read.Result.Objects)
                    {
                        ML.Usuario usuarioGet = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(fila.ToString());
                        resultado.Objects.Add(usuarioGet);
                    }

                }
                else
                {
                    resultado.Correct = false;
                }
            }

            return resultado;
        }
        public ML.Result GetByIdAPI(int IdUsuario)
        {
            ML.Result resultado = new ML.Result();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399/api/");

                //HTTP POST
                var postTask = client.GetAsync("Usuario/GetById?IdUsuario="+IdUsuario);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    resultado.Correct = true;
                    var read = result.Content.ReadAsAsync<ML.Result>();
                    read.Wait();
                    resultado.Object = new List<object>();
                        ML.Usuario usuarioGet = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(read.Result.Object.ToString());
                    resultado.Object = usuarioGet;
                    

                }
                else
                {
                    resultado.Correct = false;
                }
            }

            return resultado;
        }
        // GET: Usuario
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultRol = BL.Rol.GetAllEF();
            ML.Result result = new ML.Result();
            usuario.Rol = new ML.Rol();
            //UsuarioServices.UsuarioServicesClient servicio = new UsuarioServices.UsuarioServicesClient();
            //UsuarioServices.Result resultServicio = servicio.GetAll(usuario);
            usuario.Rol.Roles = resultRol.Objects;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            result = GetAllAPI(usuario);
            usuario.Usuarios = result.Objects;
            //servicio.Close();
            return View(usuario);
        }
        //POST: Usuario
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario,HttpPostedFileBase file)
        {
            //UsuarioServices.UsuarioServicesClient servicio = new UsuarioServices.UsuarioServicesClient();
            //UsuarioServices.Result resultServicio;
            ML.Result result = new ML.Result();
            if (file!=null)
            {
                usuario.Imagen = ConvertToBase64(file);
            }

            if (usuario.IdUsuario>0)
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                result = Update(usuario);
            }
            else
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                result = Add(usuario);
            }
            if (result.Correct)
            {
                if (usuario.IdUsuario>0)
                {
                    ViewBag.Titulo = "ACTUALIZADO";
                    ViewBag.Mensaje = "Usuario actualizado correctamente";
                }
                else
                {
                    ViewBag.Titulo = "INSERTADO";
                    ViewBag.Mensaje = "Usuario insertado correctamente";
                }
            }
            else
            {
                if (usuario.IdUsuario>0)
                {
                    ViewBag.Titulo = "ERROR";
                    ViewBag.Mensaje = "No se pudo actualizar el usuario: "+result.ErrorMessage;
                }
                else {

                    ViewBag.Titulo = "ERROR";
                    ViewBag.Mensaje = "No se pudo insertar el usuario: " + result.ErrorMessage;
                }
            }
            return PartialView("Modal");
        }
        
        //GET: Usuario formulario
        [HttpGet]
        public ActionResult Form(int IdUsuario)
        {
            ML.Result resultRol = BL.Rol.GetAllEF();//traemos todos los roles
            ML.Result resultEstado = BL.Estado.GetAllEF();
            ML.Usuario usuario = new ML.Usuario();//obj usuario
            //ML.Result result = new ML.Result();//obj result
            ML.Rol rol = new ML.Rol();//obj de rol
            rol.Roles =resultRol.Objects;//damos a nuestra list Roles los roles que trajo resultRol.Objects

            usuario.IdUsuario = IdUsuario;//damos el valor del id usuario siempre para comparar

            if (IdUsuario>0)//si el usuario si trae ID es modificar
            {
                //UsuarioServices.UsuarioServicesClient servicio = new UsuarioServices.UsuarioServicesClient();
                //UsuarioServices.Result resultServicio = servicio.GetById(usuario);
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                ML.Result result = GetByIdAPI(usuario.IdUsuario);
                usuario = (ML.Usuario)result.Object;
                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                
                ML.Result resultMunicipio = BL.Municipio.GetAllByIdEstadoEF(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                
                ML.Result resultColonia = BL.Colonia.GetAllByIdMunicipioEF(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                DateTime OnlyDate= DateTime.Parse(usuario.FechaNacimiento).Date;
                usuario.FechaNacimiento = OnlyDate.ToString("yyyy-MM-dd");
                //servicio.Close();
                return View(usuario);
            }
            else
            {
                //siempre cuando se instancie por primera vez una prop de nav se debe inicializar RECUERDALOOOOOOOOOOOOOOO
                usuario.Rol = new ML.Rol();
                //inicializamos la direccion completa
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                resultEstado=BL.Estado.GetAllEF();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                usuario.Rol.Roles = resultRol.Objects;
                return View(usuario);
            }

        }
        
        //POST Delete usuario
        [HttpGet]
        public ActionResult Delete(int IdUsuario, int IdDireccion)
        {
            //UsuarioServices.UsuarioServicesClient servicio = new UsuarioServices.UsuarioServicesClient();
            ML.Usuario usuario = new ML.Usuario();
            usuario.Direccion= new ML.Direccion();
            usuario.Direccion.IdDireccion = IdDireccion;
            usuario.IdUsuario = IdUsuario;
            //UsuarioServices.Result resultServicio = servicio.Delete(usuario);
            
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            ML.Result result = DeleteAPI(usuario.IdUsuario,usuario.Direccion.IdDireccion);
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

        //GET Municipios por el ID del estado
        [HttpGet]
        public JsonResult GetMunicipioByIdEstado(byte IdEstado) {
            ML.Result result = BL.Municipio.GetAllByIdEstadoEF(IdEstado);
            return Json(result,JsonRequestBehavior.AllowGet); 
        }

        //GET Municipios por el ID del estado
        [HttpGet]
        public JsonResult GetColoniaByIdMunicipio(byte IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetAllByIdMunicipioEF(IdMunicipio);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //GET All por CP
        [HttpGet]
        public JsonResult GetAllByCP(string CP)
        {
            ML.Result result = BL.Colonia.GetAllByCP(CP);
            ML.Colonia colonia = new ML.Colonia();
            colonia.Colonias = result.Objects;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Convertir img a base64
        public string ConvertToBase64(HttpPostedFileBase archivo)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(archivo.InputStream);
            data = reader.ReadBytes((int)archivo.ContentLength);
            return Convert.ToBase64String(data);
        }
        public JsonResult ChangeStatus(int IdUsuario, bool Status)
        {
            ML.Result result = BL.Usuario.UpdateStatus(IdUsuario,Status);           
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public JsonResult BusquedaAbierta(ML.Usuario usuario)
        {
            usuario.Nombre = usuario.Nombre != null ? usuario.Nombre : "";
            usuario.ApellidoPaterno = usuario.ApellidoPaterno != null ? usuario.ApellidoPaterno : "";
            usuario.ApellidoMaterno = usuario.ApellidoMaterno != null ? usuario.ApellidoMaterno : "";
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

    }
}