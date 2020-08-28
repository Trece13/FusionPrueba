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
    public class ttcibd001
    {
        private static MethodBase method = MethodBase.GetCurrentMethod();
        private static Recursos recursos = new Recursos();
        private static Utilidades.Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();

        Dictionary<string, object> paramList = new Dictionary<string, object>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSQL = string.Empty;
        String strwareh = string.Empty;

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".ttcibd001" + env;

        DataTable consulta = new DataTable();

        public DataTable listaRegistro_ObtieneClotUnitCOnv(ref Ent_ttcibd001 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", Parametros.item.Trim());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Item incorrect, please check."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [ttcibd001]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable listaRegistro_ObtieneDescripcionUnidad(ref Ent_ttcibd001 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", Parametros.item.Trim());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Not Found" + Parametros.item.Trim(); }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [ttcibd001]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }




        public DataTable listaRegistro_ObtieneDescUnidNOLote(ref Ent_ttcibd001 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", Parametros.item.Trim());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Wrong item, please check. " + Parametros.item.Trim(); }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [ttcibd001]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }

        public DataTable findRecordTransfers(ref string item, ref bool withLot, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", item.Trim().ToUpper());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            if (!withLot)
            {
                strSQL += " AND tcib001.T$KLTC <> 1";
            }

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Wrong item, please check. " + item.Trim(); }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [ttcibd001]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable findRecordsSupplies(ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Wrong item, please check. "; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [ttcibd001]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaLotesPorItem(string item)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", item.Trim().ToUpper());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return consulta;
        }

        public int CantidadDevueltaStock(string ITEM, string CLOT, string CWAR, string LOCA)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ITEM.Trim().ToUpper());
            paramList.Add(":T$CLOT", CLOT.Trim().ToUpper());
            paramList.Add(":T$CWAR", CWAR.Trim().ToUpper());
            paramList.Add(":T$LOCA", LOCA.Trim().ToUpper());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            string retorno = string.IsNullOrEmpty(consulta.Rows[0]["QTYR"].ToString()) || string.IsNullOrWhiteSpace(consulta.Rows[0]["QTYR"].ToString()) ? "0" : consulta.Rows[0]["QTYR"].ToString();
            return Convert.ToInt32(retorno);
        }


        public DataTable listaLocalizacionesPorWarehouses(string cwar)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAR", cwar.Trim().ToUpper());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {

            }
            return consulta;
        }

        public DataTable ListaItems()
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {

            }
            return consulta;
        }

        public DataTable ListaWarehouses()
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {

            }
            return consulta;
        }

        public DataTable findItem(string ITEM)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ITEM.Trim().ToUpper());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
            }
            return consulta;
        }
    }
}
