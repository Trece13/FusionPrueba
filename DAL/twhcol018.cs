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
    public class twhcol018
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
        string tabla = owner + ".twhcol018" + env;

        public twhcol018() 
        { 
            
        }

        public int insertarRegistro(ref List<Ent_twhcol018> parametros, ref string strError, ref string strTagId)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            //string strTagId = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();

            try
            {
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$TGID", DbType.String, parametros.tgid);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, parametros.);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.tgid);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTDL", DbType.String, parametros.tgid);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUNI", DbType.String, parametros.tgid);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.String, parametros.tgid);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.String, parametros.tgid);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);

                foreach (Ent_twhcol018 reg in parametros)
                {
                    //GRRC strTagId = nextTag_Id(reg, ref strError);

                    if (!string.IsNullOrEmpty(strError))
                    {
                        retorno = false;
                        break;
                    }
                    reg.tgid = strTagId;
                    parametrosIn = AdicionaParametrosComunes(reg);
                    parametros[0].tgid = strTagId;

                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = "Error al insertar en twhcol018. Try again or contact your administrator" ;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        private string nextTag_Id(Ent_twhcol018 reg, ref string strError)
        {
            string strTagId = string.Empty;
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", reg.orno.Trim().ToUpperInvariant());
            strTagId = recursos.readStatement(method.ReflectedType.Name, "labelRegrind_verificaTagId_Param", ref owner, ref env, tabla, paramList);

            consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strTagId, ref parametersOut, null, true);
            bool blVerificaNext = false;
            strTagId = consulta.Rows[0]["CONS"].ToString();

            if (strTagId.Length < 3)
                strTagId.PadLeft(3, '0');

            switch (strTagId)
            {
                case "0" :
                    strTagId = reg.orno.Trim().ToUpperInvariant() + '-' + ConfigurationManager.AppSettings["initialConsecTagId"].ToString();
                    break;
                case "000" :
                    strTagId = reg.orno.Trim().ToUpperInvariant() + '-' + ConfigurationManager.AppSettings["initialConsecTagId"].ToString();
                    break;
                case "999":
                    strError = "Consec has maximum Value (999). Can't create new ID";
                    log.escribirError(strError + Console.Out.NewLine, method.Module.Name, method.Name, method.ReflectedType.Name);
                    break;
                default:
                    strTagId = reg.orno.Trim().ToUpperInvariant() + '-' + (Convert.ToInt32(strTagId) + 1).ToString();
                    blVerificaNext = true;
                    break;
            }

            if (blVerificaNext)
            {
                paramList = new Dictionary<string, object>();
                paramList.Add("p1", strTagId);
                string strBusca = recursos.readStatement(method.ReflectedType.Name, "labelRegrind_buscaTagId_Param", ref owner, ref env, tabla, paramList);
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strBusca, ref parametersOut, null, true);
                if (consulta.Rows.Count > 0)
                {
                    strError = "Next Tag id already (" + strTagId + ") exists. Can't create new ID";
                    log.escribirError(strError + Console.Out.NewLine, method.Module.Name, method.Name, method.ReflectedType.Name);
                }
            }
            return strTagId;        

        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_twhcol018 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;

            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$TGID", DbType.String, parametros.tgid);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$ITEM", DbType.String, "         " + parametros.item);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$QTDL", DbType.Decimal, Convert.ToDecimal(System.Math.Abs(parametros.qtdl)).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUNI", DbType.String, parametros.cuni);
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
                strError = "Error when creating parameters [twhcol018]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }
    }
}
