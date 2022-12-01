using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proovedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Proovedors.FromSqlRaw("[ProveedorGetAll]").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Proovedor proveedor = new ML.Proovedor();

                            proveedor.IdProovedor = obj.IdProovedor;
                            proveedor.Nombre = obj.Nombre;

                            result.Objects.Add(proveedor);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Se ha producido un error");
                        Console.ReadKey();
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
    }
}
