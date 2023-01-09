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
            }
            else
            {
                ViewBag.Message = "Ocurrio un error";
            }
            return View(producto);
        }
        [HttpPost]
        public ActionResult AnadirCarrito(int? IdProducto)
        {
            ML.VentaProducto ventaProducto = new VentaProducto();
            ventaProducto.ventaProductos = new List<object>();

            bool productoAdd = false;

            if (HttpContext.Session.GetString("ProductoCompra") == null)
            {
                ventaProducto.Producto = new ML.Producto();
                ventaProducto.Producto.IdProducto = IdProducto.Value;
                ventaProducto.Cantidad = 1;

                ML.Result resultProducto = BL.Producto.GetById(IdProducto.Value);
                ventaProducto.Producto = (ML.Producto)resultProducto.Object;

                ventaProducto.ventaProductos.Add(ventaProducto);
                HttpContext.Session.SetString("ProductoCompra", JsonConvert.SerializeObject(ventaProducto.ventaProductos));

                ViewBag.Message = "Se Añadio al carrito";
                return PartialView("Modal");
            }
            else
            {
                var carritoProductos = JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("ProductoCompra"));
                if (carritoProductos != null)
                {
                    bool existe = false;

                    foreach(var producto in carritoProductos)
                    {
                        ML.VentaProducto ventaProductoObj = JsonConvert.DeserializeObject<ML.VentaProducto>(producto.ToString());
                        ventaProducto.ventaProductos.Add(ventaProductoObj);

                    }
                    foreach(ML.VentaProducto validate in ventaProducto.ventaProductos)
                    {
                        if (ventaProducto.Producto.IdProducto == IdProducto.Value)
                        {
                            existe = true;
                            validate.Cantidad++;
                        }
                    }

                    if (!existe)
                    {
                        ventaProducto.Producto = new ML.Producto();
                        ventaProducto.Producto.IdProducto = IdProducto.Value;
                        ventaProducto.Cantidad = 1;

                        ML.Result resultProducto = BL.Producto.GetById(IdProducto.Value);
                        ventaProducto.Producto = (ML.Producto)resultProducto.Object;
                        ventaProducto.ventaProductos.Add(ventaProducto);

                        HttpContext.Session.SetString("ProductoCompra", JsonConvert.SerializeObject(ventaProducto.ventaProductos));
                        productoAdd = true;
                        ViewBag.Message = "Se ha Agregado una cantidad del producto";
                    }
                    else
                    {
                        HttpContext.Session.SetString("ProductoCompra", JsonConvert.SerializeObject(ventaProducto.ventaProductos));
                        productoAdd = true;
                        ViewBag.Message = "Se añadio una cantidad extra de tu este producto";
                    }
                }
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public IActionResult Carrito(ML.VentaProducto ventaProducto)
        {
            if (HttpContext.Session.GetString("ProductoCompra") == null)
            {
                ViewBag.Message = "No tiene productos en el carrito";

                return View("Modal");
            }
            else
            {
                var ventaSession = JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("ProductoCompra"));
                ventaProducto.ventaProductos = new List<object>();

                foreach (var obj in ventaSession)
                {
                    ML.VentaProducto ventaProductoObj = JsonConvert.DeserializeObject<ML.VentaProducto>(obj.ToString());
                    ventaProducto.ventaProductos.Add(ventaProductoObj);
                }
            }

            return View(ventaProducto);

        }

        public IActionResult Desagregar(int IdProducto)
        {
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ventaProducto.ventaProductos = new List<object>();

            var carritoProdutos = JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("VentaProducto"));
            var indice = 0;

            if (carritoProdutos != null)
            {
                bool esCero = false;

                foreach (var producto in carritoProdutos)
                {
                    ML.VentaProducto ventaProductoObj = JsonConvert.DeserializeObject<ML.VentaProducto>(producto.ToString());
                    ventaProducto.ventaProductos.Add(ventaProductoObj);
                }

                foreach (ML.VentaProducto verificaProducto in ventaProducto.ventaProductos)
                {
                    if (verificaProducto.Producto.IdProducto == IdProducto)
                    {


                        verificaProducto.Cantidad = verificaProducto.Cantidad - 1;


                        if (verificaProducto.Cantidad == 0)
                        {
                            indice = ventaProducto.ventaProductos.IndexOf(verificaProducto);
                            esCero = true;

                        }
                        else
                        {
                            HttpContext.Session.SetString("VentaProducto", JsonConvert.SerializeObject(ventaProducto.ventaProductos));
                            ViewBag.Message = "Se ha eliminado una cantidad del producto";
                            esCero = false;
                        }


                    }




                }
                if (esCero)
                {
                    ventaProducto.ventaProductos.RemoveAt(indice);
                    HttpContext.Session.SetString("VentaProducto", JsonConvert.SerializeObject(ventaProducto.ventaProductos));
                    ViewBag.Message = "Se ha eliminado el producto";
                }

            }

            return PartialView("Modal");
        }

        public IActionResult Eliminar(int IdProducto)
        {
            ML.VentaProducto ventaProducto = new ML.VentaProducto();
            ventaProducto.ventaProductos = new List<object>();
            var carritoProdutos = JsonConvert.DeserializeObject<List<object>>(HttpContext.Session.GetString("VentaProducto"));
            var indice = 0;
            if (carritoProdutos != null)
            {
                bool existe = false;

                foreach (var producto in carritoProdutos)
                {
                    ML.VentaProducto ventaProductoObj = JsonConvert.DeserializeObject<ML.VentaProducto>(producto.ToString());
                    ventaProducto.ventaProductos.Add(ventaProductoObj);
                }

                foreach (ML.VentaProducto verificaProducto in ventaProducto.ventaProductos)
                {
                    if (verificaProducto.Producto.IdProducto == IdProducto)
                    {
                        
                        indice = ventaProducto.ventaProductos.IndexOf(verificaProducto);
                        existe = true;
                    
                    }


                }

                if (existe)
                {
                    ventaProducto.ventaProductos.RemoveAt(indice);
                    HttpContext.Session.SetString("VentaProducto", JsonConvert.SerializeObject(ventaProducto.ventaProductos));
                    ViewBag.Message = "Se ha eliminado una cantidad del producto";
                }



            }

            return PartialView("Modal");
        }
    }
}
