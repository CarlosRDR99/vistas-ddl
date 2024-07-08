﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IUsuarioServices
    {
        [OperationContract]
        SL_WCF.Result Add(ML.Usuario usuario);

        [OperationContract]
        SL_WCF.Result Update(ML.Usuario usuario);

        [OperationContract]
        SL_WCF.Result Delete(ML.Usuario usuario);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Usuario))]
        SL_WCF.Result GetAll(ML.Usuario usuario);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Usuario))]
        SL_WCF.Result GetById(ML.Usuario usuario);
    }
}