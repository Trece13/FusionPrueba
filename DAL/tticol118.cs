using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using whusa.Utilidades;
using System.Diagnostics;
using whusa.Entidades;
using System.Data;
using System.Configuration;

namespace whusa.Interfases
{
    public class tticol118_2
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
        private static string tabla = owner + ".tticol118" + env;

        public tticol118_2() 
        {
            //Constructor
        }

        public DataTable findRecordByItemClotCwarQtyr(ref Ent_tticol118 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CLOT", data.clot.Trim().ToUpper());
            paramList.Add(":T$ITEM", data.item.Trim().ToUpper());
            paramList.Add(":T$CWAR", data.cwar.Trim().ToUpper());
            paramList.Add(":T$QTYR", data.qtyr);

            strSentencia = recursos.readStatement(method.ReflectedType.Name.Split('_')[0], method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol118_2]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }
        public int UpdatePalletStatus_ticol022(ref Ent_tticol118 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$DELE", "3");
                paramList.Add(":T$SQNB", parametro.paid.Trim().ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                   ex.Message + " (" + ex.InnerException + ")" :
                   ex.Message;
            }
            return Convert.ToInt32(retorno);

           
        }
    }
}
