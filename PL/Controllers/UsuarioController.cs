using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            usuario.Rol = new ML.Rol();

            ML.Result resultRol = BL.Rol.GetAll();
            result = BL.Usuario.GetAll(usuario);



            if (result.Correct)
            {
                usuario.Rol.NombreRol = resultRol.Objects;
                usuario.Usuarios = result.Objects;

                return View(usuario);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error";
                return View();
            }

        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            


            ML.Result resultRol = BL.Rol.GetAll();
            result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.NombreRol = resultRol.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al consultar los Usuarios";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();



            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();




            if (IdUsuario == null)
            {
                //
                usuario.Rol.NombreRol = resultRol.Objects;

                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;


                return View(usuario);
            }
            else
            {

                //GetbyId
                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);


                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object;
                    usuario.Rol.NombreRol = resultRol.Objects;


                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                    //Estado
                    ML.Result resultEstados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

                    //Municipio
                    ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;

                    //Colonia
                    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el Usuario seleccionado";
                }
                return View(usuario);
            }

        }


        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            IFormFile image = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto usuario
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }


            if (!ModelState.IsValid)
            {
                if (usuario.IdUsuario == 0)
                {
                    //add
                    result = BL.Usuario.Add(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Message = "Se ha registrado el Usuario";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No ha registrado el Usuario" + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
                else
                {
                    //update
                    result = BL.Usuario.Update(usuario);
                    if (result.Correct)
                    {
                        ViewBag.Message = "Se ha registrado el Usuario";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No ha registrado el Usuario" + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
            }
            else
            {

                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultPais = BL.Pais.GetAll();

                usuario.Rol.NombreRol = resultRol.Objects;


                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                return View(usuario);

            }



        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.Delete(IdUsuario);
            if (result.Correct)
            {
                ViewBag.Message = "Se ha elimnado el registro";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Message = "No se ha elimnado el registro";
                return PartialView("Modal");
            }
        }


        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult GetDireccion(int IdPais)
        {
            var result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }
        public JsonResult CambiarStatus(ML.Usuario usuario)
        {
            var result = BL.Usuario.ChangeStatus(usuario);

            return Json(result.Objects);
        }




        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
