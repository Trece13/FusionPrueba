using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using whusa.Entidades;
using System.Data;

namespace ServicioWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface InterfazServicioWhusa
    {

        //[OperationContractAttribute(Action = "Interfases/RecuperaRegistro", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]

        //[OperationContract]
        //int actualizarInfoCliente(Ent_tticol125 entidad);
        [OperationContract]
        List<Ent_tticol125> enviaDatos(List<Ent_tticol125> parametros); 
        [OperationContract]
        List<Ent_tticol125> CambiaEstado(Ent_tticol125 parametros);
        [OperationContract]
        List<Ent_tticol125> recuperarCliente(Ent_tticol125 parametros);
        [OperationContract]
        DataTable listaRegistrosOrden_Param(ref Ent_tticol125 parametros);

    }
}
