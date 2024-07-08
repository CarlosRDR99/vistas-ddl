using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ML
{
    public class Producto
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre no puede estar vacio")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo {0} no puede estar vacío.")]
        [Range( 1 , 1000, ErrorMessage = "El valor para {0} debe estar entre {1} y {2}.")]
        public decimal Precio { get; set; }

        public string Imagen { get; set; }
        public List<object> Productos { get; set; }
        //props de va
        public ML.SubCategoria SubCategoria { get; set; }
    }
}
