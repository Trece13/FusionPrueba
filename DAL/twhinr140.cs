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
    public class twhinr140
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
        private static string tabla = owner + ".twhinr140" + env;

        public DataTable listaRegistros_ObtieneItem(ref Ent_twhinr140 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$LOCA", ParametrosIn.loca.Trim().ToUpperInvariant());
            paramList.Add(":T$CLOT", ParametrosIn.clot.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated."; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [twhinr140]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable consultaPorAlmacenItem(ref string cwar, ref string item, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$ITEM", item.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated."; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [twhinr140]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable consultaCantidadItemLote(ref string cwar, ref string item, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$ITEM", item.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated."; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [twhinr140]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable consultaPorAlmacenUbicacion(ref string cwar, ref string loca, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$LOCA", loca.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated."; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [twhinr140]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable consultaPorAlmacenItemLote(ref string cwar, ref string item, ref string clot, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$ITEM", item.Trim().ToUpper());
            paramList.Add(":T$CLOT", clot.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated."; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [twhinr140]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable consultaPorAlmacenItemUbicacion(ref string cwar, ref string item, ref string loca, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$ITEM", item.Trim().ToUpper());
            paramList.Add(":T$LOCA", loca.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated."; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [twhinr140]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable consultaPorAlmacenItemUbicacionLote(ref string cwar, ref string item, ref string loca, ref string lot, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());
            paramList.Add(":T$ITEM", item.Trim().ToUpper());
            paramList.Add(":T$LOCA", loca.Trim().ToUpper()== string.Empty?" ":loca.Trim().ToUpper());
            paramList.Add(":T$CLOT", lot.Trim().ToUpper()== string.Empty?" ":lot.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated."; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [twhinr140]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable consultaStks(ref string CWAR, ref string ITEM, ref string CLOT, ref string LOCA, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", CWAR.Trim().ToUpper());
            paramList.Add(":T$ITEM", ITEM.Trim().ToUpper());
            paramList.Add(":T$CLOT", CLOT.Trim() == string.Empty ? " " : CLOT.Trim());
            paramList.Add(":T$LOCA", LOCA.Trim() == string.Empty ? " " : LOCA.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "That Lot doesn't have Item Asociated."; }
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [twhinr140]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
    }
}
