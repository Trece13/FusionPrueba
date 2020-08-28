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
    public class ttccol303
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
        private static string tabla = owner + ".ttccol303" + env;

        static ttccol303() 
        { 
            
        }

        public DataTable listaRegistrosMenu_Param(ref Ent_ttccol300 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.user);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttccol303]. Try again or contact your administrator";
                throw ex;
            }
        }

        public DataTable datosMenu_Param(string USER,string PROG)
        {


            int positionWebPages = PROG.ToLower().IndexOf("webpages");

            if (positionWebPages > 0)
            {
                PROG = PROG.Remove(0, positionWebPages-1);
            }

            string retorno = string.Empty;
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$USER", USER);
            paramList.Add(":T$PROG", PROG);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            log.escribirError("Ejecucion correcta de : ", "Fusion2pop", "datosMenu_Param", "una");
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                //if (consulta.Rows.Count < 1) { strError = "-1"; }
                if (consulta.Rows.Count > 0) {
                    retorno = consulta.Rows[0]["MENG"].ToString().Trim();
                }
                log.escribirError("Ejecucion correcta de Sql : " + strSentencia, "Fusion2pop", "datosMenu_Param", "una");
                return consulta;
                
            }
            catch (Exception ex)
            {
                log.escribirError("Script Error 13: " + strSentencia, "Fusion2pop", method.ToString(), "ttccol303");
                //strError = "Error when querying data [ttccol303]. Try again or contact your administrator";
                throw ex;
            }
        }

    }
}


