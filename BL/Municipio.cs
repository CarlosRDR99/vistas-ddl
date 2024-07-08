using DLEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetAllByIdEstadoEF(byte IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var query = context.MunicipiosGetByIdEstado(IdEstado).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        //para cada fila
                        foreach (var fila in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = fila.IdMunicipio;
                            municipio.Nombre = fila.Nombre;
                            result.Objects.Add(municipio);

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
