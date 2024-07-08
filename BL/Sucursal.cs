using DLEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                   var query = context.SucursalGetAll().ToList();
                    result.Objects = new List<object>();
                    if (query.Count > 0)
                    {
                        foreach (var fila in query)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.Id = fila.Id;
                            sucursal.Nombre = fila.Nombre;
                            sucursal.Latitud = fila.Latitud;
                            sucursal.Longitud = fila.Longitud;
                            result.Objects.Add(sucursal);
                        }
                        result.Correct = true;
                    }
                    else result.Correct = false;
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
    }
}
