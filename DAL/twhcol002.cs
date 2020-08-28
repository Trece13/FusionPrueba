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
    public class twhcol002
    {
        private static MethodBase method = MethodBase.GetCurrentMethod();
        private static Recursos recursos = new Recursos();
        private static Utilidades.Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();

        Dictionary<string, object> paramList = new Dictionary<string, object>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSQL = string.Empty;
        String strwareh = string.Empty;

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".twhcol002" + env;

        DataTable consulta = new DataTable();

        public DataTable listaRegistro_ObtieneConteo(ref Ent_twhcol002 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", Parametros.cwar.Trim());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "There is no active count for the warehouse." + Parametros.cwar.Trim(); }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [twhcol002]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }
    }
}
