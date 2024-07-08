using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DL;
using ML;
using DLEF;
using System.Runtime.Remoting.Contexts;
using System.ComponentModel.Design;
using System.Security.AccessControl;
namespace BL
{
    public class Usuario
    {
        //3. Ahora recibimos los datos de la capa PL y los mandamos a la bd
        
        //METODOS CON SP
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConexionString()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("UsuarioUpdate", conexion))
                    {
                        //definir el tipo de comando
                        //existen 3: StoredProcedure,Text,TableDirect(Solo para OLE DB)
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Email", usuario.Email);
                        cmd.Parameters.AddWithValue("@Password", usuario.Password);
                        cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                        cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                        cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                        DateTime DateNacimiento = DateTime.Parse(usuario.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", DateNacimiento);
                        cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                        cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0) result.Correct = true;
                        else result.Correct = false;
                    }
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConexionString()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("UsuarioAdd", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                        cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                        cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                        cmd.Parameters.AddWithValue("@Email", usuario.Email);
                        cmd.Parameters.AddWithValue("@Password", usuario.Password);
                        cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                        cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                        cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                        DateTime DateNacimiento = DateTime.Parse(usuario.FechaNacimiento);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", DateNacimiento);
                        cmd.Parameters.AddWithValue("@CURP", usuario.CURP);
                        cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);
                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0) result.Correct = true;
                        else result.Correct = false;
                    }
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConexionString()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("UsuarioDelete", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        int RowsAffected = cmd.ExecuteNonQuery();
                        if (RowsAffected > 0) result.Correct = true;
                        else result.Correct = false;
                    }
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {

                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConexionString()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("UsuarioGetAll", conexion))
                    {
                        DataTable tablaUsuario = new DataTable();
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {

                            adapter.Fill(tablaUsuario);
                            if (tablaUsuario.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();

                                //para cada fila
                                foreach (DataRow fila in tablaUsuario.Rows)
                                {

                                    ML.Usuario usuario = new ML.Usuario();
                                    //usuario.Rol = new Rol();

                                    usuario.IdUsuario = int.Parse(fila[0].ToString());
                                    usuario.Nombre = fila[1].ToString();
                                    usuario.ApellidoPaterno = fila[2].ToString();
                                    usuario.ApellidoMaterno = fila[3].ToString();
                                    usuario.Sexo = fila[4].ToString();
                                    usuario.Rol.IdRol = byte.Parse(fila[5].ToString());
                                    usuario.UserName = fila[6].ToString();
                                    usuario.Email = fila[7].ToString();
                                    usuario.Password = fila[8].ToString();
                                    usuario.Telefono = fila[9].ToString();
                                    usuario.Celular = fila[10].ToString();
                                    usuario.FechaNacimiento = fila[11].ToString();
                                    usuario.CURP = fila[12].ToString();

                                    result.Objects.Add(usuario);

                                }

                                result.Correct = true;
                            }
                            else result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        //METODOS CON LINQ
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    //var query = context.Usuario;
                    var query = (from user in context.Usuario select user).ToList();
                    if (query.Count() > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var fila in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();

                            usuario.IdUsuario = fila.IdUsuario;
                            usuario.Nombre = fila.Nombre;
                            usuario.ApellidoPaterno = fila.ApellidoPaterno;
                            usuario.ApellidoMaterno = fila.ApellidoMaterno;
                            usuario.Sexo = fila.Sexo;
                            usuario.Rol.IdRol = fila.IdRol.Value;
                            usuario.UserName = fila.UserName;
                            usuario.Email = fila.Email;
                            usuario.Password = fila.Password;
                            usuario.Telefono = fila.Telefono;
                            usuario.Celular = fila.Celular;

                            usuario.FechaNacimiento = fila.FechaNacimiento.Value.ToString();
                            usuario.CURP = fila.CURP;

                            result.Objects.Add(usuario);

                        }
                        result.Correct = true;
                    }
                    else { result.Correct = false; }
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result GetByIDLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    //var query = context.Usuario.Where(user => user.IdUsuario == usuario.IdUsuario).ToList();
                    var query =
                        (from user in context.Usuario
                         where user.IdUsuario == usuario.IdUsuario
                         select user).FirstOrDefault();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        //para cada fila
                        //var filtrar = context.Rol.ToList(u => u.IdRol=2) ;
                        usuario.Rol = new ML.Rol();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Sexo = query.Sexo;
                        usuario.Rol.IdRol = query.IdRol.Value;
                        usuario.UserName = query.UserName;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.FechaNacimiento = query.FechaNacimiento.Value.ToString();
                        usuario.CURP = query.CURP;

                        result.Objects.Add(usuario);
                        result.Correct = true;
                    }
                    else result.Correct = false;
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result DeleteLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {

                    //var query = context.Usuario.Where(user => user.IdUsuario == usuario.IdUsuario).ToList();
                    var query =
                        (from user in context.Usuario
                         where user.IdUsuario == usuario.IdUsuario
                         select user).ToList();
                    if (query.Count > 0)
                    {
                        context.Usuario.Remove(query[0]);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else result.Correct = false;
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {

                    DateTime DateNacimiento = DateTime.Parse(usuario.FechaNacimiento);

                    var IfExist = context.Usuario.SingleOrDefault(u => u.IdUsuario == usuario.IdUsuario);
                    if (IfExist == null)
                    {
                        DLEF.Usuario user = new DLEF.Usuario()
                        {

                            Nombre = usuario.Nombre,
                            ApellidoPaterno = usuario.ApellidoPaterno,
                            ApellidoMaterno = usuario.ApellidoMaterno,
                            Sexo = usuario.Sexo,
                            IdRol = usuario.Rol.IdRol,
                            UserName = usuario.UserName,
                            Email = usuario.Email,
                            Password = usuario.Password,
                            Telefono = usuario.Telefono,
                            Celular = usuario.Celular,
                            FechaNacimiento = DateNacimiento,
                            CURP = usuario.CURP
                        };
                        context.Usuario.Add(user);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else result.Correct = false;

                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result UpdateLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {

                    DateTime DateNacimiento = DateTime.Parse(usuario.FechaNacimiento);

                    var userExist = context.Usuario.SingleOrDefault(u => u.IdUsuario == usuario.IdUsuario);
                    if (userExist != null)
                    {

                        userExist.IdUsuario = usuario.IdUsuario;
                        userExist.Nombre = usuario.Nombre;
                        userExist.ApellidoPaterno = usuario.ApellidoPaterno;
                        userExist.ApellidoMaterno = usuario.ApellidoMaterno;
                        userExist.Sexo = usuario.Sexo;
                        userExist.IdRol = usuario.Rol.IdRol;
                        userExist.UserName = usuario.UserName;
                        userExist.Email = usuario.Email;
                        userExist.Password = usuario.Password;
                        userExist.Telefono = usuario.Telefono;
                        userExist.Celular = usuario.Celular;
                        userExist.FechaNacimiento = DateNacimiento;
                        userExist.CURP = usuario.CURP;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else result.Correct = false;

                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }

        //METODOS CON EF
        public static ML.Result GetByID(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conexion = new SqlConnection(DL.Conexion.GetConexionString()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("UsuarioGetById", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        using (SqlDataAdapter Adapter = new SqlDataAdapter(cmd))
                        {
                            Adapter.Fill(dt);
                            //usuario.Rol = new Rol();
                            if (dt.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                DataRow fila = dt.Rows[0];

                                usuario.IdUsuario = int.Parse(fila[0].ToString());
                                usuario.Nombre = fila[1].ToString();
                                usuario.ApellidoPaterno = fila[2].ToString();
                                usuario.ApellidoMaterno = fila[3].ToString();
                                usuario.Sexo = fila[4].ToString();
                                usuario.Rol.IdRol = byte.Parse(fila[5].ToString());
                                usuario.UserName = fila[6].ToString();
                                usuario.Email = fila[7].ToString();
                                usuario.Password = fila[8].ToString();
                                usuario.Telefono = fila[9].ToString();
                                usuario.Celular = fila[10].ToString();
                                usuario.FechaNacimiento = fila[11].ToString();
                                usuario.CURP = fila[12].ToString();
                                result.Objects.Add(usuario);
                                result.Correct = true;
                            }
                            else result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {

                    DateTime DateNacimiento = DateTime.Parse(usuario.FechaNacimiento);

                    var rowsAffected = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno,
                       usuario.ApellidoMaterno, usuario.Sexo, usuario.Rol.IdRol, usuario.UserName, usuario.Email,
                       usuario.Password, usuario.Telefono, usuario.Celular, DateNacimiento, usuario.CURP, 
                       usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior,
                        usuario.Direccion.Colonia.IdColonia,usuario.Direccion.IdDireccion,usuario.Imagen);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else result.Correct = false;
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {

                    DateTime DateNacimiento = DateTime.Parse(usuario.FechaNacimiento);

                    var RowsAffected = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,
                        usuario.Sexo, usuario.Rol.IdRol, usuario.UserName, usuario.Email, usuario.Password, usuario.Telefono,
                        usuario.Celular, DateNacimiento, usuario.CURP,usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior,
                        usuario.Direccion.Colonia.IdColonia,usuario.Imagen,usuario.IdIdentity);

                    if (RowsAffected > 0) result.Correct = true;
                    else result.Correct = false;

                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result DeleteEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    int RowsAffected = context.UsuarioDelete(usuario.IdUsuario,usuario.Direccion.IdDireccion);
                    if (RowsAffected > 0) result.Correct = true;
                    else result.Correct = false;

                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result GetAllEF(ML.Usuario usuarioP)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var query = context.UsuarioGetAll(usuarioP.Nombre, usuarioP.ApellidoPaterno, usuarioP.ApellidoMaterno,usuarioP.Rol.IdRol).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        //para cada fila
                        foreach (var fila in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol();
                            usuario.Direccion = new ML.Direccion();

                            usuario.IdUsuario = fila.IdUsuario;
                            usuario.Nombre = fila.NombreUsuario;
                            usuario.ApellidoPaterno = fila.ApellidoPaterno;
                            usuario.ApellidoMaterno = fila.ApellidoMaterno;
                            usuario.Sexo = fila.Sexo;
                            usuario.Rol.IdRol = fila.IdRol;
                            usuario.UserName = fila.Username;
                            usuario.Email = fila.Email;
                            usuario.Password = fila.Password;
                            usuario.Telefono = fila.Telefono;
                            usuario.Celular = fila.Celular;
                            usuario.Imagen = fila.Imagen;
                            usuario.Rol.RolNombre = fila.RolNombre;
                            usuario.FechaNacimiento = fila.FechaNacimiento.Value.ToString();
                            DateTime OnlyDate = DateTime.Parse(usuario.FechaNacimiento).Date;
                            usuario.FechaNacimiento = OnlyDate.ToString("yyyy-MM-dd");

                            usuario.CURP = fila.CURP;
                            usuario.Status = fila.Status.Value;
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion=fila.IdDireccion;
                            usuario.Direccion.Calle=fila.Calle;
                            usuario.Direccion.NumeroInterior=fila.NumeroInterior;
                            usuario.Direccion.NumeroExterior=fila.NumeroExterior;
                            usuario.Direccion.Colonia=new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = fila.IdColonia;
                            usuario.Direccion.Colonia.Nombre = fila.NombreColonia;
                            usuario.Direccion.Colonia.CP = fila.CP;
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = fila.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = fila.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado=new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = fila.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre=fila.NombreEstado;
                            result.Objects.Add(usuario);

                        }

                        result.Correct = true;
                    }
                    else result.Correct = false;
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;
            }
            return result;
        }
        public static ML.Result GetByIDEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var fila = context.UsuarioGetByID(byte.Parse(usuario.IdUsuario.ToString())).SingleOrDefault();
                    if (fila != null)
                    {
                        result.Object = new List<object>();
                        //var filtrar = context.Rol.ToList(u => u.IdRol=usuario.IdUsuario) ;
                        usuario.Rol = new ML.Rol();
                        usuario.IdUsuario = fila.IdUsuario;
                        usuario.Nombre = fila.Nombre;
                        usuario.ApellidoPaterno = fila.ApellidoPaterno;
                        usuario.ApellidoMaterno = fila.ApellidoMaterno;
                        usuario.Sexo = fila.Sexo;
                        usuario.Rol.IdRol = fila.IdRol.Value;
                        usuario.UserName = fila.Username;
                        usuario.Imagen = fila.Imagen;
                        usuario.Email = fila.Email;
                        usuario.Password = fila.Password;
                        usuario.Telefono = fila.Telefono;
                        usuario.Celular = fila.Celular;
                        usuario.FechaNacimiento = fila.FechaNacimiento.Value.ToString();
                        usuario.CURP = fila.CURP;
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = fila.IdDireccion;
                        usuario.Direccion.Calle = fila.Calle;
                        usuario.Direccion.NumeroInterior=fila.NumeroInterior;
                        usuario.Direccion.NumeroExterior=fila.NumeroExterior;
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = fila.IdColonia;
                        usuario.Direccion.Colonia.CP = fila.CP;
                        usuario.Direccion.Colonia.Municipio=new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = fila.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Estado= new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = fila.IdEstado;
                        
                        result.Object=usuario;
                        result.Correct = true;
                    }
                    else result.Correct = false;
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;

            }
            return result;
        }
        public static ML.Result UpdateStatus(int IdUsuario,bool Status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var fila = context.UsuarioStatusUpdateById(IdUsuario,Status);
                    if (fila>0)
                    {
                        result.Correct = true;
                    }
                    else result.Correct = false;
                }
            }
            catch (Exception Exc)
            {
                result.Correct = false;
                result.ErrorMessage = Exc.Message;
                result.Exc = Exc;

            }
            return result;
        }

    }


}
