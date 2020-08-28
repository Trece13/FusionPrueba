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

namespace whusa.DAL
{
    public class ttccol307
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

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".ttccol307" + env;

        static ttccol307()
        {

        }


        public bool ActualizarUsuariotccol307(Ent_ttccol307 ObjTtccol307)
        {
            string strError = string.Empty;
            bool ActualizacionExitosa = false;

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":PAID", ObjTtccol307.PAID);
            paramList.Add(":USER", ObjTtccol307.USRR);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                ActualizacionExitosa = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttccol303]. Try again or contact your administrator";
                throw ex;
            }

            return ActualizacionExitosa;
        }

        public DataTable ConsultarRegistrotccol307(Ent_ttccol307 ObjTtccol307)
        {
            string strError = string.Empty;
            DataTable Consulta = new DataTable();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":PAID", ObjTtccol307.PAID.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttccol307]. Try again or contact your administrator";
                throw ex;
            }
            return Consulta;
        }
    }
}
