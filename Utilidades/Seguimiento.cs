using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;


namespace whusa.Utilidades
{
    public class Seguimiento
    {
        LogEntry entradaObj = new LogEntry();

        public void escribirError(string msgError, string aplicacion, string metodo, string clase)
        {
            //IConfigurationSource config = ConfigurationSourceFactoryCreate("whusap");
            LogWriterFactory fabricaEscritorLog = new LogWriterFactory();
            var escritor = fabricaEscritorLog.Create(); 
            string Aplicacion = "General";

            Dictionary<string, object> infAdicional = new Dictionary<string, object>();
            
            infAdicional.Add("Generado desde : ", aplicacion);
            infAdicional.Add("Clase: ", clase);
            infAdicional.Add("Metodo: ", metodo);

            infAdicional.Add("Modulo", "--- [Este valor debe ser el nombre de la clase y metodo que genera la excepcion ] --- ");


            entradaObj = new LogEntry
            {
              Categories = new string[] { "Error" },
              EventId = 9007,
              Message = msgError,
              Priority = 9,
              Severity = TraceEventType.Error,
              Title = "Excepcion en Modulo de Aplicacion (" + Aplicacion +")" + clase  + "." + metodo,
              ExtendedProperties = infAdicional
            };
        
            if (escritor.IsLoggingEnabled())
            {
                escritor.Write(entradaObj);
            }
        }
    }
}
