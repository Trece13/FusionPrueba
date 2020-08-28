using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using whusa.Utilidades;
using whusa.Entidades;
using System.Diagnostics;
using System.Data;
using System.Configuration;

namespace whusa.DAL
{
    public class tticol116
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
        private static string tabla = owner + ".tticol116" + env;

        public tticol116() 
        {
            //Constructor
        }

        public int insertarRegistro(ref Ent_tticol116 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);

                parametrosIn = AdicionaParametrosComunes(parametro);
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

        public bool updateRecordRejectedWarehouse(ref string item, ref string cwar, ref string loca, ref double qtyr, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$ITEM", item.Trim().ToUpper());
                paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
                paramList.Add(":T$LOCA", loca.Trim().ToUpper());
                paramList.Add(":T$QTYR", qtyr.ToString().Replace(',','.'));


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

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

        public int UpdatePalletStatus_ticol022(ref Ent_tticol116 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$DELE", "3");
                paramList.Add(":T$PAID", parametro.paid.Trim().ToUpper());
                paramList.Add(":T$LOGN", parametro.logr.Trim().ToUpper());

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
        public int ActualUpdateWarehouse_ticol222(ref Ent_tticol116 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$CWAT", parametro.cwam.Trim().ToUpper());
                paramList.Add(":T$SQNB", parametro.paid.Trim().ToUpper());

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);

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
        public int ActualUpdateWarehouse_whcol131(ref Ent_tticol116 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$STAT", "11");
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

        public DataTable findRecordByWarehouseItemLocation(ref string item, ref string cwar, ref string loca, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", item.Trim().ToUpper());
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$LOCA", loca.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol116]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol116 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, "         " + parametros.item.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.loca.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTYR", DbType.Decimal, parametros.qtyr);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CDIS", DbType.String, parametros.cdis.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$OBSE", DbType.String, parametros.obse);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGR", DbType.String, parametros.logr.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DISP", DbType.Int32, parametros.disp);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.proc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SUNO", DbType.String, parametros.suno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PAID", DbType.String, parametros.paid);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAM", DbType.String, parametros.cwam);

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
                strError = "Error when creating parameters [301]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }
    }
}
