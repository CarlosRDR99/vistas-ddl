using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class UsuarioServices :  IUsuarioServices
    {
        public SL_WCF.Result Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);

            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Exc = result.Exc,
                Object = result.Object,
                Objects = result.Objects
            };
        }
        public SL_WCF.Result Update(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UpdateEF(usuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Exc = result.Exc,
                Object = result.Object,
                Objects = result.Objects
            };
        }
        public SL_WCF.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(usuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Exc = result.Exc,
                Object = result.Object,
                Objects = result.Objects
            };
        }
        public SL_WCF.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            return new SL_WCF.Result{   
                Correct=result.Correct,
                ErrorMessage=result.ErrorMessage,
                Exc=result.Exc,
                Object = result.Object,
                Objects =result.Objects  
            };
        }
        public SL_WCF.Result GetById(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetByIDEF(usuario);
            return new SL_WCF.Result
            {
                Correct = result.Correct,
                ErrorMessage = result.ErrorMessage,
                Exc = result.Exc,
                Objects = result.Objects,
                Object = result.Object
            };
        }
    }
}
