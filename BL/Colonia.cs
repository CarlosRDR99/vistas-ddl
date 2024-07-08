using DLEF;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetAllByIdMunicipioEF(byte IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var query = context.ColoniasGetByIdMunicipio(IdMunicipio).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        //para cada fila
                        foreach (var fila in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = fila.IdColonia;
                            colonia.Nombre = fila.Nombre;
                            colonia.CP=fila.CP;
                            result.Objects.Add(colonia);

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
        public static ML.Result GetAllByCP(string CP)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (CDominguezEntities context = new CDominguezEntities())
                {
                    var query = context.CPGetAll(CP).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var fila in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = fila.IdColonia;
                            colonia.Nombre = fila.ColoniaNombre;

                            colonia.Municipio = new ML.Municipio();
                            colonia.Municipio.IdMunicipio = fila.IdMunicipio;
                            colonia.Municipio.Nombre = fila.MunicipioNombre;

                            colonia.Municipio.Estado= new ML.Estado();
                            colonia.Municipio.Estado.IdEstado = fila.IdEstado;
                            colonia.Municipio.Estado.Nombre = fila.EstadoNombre;

                            result.Objects.Add(colonia);
                        }
                        result.Correct = true;
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
    }
}
