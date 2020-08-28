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

namespace whusa.DAL
{
    public class twhcol080
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
        private static string tabla = owner + ".twhcol080" + env;

        public twhcol080() 
        {
            parametrosIn.Clear();
        }

        public int insertRecord(ref Ent_twhcol080 parametro, ref string strError)
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

        public DataTable findRecord(ref Ent_twhcol080 parametro, ref string strError) 
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SOUR", parametro.sour);
            paramList.Add(":T$ORNO", parametro.orno.Trim());
            paramList.Add(":T$CONJ", parametro.conj);
            paramList.Add(":T$PONO", parametro.pono);
            paramList.Add(":T$SQNB", parametro.sqnb);
            paramList.Add(":T$ORIG", parametro.orig);
            paramList.Add(":T$SERN", parametro.sern);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [twhwmd300]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public bool updateRecord(ref Ent_twhcol080 parametro, ref string strError) 
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);

                parametrosIn = AdicionaParametrosComunesUpdate(parametro);
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

        public bool updateRecordConfirm(ref Ent_twhcol080 parametro, ref string strError)
        {
            parametrosIn.Clear();
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$CONF", parametro.conf);
                paramList.Add(":T$ORNO", parametro.orno);
                paramList.Add(":T$SOUR", parametro.sour);
                paramList.Add(":T$ITEM", parametro.item);
                paramList.Add(":T$CONJ", parametro.conj);
                paramList.Add(":T$PONO", parametro.pono);
                paramList.Add(":T$SQNB", parametro.sqnb);
                paramList.Add(":T$SERN", parametro.sern);

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

        public bool updateRecordPicking(ref Ent_twhcol080 parametro, ref string strError)
        {
            parametrosIn.Clear();
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$ESTI", parametro.esti);
                paramList.Add(":T$LOGN", parametro.logn);
                paramList.Add(":T$SOUR", parametro.sour);
                paramList.Add(":T$ORNO", parametro.orno);
                paramList.Add(":T$CONJ", parametro.conj);
                paramList.Add(":T$PONO", parametro.pono);
                paramList.Add(":T$SQNB", parametro.sqnb);
                paramList.Add(":T$SERN", parametro.sern);

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

        public bool updateRecordConfPicking(ref Ent_twhcol080 parametro, ref string strError)
        {
            parametrosIn.Clear();
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$ORIG", parametro.orig);
                paramList.Add(":T$SOUR", parametro.sour);
                paramList.Add(":T$ORNO", parametro.orno);


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

        public DataTable findRecordByProc(ref string orderNumber, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", orderNumber);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [twhcol080]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable findRecordByOrnoSour(ref string orderNumber, ref string sour, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", orderNumber.Trim().ToUpper());
            paramList.Add(":T$SOUR", sour.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [twhcol080]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_twhcol080 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SOUR", DbType.Int32, parametros.sour);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORNO", DbType.String, parametros.orno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CONJ", DbType.Int32, parametros.conj);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PONO", DbType.Int32, parametros.pono);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SQNB", DbType.Int32, parametros.sqnb);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SERN", DbType.Int32, parametros.sern);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$RCNO", DbType.String, parametros.rcno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.loca);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, parametros.item);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QANA", DbType.Double, parametros.qana);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUNI", DbType.String, parametros.cuni);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot == null ? " " : parametros.clot.Trim().ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$HABL", DbType.Double, parametros.habl);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ESTI", DbType.Int32, parametros.esti);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DIFE", DbType.Int32, parametros.dife);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.proc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CONF", DbType.Int32, parametros.conf);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORIG", DbType.Int32, parametros.orig);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);

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
        
        private List<Ent_ParametrosDAL> AdicionaParametrosComunesUpdate(Ent_twhcol080 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SOUR", DbType.Int32, parametros.sour);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORNO", DbType.String, parametros.orno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CONJ", DbType.Int32, parametros.conj);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PONO", DbType.Int32, parametros.pono);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SQNB", DbType.Int32, parametros.sqnb);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QANA", DbType.Double, parametros.qana);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot == null ? " " : parametros.clot);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$HABL", DbType.Double, parametros.habl);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORIG", DbType.Int32, parametros.orig);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SERN", DbType.Int32, parametros.sern);

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
