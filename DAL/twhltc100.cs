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
    public class twhltc100
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
        private static string tabla = owner + ".twhltc100" + env;

        DataTable consulta = new DataTable();

        public DataTable listaRegistro_Clot(ref Ent_twhltc100 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CLOT", Parametros.clot.Trim());
            paramList.Add(":T$ITEM", Parametros.item.Trim());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't be Asociated to the Item."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [twhltc100]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable listaRegistro_SiExiste(ref Ent_twhltc100 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CLOT", Parametros.clot.Trim());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated"; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [twhltc100]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable listaRegistros_ObtieneItem(ref Ent_twhltc100 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CLOT", ParametrosIn.clot.Trim().ToUpperInvariant());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated."; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [twhltc100]. Try again or contact your administrator \n " + strSQL;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
    }
}
