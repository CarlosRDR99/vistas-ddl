using DLEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubCategoria
    {
        public static ML.Result GetByIdCategoria(int Id)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var query = context.SubCategoriaGetByIdCategoria(Id).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        //para cada fila
                        foreach (var fila in query)
                        {
                            ML.SubCategoria subCategoria = new ML.SubCategoria();
                            subCategoria.Id = fila.Id;
                            subCategoria.Nombre = fila.Nombre;
                            result.Objects.Add(subCategoria);

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
