using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            usuario.Rol = new ML.Rol();

            ML.Result resultRol = BL.Rol.GetAll();

            //result = BL.Usuario.GetAll(usuario);
            try
            {

                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync("Usuario/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result.Objects = new List<Object>();
                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

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
            resultRol = BL.Rol.GetAll();


            //result = BL.Usuario.GetAll(usuario);
            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync("Usuario/GetAll/" + usuario.Nombre + usuario.ApellidoPaterno + usuario.Rol.IdRol);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());

                            result.Objects.Add(resultItemList);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            if (result.Correct == true)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.NombreRol = resultRol.Objects;
                return View(usuario);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al consultar los Usuarios";
                return View(usuario);
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
                ML.Result result = new ML.Result();
                try
                {
                    //result = BL.Usuario.GetById(IdUsuario.Value);
                    string urlAPI = _configuration["UrlAPI"];
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlAPI);
                        var responseTask = client.GetAsync("Usuario/GetById/" + IdUsuario);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Usuario resultItemList = new ML.Usuario();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros Usuario";
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }

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
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                ML.Result resultPais = BL.Pais.GetAll();
                ML.Result resultRol = BL.Rol.GetAll();

                usuario.Rol.NombreRol = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                return View(usuario);
            }
            if (usuario.IdUsuario == 0)
            {

                //result = BL.Usuario.Add(usuario);
                try
                {
                    string urlAPI = _configuration["UrlAPI"];
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlAPI);

                        var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);

                        postTask.Wait();

                        var resultServicio = postTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio un error al agregar el usuairo";
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }

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
                //result = BL.Usuario.Update(usuario);

                try
                {
                    string urlAPI = _configuration["UrlAPI"];
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlAPI);

                        var postTask = client.PutAsJsonAsync<ML.Usuario>("Usuario/Update/" + usuario.IdUsuario, usuario);

                        postTask.Wait();

                        var resultServicio = postTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio un error al agregar el usuairo";
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }

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


        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            if (IdUsuario > 0)
            {

                try
                {
                    string urlAPI = _configuration["UrlAPI"];
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(urlAPI);
                        var responseTask = client.DeleteAsync("Usuario/Delete/" + IdUsuario);

                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            result.Correct = true;
                        }
                    }

                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                //ML.Result result = BL.Usuario.Delete(IdUsuario);

                if (result.Correct)
                {
                    ViewBag.Message = "Se ha elimnado el Usuario";


                }

            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            //ML.Result result = BL.Usuario.GetByIdUserName(userName);
            ML.Result result = new ML.Result();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string urlAPI = _configuration["UrlAPI"];
                    httpClient.BaseAddress = new Uri(urlAPI);

                    var request = httpClient.GetAsync($"Usuario/Login/{userName}");
                    request.Wait();

                    var response = request.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readContent = response.Content.ReadAsStringAsync().Result;

                        ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                        result.ErrorMessage = resultAPI.ErrorMessage;

                        if (resultAPI.Correct)
                        {
                            ML.Usuario user = JsonConvert.DeserializeObject<ML.Usuario>(resultAPI.Object.ToString());
                            result.Object = user;
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                result.Correct = false;
                result.ErrorMessage = error.Message;
            }
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuario.Password == password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Contraseña o UserName incorrecto ";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "Error: " + result.ErrorMessage;
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

