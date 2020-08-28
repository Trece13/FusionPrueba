using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Utilidades;
using System.Diagnostics;
using System.Reflection;
using whusa.Entidades;
using System.Data;
using System.Configuration;
using whusa;

namespace whusa.Interfases
{
    public class tticol075
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
        private static string tabla = "tticol075";

        public int insertarRegistro(ref List<Ent_tticol075> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);


                foreach (Ent_tticol075 reg in parametros)
                {
                    try
                    {
                        parametrosIn = AdicionaParametrosComunes(reg);
                        retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    }
                    catch
                    {
                    }
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [tticol075]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        public int eliminarRegistro(string orden, DateTime dtStart, DateTime dtEnd, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", orden);
            paramList.Add("p2", dtStart.ToString("MM/dd/yyyy HH:mm"));
            paramList.Add("p3", dtEnd.ToString("MM/dd/yyyy HH:mm"));

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol075]. Try again or contact your administrator";
                throw ex;
            }
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol075 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CHEC", DbType.Int32, parametros.chequeado);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$COMM", DbType.String, parametros.comentario);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWTT", DbType.String, parametros.tarifaHoraria);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATE", DbType.String, parametros.fecha.ToString("dd/MM/yyyy HH:mm:ss"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$HREA", DbType.Decimal, parametros.horas);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.usuario);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mensaje);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$OPNO", DbType.Int32, parametros.numOperacion);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PDNO", DbType.String, parametros.orden);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.procesado);

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
                strError = "Error when creating parameters [075]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }
    }
}
