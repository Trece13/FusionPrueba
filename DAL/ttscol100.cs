using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.Utilidades;
using System.Diagnostics;
using System.Reflection;

namespace whusa.DAL
{
    public class ttscol100
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
        private static string tabla = owner + ".ttscol100" + env;

        static ttscol100() 
        { 
            
        }

        public int insertarRegistro(ref List<Ent_ttscol100> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);

                foreach (Ent_ttscol100 reg in parametros)
                {
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [scol100]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);

        }

        public int actualizarRegistro_Param(ref List<Ent_ttscol100> parametros, ref string strError)
        { 
            method = MethodBase.GetCurrentMethod();
            bool retorno = false; 

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                string strCondicion = string.Empty;

                foreach (Ent_ttscol100 reg in parametros)
                {
                    strCondicion = " WHERE ROWID = '" + reg.idrecord + "'";
                    parametrosIn = AdicionaParametrosComunesUpdate(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia + strCondicion, ref parametersOut, parametrosIn, false);
                    if (!string.IsNullOrEmpty(strError))
                        break;
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error when updating data [scol100]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        public DataTable spareDelivery_verificaOrdenes_Param(ref Ent_ttscol100 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            /* Estados
            1: Libre
            5. Libre
            10. PLaneada
            15. Liberada o release
            20. COmpleta o Completed
            23. Costos o Costed
            25. Cerrada o Closed
            35. Cancelada o Canceled
            */

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.orno.Trim().ToUpperInvariant());
            strSentencia = recursos.readStatement("ttssoc200", method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Service order doesn't exist or status doesn't allowed. Cannot Continue."; }
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable spareDelivery_listaRegistroItem_Param(ref Ent_ttscol100 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.item.Trim().ToUpperInvariant());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Service order doesn't exist or status doesn't allowed. Cannot Continue."; }
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable spareDelivery_listaRegistroUbicacion_Param(ref Ent_ttscol100 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.loca.Trim().ToUpperInvariant());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Location doesn't exist. Cannot Continue."; }
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable spareDelivery_listaRegistrosOrdenParam(ref Ent_ttscol100 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.orno.Trim().ToUpperInvariant());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Service order doesn't exist or status doesn't allowed. Cannot Continue."; }
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_ttscol100 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORNO", DbType.String, parametros.orno.ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, "         " + parametros.item.Trim().ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.loca);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar.ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QDEL", DbType.Decimal, Convert.ToDecimal(parametros.qdel).ToString("#.##0,0000"));
//                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CDAT", DbType.String, parametros.cdat);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);


                if (!string.IsNullOrEmpty(parametros.logn))
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);    
                if (!string.IsNullOrEmpty(parametros.cusr))
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUSR", DbType.String, parametros.cusr);   
                if (parametros.conf > 0)
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CONF", DbType.Int32, parametros.conf);


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
                strError = "Error when creating parameters [scol100]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunesUpdate(Ent_ttscol100 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORNO", DbType.String, parametros.orno.ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, "         " + parametros.item.Trim().ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.loca);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar.ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QDEL", DbType.Decimal, Convert.ToDecimal(parametros.qdel).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUSR", DbType.String, parametros.cusr);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CONF", DbType.Int32, parametros.conf);


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
                strError = "Error when creating parameters [scol100]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }
    }
}
