using Microsoft.AspNetCore.Mvc;
using ML;
using System.Configuration;
using System.IO;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = new Result();
            return View(result);
        }

        [HttpPost]
        public ActionResult CargaTXT()
        {
            IFormFile fileTXT = Request.Form.Files["archivoTXT"];

            if (fileTXT != null)
            {
                StreamReader Textfile = new StreamReader(fileTXT.OpenReadStream());

                string line;
                line = Textfile.ReadLine();

                ML.Result resultError = new ML.Result();
                resultError.Objects = new List<object>();

                while ((line = Textfile.ReadLine()) != null)
                {
                    string[] lines = line.Split('/');

                    ML.Usuario usuario = new ML.Usuario();

                    usuario.Nombre = lines[0];
                    usuario.ApellidoPaterno = lines[1];
                    usuario.ApellidoMaterno = lines[2];
                    usuario.FechaDeNacimiento = lines[3];
                    usuario.Sexo = lines[4];
                    usuario.UserName = lines[5];
                    usuario.Email = lines[6];
                    usuario.Password = lines[7];
                    usuario.Telefono = lines[8];
                    usuario.Celular = lines[9];
                    usuario.CURP = lines[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = int.Parse(lines[11]);


                    usuario.Imagen = null;

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = lines[12];
                    usuario.Direccion.NumeroInterior = lines[13];
                    usuario.Direccion.NumeroExterior = lines[14];

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);

                    ML.Result result = BL.Usuario.Add(usuario);

                    if (!result.Correct)
                    {
                        resultError.Objects.Add(
                            usuario.Nombre + " / " +
                         usuario.ApellidoPaterno + " / " +
                         usuario.ApellidoMaterno + " / " +
                        usuario.FechaDeNacimiento + " / " +
                        usuario.Sexo + " / " +
                        usuario.UserName + " / " +
                         usuario.Email + " / " +
                       usuario.Password + " / " +
                       usuario.Telefono + " / " +
                         usuario.Celular + " / " +
                         usuario.CURP + " / " +
                        usuario.Rol.IdRol + " / " +
                         usuario.Direccion.Calle + " / " +
                        usuario.Direccion.NumeroInterior + " / " +
                         usuario.Direccion.NumeroExterior + " / " +
                        usuario.Direccion.Colonia.IdColonia + " / " +
                        result.ErrorMessage);



                    }
                    if (resultError == null)
                    {
                        ViewBag.Message = "Se ha realizado el registro con exito";
                        return PartialView("Modal");
                    }
                    else
                    {
                        TextWriter text = new StreamWriter(@"C:\Users\digis\OneDrive\Documents\Aviles Coria Javier\ErroresLayout.txt");
                        foreach (var error in resultError.Objects)
                        {
                            text.WriteLine(error);
                        }
                        text.Close();
                        ViewBag.Message = "Error al cargar datos";
                        return PartialView("Modal");
                    }
                }
            }
            ViewBag.Message = "Ha terminado la Carga";
            return PartialView("Modal");

        }


        [HttpPost]
        public ActionResult UsuarioCargaMasiva(ML.Usuario usuario)
        {
            IFormFile archivo = Request.Form.Files["FileExcel"];
            //sesion

            if (HttpContext.Session.GetString("PathArchivo") == null)// Validación de sesion nula o que no exista 
            {
                //Validar que sea .xlsx
                string fileName = Path.GetFileName(archivo.FileName);
                string folderPath = _configuration["PathFolder:value"];
                string extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();
                string extensionModulo = _configuration["TipoExcel"];

                if (extensionArchivo == extensionModulo)
                {
                    //crear copia del archvio excel que se subio, Esta copia será la que se "Leera" y validara si los datos son correctos
                    string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + extensionArchivo;

                    if (!System.IO.File.Exists(filePath))
                    {
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            //Creación de copia del archivo
                            archivo.CopyTo(stream);
                        }
                        //Inicia lectura de copia hecha en ese moemento

                        //Connexion Con OLEDB Para poder realizar la lectura del archivo .xlsx
                        string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                        //Creación de objeto resultado del metodo ConvertitExcelDataTable(tabla/ Lista de Objetos)
                        ML.Result resultConvertExcel = BL.Usuario.ConvertirExcelDataTable(connectionString);

                        //Si se convirtio de manera correcta
                        if (resultConvertExcel.Correct)
                        {
                            //Validar y Sacar errores encontrados en el excel
                            ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultConvertExcel.Objects);

                            //
                            if (resultValidacion.Objects.Count == 0)
                            {
                                resultValidacion.Correct = true;
                                HttpContext.Session.SetString("PathArchivo", filePath);

                            }
                            return View("CargaMasiva", resultValidacion);

                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio un error en el archivo " + " "+resultConvertExcel.ErrorMessage;
                            return PartialView("Modal");
                        }
                    }
                }

                //verificar que no tenga errores 
                //le avisamos al usuario que el excel esta mal ML.ErrorExcel 
                //crea la sesion 
            }
            else
            {
                //add 
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertirExcelDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {
                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se Inserto el usuario: " + usuarioItem.Nombre + "Error: " + resultAdd.ErrorMessage);

                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {
                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"~\Files\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los Usuarios No han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los Usuarios han sido registrados correctamente";

                    }
                }


                //errores al agregar 
            }
            return PartialView("Modal");
        }
    }
}

