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

namespace whusa.Interfases
{
    public class twhcol130
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
        private static string tabla = owner + ".twhcol130" + env;

        Ent_twhcol130 whcol130 = new Ent_twhcol130();

        public twhcol130()
        {
            parametrosIn.Clear();
        }

        public int insertarRegistro(ref Ent_twhcol124 parametro, ref string strError)
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

        public bool updateLocation(ref string loca, ref string paid, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PAID", paid.Trim().ToUpper());
                paramList.Add(":T$LOCA", loca.Trim().ToUpper());

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

        public DataTable validateExistsPalletId(ref string palletId, ref string uniqueId, ref string bodegaori, ref string bodegades, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", palletId.Trim());
            //paramList.Add(":T$UNID", uniqueId.Trim());
            paramList.Add(":T$WHSO", bodegaori.Trim());
            paramList.Add(":T$WHTA", bodegades.Trim());

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
        public DataTable vallidatePurchaseOrder(ref string returnorder, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", returnorder.Trim().ToUpperInvariant());
           // string returnorder = returnorder.Trim().ToUpperInvariant();
           
            string tableName = string.Empty;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "vallidatePurchaseOrder", ref owner, ref env, tabla, paramList);

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
        public DataTable vallidatePurchaseOrderWithPosition(ref string returnorder, ref string position, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", returnorder.Trim().ToUpperInvariant());
            paramList.Add("p2", position.Trim().ToUpperInvariant());
            // string returnorder = returnorder.Trim().ToUpperInvariant();

            string tableName = string.Empty;
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "vallidatePurchaseOrderWithPosition", ref owner, ref env, tabla, paramList);

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
        public DataTable validarRegistroByUniqueId(ref string uniqueId, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$UNID", uniqueId.Trim());

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

        public DataTable validarPalletsRecibidosByUniqueId(ref string uniqueId, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$UNID", uniqueId.Trim());

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

        public DataTable findByOrderNumberSugUbicacionConOrigen(ref string pdno, ref string oorg, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$OORG", oorg.Trim());
            paramList.Add(":T$PDNO", pdno.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [twhcol124]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable findBySqnbPdnoNoClot(ref string loca, ref string cwar, ref string pdno, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$LOCA", loca.Trim().ToUpper());
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttisfc001]. Try again or contact your administrator";
                throw ex;
            }
        }

        public DataTable findBySqnbPdnoYesClot(ref string loca, ref string cwar, ref string pdno, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$LOCA", loca.Trim().ToUpper());
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttisfc001]. Try again or contact your administrator";
                throw ex;
            }
        }

        public DataTable validateLocationByPaid(ref string paid, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", paid.Trim().ToUpper());

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

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_twhcol124 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$UNID", DbType.String, parametros.unid.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PAID", DbType.String, parametros.paid.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, parametros.item.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTDT", DbType.Double, parametros.qtdt);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.proc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MES1", DbType.String, parametros.mes1);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.loca.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCS", DbType.Int32, parametros.locs);

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

        #region Method´s

        public DataTable PaidMayorwhcol130(string ORNO)
        {
            DataTable retorno = new DataTable();

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);


            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return retorno;
        }
        public bool UpdateStatusPicked(Ent_twhcol130131 MyObj)
        {
            bool retorno1 = false;

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", MyObj.PAID);
            paramList.Add(":T$STAT", 2);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);


            try
            {
                //retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, true) .ToString();
                retorno1 = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                //DataTable Existencia = ConsultarPorPalletIDReimpresion(MyObj.PAID);
                //if (Existencia.Rows.Count > 0)
                //{
                //    retorno = "true";
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "String enviado :");
            }

            return retorno1;
        }
        public bool InsertarReseiptRawMaterial(Ent_twhcol130131 MyObj)
        {
            bool retorno = false;

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$OORG", MyObj.OORG);
            paramList.Add(":T$ORNO", MyObj.ORNO);
            paramList.Add(":T$ITEM", MyObj.ITEM);
            paramList.Add(":T$PAID", MyObj.PAID);
            paramList.Add(":T$PONO", MyObj.PONO);
            paramList.Add(":T$SEQN", MyObj.SEQN);
            paramList.Add(":T$CLOT", MyObj.CLOT == "" ? " " : MyObj.CLOT);
            paramList.Add(":T$CWAR", MyObj.CWAR);
            paramList.Add(":T$QTYS", MyObj.QTYS);
            paramList.Add(":T$UNIT", MyObj.UNIT);
            paramList.Add(":T$QTYC", MyObj.QTYC);
            paramList.Add(":T$UNIC", MyObj.UNIC);
            //paramList.Add(":T$DATE", MyObj.DATE);
            paramList.Add(":T$CONF", MyObj.CONF);
            paramList.Add(":T$RCNO", MyObj.RCNO);
            //paramList.Add(":T$DATR", MyObj.DATR);
            paramList.Add(":T$LOCA", MyObj.LOCA==string.Empty?" ":MyObj.LOCA);
            //paramList.Add(":T$DATL", MyObj.DATL);
            paramList.Add(":T$PRNT", MyObj.PRNT);
            //paramList.Add(":T$DATP", MyObj.DATP);
            paramList.Add(":T$LOGN", MyObj.LOGN);
            paramList.Add(":T$LOGT", MyObj.LOGT);
            paramList.Add(":T$STAT", MyObj.STAT);
            paramList.Add(":T$NPRT", MyObj.NPRT);
            paramList.Add(":T$FIRE", MyObj.FIRE);
            paramList.Add(":T$PSNO", MyObj.PSLIP == string.Empty ? " " : MyObj.PSLIP);
            paramList.Add(":T$ALLO", MyObj.ALLO == string.Empty ? "0" : MyObj.ALLO);
            
            


            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);


            try
            {
                //retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, true) .ToString();
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                //DataTable Existencia = ConsultarPorPalletIDReimpresion(MyObj.PAID);
                //if (Existencia.Rows.Count > 0)
                //{
                //    retorno = "true";
                //}
            }
            catch (Exception ex)
            {
                log.escribirError("My Query"+strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);

            }

            return retorno;
        }

        public bool InsertarReseiptRawMaterial131(Ent_twhcol130131 MyObj)
        {
            bool retorno = false;

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$OORG", MyObj.OORG);
            paramList.Add(":T$ORNO", MyObj.ORNO);
            paramList.Add(":T$ITEM", MyObj.ITEM);
            paramList.Add(":T$PAID", MyObj.PAID);
            paramList.Add(":T$PONO", MyObj.PONO);
            paramList.Add(":T$SEQN", MyObj.SEQN);
            paramList.Add(":T$CLOT", MyObj.CLOT == "" ? " " : MyObj.CLOT);
            paramList.Add(":T$CWAR", MyObj.CWAR);
            paramList.Add(":T$QTYS", MyObj.QTYS);
            paramList.Add(":T$UNIT", MyObj.UNIT);
            paramList.Add(":T$QTYC", MyObj.QTYC);
            paramList.Add(":T$UNIC", MyObj.UNIC);
            //paramList.Add(":T$DATE", MyObj.DATE);
            paramList.Add(":T$CONF", MyObj.CONF);
            paramList.Add(":T$RCNO", MyObj.RCNO);
            //paramList.Add(":T$DATR", MyObj.DATR);
            paramList.Add(":T$LOCA", MyObj.LOCA == string.Empty ? " " : MyObj.LOCA);
            //paramList.Add(":T$DATL", MyObj.DATL);
            paramList.Add(":T$PRNT", MyObj.PRNT);
            //paramList.Add(":T$DATP", MyObj.DATP);
            paramList.Add(":T$LOGN", MyObj.LOGN);
            paramList.Add(":T$LOGT", MyObj.LOGT);
            paramList.Add(":T$STAT", MyObj.STAT);
            paramList.Add(":T$NPRT", MyObj.NPRT);
            paramList.Add(":T$DATE", MyObj.DATE);
            paramList.Add(":T$DATR", MyObj.DATR);
            paramList.Add(":T$DATL", MyObj.DATL);
            paramList.Add(":T$DATP", MyObj.DATP);
            paramList.Add(":T$FIRE", MyObj.FIRE.Trim()== string.Empty ? "0" : MyObj.FIRE.Trim());
            paramList.Add(":T$PSNO", MyObj.PSLIP.Trim() == string.Empty ? "0" : MyObj.PSLIP.Trim());
            paramList.Add(":T$ALLO", MyObj.ALLO.Trim() == string.Empty ? "0" : MyObj.PSLIP.Trim());


            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);


            try
            {
                //retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, true) .ToString();
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                //DataTable Existencia = ConsultarPorPalletIDReimpresion(MyObj.PAID);
                //if (Existencia.Rows.Count > 0)
                //{
                //    retorno = "true";
                //}
            }
            catch (Exception ex)
            {
                log.escribirError("My Query" + strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);

            }

            return retorno;
        }
        public DataTable ValidarOrderID(Ent_twhcol130 whcol130)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", whcol130.ORNO.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                //if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return consulta;
        }

        public DataTable ValidarItem(Ent_twhcol130 whcol130)
            {
                method = MethodBase.GetCurrentMethod();
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$ORNO", whcol130.ORNO.Trim());
                paramList.Add(":T$ITEM", whcol130.ITEM.Trim());
                
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                try
                {
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                return consulta;
            }

        public DataTable ValidarLote(Ent_twhcol130 whcol130)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", whcol130.ITEM.Trim());
            paramList.Add(":T$CLOT", whcol130.CLOT.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ValidarUnidad(Ent_twhcol130 whcol130)
        {
            return new DataTable();
        }

        public DataTable ListaSalesOrderReturn()
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ListaTransferOrder()
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ListaPurchaseOrders()
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return consulta;
        }
        #endregion

        public DataTable FactorConvercionDiv(string ITEM, string STUN, string CUNI)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ITEM.Trim());
            paramList.Add(":T$STUN", STUN.Trim());
            paramList.Add(":T$CUNI", CUNI.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to select factor[ttcibd003]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable FactorConvercionMul(string ITEM, string STUN, string CUNI)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ITEM.Trim());
            paramList.Add(":T$STUN", STUN.Trim());
            paramList.Add(":T$CUNI", CUNI.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to select factor [ttcibd003]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultaNOOrdencompra(string ORNO, string PONO, string OORG, decimal CANT, string ITEM, string CLOT)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO.Trim());
            paramList.Add(":T$PONO", PONO.Trim());
            paramList.Add(":T$OORG", OORG.Trim());
            paramList.Add(":T$CANT", CANT);
            paramList.Add(":T$ITEM", ITEM.Trim());
            paramList.Add(":T$CLOT", CLOT == "" ? " " : CLOT);
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to select  sales orders. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultaOrdencompra(string ORNO, string PONO, decimal CANT, string ITEM, string CLOT)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO.ToUpper().Trim());
            paramList.Add(":T$PONO", PONO.Trim());
            paramList.Add(":T$CANT", CANT.ToString().Replace(",", "."));
            paramList.Add(":T$ITEM", ITEM.Trim());
            paramList.Add(":T$CLOT", CLOT == "" ? " " : CLOT);
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to select purchase orders. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }


        public DataTable Insertar130(string ORNO, string PONO, decimal CANT, string ITEM, string CLOT)
        {

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO.Trim());
            paramList.Add(":T$PONO", PONO.Trim());
            paramList.Add(":T$CANT", CANT);
            paramList.Add(":T$ITEM", ITEM.Trim());
            paramList.Add(":T$CLOT", CLOT);
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to insert [twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }



        public DataTable ConsultaUnidadesMedida()
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to update reprint count [twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultafactoresporItem(string ITEM)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ITEM.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to search factor for item [ttcibd003]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }


        public DataTable ConsultarPorPalletIDReimpresion(string PAID)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to search pallet ti reprint count [twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultarPorPalletIDReimpresion131(string PAID)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to update reprint count [twhcol131]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultarPorPalletIDReimpresionLogp(string PAID, string USER)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            paramList.Add(":T$LOGP", USER.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to update logp reprint [twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ActualizarConteoReimpresion(string PAID,string LOGR)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            paramList.Add(":T$LOGR", LOGR.Trim());
            
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to update reprint count [twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ActualizarConteoReimpresion131(string PAID, string LOGR)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            paramList.Add(":T$LOGR", LOGR.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to update [twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable Consultarttccol307(string PAID, string USRR)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID",PAID.Trim() /*PAID.Trim()*/);
            paramList.Add(":T$USER", USRR.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to consult [tccol307]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }


        public bool Insertarttccol307(Ent_ttccol307 tccol307)
        {
            bool respuesta = true;

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", tccol307.PAID.Trim());
            paramList.Add(":T$USER", tccol307.USRR.Trim());
            paramList.Add(":T$REFCNTD", tccol307.REFCNTD);
            paramList.Add(":T$REFCNTU", tccol307.REFCNTU);
            paramList.Add(":T$STAT", tccol307.STAT.Trim());
            paramList.Add(":T$PROC", tccol307.PROC.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                respuesta = false;
                strError = "Error to insert in [tccol307]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return respuesta;
        }

    
        public DataTable ConsultarLocation(string CWAR, string LOCA)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", CWAR.Trim().ToUpper());
            paramList.Add(":T$LOCA", LOCA.Trim().ToUpper());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to search locate [twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        //public DataTable ActualizacionLocaWhcol130(string PAID, string CWAR, string LOCA)
        //{
        //    method = MethodBase.GetCurrentMethod();
        //    paramList = new Dictionary<string, object>();
        //    paramList.Add(":T$PAID", PAID.Trim());
        //    paramList.Add(":T$CWAR", CWAR.Trim());
        //    paramList.Add(":T$LOCA", LOCA.Trim());
        //    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
        //    try
        //    {
        //        consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    return consulta;
        //}
        //public bool Actualizartwhcol130(EntidadMaligna)
        //{
        //    bool respuesta = true;

        //    method = MethodBase.GetCurrentMethod();
        //    paramList = new Dictionary<string, object>();
        //    paramList.Add(":T$PAID", tccol307.PAID.Trim());
        //    paramList.Add(":T$USRR", tccol307.USRR.Trim());
        //    paramList.Add(":T$REFCNTD", tccol307.REFCNTD);
        //    paramList.Add(":T$REFCNTU", tccol307.REFCNTU);
        //    paramList.Add(":T$STAT", tccol307.STAT.Trim());

        //    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

        //    try
        //    {
        //        consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta = false;
        //    }
        //    return consulta;
        //}

        public DataTable ActualizacionPickMaterialWhcol130(string PAID,string PICK, string DATK, string LOGP, string STAT)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            paramList.Add(":T$PICK", PICK.Trim());
            paramList.Add(":T$DATK", DATK.Trim());
            paramList.Add(":T$LOGP", LOGP.Trim());
            paramList.Add(":T$STAT", STAT.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to update PickMaterial[twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public bool ActualizacionLocaWhcol130(string PAID, string LOCA,string CWAA, string LOGT, string STAT)
        {
            bool retorno = false;

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            paramList.Add(":T$LOAA", LOCA.Trim() == string.Empty ? " " : LOCA.Trim());
            paramList.Add(":T$CWAA", CWAA.Trim());
            paramList.Add(":T$LOGT", LOGT.Trim());
            paramList.Add(":T$STAT", STAT.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                retorno = true;
            }
            catch (Exception ex)
            {
                strError = "Error to update [twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return retorno;
        }

        public bool EliminarRegistrotccol307(string PAID, string USER)
        {
            bool retorno = false;

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                DataTable DtTccol307 = Consultarttccol307(PAID, USER);
                if (DtTccol307.Rows.Count == 0)
                {
                    retorno = true;
                }
                
            }
            catch (Exception ex)
            {
                strError = "Error to delete in [ttccol307]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return retorno;
        }

        public DataTable ConsultarPrioridadNativa(string CWAR)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", CWAR.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to select prior in [ttccol310]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultarLocationNativa(string CWAR,string PRIO)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", CWAR.Trim());
            paramList.Add(":T$PRIO", PRIO.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
               
                strError = "Error to select location in [ttccol310]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public string ConsultarSumatoriaCantidades130(string ORNO,string PONO,string SEQNR)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO.Trim());
            paramList.Add(":T$PONO", PONO.Trim());
            paramList.Add(":T$SEQNR", SEQNR.Trim());
            string retorno = string.Empty;

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                DataTable Dtresult = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                retorno = Dtresult.Rows[0]["SUMQTYC"].ToString();
            }
            catch (Exception ex)
            {

                strError = "Error to search quantity in [ttccol310]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return retorno;
        }

        public string ConsultarSumatoriaCantidades130NOOC(string ORNO, string PONO)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO.Trim());
            paramList.Add(":T$PONO", PONO.Trim());
            string retorno = string.Empty;

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                DataTable Dtresult = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                retorno = Dtresult.Rows[0]["SUMQTYC"].ToString();
            }
            catch (Exception ex)
            {
                strError = "Error to search quantity in [ttccol310]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return retorno;
        }

        public DataTable ConsultaOrdenImportacion(string COTP)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$COTP", COTP.Trim());
            DataTable consulta = new DataTable();
 

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to search order in [ttccol310]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultaPresupuestoImportacion(string ORNO)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$OCIS$C", ORNO.Trim());
            DataTable consulta = new DataTable();

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to search Import estimate in [ttccol310]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public string strError { get; set; }

        public bool Eliminartccol130(Ent_twhcol130131 MyObj)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", MyObj.PAID.Trim().ToUpper());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

            }
            catch (Exception ex)
            {
                strError = "Error to delete [twhcol130]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return retorno;
        }

        public bool Insertartwhcol131(Ent_twhcol130131 MyObj)
        {
            bool retorno = false;

            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$OORG", MyObj.OORG);
            paramList.Add(":T$ORNO", MyObj.ORNO);
            paramList.Add(":T$ITEM", MyObj.ITEM);
            paramList.Add(":T$PAID", MyObj.PAID);
            paramList.Add(":T$PONO", MyObj.PONO);
            paramList.Add(":T$SEQN", MyObj.SEQN);
            paramList.Add(":T$CLOT", MyObj.CLOT == "" ? " " : MyObj.CLOT);
            paramList.Add(":T$CWAR", MyObj.CWAR);
            paramList.Add(":T$QTYS", MyObj.QTYS);
            paramList.Add(":T$UNIT", MyObj.UNIT);
            paramList.Add(":T$QTYC", MyObj.QTYC);
            paramList.Add(":T$UNIC", MyObj.UNIC);
            paramList.Add(":T$CONF", MyObj.CONF);
            paramList.Add(":T$RCNO", MyObj.RCNO);
            paramList.Add(":T$LOCA", MyObj.LOCA == string.Empty ? " " : MyObj.LOCA);
            paramList.Add(":T$PRNT", MyObj.PRNT);
            paramList.Add(":T$LOGN", MyObj.LOGN);
            paramList.Add(":T$LOGT", MyObj.LOGT);
            paramList.Add(":T$STAT", MyObj.STAT);
            paramList.Add(":T$NPRT", MyObj.NPRT);
            paramList.Add(":T$FIRE", MyObj.FIRE);
            paramList.Add(":T$PSNO", MyObj.PSLIP);
            paramList.Add(":T$CWAA", MyObj.CWAR.Trim());
            paramList.Add(":T$LOAA", MyObj.LOCA.Trim() == string.Empty ? " " : MyObj.LOCA.Trim());
            paramList.Add(":T$QTYA", MyObj.QTYS == string.Empty ? "0" : MyObj.QTYS.Trim());
            paramList.Add(":T$ALLO", MyObj.QTYS == string.Empty ? "0" : MyObj.QTYS.Trim());


            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);


            try
            {
                //retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, true) .ToString();
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                //DataTable Existencia = ConsultarPorPalletIDReimpresion(MyObj.PAID);
                //if (Existencia.Rows.Count > 0)
                //{
                //    retorno = "true";
                //}
            }
            catch (Exception ex)
            {
                log.escribirError("My Query" + strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);

            }

            return retorno;
        }


        public DataTable ListaPurchaseOrder(string ORNO)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO.Trim().ToUpper());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ListaTransferOrders(string ORNO)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO.Trim().ToUpper());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ListaSalesOrdersReturn(string ORNO)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO.Trim().ToUpper());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable VerificarPalletID(string PAID)
        {
            DataTable Retorno = new DataTable();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":PAID", PAID.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
            }

            return Retorno;
        }

        public DataTable VerificarPalletIDz(string PAID)
        {
            DataTable Retorno = new DataTable();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":PAID", PAID.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return Retorno;
        }
        public DataTable VerificarZoneCode(string ZONE)
        {
            DataTable Retorno = new DataTable();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":ZONE", ZONE.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
            }

            return Retorno;
        }

        public DataTable maximaSecuencia(string ORNO)
        {
            DataTable Retorno = new DataTable();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", ORNO.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
            }

            return Retorno;
        }
    }

    

}
