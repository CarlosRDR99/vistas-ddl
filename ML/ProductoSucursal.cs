﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ProductoSucursal
    {
        public int Id { get; set; }
        public int Stock { get; set; }

        //props de nav
        public ML.Producto Producto { get; set; }
        public ML.Sucursal Sucursal { get; set; }

        public List<object> ProductoSucursales { get; set; }
    }
}