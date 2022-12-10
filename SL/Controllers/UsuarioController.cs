using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ML;

namespace SL.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GETALL POST
        [HttpPost("GetAll")]
        public IActionResult GetAll(string? nombre, string? ApellidoPaterno, string? ApellidoMaterno)
        {

            ML.Usuario usuario = new ML.Usuario();

            //alumno.Nombre = nombre;
            usuario.Nombre = (nombre == null) ? "" : nombre;
            usuario.ApellidoPaterno = (ApellidoPaterno == null) ? "" : ApellidoPaterno;
            usuario.ApellidoMaterno = (ApellidoMaterno == null) ? "" : ApellidoMaterno;

            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("GetById/{idUsuario}")]
        public IActionResult Get(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetById(idUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        

        [HttpDelete("Delete/{idUsuario}")]
        public ActionResult Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.Delete(idUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("Add")]
        public ActionResult Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();


            result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("Update")]
        public ActionResult Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.Update(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("Login/{UserName}")]

        public IActionResult GetByIdUserName(string userName)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetByUserName(userName);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
