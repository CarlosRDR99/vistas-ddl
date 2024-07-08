using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            try
            {
                ML.DetallePedido detallePedido = (ML.DetallePedido)Session["Carrito"];

                string pathCorreoTemplate = Server.MapPath("~/Content/MailTemplate/CorreoTemplate.html");
                string body=string.Empty;

                using (StreamReader reader = new StreamReader(pathCorreoTemplate))
                {
                    body = reader.ReadToEnd();

                }

                string productoTabla= GenerarTablaProducto(detallePedido.DetallePedidos);

                body=body.Replace("{NombreUsuario}","Jorge");
                body = body.Replace("{ProductosTabla}", productoTabla);
                body = body.Replace("{LINK}", "https://ii.ct-stc.com/2/logos/empresas/2022/02/16/c87fc922c9bc4771b0dc220232167thumbnail.png");
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("cardomrom@gmail.com", "lrluxlralslpwbil"),
                    EnableSsl = true
                };
                var mensaje = new System.Net.Mail.MailMessage
                {
                    From = new System.Net.Mail.MailAddress("cardomrom@gmail.com"),
                    Subject = "Test de correo",
                    Body = body,
                    IsBodyHtml = true
                };
                mensaje.To.Add("jguevaraflores3@gmail.com");
                smtpClient.Send(mensaje);
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
        private string GenerarTablaProducto(List<object> detallePedidos)
        {
            string tabla = "<table>" +
                "<tr>" +
                "<th>PRODUCTO</th>" +
                "<th>PRECIO</th>" +
                "<th>CANTIDAD</th>" +
                "</tr>";

            foreach (ML.DetallePedido item in detallePedidos)
            {
                tabla +=$"<tr><td>{item.Producto.Nombre}</td>" +
                    $"<td>{item.Producto.Precio}</td>" +
                    $"<td>{item.Cantidad}</td></tr>";
            }
            tabla += "</table>";
            return tabla;
        }
    }
}