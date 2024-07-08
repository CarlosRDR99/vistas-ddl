using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class VentaProducto
    {
        public ML.Pedido Pedido { get; set; }
        public ML.Sucursal Sucursal { get; set; }

        public List<object> VentaProductos{ get; set; }


    }
}
