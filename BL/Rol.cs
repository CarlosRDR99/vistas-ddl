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
    public class Rol
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {

                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var query = context.RolGetAll().ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        //para cada fila
                        foreach (var fila in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = fila.IdRol;
                            rol.RolNombre = fila.RolNombre;
                            result.Objects.Add(rol);

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
