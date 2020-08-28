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
    public class tticol032
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
        private static string tabla = owner + ".tticol032" + env;

        public int insertarRegistro(ref Ent_tticol032 parametros, ref string strError)
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
                strError = "Error when inserting data [tticol032]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }

            return Convert.ToInt32(retorno);

        }

        public int consultarConsecutivoRegistro(ref string pdno, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PDNO", pdno.Trim().ToUpper());

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

            return Convert.ToInt32(consulta.Rows[0]["SEC"].ToString());
        }

        public DataTable consultaPorPalletBodegas(ref string sqnb, ref string cwar, ref string orno, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$SQNB", sqnb.Trim().ToUpper());
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$ORNO", orno.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol032]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol032 parametros, bool blnUsarPRetorno = false)
        {
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$OORG", DbType.Int32, Convert.ToInt32(parametros.oorg));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORNO", DbType.String, parametros.orno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SQNB", DbType.String, parametros.sqnb);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$OSET", DbType.Int32, Convert.ToInt32(parametros.oset));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PONO", DbType.Int32, Convert.ToInt32(parametros.pono));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SEQN", DbType.Int32, Convert.ToInt32(parametros.seqn));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MITM", DbType.String, "         " + parametros.mitm);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DSCA", DbType.String, parametros.dsca);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.loca);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTDL", DbType.Decimal, Convert.ToDecimal(parametros.qtdl).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUNI", DbType.String, parametros.cuni);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$USER", DbType.String, parametros.user);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.String, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.String, parametros.refcntu);

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
                throw (ex);
            }
            return parameterCollection;
        }
    }
}
