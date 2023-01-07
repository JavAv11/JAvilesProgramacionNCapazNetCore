using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var departamentos = context.Departamentos.FromSqlRaw($"[DepartamentoGetAll]").ToList();
                    result.Objects = new List<object>();
                    if (departamentos != null)
                    {
                        foreach (var obj in departamentos)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;

                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;
                            departamento.Area.Nombre = obj.NombreArea;


                            result.Objects.Add(departamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var departamentos = context.Departamentos.FromSqlRaw($"[DepartamentoGetById]{IdDepartamento}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (departamentos != null)
                    {

                        ML.Departamento departamento = new ML.Departamento();
                        departamento.IdDepartamento = departamentos.IdDepartamento;
                        departamento.Nombre = departamentos.Nombre;

                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = departamentos.IdArea.Value;
                        departamento.Area.Nombre = departamentos.NombreArea;


                        result.Objects.Add(departamento);

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result DepartamentoGetByArea(int IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetByArea {IdArea}").AsEnumerable().ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {

                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;

                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;
                            departamento.Area.Nombre = obj.NombreArea;

                            result.Objects.Add(departamento);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoAdd '{departamento.Nombre}', {departamento.Area.IdArea}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo hacer el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result Update(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoUpdate {departamento.IdDepartamento}, '{departamento.Nombre}', {departamento.Area.IdArea}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result Delete(int IdDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSql($"DepartamentoDelete {IdDepartamento}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo Borrar el producto";
                    }
                    else
                    {

                        result.Correct = true;
                    }
                    //result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
