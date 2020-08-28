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
using whusap.Entidades;

namespace whusa.DAL
{
    public class ttdcol524
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
        private static string tabla = owner + ".ttdcol524" + env;

        public DataTable GetData(ref Ent_ttdcol524 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.item.Trim().ToUpperInvariant());
            paramList.Add("p2", ParametrosIn.adjustmentQuantity.Trim().ToUpperInvariant());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Not found records for confirm"; }
            }

            catch (Exception ex)
            {
                strError = "Error when querying data [ttdcol524]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public int insertarDatos(ref List<Ent_ttdcol524> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);


                foreach (Ent_ttdcol524 reg in parametros)
                {
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [ttdcol524]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        public int actualizarContadores(ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [ttdcol524]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_ttdcol524 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$ORNO$EAM", DbType.String, parametros.orderNumber);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$DATE", DbType.DateTime, parametros.date.ToString("dd/MM/yyyy HH:mm:ss"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$EMNO", DbType.String, parametros.employee);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$CWAR", DbType.String, parametros.warehouse);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$ITEM", DbType.String, parametros.item);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$VSTK", DbType.Int64, parametros.adjustmentQuantity);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$STUN", DbType.String, parametros.unit);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$RECD", DbType.String, parametros.adjustmentReason);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$ESTA", DbType.Int16, parametros.status);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$ORNO", DbType.String, parametros.adjustmentBaanOrder);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$PONO", DbType.String, parametros.adjustmentBaanOrderLine);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$REFCNTD", DbType.Int64, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$REFCNTU", DbType.Int64, parametros.refcntu);

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
                strError = "Error when creating parameters [524]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }

    }
}
