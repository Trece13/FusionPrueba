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
    public class ttwhcol016
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
        string tabla = owner + ".twhcol016" + env;

        public ttwhcol016() 
        { 
            
        }

        public int insertarRegistro(ref List<Ent_ttwhcol016> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string MsgstrError = string.Empty;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla); 
                
                foreach (Ent_ttwhcol016 reg in parametros)
                {
                    parametrosIn = AdicionaParametrosComunes(reg);
                    MsgstrError = "Error when inserting data [ttwhcol016]. ";
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

                    paramList = new Dictionary<string, object>();
                    paramList.Add("p1", reg.zone.Trim().ToUpperInvariant());

                    strSentencia = recursos.readStatement(method.ReflectedType.Name, "actualizarSecuenciaInsert", ref owner, ref env, tabla, paramList);                     

                    //" SET t$ffno = (SELECT MAX(t$ffno) " +
                    //                 "FROM " + owner + ".ttcmcs050" + env +
                    //                " WHERE TRIM(T$NRGR) = '" + reg.zone.Trim() + "')  + ROWNUM " +

                    MsgstrError = "Error al Actualizar ttcmcs050. ";                    
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null , false);

                }
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = MsgstrError + "Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public int TakeMaterialInv_verificaConsLabel_Param(ref Ent_ttwhcol016 parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", parametros.zone.Trim().ToUpperInvariant());

            Dictionary<string, object> parametersOut = new Dictionary<string, object>();
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1)
                {
                    strError = "-1";
                    return Convert.ToInt32(strError);
                }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttwhcol016]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(consulta.Rows[0]["CONSEC"].ToString());
        }

        public DataTable TakeMaterialInv_listaConsLabel_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.labl.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 
            
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Label " + Parametros.labl + " not found"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttwhcol016]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable TakeMaterialInv_verificaBodega_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.cwar.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Wharehouse code doesn´t exist. Cannot continue"; }
                return consulta;
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable TakeMaterialInv_verificaItem_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.item.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Item code doesn´t exist. Cannot continue"; }
                return consulta;
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable TakeMaterialInv_verificaZona_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.zone.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Zone code doesn´t exist. Cannot continue"; }
                return consulta;
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable TakeMaterialInv_verificaLote_Param(ref Ent_ttwhcol016 ParametrosIn, ref string strError)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.clot.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Lot Code doesn´t exist. Cannot Continue"; }
                return consulta;
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_ttwhcol016 parametros, bool blnUsarPRetorno = false)
        {
            string strError = string.Empty;
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ZONE", DbType.String, parametros.zone);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LABL", DbType.String, parametros.labl);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, "         " + parametros.item);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTYR", DbType.Decimal, Convert.ToDecimal(parametros.qtyr).ToString("#.##0,0000") );
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);
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
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
                throw (ex);
            }
            return parameterCollection;
        }

    }
}









