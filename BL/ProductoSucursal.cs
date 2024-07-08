using DLEF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProductoSucursal
    {
        public static ML.Result GetByIdSucursal(int Id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var query = context.ProductoSucursalGetByIdSucursal(Id).ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        //var filtrar = context.Rol.ToList(u => u.IdRol=usuario.IdUsuario) ;
                        foreach(var fila in query) { 
                        ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
                        productoSucursal.Producto = new ML.Producto();
                        productoSucursal.Id = fila.Id;
                        productoSucursal.Producto.Nombre = fila.NombreProducto;
                        productoSucursal.Producto.Precio = fila.Precio.Value;
                        productoSucursal.Producto.Imagen = fila.Imagen;
                        productoSucursal.Stock = fila.Stock.Value;
                        result.Objects.Add(productoSucursal);
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
        public static ML.Result UpdateStock(int Id,int Stock)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var RowsAffected = context.ProductoSucursalUpdateStock(Id, Stock);

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
    }
}
