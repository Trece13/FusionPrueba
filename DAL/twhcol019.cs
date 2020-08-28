using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.Utilidades;
using System.Diagnostics;
using System.Reflection;
using System.Data;
using System.Configuration;

namespace whusa.Interfases
{
    public class twhcol019
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
        private static string tabla = owner + ".twhcol019" + env;

        public bool insertRegistertwhcol019(ref Ent_twhcol019 objTwhcol019, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PAID",objTwhcol019.PAID.Trim().ToUpper());
                paramList.Add(":T$SQNB",objTwhcol019.SQNB);
                paramList.Add(":T$ZONE",objTwhcol019.ZONE.ToUpper());
                paramList.Add(":T$CWAR",objTwhcol019.CWAR.Trim().ToUpper());
                paramList.Add(":T$LOCA",objTwhcol019.LOCA.ToUpper());
                paramList.Add(":T$ITEM",objTwhcol019.ITEM);
                paramList.Add(":T$CLOT",objTwhcol019.CLOT.ToUpper());
                paramList.Add(":T$QTDL",objTwhcol019.QTDL);
                paramList.Add(":T$CUNI",objTwhcol019.CUNI.Trim());
                paramList.Add(":T$LOGN",objTwhcol019.LOGN.ToUpper().Trim());
                paramList.Add(":T$DATE",objTwhcol019.DATE);
                paramList.Add(":T$COUN",objTwhcol019.COUN);
                paramList.Add(":T$PROC",objTwhcol019.PROC);
                paramList.Add(":T$REFCNTD",objTwhcol019.REFCNTD);
                paramList.Add(":T$REFCNTU",objTwhcol019.REFCNTU);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);
                return retorno;
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }

            return retorno;
        }

        public DataTable Consetwhcol019(string PAID, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PAID", PAID.Trim().ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);                
                return retorno;
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }

            return retorno;
        }
    }
}
