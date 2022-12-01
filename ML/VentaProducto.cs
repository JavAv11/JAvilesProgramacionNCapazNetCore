using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class VentaProducto
    {
        public int IdVentaProdcuto { get; set; }
        public ML.Venta venta { get; set; }
        public int Cantidad { get; set; }
        public ML.Producto Producto { get; set; }
        
        public List<Object> ventaProductos { get; set; }

    }
}
