using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Categoria
    {
        public int Id { get; set; }
        [MaxLength(length: 100, ErrorMessage = "El campo nombre de categoría no puede tener más de 100 caracteres")]
        public string Nombre { get; set; }
        public List<object> Categorias { get; set; }
    }
}
