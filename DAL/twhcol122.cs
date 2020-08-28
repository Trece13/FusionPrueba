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
    public class twhcol122
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
        private static string tabla = owner + ".twhcol122" + env;
        private static string tabla307 = owner + ".ttccol307" + env;
        public int flag22 = 0;

        public twhcol122()
        {
            //Constructor
        }

        public bool insertarRegistro(ref Ent_twhcol122 parametro, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$UNID", parametro.unid.Trim());
            paramList.Add(":T$LOGN", parametro.logn.Trim());
            paramList.Add(":T$LOSO", parametro.loso);
            paramList.Add(":T$PAID", parametro.paid);
            paramList.Add(":T$ITEM", parametro.item);
            paramList.Add(":T$CLOT", parametro.clot);
            paramList.Add(":T$QTDT", parametro.qtdt);
            paramList.Add(":T$PROC", parametro.proc);
            paramList.Add(":T$MES1", parametro.mes1);
            paramList.Add(":T$REFCNTD", parametro.refcntd);
            paramList.Add(":T$REFCNTU", parametro.refcntu);           
            

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla,paramList);
                log.escribirError("My sql: " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);                
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
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

        public DataTable validarRegistroByPalletId(ref string palletId, ref string uniqueID, ref string bodegaori, ref string bodegades, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", palletId.Trim());
            paramList.Add(":T$UNID", uniqueID.Trim());
            paramList.Add(":T$WHSO", bodegaori.Trim());
            paramList.Add(":T$WHTA", bodegades.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                log.escribirError(strError + Console.Out.NewLine + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [twhwmd300]. Try again or contact your administrator \n ";
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

        public DataTable buscarbodegasuid(ref string uniqueId, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$UNID", uniqueId.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Unique Id doesn't exist, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [twhcol120]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_twhcol122 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$USER", DbType.String, parametros.unid.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$STAT", DbType.String, parametros.logn.ToUpper());
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOSO", DbType.String, parametros.loso.ToUpper());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PAID", DbType.String, parametros.paid.ToUpper());
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, parametros.item.ToUpper());
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot.ToUpper());
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTDT", DbType.Double, parametros.qtdt);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.proc);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MES1", DbType.String, parametros.mes1);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
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
        //inicio--- 
        public DataTable ConsultarPalletPicking22(string PAID, string USER)
        {
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            paramList.Add(":T$LOGP", USER.Trim());
            string tabla = ".tticol222";
            string name1 = "ConsultarPalletPicking22140";
            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                flag22 = 1;
            }
            catch (Exception ex)
            {
                strError = "Error finding table [tticol222140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultarPalletPicking042(string PAID, string USER)
        {
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            paramList.Add(":T$LOGP", USER.Trim());
            string tabla = ".tticol042";
            string name1 = "ConsultarPalletPicking042";
            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [tticol042140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultarPalletPicking131(string PAID, string USER)
        {
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim());
            paramList.Add(":T$LOGP", USER.Trim());
            string tabla = ".twhcol131";
            string name1 = "ConsultarPalletPicking131";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [twhcol131140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }
        public string strError { get; set; }

        //insert picking ok


        public int IngRegistrott307140(string USER, int PDNO, string SQNB, int REFCNTD, int REFCNTU)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string metodo2 = "tticol082";
            string tabla = ".ttccol307";
            string name1 = "InserPickl307140";

            string strError = string.Empty;

            Ent_ttccol307 Objtticol307140 = new Ent_ttccol307();
            Objtticol307140.USRR = USER.Trim();
            Objtticol307140.STAT = PDNO.ToString();
            Objtticol307140.PAID = SQNB.Trim();
            Objtticol307140.REFCNTD = REFCNTD;
            Objtticol307140.REFCNTU = REFCNTU;
            List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$USER", Objtticol307140.USRR);
            paramList.Add(":T$STAT", Objtticol307140.STAT);
            paramList.Add(":T$PAID", Objtticol307140.PAID);
            paramList.Add(":T$REFCNTD", Objtticol307140.REFCNTD);
            paramList.Add(":T$REFCNTU", Objtticol307140.REFCNTU);

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {

                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [ttccol301]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);
        }

        public DataTable ConsultarTt307140(Ent_ttccol307 Objttccol307)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();

            string strError = string.Empty;

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$USER", Objttccol307.USRR);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [ttccol301]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return retorno;
        }
        //fin picking ok

        public int actRegtticol022140(string SQNB)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", SQNB.Trim());
            string tabla = ".tticol022";
            string name1 = "UpdateTbl022";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [tticol022140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);


        }


        public int actRegtticol042140(string SQNB)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", SQNB.Trim());
            string tabla = ".UpdateTbl042";
            string name1 = "UpdateTbl042";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [UpdateTbl042]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);

        }



        public int actRegtticol131140(string SQNB)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", SQNB.Trim());
            string tabla = ".UpdateTbl131";
            string name1 = "UpdateTbl131";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [UpdateTbl131]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);

        }


        public int actRegtticol082140(string user, string pallet, string Location, int stat, string t, string OORG, string ORNO, string OSET, string PONO, string SQNB, string ADVS)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$LOGN", user);
            paramList.Add(":T$STAT", stat);
            paramList.Add(":T$LOCA", Location.Trim() == string.Empty ? " " : Location.Trim());
            paramList.Add(":T$PAID", pallet);
            paramList.Add(":T$PICK", t);
            paramList.Add(":T$OORG", OORG.Trim());
            paramList.Add(":T$ORNO", ORNO.Trim());
            paramList.Add(":T$OSET", OSET.Trim());
            paramList.Add(":T$PONO", PONO.Trim());
            paramList.Add(":T$SQNB", SQNB.Trim());
            paramList.Add(":T$ADVS", ADVS.Trim());

            string tabla = ".tticol082";
            string name1 = "UpdateTbl082";


            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                bool Retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                log.escribirError("ejecucion:" + Retorno + " " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                //}
                //catch (Exception ex)
                //{
                log.escribirError("ejecucion:" + Retorno + " " + strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [tticol082140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);


        }

        public int InsertRegCausalCOL084(string pallet, string user, int statCausal)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", pallet.Trim());
            paramList.Add(":T$LOGN", user.Trim());
            paramList.Add(":T$STAF", statCausal);


            string tabla = ".InsertCausalCOL084";
            string name1 = "InsertCausalCOL084";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [tticol084140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);

        }


        public int ingRegTticol092140(string maximo, string pallet, string txtpallet, int causal, string _operator)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PICK", maximo);
            paramList.Add(":T$PAIO", pallet.Trim());
            paramList.Add(":T$PAIN", txtpallet.Trim());
            paramList.Add(":T$CDIS", causal);
            paramList.Add(":T$LOGN", _operator);

            string tabla = ".IngresarCauTticol092140";
            string name1 = "IngresarCauTticol092140";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [Tticol092140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);

        }

        public int ActCausalTICOL022(string pallet, int stat)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", pallet.Trim());
            paramList.Add(":T$DELE", stat);

            string tabla = owner + ".ActCausalTICOL022";
            string name1 = "ActCausalTICOL022";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [tticol022140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);

        }



        public int ActCausalTICOL042(string pallet, int stat)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", pallet.Trim());
            paramList.Add(":T$DELE", stat.ToString());

            string tabla = owner + ". ActCausalTICOL042";
            string name1 = "ActCausalTICOL042";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                strError = strSentencia + "Error finding table [TICOL042]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);

        }


        public int ActCausalcol131140(string pallet, int stat)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", pallet.Trim());
            paramList.Add(":T$DELE", stat);

            string tabla = owner + ". ActCausalcol131140";
            string name1 = "ActCausalcol131140";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [twhcol131140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);

        }

        public DataTable invLabelRegrind_listaRegistrosOrdenParam()
        {

            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();

            string tabla = owner + ".consultarCausales";
            string name1 = "consultarCausales";


            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [tticol022140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable VerificarLocate(string CWAR,string LOCA)
        {
            DataTable Retorno = new DataTable();
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":CWAR", CWAR.Trim());
            paramList.Add(":LOCA", LOCA.Trim());

          
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
        //fin



        public bool InsertarTccol307140(string USER, string STAT, string PAID, string PROC, string REFCNTD, string REFCNTU)
        {
            bool Retorno = false;
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();

            paramList.Add(":T$USER",USER.Trim());
            paramList.Add(":T$STAT",STAT.Trim());
            paramList.Add(":T$PAID",PAID.Trim());
            paramList.Add(":T$PROC",PROC.Trim());
            paramList.Add(":T$REFCNTD",REFCNTD.Trim());
            paramList.Add(":T$REFCNTU", REFCNTU.Trim());
   
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
            }
            catch (Exception ex)
            {
            }

            return Retorno;
        }

        public DataTable ConsultarPalletPicking22PAID(string PAID, string USER)
        {
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":PAID", PAID.Trim());
            string tabla = owner + ".tticol222140";
            string name1 = "ConsultarPalletPicking22140PAID";
            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [tticol222140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultarPalletPicking042PAID(string PAID, string USER)
        {
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":PAID", PAID.Trim());
            string tabla = owner + ".tticol042140";
            string name1 = "ConsultarPalletPicking042PAID";
            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [tticol042140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public DataTable ConsultarPalletPicking131PAID(string PAID, string USER)
        {
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":PAID", PAID.Trim());
            string tabla = owner + ".twhcol131140";
            string name1 = "ConsultarPalletPicking131PAID";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [twhcol131140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public bool EliminarTccol307140(string PAID)
        {
            bool Retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();

            paramList.Add(":PAID", PAID.Trim());


            strSentencia = recursos.readStatement(metodo2, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
            }
            catch (Exception ex)
            {
            }

            return Retorno;
        }

        public bool Actualizar307(string PAID_NEW, string PAID_OLD)
        {
            bool Retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();

            paramList.Add(":PAID_NEW", PAID_NEW.Trim().ToUpper());
            paramList.Add(":PAID_OLD", PAID_OLD.Trim().ToUpper());

            strSentencia = recursos.readStatement(metodo2, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                Retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
            }
            catch (Exception ex)
            {
            }

            return Retorno;
        }

        public int ActCausalcol130140(string pallet, int stat)
        {
            bool retorno = false;
            method = MethodBase.GetCurrentMethod();
            string metodo2 = "tticol082";
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", pallet.Trim());
            paramList.Add(":T$DELE", stat);

            string tabla = owner + ". ActCausalcol130140";
            string name1 = "ActCausalcol130140";

            strSentencia = recursos.readStatement(metodo2, name1, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error finding table [twhcol130140]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                Console.WriteLine(ex);
            }
            return Convert.ToInt32(retorno);

        }
    }
}

