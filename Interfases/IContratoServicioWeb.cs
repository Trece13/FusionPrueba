using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using whusa.Entidades;

namespace whusa.Interfases
{
    /// <summary>
    /// Class Name: IContratosServicioWeb
    /// Author: Edwing Loaiza
    /// Description: Interfaz para invocar los servicios de comunicacion hacia la fuente de datos
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "Interfases", ConfigurationName = "IContratoServicioWeb")]
    public interface IContratoServicioWeb
    {
        /// <summary>
        /// Date: 28-01-2014
        /// Class Name: EntidadListaImpuestos
        /// Author: Edwing Loaiza
        /// Description: Metodo que retorna una lista con los registros activos de la tabla impuestos
        /// </summary>
        /// 
        [System.ServiceModel.OperationContractAttribute(Action = "Interfases/RecuperaRegistro", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        List<Ent_tticol125> RecuperaRegistro();

        /// <summary>
        /// Date: 29-01-2014
        /// Class Name: EntidadListaImpuestos
        /// Author: Edwing Loaiza
        /// Description: Metodo que Inserta un registro en la tabla clientesapp
        /// </summary>
        int enviaDatos(Ent_tticol125 parametros); //int ultDig, string nit, string razonSocial, int idCiudad, int rangoVentas, int regimenComun, string[] impuestos);

        /// <summary>
        /// Date: 29-01-2014
        /// Class Name: EntidadListaImpuestos
        /// Author: Edwing Loaiza
        /// Description: Metodo que retorna una lista con los registros retornados por la consulta del calendario de impuestos correspondiente al cliente
        /// </summary>
        List<Ent_tticol125> recuperarCliente(Ent_tticol125 parametros);

        /// <summary>
        /// Date: 29-01-2014
        /// Class Name: EntidadListaImpuestos
        /// Author: Edwing Loaiza
        /// Description: Metodo que permite actulizar la información del cliente
        /// </summary>
        int actualizarInfoCliente(string idMovil, int ultDig, string nit, string razonSocial, int idCiudad, int rangoVentas, int regimenComun, string[] impuestos);



    }
}

