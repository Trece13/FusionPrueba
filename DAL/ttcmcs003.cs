using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using whusa.Utilidades;
using whusa.Entidades;
using System.Data;
using System.Configuration;

namespace whusa.DAL
{
    public class ttcmcs003
    {
        private static MethodBase method;
        private static Seguimiento log = new Seguimiento();
        private static Recursos recursos = new Recursos();
        private static StackTrace stackTrace = new StackTrace();

        String strSentencia = string.Empty;
        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        Dictionary<string, object> paramList = new Dictionary<string, object>();
        DataTable consulta = new DataTable();

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".ttcmcs003" + env;

        public ttcmcs003() 
        {
            //Constructor
        }

        public DataTable findRecordByCwar(ref string cwar, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [ttcmcs003]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable listRecordCwar(ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
           // paramList.Add(":T$CWAR", cwar.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
 
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [ttcmcs003]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }
    }
}
