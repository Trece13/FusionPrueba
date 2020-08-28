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
    public class tticol080
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
        private static string tabla = owner + ".tticol080" + env;

        static tticol080() 
        { }

        public int insertarRegistro(ref List<Ent_tticol080> parametros, ref string strError, ref string strTagId)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            //Ent_twhcol018 obj018;
            //twhcol018 dal018 = new twhcol018();

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                //int cant = parametros.Count;

                foreach (Ent_tticol080 reg in parametros)
                {
                    //obj018 = new Ent_twhcol018();
                    //parametrosIn = AdicionaParametrosComunes(reg);

                    method = MethodBase.GetCurrentMethod();
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$ORNO", reg.orno);
                    paramList.Add(":T$PONO", reg.pono);
                    paramList.Add(":T$CWAR", reg.cwar);
                    paramList.Add(":T$ITEM", reg.item);
                    paramList.Add(":T$QUNE", reg.qune);
                    paramList.Add(":T$LOGN", reg.logn);
                    paramList.Add(":T$DATE", reg.date);
                    paramList.Add(":T$PROC", reg.proc);
                    paramList.Add(":T$REFCNTD", reg.refcntd);
                    paramList.Add(":T$REFCNTU", reg.refcntu);
                    paramList.Add(":T$CLOT", reg.clot);
                    paramList.Add(":T$OORG", reg.oorg);
                    paramList.Add(":T$PICK", reg.pick);
                    //strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                    strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList, 1);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [tticol080]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);

        }

        public int insertarRegistro_regrindM(ref List<Ent_tticol080> parametros, ref string strError, ref string strTagId)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            Ent_twhcol018 obj018;
            twhcol018 dal018 = new twhcol018();
            List<Ent_twhcol018> Listparametros018;
            int retorno018;
            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                int cant = parametros.Count;

                foreach (Ent_tticol080 reg in parametros)
                {
                    obj018 = new Ent_twhcol018();
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    if (retorno)
                    {
                        Listparametros018 = new List<Ent_twhcol018>();

                        obj018.orno = reg.orno;
                        obj018.item = reg.item;
                        obj018.cuni = reg.idrecord;
                        obj018.qtdl = reg.qune;
                        obj018.clot = " ";
                        obj018.refcntd = 0;
                        obj018.refcntu = 0;
                        Listparametros018.Add(obj018);
                        retorno018 = dal018.insertarRegistro(ref Listparametros018, ref strError, ref strTagId);

                        if (retorno018 < 1 || !string.IsNullOrEmpty(strError))
                            break;
                        cant++;
                    }
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError = "Error when inserting data [tticol080]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, method.Module.Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);

        }
                
        public int actualizarRegistro_Param(ref List<Ent_tticol080> parametros, ref string strError, ref string strTagId)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            Ent_twhcol018 obj018;
            twhcol018 dal018 = new twhcol018();
            List<Ent_twhcol018> Listparametros018;
            int retorno018;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla); 
                string strCondicion = string.Empty;

                foreach (Ent_tticol080 reg in parametros)
                {
                    strSentencia += " WHERE T$PONO = '" + reg.pono + 
                                     "' AND T$ORNO = '" + reg.orno.Trim().ToUpperInvariant() + 
                                     "' AND TRIM(T$ITEM) = '" + reg.item.Trim().ToUpperInvariant() + "'";

                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                    //if (retorno)
                    //{
                    //    Listparametros018 = new List<Ent_twhcol018>();
                    //    obj018 = new Ent_twhcol018();

                    //    obj018.orno = reg.orno;
                    //    obj018.item = reg.item;
                    //    obj018.cuni = reg.idrecord;
                    //    obj018.qtdl = reg.qune;
                    //    obj018.clot = " ";
                    //    obj018.refcntd = 0;
                    //    obj018.refcntu = 0;
                    //    Listparametros018.Add(obj018);
                    //    retorno018 = dal018.insertarRegistro(ref Listparametros018, ref strError, ref strTagId);

                    //    if (!string.IsNullOrEmpty(strError))
                    //        break;
                    //}
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError += "Error updating data[tticol080]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return 0;
        }

        public bool updateRecordRollAnnounce(ref Ent_tticol080 data, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$QUNE", data.qune);
                paramList.Add(":T$ORNO", data.orno.Trim());
                paramList.Add(":T$PONO", data.pono);
                paramList.Add(":T$ITEM", data.item.Trim());

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

        public bool updateRecordCosts(ref Ent_tticol080 data, ref string strError)
        {
            parametrosIn.Clear();
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$QUNE", data.qune.ToString().Replace(',','.'));
                paramList.Add(":T$LOGN", data.logn.Trim().ToUpper());
                paramList.Add(":T$ORNO", data.orno.Trim().ToUpper());
                paramList.Add(":T$PONO", data.pono);
                
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

        public DataTable findRecordByOrnoPonoItem(ref string orno, ref string pono, ref string item, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", orno.Trim().ToUpper());
            paramList.Add(":T$PONO", pono.Trim().ToUpper());
            paramList.Add(":T$ITEM", item.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol080]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }

        public DataTable findRecordByOrnoPono(ref string orno, ref string pono, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ORNO", orno.Trim().ToUpper());
            paramList.Add(":T$PONO", pono.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Incorrect location, please verify."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol080]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }

            return consulta;
        }
        
        public DataTable listaRegistroImprimir_Param(ref Ent_tticol080 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.pono);
            paramList.Add("p2", ParametrosIn.orno.Trim().ToUpperInvariant());
            paramList.Add("p3", ParametrosIn.item.Trim().ToUpperInvariant());
            paramList.Add("p4", ParametrosIn.cwar.Trim().ToUpperInvariant());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol080]. Try again or contact your administrator";
                throw ex;
            }
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol080 parametros, bool blnUsarPRetorno = false)
        {
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ORNO", DbType.String, parametros.orno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PONO", DbType.String, parametros.pono);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, "         " + parametros.item.Trim().ToUpperInvariant());
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QUNE", DbType.Decimal, Convert.ToDecimal(parametros.qune).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGN", DbType.String, parametros.logn);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.String, parametros.proc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.String, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.String, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PICK", DbType.String, parametros.pick);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$OORG", DbType.String, parametros.oorg);

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
