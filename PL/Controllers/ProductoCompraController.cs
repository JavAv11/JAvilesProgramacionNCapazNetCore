using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using System.IdentityModel.Tokens.Jwt;

namespace PL.Controllers
{
    public class ProductoCompraController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public ProductoCompraController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


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
        public IActionResult Carrito(int? IdProducto)
        {
            ML.Result result = new ML.Result();
            if(IdProducto != 0)
            {
                if (HttpContext.Session.GetString("Carrito") == null)
                {
                    ML.VentaProducto ventaProducto = new ML.VentaProducto();

                    ventaProducto.Producto = new ML.Producto();

                    ventaProducto.Producto.IdProducto = IdProducto.Value;

                    ventaProducto.Cantidad = 1;

                    ML.Result resultProducto = BL.Producto.GetById(IdProducto.Value);

                    result.Objects = new List<object>();
                    if (resultProducto.Correct)
                    {
                        ventaProducto.Producto = (ML.Producto)resultProducto.Object;
                        result.Objects.Add(ventaProducto);
                    }
                    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(result.Objects)) ;
                    result.Correct = true;
                }
                else
                {
                    result.Objects = JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("Carrito"));

                    bool Existe = false;
                    var indice = 0;

                    foreach (ML.VentaProducto ventaProducto in result.Objects)
                    {
                        if(ventaProducto.Producto.IdProducto == IdProducto)
                        {
                            Existe = true;
                            indice = result.Objects.IndexOf(ventaProducto);

                        }
                    }
                    if (Existe == true)
                    {
                        foreach(ML.VentaProducto ventaProducto in result.Objects)
                        {
                            ventaProducto.Cantidad = ventaProducto.Cantidad + 1;
                        }
                    }
                    else
                    {
                        ML.VentaProducto ventaProducto = new ML.VentaProducto();
                        ventaProducto.Producto = new ML.Producto();
                        ventaProducto.Producto.IdProducto = IdProducto.Value;
                        ventaProducto.Cantidad = 1;

                        ML.Result resultProducto = BL.Producto.GetById(IdProducto.Value);
                        result.Objects.Add(resultProducto);
                    }
                }
            }
            return View("Carrito", result);

            //ML.VentaProducto ventaProducto = new ML.VentaProducto();
            //ventaProducto.ventaProductos = new List<object>();          
            //if (HttpContext.Session.GetString("Carrito") == null)
            //{
            //    ventaProducto.Cantidad = ventaProducto.Cantidad = 1;
            //    ventaProducto.ventaProductos.Add(IdProducto);
            //    HttpContext.Session.SetString("Carrito", Newtonsoft.Json.JsonConvert.SerializeObject(ventaProducto.ventaProductos));
            //    var session = HttpContext.Session.GetString("Carrito");
            //
        
        }
    }
}
