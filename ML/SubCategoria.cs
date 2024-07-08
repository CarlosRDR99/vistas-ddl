using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ML
{
    public class SubCategoria
    {
        public int Id { get; set; }
        [MaxLength(length: 100, ErrorMessage = "El campo nombre de subcategoría no puede tener más de 100 caracteres")]
        public string Nombre { get; set; }
        public List<object> SubCategorias { get; set; }
        //props de nav
        public ML.Categoria Categoria { get; set; }
    }
}
