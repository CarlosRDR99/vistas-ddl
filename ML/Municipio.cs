using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Municipio
    {
        public byte IdMunicipio { get; set; }
        public string Nombre { get; set; }

        //lista de Municipios
        public List<object> Municipios { get; set; }

        //props de navegacion 
        public ML.Estado Estado { get; set; }
    }
}
