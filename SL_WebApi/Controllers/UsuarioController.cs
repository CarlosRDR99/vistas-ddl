using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET: api/Usuario
        [HttpPost]
        [Route("api/Usuario/GetAll")]
        public IHttpActionResult GetAll([FromBody]ML.Usuario usuario)
        {
            usuario.Nombre = usuario.Nombre != null ? usuario.Nombre : "";
            usuario.ApellidoPaterno = usuario.ApellidoPaterno != null ? usuario.ApellidoPaterno : "";
            usuario.ApellidoMaterno = usuario.ApellidoMaterno != null ? usuario.ApellidoMaterno : "";
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            return Ok(result);
        }

        // GET: api/Usuario/5
        [HttpGet]
        [Route("api/Usuario/GetById")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            ML.Result result = BL.Usuario.GetByIDEF(usuario);
            return Ok(result); 
        }

        // POST: api/Usuario
        [HttpPost]
        [Route("api/Usuario/Add")]
        public IHttpActionResult Add([FromBody]ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }


        // PUT: api/Usuario/5
        [HttpPut]
        [Route("api/Usuario/Update")]
        public IHttpActionResult Update([FromBody]ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UpdateEF(usuario);
            return Ok(result);
        }

        // DELETE: api/Usuario/5
        [HttpDelete]
        [Route("api/Usuario/Delete")]
        public IHttpActionResult Delete(int IdUsuario,int IdDireccion)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.IdDireccion = IdDireccion;
            ML.Result result = BL.Usuario.DeleteEF(usuario);
            return Ok(result);
        }
    }
}
