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
    public class tticol011
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
        private static string tabla = owner + ".tticol011" + env;
        private static string tticst002 = owner + ".tticst002" + env;

        static tticol011() 
        { }

        public DataTable invLabel_listaRegistrosOrdenMaquina_Param(ref Ent_tticol011 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.mcno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Order doesn't initiated for machine. " + ParametrosIn.mcno.Trim().ToUpperInvariant(); }
                if (consulta.Rows.Count > 1) { strError = "More than one work order found"; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol011]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable invLabel_listaRegistrosOrdenMaquina_Workorder(ref Ent_tticol011 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.pdno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Order doesn't initiated for machine. " + ParametrosIn.mcno.Trim().ToUpperInvariant(); }
                if (consulta.Rows.Count > 1) { strError = "More than one work order found"; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol011]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable invLabel_listaRegistrosOrdenParam(ref Ent_tticol011 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.pdno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Work Order " + ParametrosIn.pdno.Trim() + " not found"; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol011]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;        
        }

        public DataTable invLabelRegrind_listaRegistrosOrdenMaquina_Param(ref Ent_tticol011 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.pdno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Order doesn't initiated for machine. " + ParametrosIn.mcno.Trim().ToUpperInvariant(); }
                if (consulta.Rows.Count > 1) { strError = "More than one work order found"; }
            }
            catch (Exception ex)    
            {
                strError = "Error when querying data [tticol011]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable invLabelRegrind_listaRegistrosOrdenParam(ref Ent_tticol011 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.pdno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1)
                { strError = "Order " + ParametrosIn.pdno.Trim() + " doesn't exist or the status is not active, release or completed."; }
                else
                {
                    if (ParametrosIn.Acceso)
                    {
                        strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1);
                        try
                        {
                            consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                            if (consulta.Rows.Count < 1)
                            { strError = "Order " + ParametrosIn.pdno.Trim() + " doesn't contains item for list."; }
                        }
                        catch (Exception e)
                        {
                            strError = "Error al consultar para la lista. Try again or contact your administrator \n " + strSentencia;
                            log.escribirError(strError + e.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                        }
                    }
                
                }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol011]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaRegistrosMaquina_Param(ref Ent_tticol011 ParametrosIn, ref string strError)
        {
            String env = ConfigurationManager.AppSettings["env"].ToString();
            String owner = ConfigurationManager.AppSettings["owner"].ToString();
            string tabla = owner + ".ttirou002" + env;

            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.mcno.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Machine doesn't exist. " + ParametrosIn.mcno.Trim().ToUpperInvariant(); }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttirou002]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable findByPdnoAndInStatus(ref string pdno, ref string status, ref string strError) 
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim());
            paramList.Add(":T$STATUS", status.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol011]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public int countByMachineStatusAndDiferentPdno(ref string pdno, ref string stat, ref string mcno, ref string strError) 
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());
            paramList.Add(":T$STAT", stat.Trim().ToUpper());
            paramList.Add(":T$MCNO", mcno.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol011]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(consulta.Rows[0]["CONT"]);
        }

        public bool updateStatusInitiated(ref Ent_tticol011 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$STAT", data.stat);
                paramList.Add(":T$INIB", data.inib.Trim().ToUpper());
                paramList.Add(":T$PDNO", data.pdno.Trim().ToUpper());
                paramList.Add(":T$MCNO", data.mcno.Trim().ToUpper());

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

        public bool updateStatusOnHold(ref Ent_tticol011 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$STAT", data.stat);
                paramList.Add(":T$PDNO", data.pdno.Trim().ToUpper());
                paramList.Add(":T$MCNO", data.mcno.Trim().ToUpper());

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

        public bool updateStatusFinish(ref Ent_tticol011 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$STAT", data.stat);
                paramList.Add(":T$COMB", data.comb.Trim().ToUpper());
                paramList.Add(":T$PDNO", data.pdno.Trim().ToUpper());
                paramList.Add(":T$MCNO", data.mcno.Trim().ToUpper());

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

        public DataTable findRecordByPdno(ref string pdno, ref string strError) 
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim());


            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tticst002, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol011]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }   
    }
}
