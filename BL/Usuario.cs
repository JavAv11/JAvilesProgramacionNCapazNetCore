using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
            
                    usuario.Rol.IdRol = (usuario.Rol.IdRol == null) ? 0 : usuario.Rol.IdRol;
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}',{usuario.Rol.IdRol}").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Usuario objUsuario = new ML.Usuario();

                            objUsuario.IdUsuario = obj.IdUsuario;
                            objUsuario.Nombre = obj.Nombre;
                            objUsuario.ApellidoPaterno = obj.ApellidoPaterno;
                            objUsuario.ApellidoMaterno = obj.ApellidoMaterno;
                            objUsuario.FechaDeNacimiento = obj.FechaDeNacimiento.ToString("dd-MM-yyyy");
                            objUsuario.Sexo = obj.Sexo;
                            objUsuario.UserName = obj.UserName;
                            objUsuario.Email = obj.Email;
                            objUsuario.Password = obj.Password;
                            objUsuario.Telefono = obj.Telefono;
                            objUsuario.Celular = obj.Celular;
                            objUsuario.CURP = obj.Curp;

                            objUsuario.Rol = new ML.Rol();
                            objUsuario.Rol.IdRol = obj.IdRol.Value;
                            objUsuario.Rol.Nombre = obj.NombreRol;

                            objUsuario.Imagen = obj.Imagen;
                            objUsuario.Status = obj.Status.Value;


                            //Direccion
                            objUsuario.Direccion = new ML.Direccion();
                            objUsuario.Direccion.IdDireccion = obj.IdDireccion;
                            objUsuario.Direccion.Calle = obj.Calle;
                            objUsuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            objUsuario.Direccion.NumeroExterior = obj.NumeroExterior;

                            //////Colonia
                            objUsuario.Direccion.Colonia = new ML.Colonia();
                            objUsuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            objUsuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            objUsuario.Direccion.Colonia.CP = obj.CodigoPostal;

                            ////Municipio

                            objUsuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            objUsuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            objUsuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            ////Estado
                            objUsuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            objUsuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            objUsuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            ////Pais
                            objUsuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            objUsuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            objUsuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;



                            result.Objects.Add(objUsuario);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Se ha producido un error");

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

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.FechaDeNacimiento}', '{usuario.Sexo}', '{usuario.UserName}', '{usuario.Email}', '{usuario.Password}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.Rol.IdRol}, '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");
                    if (usuarios > 0)
                    {

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar al objUsuario";
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

        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var objUsuario = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();


                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.FechaDeNacimiento = objUsuario.FechaDeNacimiento.ToString("dd-MM-yyyy");
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.UserName = objUsuario.UserName;
                        usuario.Email = objUsuario.Email;
                        usuario.Password = objUsuario.Password;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.CURP = objUsuario.Curp;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objUsuario.IdRol.Value;
                        usuario.Rol.Nombre = objUsuario.NombreRol;

                        usuario.Imagen = objUsuario.Imagen;
                        usuario.Status = objUsuario.Status.Value;


                        //Direccion
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objUsuario.IdDireccion;
                        usuario.Direccion.Calle = objUsuario.Calle;
                        usuario.Direccion.NumeroInterior = objUsuario.NumeroInterior;
                        usuario.Direccion.NumeroExterior = objUsuario.NumeroExterior;

                        //////Colonia
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objUsuario.IdColonia;
                        usuario.Direccion.Colonia.Nombre = objUsuario.NombreColonia;
                        usuario.Direccion.Colonia.CP = objUsuario.CodigoPostal;

                        ////Municipio

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objUsuario.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = objUsuario.NombreMunicipio;

                        ////Estado
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objUsuario.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objUsuario.NombreEstado;

                        ////Pais
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objUsuario.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objUsuario.NombrePais;

                        result.Object = usuario;
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

        public static ML.Result GetByUserName(string UserName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var objUsuario = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName {UserName}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();


                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.FechaDeNacimiento = objUsuario.FechaDeNacimiento.ToString("dd-MM-yyyy");
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.UserName = objUsuario.UserName;
                        usuario.Email = objUsuario.Email;
                        usuario.Password = objUsuario.Password;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.CURP = objUsuario.Curp;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objUsuario.IdRol.Value;
                        usuario.Rol.Nombre = objUsuario.NombreRol;

                        usuario.Imagen = objUsuario.Imagen;
                        usuario.Status = objUsuario.Status.Value;


                        //Direccion
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objUsuario.IdDireccion;
                        usuario.Direccion.Calle = objUsuario.Calle;
                        usuario.Direccion.NumeroInterior = objUsuario.NumeroInterior;
                        usuario.Direccion.NumeroExterior = objUsuario.NumeroExterior;

                        //////Colonia
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objUsuario.IdColonia;
                        usuario.Direccion.Colonia.Nombre = objUsuario.NombreColonia;
                        usuario.Direccion.Colonia.CP = objUsuario.CodigoPostal;

                        ////Municipio

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objUsuario.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = objUsuario.NombreMunicipio;

                        ////Estado
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objUsuario.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objUsuario.NombreEstado;

                        ////Pais
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objUsuario.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objUsuario.NombrePais;

                        result.Object = usuario;
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

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.FechaDeNacimiento}', '{usuario.Sexo}', '{usuario.UserName}', '{usuario.Email}', '{usuario.Password}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.Rol.IdRol}, '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");

                    if (usuarios > 0)
                    {

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar al objUsuario";
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

        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {IdUsuario}");

                    if (query >0)
                    {
                        result.Correct = true;
                    }
                    else
                    { 
                        result.Correct = false;
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

        public static ML.Result ConvertirExcelDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tablaUsuario = new DataTable();

                        da.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tablaUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[2].ToString();
                                usuario.FechaDeNacimiento = row[3].ToString();
                                usuario.Sexo = row[4].ToString();
                                usuario.UserName = row[5].ToString();
                                usuario.Email = row[6].ToString();
                                usuario.Password = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = byte.Parse(row[11].ToString());


                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = byte.Parse(row[15].ToString());

                                result.Objects.Add(usuario);

                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
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

        public static ML.Result ValidarExcel(List<Object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();

                int i = 1; //Variable para marcar errores e ir autoincrementando
                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel errorExcel = new ML.ErrorExcel();
                    errorExcel.IdRegistro = i++;

                    usuario.Nombre = (usuario.Nombre == "") ? errorExcel.Mensaje += "Ingresar un Nombre " : usuario.Nombre;
                    usuario.ApellidoPaterno = (usuario.ApellidoPaterno == "") ? errorExcel.Mensaje += "Ingresar un ApellidoPaterno " : usuario.ApellidoPaterno;
                    usuario.ApellidoMaterno = (usuario.ApellidoMaterno == "") ? errorExcel.Mensaje += "Ingresar un ApellidoMaterno " : usuario.ApellidoMaterno;
                    usuario.FechaDeNacimiento = (usuario.FechaDeNacimiento == "") ? errorExcel.Mensaje += "Ingresar un FechaDeNacimiento " : usuario.FechaDeNacimiento;
                    usuario.Sexo = (usuario.Sexo == "") ? errorExcel.Mensaje += "Ingresar un Sexo " : usuario.Sexo;
                    usuario.UserName = (usuario.UserName == "") ? errorExcel.Mensaje += "Ingresar un UserName " : usuario.UserName;
                    usuario.Email = (usuario.Email == "") ? errorExcel.Mensaje += "Ingresar un Email " : usuario.Email;
                    usuario.Password = (usuario.Password == "") ? errorExcel.Mensaje += "Ingresar un Password " : usuario.Password;
                    usuario.Telefono = (usuario.Telefono == "") ? errorExcel.Mensaje += "Ingresar un Telefono " : usuario.Telefono;
                    usuario.Celular = (usuario.Celular == "") ? errorExcel.Mensaje += "Ingresar un Celular " : usuario.Celular;
                    usuario.CURP = (usuario.CURP == "") ? errorExcel.Mensaje += "Ingresar un CURP " : usuario.CURP;

                    if (usuario.Rol.IdRol.ToString() == "")
                    {
                        errorExcel.Mensaje += "Ingresa el Rol";
                    }

                    usuario.Direccion.Calle = (usuario.Direccion.Calle == "") ? errorExcel.Mensaje += "Ingresar una Calle " : usuario.Direccion.Calle;
                    usuario.Direccion.NumeroInterior = (usuario.Direccion.NumeroInterior == "") ? errorExcel.Mensaje += "Ingresar un NumeroInterior " : usuario.Direccion.NumeroInterior;
                    usuario.Direccion.NumeroExterior = (usuario.Direccion.NumeroExterior == "") ? errorExcel.Mensaje += "Ingresar un NumeroExterior " : usuario.Direccion.NumeroExterior;


                    if (usuario.Direccion.Colonia.IdColonia.ToString() == "")
                    {
                        errorExcel.Mensaje += "Ingresa una colonia";
                    }

                    if (errorExcel.Mensaje != null)
                    {
                        result.Objects.Add(errorExcel);
                    }
                }
                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result ChangeStatus(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JavilesProgramacionNcapasContext context = new DL.JavilesProgramacionNcapasContext())
                {
                    var usuarios = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus {usuario.IdUsuario}, {usuario.Status}");
                    if (usuarios > 0)
                    {

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar al objUsuario";
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
