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
    public class ttwhcol017
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
        string tabla = owner + ".twhcol017" + env;
        string tablaCons = owner + ".twhcol016" + env;

        public ttwhcol017() 
        { 
            
        }

        public int insertarRegistro(ref List<Ent_ttwhcol017> parametros, ref string strError)
        {
            string tablaCoun = owner + ".twhcol002" + env;
            bool retorno = false;
            Dictionary<string, object> parametersOut = new Dictionary<string, object>();
            int sqnb = 0, coun = 0;

            try
            {
                method = MethodBase.GetCurrentMethod();
                // Sentencia de busquedade id de etiqueta existente
                //string strSentenciaBusca = "SELECT NVL(MAX(T$SQNB),0) + 1 SQNB " +
                //                        "FROM " + tabla + " " +
                //                       "WHERE TRIM(T$LABL) = TRIM('{0}')";




                string strSentenciaBusca = string.Empty; 

                //string strSentenciaBuscaCoun = "SELECT T$COUN COUN " +
                //                        "FROM " + tablaCoun + " " +
                //                       "WHERE TRIM(T$CWAR) = TRIM('{0}')";

                string strSentenciaBuscaCoun = string.Empty;

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                
                foreach (Ent_ttwhcol017 reg in parametros)
                {
                    // Buscar id de etiqueta existente
                    paramList = new Dictionary<string, object>();
                    paramList.Add("p1", reg.labl.Trim().ToUpperInvariant());
                    strSentenciaBusca = recursos.readStatement(method.ReflectedType.Name, "TakeMaterialInv_verificaidLabel_Param", ref owner, ref env, tabla, paramList);

                    DataTable conf = DAL.BaseDAL.BaseDal.EjecutarCons("text", strSentenciaBusca, ref parametersOut, null, false);

                    if (conf.Rows.Count > 0) { sqnb = Convert.ToInt32(conf.Rows[0]["SQNB"].ToString()); }
                    reg.sqnb = sqnb;

                    // Buscar campo coun de twhcol002
                    paramList = new Dictionary<string, object>();
                    paramList.Add("p1", reg.cwar.Trim().ToUpperInvariant());
                    strSentenciaBuscaCoun = recursos.readStatement(method.ReflectedType.Name, "TakeMaterialInv_verificaConsBodega_Param", ref owner, ref env, tablaCoun, paramList);

                    conf = DAL.BaseDAL.BaseDal.EjecutarCons("text", strSentenciaBuscaCoun, ref parametersOut, null, false);

                    if (conf.Rows.Count > 0) { coun = Convert.ToInt32(conf.Rows[0]["COUN"].ToString()); }
                    reg.coun = coun;

                    // Inicia la insercion del registro
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = "Error when inserting data [ttwhcol017]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public DataTable TakeMaterialInv_listaConsLabel_Param(ref Ent_ttwhcol017 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            string tableSeco = owner + ".tticol022" + env;
            string tableSeco1 = owner + ".ttisfc001" + env;
            string tableSeco2 = owner + ".ttcmcs003" + env; // item
            string tableSeco3 = owner + ".ttcibd001" + env; // Bodega


            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$LABL", Parametros.labl.Trim());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, "TakeMaterialInv_listaConsLabel_Param", ref owner, ref env, tabla, paramList);
            //strSentencia = "SELECT T016.T$ZONE, T016.T$LABL, T016.T$ITEM, T002.T$DSCA ARTICULO, T002.T$CUNI UNIDAD, T002.T$KLTC T$KITM, " +
            //                      "T016.T$CWAR, T003.T$DSCA BODEGA, T016.T$CLOT, T016.T$QTYR, T016.T$LOGN, " + 
            //                      "TO_CHAR(((T016.T$DATE)-5/24), 'DD/MM/YYYY hh24:mi:ss') T$DATE, T016.T$REFCNTD, T016.T$REFCNTU " +
            //               "FROM " + tablaCons + " T016 LEFT JOIN " + tableSeco3 + " T002 ON TRIM(T016.T$ITEM) = TRIM(T002.T$ITEM)" +
            //                                           "LEFT JOIN " + tableSeco2 + " T003 ON T016.T$CWAR = T003.T$CWAR  " +
            //                  " WHERE TRIM(t$labl) = '" + Parametros.labl.Trim() + "'";

            //SELECT T016.T$ZONE, T016.T$LABL, T016.T$ITEM, T002.T$DSCA ARTICULO, T002.T$CUNI UNIDAD, T002.T$KLTC T$KITM,
            //T016.T$CWAR, T003.T$DSCA BODEGA, T016.T$CLOT, T016.T$QTYR, T016.T$LOGN,
            //TO_CHAR(((T016.T$DATE)-5/24), 'DD/MM/YYYY hh24:mi:ss') T$DATE, T016.T$REFCNTD, T016.T$REFCNTU
            //FROM baan.twhcol016140 T016 LEFT JOIN baan.ttcibd001140 T002 ON TRIM(T016.T$ITEM) = TRIM(T002.T$ITEM)
            //LEFT JOIN baan.ttcmcs003140 T003 ON T016.T$CWAR = T003.T$CWAR
            //WHERE TRIM(t$labl) = '[:T$LABL]'

            Dictionary<string, object> parametersOut = new Dictionary<string, object>();

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1)
                {
                    string tablaPrinc = owner + ".tticol022" + env;

                    strSentencia = recursos.readStatement(method.ReflectedType.Name, "selecttticol022", ref owner, ref env, tabla, paramList);

                    //strSentencia = "SELECT T022.T$SQNB T$LABL, T022.T$MITM T$ITEM, T002.T$DSCA ARTICULO, T002.T$CUNI UNIDAD, " +
                    //               "       T003.T$CWAR, T022.T$QTDL T$QTYR, T003.T$DSCA BODEGA, NVL(T002.T$KLTC ,3) T$KITM,  " +
                    //               "       CASE NVL(T002.T$KLTC,3) WHEN 1 THEN T022.T$PDNO ELSE '' END T$CLOT  " +                                   
                    //               "FROM  " + tableSeco + " T022 LEFT JOIN " + tableSeco1 + " T001 ON T022.T$PDNO = T001.T$PDNO " +
                    //                                             "LEFT JOIN " + tableSeco2 + " T003 ON T001.T$CWAR = T003.T$CWAR  " +
                    //                                             "LEFT JOIN " + tableSeco3 + " T002 ON TRIM(T022.T$MITM)= TRIM(T002.T$ITEM) " +
                    //                 "WHERE TRIM(T$SQNB) = '" + Parametros.labl.Trim() + "'";

                    //SELECT T022.T$SQNB T$LABL, T022.T$MITM T$ITEM, T002.T$DSCA ARTICULO, T002.T$CUNI UNIDAD,
                    //T003.T$CWAR, T022.T$QTDL T$QTYR, T003.T$DSCA BODEGA, NVL(T002.T$KLTC ,3) T$KITM,
                    //CASE NVL(T002.T$KLTC,3) WHEN 1 THEN T022.T$PDNO ELSE '' END T$CLOT
                    //FROM BAAN baan.tticol022140 T022 LEFT JOIN  BAAN.ttisfc001140 T001 ON T022.T$PDNO = T001.T$PDNO 
                    //LEFT JOIN BAAN.ttcmcs003140 T003 ON T001.T$CWAR = T003.T$CWAR
                    //LEFT JOIN BAAN.ttcibd001140 T002 ON TRIM(T022.T$MITM)= TRIM(T002.T$ITEM)
                    //WHERE TRIM(T$SQNB) = '[:T$LABL]'

                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                    if (consulta.Rows.Count < 1)
                    {
                        tablaPrinc = owner + ".twhcol018" + env;
                        strSentencia = recursos.readStatement(method.ReflectedType.Name, "selecttwhcol018", ref owner, ref env, tabla, paramList);

                        //strSentencia = "SELECT T022.T$TGID T$LABL, T022.T$ITEM, T002.T$DSCA ARTICULO, T002.T$CUNI UNIDAD,  " +
                        //               " T003.T$CWAR, SUBSTR(T022.T$TGID, 0, INSTR(T022.T$TGID, '-')) T$PDNO, T022.T$CLOT, " +
                        //               " T022.T$QTDL T$QTYR, T003.T$DSCA BODEGA, T002.T$KLTC T$KITM " +
                        //               " FROM  " + tablaPrinc + " T022 LEFT JOIN " + tableSeco1 + " T001 ON " +
                        //               " SUBSTR(T022.T$TGID, 0, INSTR(T022.T$TGID, '-')) = T001.T$PDNO  " +
                        //               " LEFT JOIN  " + tableSeco2 + "  T003 ON T001.T$CWAR = T003.T$CWAR   " +
                        //               " LEFT JOIN  " + tableSeco3 + " T002 ON LTRIM(RTRIM(T022.T$ITEM))= LTRIM(RTRIM(T002.T$ITEM))  " +
                        //               " WHERE LTRIM(RTRIM(T$TGID)) = '" + Parametros.labl.Trim() + "'";


                        //SELECT T022.T$TGID T$LABL, T022.T$ITEM, T002.T$DSCA ARTICULO, T002.T$CUNI UNIDAD,
                        //T003.T$CWAR, SUBSTR(T022.T$TGID, 0, INSTR(T022.T$TGID, '-')) T$PDNO, T022.T$CLOT,
                        //T022.T$QTDL T$QTYR, T003.T$DSCA BODEGA, T002.T$KLTC T$KITM
                        //FROM  baan,twhcol018140 T022 LEFT JOIN baan.ttisfc001140 T001 ON
                        //SUBSTR(T022.T$TGID, 0, INSTR(T022.T$TGID, '-')) = T001.T$PDNO
                        //LEFT JOIN  baan.ttcmcs003140  T003 ON T001.T$CWAR = T003.T$CWAR
                        //LEFT JOIN  baan.ttcibd001140 T002 ON LTRIM(RTRIM(T022.T$ITEM))= LTRIM(RTRIM(T002.T$ITEM))
                        //WHERE LTRIM(RTRIM(T$TGID)) = '[:T$LABL]'

                        consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

                        if (consulta.Rows.Count < 1)
                            strError = "Label ID " + Parametros.labl + " doesn't exist. Cannot Continue";
                    }
                }
                return consulta;
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttwhcol017]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable TakeMaterialInv_verificaBodegaZone_Param(ref Ent_ttwhcol016 parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add("p1", parametros.zone.Trim().ToUpperInvariant());

            //strSentencia = "SELECT T$CWAR " +
            //               "FROM " + owner + ".twhwmd310" + env + " TWAR " + 
            //               "WHERE TRIM(T$ZONE) = '" + Parametros.zone.Trim() + "'";

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList); 

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1)
                {
                    strError = "Zone don´t have warehouse";
                }

                ttwhcol016 dal016 = new ttwhcol016();
                parametros.cwar = consulta.Rows[0]["T$CWAR"].ToString();
                consulta = dal016.TakeMaterialInv_verificaBodega_Param(ref parametros, ref strError);
            }
            catch (Exception ex)
            {
                strError = "Error when querying data [ttwhcol017]. Try again or contact your administrator";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_ttwhcol017 parametros, bool blnUsarPRetorno = false)
        {
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$LABL", DbType.String, parametros.labl);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$SQNB", DbType.Int32, parametros.sqnb);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$CWAR", DbType.String, parametros.cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$ITEM", DbType.String, "         " + parametros.item);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$CLOT", DbType.String, parametros.clot);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$QTDL", DbType.Decimal, Convert.ToDecimal(parametros.qtdl).ToString("#.##0,0000"));
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$CUNI", DbType.String, parametros.cuni);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$LOGN", DbType.String, parametros.logn);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$COUN", DbType.Int32, parametros.coun);   
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$PROC", DbType.Int32, parametros.proc);   
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$REFCNTU", DbType.Int32, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, "T$ZONE", DbType.String, parametros.zone);

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

