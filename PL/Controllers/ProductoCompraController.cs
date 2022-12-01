using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace PL.Controllers
{
    public class ProductoCompraController : Controller
    {
        //private readonly IConfiguration _configuration;

        //private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        //public ProductoCompraController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        //{
        //    _configuration = configuration;
        //    _hostingEnvironment = hostingEnvironment;
        //}


        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = new ML.Result();
            producto.Proovedor = new ML.Proovedor();

            ML.Result resultProovedor = BL.Proovedor.GetAll();
            result = BL.Producto.GetAll(producto);

            if (result.Correct)
            {
                producto.Proovedor.Proveedores = resultProovedor.Objects;
                producto.Productos = result.Objects;

                return View(producto);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error";
                return View();
            }
        }
        [HttpPost]
        public ActionResult GetAll(ML.Producto producto)
        {
            producto.Proovedor = new ML.Proovedor();

            producto.Nombre = (producto.Nombre == null) ? "" : producto.Nombre;
            //producto.Proovedor = producto.Proovedor == null ? "" : producto.Proovedor;

            ML.Result resultProovedor = BL.Proovedor.GetAll();
            ML.Result result = BL.Producto.GetAll(producto);
            if (result.Correct)
            {
                producto.Proovedor.Proveedores = resultProovedor.Objects;
                producto.Productos = result.Objects;

                return View(producto);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Carrito(ML.Producto producto)
        {

            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            
            ventaProducto.ventaProductos = new List<object>();

            if (HttpContext.Session.GetString("Carrito") == null)
            {
                producto.Cantidad = producto.Cantidad = 1;
                ventaProducto.ventaProductos.Add(producto);
                HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(ventaProducto.ventaProductos));
                var session = HttpContext.Session.GetString("Carrito");
            }
            else
            {
                var ventaSession = JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Carrito"));

                foreach(var obj in ventaSession)
                {
                    ML.Producto objproducto = JsonConvert.DeserializeObject<ML.Producto>(obj.ToString());
                    ventaProducto.ventaProductos.Add(objproducto);
                }
                foreach(ML.Producto venta in ventaProducto.ventaProductos.ToList())
                {
                    if(producto.IdProducto == venta.IdProducto)
                    {
                        venta.Cantidad = venta.Cantidad + 1;
                    }
                    else
                    {
                        producto.Cantidad = producto.Cantidad = 1;
                        ventaProducto.ventaProductos.Add(producto);

                    }
                    HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(ventaProducto.ventaProductos));
                    if (HttpContext.Session.GetString("Carrito") != null)
                    {
                        ViewBag.Message = "Se ha agregado el producto a tu carrito!";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se pudo agregar tu producto ):";
                        return PartialView("Modal");
                    }
                }
            }
            return View();
        
        }
    }
}
