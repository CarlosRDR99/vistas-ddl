using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pedido
    {
        public int Id { get; set; }
        public ML.Usuario Usuario { get; set; }
        public ML.Sucursal Sucursal { get; set; }
        public ML.TipoPago TipoPago { get; set; }
        public ML.Direccion dirección { get; set; }
        public ML.EstadoPedido EstadoPedido { get; set; }
        public string FechaCreacion { get; set; }
        public ML.Repartidor Repartidor { get; set; }
        
        public List<object> Carrito { get; set; }
        public List<object> Pedidos{ get; set; }
    }
}
