using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    producto.Proovedor.IdProovedor = (producto.Proovedor.IdProovedor == null) ? 0 : producto.Proovedor.IdProovedor;
                    var query = context.Productos.FromSqlRaw($"ProductoGetAll '{producto.Nombre}', {producto.Proovedor.IdProovedor}").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Producto objProducto = new ML.Producto();

                            objProducto.IdProducto = item.IdProducto;
                            objProducto.Nombre = item.Nombre;
                            objProducto.PrecioUnitario = item.PrecioUnitario;
                            objProducto.Stock = item.Stock;

                            objProducto.Proovedor = new ML.Proovedor();
                            objProducto.Proovedor.IdProovedor = item.IdProovedor.Value;
                            objProducto.Proovedor.Nombre = item.NombreProovedor;

                            objProducto.Departamento = new ML.Departamento();
                            objProducto.Departamento.IdDepartamento = item.IdDepartamento.Value;
                            objProducto.Departamento.Nombre = item.NombreDepartamento;

                            objProducto.Departamento.Area = new ML.Area();
                            objProducto.Departamento.Area.IdArea = item.IdArea.Value;
                            objProducto.Departamento.Area.Nombre = item.NombreArea;


                            objProducto.Descripcion = item.Descripcion;

                            objProducto.Imagen = item.Imagen;

                            result.Objects.Add(objProducto);

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

        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSql($"ProductoGetById {IdProducto}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.PrecioUnitario = query.PrecioUnitario;
                        producto.Stock = query.Stock;

                        producto.Proovedor = new ML.Proovedor();
                        producto.Proovedor.IdProovedor = query.IdProovedor.Value;


                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = query.IdDepartamento.Value;

                        producto.Departamento.Area = new ML.Area();
                        producto.Departamento.Area.IdArea = query.IdArea.Value;
                        producto.Departamento.Area.Nombre = query.NombreArea;


                        producto.Descripcion = query.Descripcion;

                        producto.Imagen = query.Imagen;

                        result.Object = producto;
                        result.Correct = true;
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

        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proovedor.IdProovedor}, {producto.Departamento.IdDepartamento}, '{producto.Descripcion}','{producto.Imagen}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo hacer le registro del objProducto";
                    }
                    result.Correct = true;
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

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoUpdate {producto.IdProducto}, '{producto.Nombre}', {producto.PrecioUnitario}, {producto.Stock}, {producto.Proovedor.IdProovedor}, {producto.Departamento.IdDepartamento}, '{producto.Descripcion}','{producto.Imagen}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo hacer le registro del objProducto";
                    }
                    result.Correct = true;
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

        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSql($"ProductoDelete {IdProducto}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo Borrar el objProducto";
                    }
                    else
                    {

                        result.Correct = true;
                    }
                    result.Correct = true;
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
