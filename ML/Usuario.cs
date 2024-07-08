using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        //1. Primero definimos nuestras propiedades/atributos
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public string Imagen { get; set; }
        public bool Status { get; set; }
        public string IdIdentity { get; set; }

        //lista de usuarios
        public List<object> Usuarios { get; set; }

        //props de navegación
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
    }
}
