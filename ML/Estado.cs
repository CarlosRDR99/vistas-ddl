﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Estado
    {
        public byte IdEstado { get; set; }
        public string Nombre { get; set; }

        //lista de estados
        public List<object> Estados { get; set; }
    }
}
