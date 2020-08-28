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
    public class tticol090
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
        private static string tabla = owner + ".tticol090" + env;

        static tticol090() 
        { }

        public int insertarRegistro(ref List<Ent_tticol090> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod(); 
            bool retorno = false;

            int secuencia = 0;
            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla); 

                foreach (Ent_tticol090 reg in parametros)
                {
                    secuencia = lineCleareance_verificaItemInsert(reg, strError);
                    reg.srno = secuencia;
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [090]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            
            return Convert.ToInt32(retorno); 
            
        }

        public int actualizarRegistro_Param(ref List<Ent_tticol090> parametros, ref string strError, string Aplicacion)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false; 
            string strCondicion = string.Empty;


            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla); 
                foreach (Ent_tticol090 reg in parametros)
                {
                    strSentencia += " WHERE ROWID = '" + reg.idrecord + "'";

                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    if (!string.IsNullOrEmpty(strError))
                        break;
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error updating data [090]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
                return Convert.ToInt32(retorno);
        }
    
        public int lineCleareance_verificaItemInsert(Ent_tticol090 Parametros, string strError)
        {

            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.item.Trim().ToUpperInvariant());
            paramList.Add("p2", Parametros.fpdn.Trim().ToUpperInvariant());
            
            
            string strSentencFin = string.Empty;
            int resultado = 0;

            strSentencFin = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencFin, ref parametersOut, null, true);
                resultado = int.Parse(consulta.Rows[0]["CANT"].ToString());
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return resultado;
        }

        public DataTable lineClearance_verificaOrdenes_Param(ref Ent_tticol090 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.fpdn.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable lineClearance_listaRegistrosOrden_Param(ref Ent_tticol090 Parametros, ref string strError)
        {

            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.fpdn.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;               
        }

        public DataTable lineClearance_verificaRegistrosOrden_Param(ref Ent_tticol090 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.item.Trim().ToUpperInvariant());
            paramList.Add("p2", Parametros.fpdn.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Material to move doesn´t belong to target WO"; }
            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;               
        }
        
        public DataTable lineClearance_verificaRegistrosOrden_Param(ref Ent_tticol090 Parametros, ref Ent_ttwhcol016 Parametros016, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros016.item.Trim().ToUpperInvariant());
            paramList.Add("p2", Parametros016.clot.Trim().ToUpperInvariant());
            paramList.Add("p3", Parametros.fpdn.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                if (consulta.Rows.Count < 1) { strError = "Lot Code was not on Work Order. Cannot Continue"; }
                //return consulta;

            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;               
        }

        public DataTable lineClearance_verificaLote_Param(ref Ent_tticol090 Parametros, ref string strError)
        { 
        
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", Parametros.tpdn.Trim().ToUpperInvariant());
            paramList.Add("p2", Parametros.item.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                if (consulta.Rows.Count < 1) { strError = "Lot Code was not on Work Order. Cannot Continue"; }
                //return consulta;

            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;               
        
        }

        public DataTable ConsultarCantidadPoritem022042131( MyLioEntidad objEnt, ref string strError)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", objEnt.SITM);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                if (consulta.Rows.Count < 1) { strError = "there are not results for item"; }
                //return consulta;

            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable ConsultarCantidad215( MyLioEntidad objEnt, ref string strError)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", objEnt.SITM);
            paramList.Add(":T$CWAR", objEnt.CWAR);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                if (consulta.Rows.Count < 1) { strError = "there are not results for item"; }
                //return consulta;

            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable InsertTticol088( Ent_tticol088 obj088, ref string strError)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO",obj088.orno);
            paramList.Add(":T$PONO",obj088.pono);
            paramList.Add(":T$ITEM",obj088.item);
            paramList.Add(":T$QUNE",obj088.qune);
            paramList.Add(":T$LOGN",obj088.logn);
            paramList.Add(":T$DATE",obj088.date);
            paramList.Add(":T$PROC",obj088.proc);
            paramList.Add(":T$REFCNTD",obj088.refcntd);
            paramList.Add(":T$REFCNTU", obj088.refcntu);
            paramList.Add(":T$OORG", obj088.oorg);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                if (consulta.Rows.Count < 1) { strError = "there are not results for item"; }
                //return consulta;

            }
            catch (Exception ex)
            {
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol090 parametros, bool blnUsarPRetorno = false)
        {
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$FPDN", DbType.String, parametros.fpdn);   
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$TPDN", DbType.String, parametros.tpdn);   
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, parametros.item);   
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SRNO", DbType.Int32, parametros.srno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QANA", DbType.Decimal, Convert.ToDecimal(parametros.qana).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.proc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);

                if (!string.IsNullOrEmpty(parametros.logn))
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);


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
                log.escribirError(ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name); throw (ex);
            }
            return parameterCollection;
        }

    }
}
