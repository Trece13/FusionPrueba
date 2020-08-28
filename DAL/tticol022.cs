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
    public class tticol022
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
        private static string tabla = owner + ".tticol022" + env;
        private static string tabla222 = owner + ".tticol222" + env;
        /* Metodos */
        public tticol022()
        {
            parametrosIn.Clear();
        }

        public int insertarRegistro(ref List<Ent_tticol022> parametros, ref List<Ent_tticol020> parametros020, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                log.escribirError(strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                foreach (Ent_tticol022 reg in parametros)
                {
                    if (!invLabel_tiempoGrabacion(reg, ref strError))
                        break;

                    // Inicia la insercion del registro
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    if (retorno)
                    {
                        DAL.tticol020 dal020 = new tticol020();
                        int intRetorno = dal020.insertarRegistro(ref parametros020, ref strError);
                        Ent_tticol022 nreg = reg;
                        int retorno222 = InsertarRegistroTicol222(ref nreg, ref strError);
                    }

                }
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = "Error when inserting data [col022]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }
        //my nuevo metodo 
        public bool insertarRegistroAux(Ent_tticol022 Objtticol022, Ent_tticol020 Objtticol020)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            paramList = new Dictionary<string, object>();

            paramList.Add(":T$PDNO", Objtticol022.pdno.Trim().ToUpper().ToString());
            paramList.Add(":T$SQNB", Objtticol022.sqnb.ToUpper().ToString());
            paramList.Add(":T$PROC", Objtticol022.proc.ToString());
            paramList.Add(":T$LOGN", Objtticol022.logn.ToString());
            paramList.Add(":T$MITM", Objtticol022.mitm.ToString());
            paramList.Add(":T$QTDL", Objtticol022.qtdl.ToString());
            paramList.Add(":T$CUNI", Objtticol022.cuni.ToUpper().ToString());
            paramList.Add(":T$LOG1", Objtticol022.log1.ToUpper().ToString());
            paramList.Add(":T$QTD1", Objtticol022.qtd1.ToString());
            paramList.Add(":T$PRO1", Objtticol022.pro1.ToString());
            paramList.Add(":T$LOG2", Objtticol022.log2.ToUpper().ToString());
            paramList.Add(":T$QTD2", Objtticol022.qtd2.ToString());
            paramList.Add(":T$PRO2", Objtticol022.pro2.ToString());
            paramList.Add(":T$LOCA", Objtticol022.loca.ToUpper().ToString());
            paramList.Add(":T$NORP", Objtticol022.norp.ToString());
            paramList.Add(":T$DELE", Objtticol022.dele.ToString());
            paramList.Add(":T$LOGD", Objtticol022.logd.ToUpper().ToString());
            paramList.Add(":T$REFCNTD", "0");
            paramList.Add(":T$REFCNTU", "0");

            try
            {
                    string strError = string.Empty;
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    if (retorno)
                    {
                        DAL.tticol020 dal020 = new tticol020();
                        bool intRetorno = dal020.insertarRegistroAux(Objtticol020);
                        Ent_tticol022 nreg = Objtticol022;
                        int retorno222 = InsertarRegistroTicol222(ref nreg, ref strError);
                    }
                return retorno;
            }
            catch (Exception ex)
            {
                string strError = "Error when inserting data [col022]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return retorno;
        }

        public int insertarRegistroSimple(ref Ent_tticol022 parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);

                parametrosIn = AdicionaParametrosComunes(parametros);
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

        public int InsertarRegistroTicol222(ref Ent_tticol022 parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            paramList = new Dictionary<string, object>();

            paramList.Add(":T$PDNO", parametros.pdno.Trim().ToUpper());
            paramList.Add(":T$SQNB", parametros.sqnb.Trim().ToUpper());
            paramList.Add(":T$URPT", parametros.urpt.ToUpper());
            paramList.Add(":T$ACQT", parametros.acqt.ToString().Contains(",") == true ? parametros.acqt.ToString().Replace(",", ".") : parametros.acqt.ToString().Replace(".", ","));
            paramList.Add(":T$CWAF", parametros.cwaf.ToUpper().Trim());
            paramList.Add(":T$CWAT", parametros.cwat.ToUpper().Trim()==string.Empty?" ":parametros.cwat.ToUpper().Trim());
            paramList.Add(":T$ACLO", parametros.aclo.ToUpper());
            paramList.Add(":T$ALLO", parametros.allo.ToString().Contains(",") == true ? parametros.allo.ToString().Replace(",", ".") : parametros.allo.ToString().Replace(".", ","));
   
            try
            {
                strSentencia = recursos.readStatement("tticol222", method.Name, ref owner, ref env, tabla222, paramList);
                log.escribirError("Sentencia SQL: " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                parametrosIn = AdicionaParametrosComunes(parametros);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);

                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                log.escribirError(strError + Console.Out.NewLine + ex.Message + "Sentencia SQL: " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        public string WharehouseTisfc001(string PDNO, ref string strError)
        {
            consulta.Rows.Clear();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", PDNO.Trim().ToUpper());

            string resultado = string.Empty;

            strSentencia = recursos.readStatement("tticol222", method.Name, ref owner, ref env, tabla222, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1)
                {
                    strError = "Incorrect location, please verify.";
                }
                else
                {
                    resultado = consulta.Rows[0]["CWAR"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return resultado;
        }

        public int actualizarRegistro_Param(ref List<Ent_tticol022> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strCondicion = string.Empty;


            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                foreach (Ent_tticol022 reg in parametros)
                {
                    //strSentencia += " WHERE ROWID = '" + reg.idrecord + "'";
                    strSentencia += " WHERE T$SQNB = '" + reg.sqnb + "'";

                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    if (!string.IsNullOrEmpty(strError))
                        break;
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error updating data [022]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public int countRecordsByPdnoAndDele(ref string pdno, ref string dele, ref string strError)
        {
            consulta.Rows.Clear();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());
            paramList.Add(":T$DELE", dele.Trim().ToUpper());

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

            return Convert.ToInt32(consulta.Rows[0]["PDNOS"].ToString());
        }

        public string invLabel_generaSecuenciaOrden(ref Ent_tticol022 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            string strOrden = Parametros.pdno.Trim().ToUpperInvariant();
            string strSecuencia = strOrden + "-001";
            string strSentenciaS = string.Empty;

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", strOrden);

            strSentenciaS = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentenciaS, ref parametersOut, null, true);
                if (consulta.Rows.Count > 0) { strSecuencia = strOrden + "-" + consulta.Rows[0]["SECUENCIA"].ToString(); }
            }
            catch (Exception ex)
            {
                strError = "Error al buscar secuencia [tticol022]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return strSecuencia;

        }

        public bool actualizaRegistroAnuncioOrd(ref Ent_tticol022 data, ref string strError)
        {
            parametrosIn.Clear();
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$QTDL", Convert.ToInt32(data.qtdl));
                paramList.Add(":T$PDNO", data.pdno);
                paramList.Add(":T$SQNB", data.sqnb);


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

        public bool actualizaRegistroSugUbicaciones(ref Ent_tticol022 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$USER", data.log2.Trim());
                paramList.Add(":T$QTD2", data.qtd2);
                paramList.Add(":T$LOCA", data.loca.Trim());
                paramList.Add(":T$PDNO", data.pdno);
                paramList.Add(":T$SQNB", data.sqnb);
                paramList.Add(":T$DELE", data.dele);

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

        public bool actualizaRegistroConfirmReceipt(ref Ent_tticol022 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$LOG1", data.log1.Trim().ToUpper());
                paramList.Add(":T$QTD1", data.qtd1);
                paramList.Add(":T$PDNO", data.pdno);
                paramList.Add(":T$SQNB", data.sqnb);


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

        public bool invLabel_tiempoGrabacion(Ent_tticol022 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool blLibre = false;
            bool convert = false;
            decimal timeDelay = 0;
            string strSentenciaS = string.Empty;
            // Verificar la existencia de la clave para el tiempo de espera en el archivo config de la aplicación
            if (ConfigurationManager.AppSettings.AllKeys.Contains("timeOutRollSave")) // Existe
            {
                convert = decimal.TryParse(ConfigurationManager.AppSettings["timeOutRollSave"].ToString(), out timeDelay);
            }
            else { strError = "Time delay to roll save not found"; } // No existe

            if (convert && string.IsNullOrEmpty(strError))
            {

                paramList = new Dictionary<string, object>();
                paramList.Add("p1", Parametros.pdno.Trim().ToUpperInvariant());
                paramList.Add("p2", timeDelay.ToString());

                strSentenciaS = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentenciaS, ref parametersOut, null, true);
                if (consulta.Rows.Count > 0)
                {
                    if (consulta.Rows[0]["ORDEN"].ToString() == "0") { blLibre = true; }
                    else { strError = "Another Roll was announced " + timeDelay.ToString() + " minutes before."; }
                }

            }
            return blLibre;
        }

        public DataTable invLabel_registroImprimir_Param(ref Ent_tticol022 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            string strSentenciaS = string.Empty;
            string strOrden = Parametros.pdno.Trim().ToUpperInvariant();
            string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant(); ;

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", strOrden);
            paramList.Add("p2", strSecuencia);

            strSentenciaS = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentenciaS, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error to search information to print [tticol022]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable wrapValidation_listaRegistroSec_param(ref Ent_tticol022 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            string strSecuencia = Parametros.sqnb.Trim().ToUpperInvariant(); ;

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", strSecuencia);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Pallet doesn’t exist"; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable validarRegistroByPalletId(ref string palletID, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", palletID.Trim());

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

        public DataTable findBySqnbPdnoAndQtdl(ref string pdno, ref string sqnb, ref string qtdl, ref string strError)
        {
            consulta.Rows.Clear();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());
            paramList.Add(":T$SQNB", sqnb.Trim().ToUpper());
            paramList.Add(":T$QTDL", qtdl.Trim());

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

        public DataTable selectMaxSqnbByPdno(ref string pdno, ref string qtdlzero, ref string strError)
        {
            consulta.Rows.Clear();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);


            if (qtdlzero == "true")
            {
                strSentencia += " AND T$QTDL = 0";
            }
            else if (qtdlzero == "false")
            {
                strSentencia += " AND T$QTDL <> 0";
            }

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable selectDatesBySqnbPdno(ref string pdno, ref string sqnb, ref string strError)
        {
            consulta.Rows.Clear();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());
            paramList.Add(":T$SQNB", sqnb.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable consultambpl(ref string strError)
        {
            consulta.Rows.Clear();
            parametrosIn.Clear();
            method = MethodBase.GetCurrentMethod();

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable consultambrl(ref string strError)
        {
            consulta.Rows.Clear();
            parametrosIn.Clear();
            method = MethodBase.GetCurrentMethod();

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search parameter time [tticol000]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable findBySqnbPdnoLabelPallet(ref string pdno, ref string sqnb, ref string strError)
        {
            consulta.Rows.Clear();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());
            paramList.Add(":T$SQNB", sqnb.Trim().ToUpper());

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

        public DataTable findRecordBySqnbRejectedPlant(ref string sqnb, ref string strError)
        {
            consulta.Rows.Clear();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", sqnb.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }
		
        public DataTable findRecordBySqnbRejectedPlantMRBRejection(ref string sqnb, ref string strError)
        {
            consulta.Rows.Clear();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", sqnb.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }																								
        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol022 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PDNO", DbType.String, parametros.pdno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SQNB", DbType.String, parametros.sqnb);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.proc);
                //if (!string.IsNullOrEmpty(parametros.logn.Trim()))
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);

                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATE", DbType.String, parametros.date);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MITM", DbType.String, "         " + parametros.mitm);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTDL", DbType.Decimal, Convert.ToDecimal(parametros.qtdl).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUNI", DbType.String, parametros.cuni);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOG1", DbType.String, parametros.log1);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATC", DbType.String, parametros.datc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTD1", DbType.Int32, parametros.qtd1);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PRO1", DbType.Int32, parametros.pro1);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOG2", DbType.String, parametros.log2);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATU", DbType.String, parametros.datu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTD2", DbType.Int32, parametros.qtd2);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PRO2", DbType.Int32, parametros.pro2);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.loca);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$NORP", DbType.Int32, parametros.norp);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DLRP", DbType.String, parametros.dlrp);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DELE", DbType.Int32, parametros.dele);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGD", DbType.String, parametros.logd);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATD", DbType.String, parametros.datd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);

                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$URPT", DbType.String, parametros.refcntu);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ACQT", DbType.Double, parametros.refcntu);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAF", DbType.String, parametros.refcntu);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAT", DbType.String, parametros.refcntu);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ACLO", DbType.String, parametros.refcntu);

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
                strError = "Error when creating parameters [022]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }

        public DataTable validateTimeSaveRecord(ref string pdno, ref int tiempo, ref string strError)
        {
            consulta.Rows.Clear();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());
            //paramList.Add(":T$TIME", ConfigurationManager.AppSettings["calcInvLabel"].Trim().ToUpper());
            paramList.Add(":T$TIME", tiempo);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count > 0) { strError = "Debe Esperar"; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public bool ActualizarRegistroTicol222(string USER, string PDNO, string SQNB)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$URPT", USER.ToUpper());
                paramList.Add(":T$PDNO", PDNO.ToUpper());
                paramList.Add(":T$SQNB", SQNB.ToUpper());

                strSentencia = recursos.readStatement("tticol222", method.Name, ref owner, ref env, tabla222, paramList);

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

        public bool ActualizarUbicacionTicol222(string PDNO, string SQNB, string ACLO, string CWAR)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$PDNO", PDNO.ToUpper());
                paramList.Add(":T$SQNB", SQNB.ToUpper());
                paramList.Add(":T$ACLO", ACLO.ToUpper().Trim());
                paramList.Add(":T$CWAT", CWAR.ToUpper().Trim());

                strSentencia = recursos.readStatement("tticol022", method.Name, ref owner, ref env, tabla222, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);

                return retorno;
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return retorno;
        }
        
        public bool ActualizarCantidadRegistroTicol222(decimal ACQT, string PDNO)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$ACQT", ACQT.ToString().Contains(",") == true ? ACQT.ToString().Replace(",", ".") : ACQT.ToString().Replace(".", ","));
                paramList.Add(":T$SQNB", PDNO.ToUpper());


                strSentencia = recursos.readStatement("tticol222", method.Name, ref owner, ref env, tabla222, paramList);

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

        public bool ActualizarNorpTicol022(Ent_tticol022 Obj_tticol022)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            bool retorno = false;
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$NORP", Obj_tticol022.norp);
                paramList.Add(":T$SQNB", Obj_tticol022.sqnb);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla222, paramList);
                DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return retorno;
        }

        public DataTable ConsultarPorPalletID(ref string PDNO, ref string strError)

        {

            consulta.Rows.Clear();

            method = MethodBase.GetCurrentMethod();

 

            paramList = new Dictionary<string, object>();

            paramList.Add("p1", PDNO.ToUpper());

 

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

 

            try

            {

                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                if (consulta.Rows.Count < 1) { strError = "Pallet Id not Confirmed."; }

            }

            catch (Exception ex)

            {

                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n ";

                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);

            }

            return consulta;

        }

 

        public DataTable ActualizacionPalletId(string PAID,string STAT, string strError)

        {

            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();

            paramList.Add("p1", PAID.Trim().ToUpper());
            paramList.Add(":STAT", STAT.Trim());

 

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try

            {

                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

            }

            catch (Exception ex)

            {

                strError = "Error to update status (to delete) [tticol022]. Try again or contact your administrator \n ";

                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);

                Console.WriteLine(ex);

            }

            return consulta;

        }


        public bool ActualizarCantidadAlmacenRegistroTicol222(string _operator, decimal ACQT, string ACLO ,string CWAR, string PAID)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$CWAR", CWAR.ToUpper().Trim());
                paramList.Add(":T$ACQT", ACQT.ToString().Contains(",") == true ? ACQT.ToString().Replace(",", ".") : ACQT.ToString().Replace(".", ","));
                paramList.Add(":T$ACLO", ACLO.ToUpper().Trim());               
                paramList.Add(":T$SQNB", PAID.ToUpper());


                strSentencia = recursos.readStatement("tticol222", method.Name, ref owner, ref env, tabla222, paramList);

                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }

            return retorno;
        }

        public DataTable SecuenciaMayor022(string id)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = "";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", id);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Not item's found for Sequence "; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable SecuenciaMayor022RT(string id)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = "";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", id);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Not item's found for Sequence "; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol022]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

    }
    
}
