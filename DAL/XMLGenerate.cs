using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusa.Interfases
{
    public class XMLGenerate
    {
        private static Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method;
        private static Recursos recursos = new Recursos();

        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> paramList = new Dictionary<string, object>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSentencia = string.Empty;
        DataTable consulta = new DataTable();

        private String env = ConfigurationManager.AppSettings["env"].ToString();
        private String owner = ConfigurationManager.AppSettings["owner"].ToString();

        /* Metodos */
        public XMLGenerate()
        {

        }

        /// <summary>
        /// Consultar etiqueta toma de materiales
        /// </summary>
        /// <param name="Ent_ttwhcol016">Objetos de tipo Entidad[Ent_ttwhcol016] con parametros a usar</param>
        /// <param name="strError">Cadena de error para controlar el resultado</param>
        /// <returns>DataTable: Tabla virtual con el registro correspondiente al parametro (En caso de existir) u Objeto nulo en caso contrario .  </returns>
        public DataTable listaRegistrosMaquinas(ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, "tticol003", paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "No existen maquinas para procesar"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error consultando tticol003. Intente de nuevo o contacte al administrador";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaRegistrosItems(ref string lstMachines, ref string strError, int last = 1)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", lstMachines.Replace("''", "'"));

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, "tticol003", paramList, last);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "No existen maquinas para procesar"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error consultando tticol003. Intente de nuevo o contacte al administrador";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable listaRegistrosItemsUSA(ref string lstMachines, ref string strError, int last = 1)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", lstMachines.Replace("''", "'"));

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, "tticol003", paramList, last);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "No existen máquinas para procesar"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error consultando. Intente de nuevo o contacte al administrador";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

    }
}
