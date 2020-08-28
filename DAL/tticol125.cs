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
    public class tticol125
    {
        private static Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method;
        private static Recursos recursos = new Recursos();

        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> paramList = new Dictionary<string, object>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSentencia = string.Empty;
        String strSentencia1 = string.Empty;
        String strSentencia2 = string.Empty;
        String strSentencia3 = string.Empty;
        DataTable consulta = new DataTable();

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".tticol125" + env;

        public tticol125()
        {

        }

        public int insertarRegistro(ref List<Ent_tticol125> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            
            foreach(var item in parametros){
                if (item.paid == "")
                {
                    item.paid = " ";
                }
            }

            List<Ent_tticol080> Parametros080 = new List<Ent_tticol080>();
            Ent_tticol080 t080 = new Ent_tticol080();
            tticol080 dal080 = new tticol080();
            string strTagId = string.Empty;
            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);

                foreach (Ent_tticol125 reg in parametros)
                {
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

                    //Requerimiento No. 46122.
                    //Se quitó la inserción en la tabla ticol080
                    //CChaverra 28/07/2017
                    //if (Convert.ToInt32(retorno) > 0)
                    //{
                    //    t080 = new Ent_tticol080();
                    //    t080.orno = reg.pdno;
                    //    t080.pono = reg.pono;
                    //    t080.item = reg.item;
                    //    t080.cwar = reg.cwar;
                    //    t080.qune = reg.reqt * -1;
                    //    t080.logn = reg.user;
                    //    t080.proc = 2;
                    //    t080.refcntd = 0;
                    //    t080.refcntu = 0;
                    //    t080.clot = reg.clot;
                    //    Parametros080.Add(t080);
                    //    dal080.insertarRegistro(ref Parametros080, ref strError, ref strTagId);
                    //    Parametros080.Clear();

                    //    if (!string.IsNullOrEmpty(strError))
                    //        break;
                    //}

                }
            }

            catch (Exception ex)
            {
                strError += "Error when inserting data [025]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public int actualizarRegistro_Param(ref List<Ent_tticol125> parametros, ref string strError, string Aplicacion, bool updHist = false)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string table = owner + ".tticol125" + env;
            List<Ent_tticol080> Parametros080 = new List<Ent_tticol080>();
            Ent_tticol080 t080 = new Ent_tticol080();
            tticol080 dal080 = new tticol080();
            string strTagId = string.Empty;

            if (updHist)
                table = owner + ".tticol126" + env;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, table);
                string strCondicion = string.Empty;

                foreach (Ent_tticol125 reg in parametros)
                {
                    strCondicion = " WHERE ROWID = '" + reg.idrecord + "'";
                    parametrosIn = AdicionaParametrosComunesUpdate(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia + strCondicion, ref parametersOut, parametrosIn, false);

                    //Requerimiento No. 46122.
                    //Insertar en la tabla ticol080 - Comentado por nuevos requerimientos 
                    //CChaverra 28/07/2017 - 09/03/2017
                    //if (Convert.ToInt32(retorno) > 0)
                    //{
                    //    t080 = new Ent_tticol080();
                    //    t080.orno = reg.pdno;
                    //    t080.pono = reg.pono;
                    //    t080.item = reg.item;
                    //    t080.cwar = reg.cwar;
                    //    t080.qune = reg.reqt * -1;
                    //    t080.logn = reg.user;
                    //    t080.proc = 2;
                    //    t080.refcntd = 0;
                    //    t080.refcntu = 0;
                    //    t080.clot = reg.clot;
                    //    Parametros080.Add(t080);
                    //    dal080.insertarRegistro(ref Parametros080, ref strError, ref strTagId);
                    //    Parametros080.Clear();

                    //    if (!string.IsNullOrEmpty(strError))
                    //        break;
                    //}
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError += "Error updating data [125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public int listaRegistrosPendConfItem_Param(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            int retorno = 0;
            paramList = new Dictionary<string, object>();
            //paramList.Add("p1", ParametrosIn.item.Trim().ToUpperInvariant());
            paramList.Add("p1", ParametrosIn.pdno.Trim().ToUpperInvariant());
            paramList.Add("p2", ParametrosIn.pono.ToString().Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {

                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                retorno = Convert.ToInt32(consulta.Rows[0]["CANT"].ToString());
                if (retorno > 0) { strError = "ReturnPending to Confirm"; }
                return retorno;
            }
            catch (Exception ex)
            {
                strError = ex.InnerException.ToString();
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return retorno;

        }

        public double cantidadMaximaPorLote(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            double retorno = 0.0;

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.pdno.Trim().ToUpperInvariant());
            paramList.Add("p2", ParametrosIn.pono.ToString().Trim().ToUpperInvariant());
            paramList.Add("p3", ParametrosIn.item.Trim().ToUpperInvariant());
            paramList.Add("p4", ParametrosIn.clot.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {

                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                retorno = Convert.ToDouble(consulta.Rows[0]["CANTMAX"].ToString() == "" ? "0" : consulta.Rows[0]["CANTMAX"].ToString());
                //retorno = Convert.ToInt32(consulta.Rows[0]["cantMax"]);
                if (retorno < 1) { strError = "No returns for this lot"; }
                return retorno;
            }
            catch (Exception ex)
            {
                strError = ex.InnerException.ToString();
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return retorno;

        }

        private DateTime Cast(double retorno)
        {
            throw new NotImplementedException();
        }

        public DataTable consultaPorOrnoItem(ref string orno, ref string item, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", orno.Trim().ToUpper());
            paramList.Add(":T$ITEM", item.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Lot Code doesn´t exist for this item, please check"; }
                else
                {
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1);
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                    if (consulta.Rows.Count < 1) { strError = "Lot Code was not on Work Order. Cannot Continue"; }
                }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = ex.InnerException.ToString();
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable listaRegistrosporConfirmar_Param(ref Ent_tticol125 ParametrosIn, ref string strError, bool print = false)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.pdno.Trim().ToUpperInvariant());
            paramList.Add("p2", ParametrosIn.conf);
            paramList.Add("p3", ParametrosIn.paid.Trim().ToUpperInvariant());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                if (consulta.Rows.Count < 1) { strError = "Not found records for confirm"; }
            }

            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable vallidatePalletID(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.paid.Trim().ToUpperInvariant());
            string paid = ParametrosIn.paid.Trim().ToUpperInvariant();
            decimal quantity = ParametrosIn.reqt;
            string tableName = string.Empty;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "vallidatePalletID", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable vallidatePalletData(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.paid.Trim().ToUpperInvariant());
            string paid = ParametrosIn.paid.Trim().ToUpperInvariant();
          
            string tableName = string.Empty;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "vallidatePalletData", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
        public DataTable vallidatePalletIDMRB(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.paid.Trim().ToUpperInvariant());
            string paid = ParametrosIn.paid.Trim().ToUpperInvariant();
            decimal quantity = ParametrosIn.reqt;
            string tableName = string.Empty;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "vallidatePalletIDMRB", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
        public DataTable getReasonCodes(ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            string tableName = string.Empty;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "getReasonCodes", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable getCostCenters(ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            string tableName = string.Empty;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "getCostCenters", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
        public DataTable invGetPalletInfo(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.paid.Trim().ToUpperInvariant());
            string paid = ParametrosIn.paid.Trim().ToUpperInvariant();
            decimal quantity = ParametrosIn.reqt;
            decimal actualQuantity = 0;
            decimal status;
            string tableName = string.Empty;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "vallidatePalletInfo", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public bool updataPalletStatus(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.paid.Trim().ToUpperInvariant());
            paramList.Add("p2", 3);// , whcol131 -Delieverd status=9
            paramList.Add("p3", 7); // , ticol022, ticol042 -Delieverd status=11
            //string paid = ParametrosIn.paid.Trim().ToUpperInvariant();
            //int paidStatus = ParametrosIn.conf;
            strSentencia1 = recursos.readStatement(method.ReflectedType.Name, "updataPalletStatus022", ref owner, ref env, tabla, paramList);
            strSentencia2 = recursos.readStatement(method.ReflectedType.Name, "updataPalletStatus042", ref owner, ref env, tabla, paramList);
            strSentencia3 = recursos.readStatement(method.ReflectedType.Name, "updataPalletStatus131", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia1, ref parametersOut, null, true);
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia2, ref parametersOut, null, true);
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia3, ref parametersOut, null, true);
                //  strError = "Status updated successfully";
                return true;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                return false;
            }
        }

        public DataTable getReasonCode(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.paid.Trim().ToUpperInvariant());
            string paid = ParametrosIn.paid.Trim().ToUpperInvariant();
            //decimal paidStatus = parametrosIn.status;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "reasonCode", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }


        public DataTable getCostCenter(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.paid.Trim().ToUpperInvariant());
            string paid = ParametrosIn.paid.Trim().ToUpperInvariant();
            //decimal paidStatus = parametrosIn.status;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "costCenter", ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
        public DataTable listaRegistrosOrden_Param(ref Ent_tticol125 ParametrosIn, ref string strError, bool print = false)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.pdno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            if (print)
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1);
            }

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (print) { return consulta; }
                if (consulta.Rows.Count < 1)
                { strError = "-1"; }
                else
                {
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, "listaRegistrosOrden_ParamLast", ref owner, ref env, tabla, paramList);
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaRegistrosOrden_ParamHis(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.pdno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaRegistrosLoteItem_Param(ref Ent_tticol125 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.item.Trim().ToUpperInvariant());
            paramList.Add("p2", ParametrosIn.clot.Trim().ToUpperInvariant());
            paramList.Add("p3", ParametrosIn.pdno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Lot Code doesn´t exist for this item, please check"; }
                else
                {
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1);
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                    if (consulta.Rows.Count < 1) { strError = "Lot Code was not on Work Order. Cannot Continue"; }
                }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = ex.InnerException.ToString();
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable listaRegistrosLoteItem_Param(ref Ent_tticol125 ParametrosIn)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.item.Trim().ToUpperInvariant());
            paramList.Add("p2", ParametrosIn.clot.Trim().ToUpperInvariant());
            paramList.Add("p3", ParametrosIn.pdno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                return consulta;
            }
            catch (Exception ex)
            {
                //strError = ex.InnerException.ToString();
                log.escribirError(Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }
        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol125 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();

            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PDNO", DbType.String, parametros.pdno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PONO", DbType.Int32, parametros.pono);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, "         " + parametros.item.Trim());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REQT", DbType.Decimal, Convert.ToDecimal(parametros.reqt).ToString("#.##0,0000")); //DbType.Int32, parametros.reqt);
                if (!string.IsNullOrEmpty(parametros.user))
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$USER", DbType.String, parametros.user);
                if (parametros.prin > 0)
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PRIN", DbType.Int32, parametros.prin);

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CONF", DbType.Int32, parametros.conf);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.String, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.String, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PAID", DbType.String, parametros.paid);
                //if (!string.IsNullOrEmpty(parametros.idrecord))
                //    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":ROWID", DbType.String, parametros.idrecord);


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
                strError = "Error when creating parameters [125]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunesUpdate(Ent_tticol125 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();

            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PDNO", DbType.String, parametros.pdno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PONO", DbType.Int32, parametros.pono);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, parametros.item);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot);
                if (parametros.prin > 0)
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PRIN", DbType.Int32, parametros.prin);

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CONF", DbType.Int32, parametros.conf);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PAID", DbType.String, parametros.paid);
                //if (!string.IsNullOrEmpty(parametros.idrecord))
                //    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":ROWID", DbType.String, parametros.idrecord);


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
                strError = "Error when creating parameters [125]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return parameterCollection;
        }

        public DataTable ConsultarRegistroPalletID(ref Ent_tticol125 ParametrosIn, ref string strError, bool print = false)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.paid.Trim().ToUpperInvariant());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1)
                {
                    strError = "Not found records for this pallet id";
                }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol125]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable ConsultarQtdl(Ent_tticol125 obj, string strError)
        {
            DataTable retorno = new DataTable();
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("T$PDNO", obj.pdno.Trim());
            paramList.Add("T$PONO", obj.pono);
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttico025]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return retorno;
        }
    }
}
