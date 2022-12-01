using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {

        // GET: Producto
        [HttpGet]
        public ActionResult GetAll()
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
            ML.Result result = new ML.Result();
            //producto.Proovedor = new ML.Proovedor();

            ML.Result resultProovedor = BL.Proovedor.GetAll();
            result = BL.Producto.GetAll(producto);

            if (result.Correct)
            {

                producto.Productos = result.Objects;
                producto.Proovedor.Proveedores = resultProovedor.Objects;
                return View(producto);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la Consulta";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {

            ML.Producto producto = new ML.Producto();

            producto.Departamento = new ML.Departamento();
            producto.Proovedor = new ML.Proovedor();
            producto.Departamento.Area = new ML.Area();


            ML.Result resultDepartamento = BL.Departamento.GetAll();
            ML.Result resultProveedor = BL.Proovedor.GetAll();
            ML.Result resultArea = BL.Area.GetAll();

            if (IdProducto == null)
            {
                //
                //producto.Rol.Roles = resultRol.Objects;
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                producto.Proovedor.Proveedores = resultProveedor.Objects;
                producto.Departamento.Area.Areas = resultArea.Objects;
                return View(producto);
            }
            else
            {

                //GetbyId
                ML.Result result = BL.Producto.GetById(IdProducto.Value);

                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object;
                    producto.Departamento.Departamentos = resultDepartamento.Objects;
                    producto.Proovedor.Proveedores = resultProveedor.Objects;
                    producto.Departamento.Area.Areas = resultArea.Objects;


                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el Usuario seleccionado";
                }
                return View(producto);
            }

        }

        [HttpPost]

        public ActionResult Form(ML.Producto producto)
        {

            IFormFile image = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto usuario
                producto.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            if (!ModelState.IsValid)
            {
                //ML.Producto producto = new ML.Producto();

                producto.Departamento = new ML.Departamento();
                producto.Proovedor = new ML.Proovedor();
                producto.Departamento.Area = new ML.Area();


                ML.Result resultDepartamento = BL.Departamento.GetAll();
                ML.Result resultProveedor = BL.Proovedor.GetAll();
                ML.Result resultArea = BL.Area.GetAll();

                return View(producto);

            }
            else
            {

                if (producto.IdProducto == 0)
                {
                    ML.Result result = BL.Producto.Add(producto);
                    if (result.Correct)
                    {
                        ViewBag.Message = "Se ha registrado el producto";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se ha registrado el Producto" + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
                else
                {
                    ML.Result result = BL.Producto.Update(producto);
                    if (result.Correct)
                    {

                        ViewBag.Message = "Se ha actualizado el Producto";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No ha actualizado el Producto" + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();

            result = BL.Producto.Delete(IdProducto);
            if (result.Correct)
            {
                ViewBag.Message = "Se ha elimnado el registro";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se ha elimnado el registro" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }

        //Convertir imagen
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }


        public JsonResult GetDepartamento(int IdArea)
        {
            var result = BL.Departamento.DepartamentoGetByArea(IdArea);

            return Json(result.Objects);
        }

    }
}
