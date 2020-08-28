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
    public class tticol020
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
        private static string tabla = owner + ".tticol020" + env;

        public tticol020() 
        { 
            
        }

        public int insertarRegistro(ref List<Ent_tticol020> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                log.escribirError(strSentencia, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                foreach (Ent_tticol020 reg in parametros)
                {
                    // Inicia la insercion del registro
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = "Error when inserting data [col020]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }
        // my nuevo metodo
        public bool insertarRegistroAux(Ent_tticol020 Objtticol020)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            paramList = new Dictionary<string, object>();
            string strError = string.Empty;

            paramList.Add(":T$PDNO", Objtticol020.pdno.Trim().ToUpper().ToString());
            paramList.Add(":T$MITM", Objtticol020.mitm.ToUpper().ToString());
            paramList.Add(":T$DSCA", Objtticol020.dsca.ToUpper().ToString());
            paramList.Add(":T$QTDL", Objtticol020.qtdl.ToString());
            paramList.Add(":T$CUNI", Objtticol020.cuni.ToString());
            paramList.Add(":T$MESS", Objtticol020.mess.ToUpper().ToString());
            paramList.Add(":T$USER", Objtticol020.user.ToUpper().ToString());
            paramList.Add(":T$REFCNTD", Objtticol020.refcntd.ToString());
            paramList.Add(":T$REFCNTU", Objtticol020.refcntu.ToString());

            try            
            {

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                return retorno;
            }
            catch (Exception ex)
            {
                strError = "Error when inserting data [col020]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return retorno;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol020 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PDNO", DbType.String, parametros.pdno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MITM", DbType.String, "         " + parametros.mitm); 
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DSCA", DbType.String, parametros.dsca); 
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTDL", DbType.Decimal, parametros.qtdl); 
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUNI", DbType.String, parametros.cuni); 
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATE", DbType.String, parametros.date); 
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess); 
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$USER", DbType.String, parametros.user); 
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
                strError = "Error when creating parameters [020]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }

    }
}
