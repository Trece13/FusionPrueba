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
    public class twhcol072
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
        private static string tabla = owner + ".twhcol072" + env;

        public int insertarRegistro(ref List<Ent_twhcol072> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                log.escribirError("My Query 072: "+strSentencia, method.Module.Name, method.Name, method.ReflectedType.Name);

                foreach (Ent_twhcol072 reg in parametros)
                {
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = "Error when inserting data [twhcol072]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_twhcol072 parametros, bool blnUsarPRetorno = false)
        {
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SOUR", DbType.String, parametros.sour);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORNO", DbType.String, parametros.orno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PONO", DbType.String, parametros.pono);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DWMS", DbType.String, parametros.dwms);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PWMS", DbType.String, parametros.pwms);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, parametros.item);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QANA", DbType.Decimal, Convert.ToDecimal(parametros.qana).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUNI", DbType.String, parametros.cuni);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$INDT", DbType.String, parametros.indt);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PRDT", DbType.String, parametros.prdt);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.String, parametros.proc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$RCNO", DbType.String, parametros.rcno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$RWMS", DbType.String, parametros.rwms);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ERRO", DbType.String, parametros.erro);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SEQN", DbType.String, parametros.seqn);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.String, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.String, parametros.refcntu);

                if (blnUsarPRetorno)
                {
                    Ent_ParametrosDAL pDal = new Ent_ParametrosDAL();
                    pDal.Name = "@p_Int_Resultado";
                    pDal.Type = DbType.Int32;
                    pDal.ParDirection = ParameterDirection.Output;
                    parameterCollection.Add(pDal);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return parameterCollection;
        }
    }
}
