﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Proovedor
    {
        public int IdProovedor { get; set; }

        public string Nombre { get; set; }
        public string Telefono { get; set; }

        public List<object> Proveedores { get; set; }
    }
}
