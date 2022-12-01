using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Departamento : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = new ML.Result();
            result = BL.Departamento.GetAll();

            if (result.Correct)
            {
                ML.Departamento departamento = new ML.Departamento();
                departamento.Departamentos = result.Objects;

                return View(departamento);
            }
            else
            {
                ViewBag.Message = "ocurrio un error";
                return View();
            }

        }

        [HttpGet]
        public ActionResult Form(int? IdDepartamento)
        {
            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            ML.Result resultArea = BL.Area.GetAll();

            if (IdDepartamento == null)
            {
                departamento.Area.Areas = resultArea.Objects;
                return View(departamento);
            }
            else
            {
                ML.Result result = BL.Departamento.GetById(IdDepartamento.Value);

                if (result.Correct)
                {
                   
                    departamento.Area.Areas = resultArea.Objects;
                    departamento = (ML.Departamento)result.Object;
                   
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el Usuario seleccionado";
                }
                return View(departamento);
            }

        }

        [HttpPost]

        public ActionResult Form(ML.Departamento departamento)
        {
            if (departamento.IdDepartamento == 0)
            {
                ML.Result result = BL.Departamento.Add(departamento);
                if (result.Correct)
                {
                    ViewBag.Message = "Se ha registrado el Departamento";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se ha registrado el Departamento" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
            else
            {
                ML.Result result = BL.Departamento.Update(departamento);
                if (result.Correct)
                {

                    ViewBag.Message = "Se ha Actualizado el Departamento";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No ha registrado el Departamento" + result.ErrorMessage;
                    return PartialView("Modal");
                }
            }

        }

        [HttpGet]
        public ActionResult Delete(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            result = BL.Departamento.Delete(IdDepartamento);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se ha elimnado el registro";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Mensaje = "No see ha elimnado el registro" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }
        public JsonResult GetDepartamento(int IdArea)
        {
            var result = BL.Departamento.DepartamentoGetByArea(IdArea);

            return Json(result.Objects);
        }
    }
}
