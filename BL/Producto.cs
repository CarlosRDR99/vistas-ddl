using DLEF;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var RowsAffected = context.ProductoAdd(producto.Nombre,producto.Descripcion,producto.Precio,producto.Imagen,producto.SubCategoria.Id);

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
        public static ML.Result Delete(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    int RowsAffected = context.ProductoDelete(producto.Id);
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {

                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var query = context.ProductoGetAll().ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        //para cada fila
                        foreach (var fila in query)
                        {
                            ML.Producto producto =new ML.Producto();
                            producto.SubCategoria = new ML.SubCategoria();
                            producto.SubCategoria.Categoria = new ML.Categoria();

                            producto.Id = fila.Id;
                            producto.Nombre = fila.Nombre;
                            producto.Descripcion = fila.Descripcion;
                            producto.Precio = fila.Precio.Value;
                            producto.Imagen = fila.Imagen;
                            producto.SubCategoria.Id = fila.IdSubCategoria.Value;
                            producto.SubCategoria.Nombre = fila.SubCategoriaNombre;
                            producto.SubCategoria.Categoria.Id = fila.IdCategoria.Value;
                            producto.SubCategoria.Categoria.Nombre = fila.CategoriaName;
                            result.Objects.Add(producto);

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
        public static ML.Result GetByID(int Id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var fila = context.ProductoGetById(Id).SingleOrDefault();
                    if (fila != null)
                    {
                        result.Object = new List<object>();
                        //var filtrar = context.Rol.ToList(u => u.IdRol=usuario.IdUsuario) ;
                        ML.Producto producto = new ML.Producto();
                        producto.SubCategoria = new ML.SubCategoria();
                        producto.SubCategoria.Categoria = new ML.Categoria();

                        producto.Id = fila.Id;
                        producto.Nombre = fila.Nombre;
                        producto.Descripcion = fila.Descripcion;
                        producto.Precio = fila.Precio.Value;
                        producto.Imagen = fila.Imagen;
                        producto.SubCategoria.Id = fila.IdSubCategoria.Value;
                        producto.SubCategoria.Nombre = fila.SubCategoriaNombre;
                        producto.SubCategoria.Categoria.Id = fila.IdCategoria.Value;
                        producto.SubCategoria.Categoria.Nombre = fila.CategoriaNombre;
                        result.Object=producto;
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
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {


                    var rowsAffected = context.ProductoUpdate(producto.Id,producto.Nombre,producto.Descripcion,producto.Precio,producto.Imagen,producto.SubCategoria.Id);
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

        public static ML.Result LeerExcel(string cadenaCon) {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(cadenaCon))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    OleDbCommand cmd = new OleDbCommand(query,context);
                    cmd.Connection.Open();
                    OleDbDataReader reader= cmd.ExecuteReader();
                    if (reader!=null)
                    {
                        result.Objects = new List<object>();
                        while (reader.Read())
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.Nombre = reader[0].ToString();
                            producto.Descripcion = reader[1].ToString();
                            producto.Precio = Decimal.Parse(reader[2].ToString());
                            producto.SubCategoria = new ML.SubCategoria();
                            producto.SubCategoria.Id = int.Parse(reader[3].ToString());
                            result.Objects.Add(producto);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se pudo hacer el select del archivo";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Exc = ex;
            }
            return result;
        }
        public static ML.Result ValidarExcel(List<object> productos)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            int count = 1;
            foreach (ML.Producto producto in productos)
            {
                ML.ErrorExcel error = new ML.ErrorExcel();
                if (producto.Nombre=="")
                {
                    error.Mensaje += "No puede ir el nombre vacío";
                }
                if (producto.Descripcion=="")
                {
                    error.Mensaje += "No puede ir la descripción vacía";
                }
                if (producto.Precio==0)
                {
                    error.Mensaje += "No puede ir el precio en 0";
                }
                if (producto.SubCategoria.Id==0)
                {
                    error.Mensaje += "No puede ir el id de subcategoria en 0";
                }
                if (error.Mensaje!=null)
                {
                    error.NumeroRegistro = count;
                    result.Objects.Add(error);
                }
                count ++;
            }
            return result;
        }
        public static ML.Result GetAllBusqueda(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var query = context.ProductoGetAllByIdCategoria(producto.SubCategoria.Categoria.Id,producto.SubCategoria.Id).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        //para cada fila
                        foreach (var fila in query)
                        {
                            ML.Producto productoNew = new ML.Producto();
                            productoNew.SubCategoria = new ML.SubCategoria();
                            productoNew.SubCategoria.Categoria = new ML.Categoria();
                            productoNew.Id = fila.Id;
                            productoNew.Nombre = fila.Nombre;
                            productoNew.Descripcion = fila.Descripcion;
                            productoNew.Precio = fila.Precio.Value;
                            productoNew.Imagen = fila.Imagen;
                            productoNew.SubCategoria.Id = fila.IdSubCategoria.Value;
                            productoNew.SubCategoria.Nombre = fila.SubCategoriaNombre;
                            productoNew.SubCategoria.Categoria.Id = fila.IdCategoria.Value;
                            productoNew.SubCategoria.Categoria.Nombre = fila.CategoriaName;

                            result.Objects.Add(productoNew);

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

    }
}
