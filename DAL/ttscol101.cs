using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.Utilidades;
using System.Diagnostics;
using System.Reflection;

namespace whusa.DAL
{
    public class ttscol101
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
        private static string tabla = owner + ".ttscol101" + env;

        static ttscol101() 
        { 
            
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_ttscol101 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORNO", DbType.String, parametros.orno.ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, "         " + parametros.item.Trim().ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOCA", DbType.String, parametros.loca);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar.ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QDEL", DbType.Decimal, Convert.ToDecimal(parametros.qdel).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CDAT", DbType.String, parametros.cdat);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);


                if (!string.IsNullOrEmpty(parametros.logn))
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);
                if (!string.IsNullOrEmpty(parametros.cusr))
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CUSR", DbType.String, parametros.cusr);
                if (parametros.conf > 0)
                    Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CONF", DbType.Int32, parametros.conf);


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
                strError = "Error when creating parameters [scol101]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }
    }
}
