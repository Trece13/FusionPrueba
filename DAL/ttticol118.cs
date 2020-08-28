using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Reflection;
using whusa.Entidades;
using whusa.Utilidades;
using System.Diagnostics;


namespace whusa.DAL
{
    public class tticol118
    {
        private static Utilidades.Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method = MethodBase.GetCurrentMethod();
        private static string metodo = method.Name;
        private static Recursos recursos = new Recursos();

        private static string env = ConfigurationManager.AppSettings["env"].ToString();
        private static string owner = ConfigurationManager.AppSettings["owner"].ToString();

        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSentencia = string.Empty;
        DataTable consulta = new DataTable();
        string tabla = owner + ".tticol118" + env;
        String strSQL = string.Empty;

        public int insertarRegistro(ref List<Ent_tticol118> parametros, ref string strError)
        {
            bool retorno = false;
            Dictionary<string, object> parametersOut = new Dictionary<string, object>();
            string MsgstrError = string.Empty;
            try
            {
                strSentencia = "INSERT INTO " + tabla + " ( T$ITEM, T$CWAR, T$CLOT, T$QTYR, T$CDIS, T$OBSE, T$LOGR, T$DATR, T$DISP, T$STOC, T$RITM, T$PROC, T$MESS, T$SUNO, T$REFCNTD, T$REFCNTU,T$PAID) " +
                                           "VALUES (:T$ITEM, :T$CWAR, :T$CLOT, :T$QTYR, :T$CDIS, :T$OBSE, :T$LOGR, sysdate+(5/24), :T$DISP, :T$STOC, :T$RITM, :T$PROC, :T$MESS, :T$SUNO, :T$REFCNTD, :T$REFCNTU, UPPER(:T$PAID))";
                foreach (Ent_tticol118 reg in parametros)
                {
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }
            catch (Exception ex)
            {
                strError = MsgstrError + "Error when inserting data [col118]. Try again or contact your administrator";
                log.escribirError(strError + " - " + ex.Message, stackTrace.GetFrame(2).GetMethod().Name, metodo, method.ReflectedType.Name); 
            }
            return Convert.ToInt32(retorno);
        }

        public int actualizarRegistro_Param(ref List<Ent_tticol118> parametros, ref string strError, string Aplicacion)
        {
            bool retorno = false;
            Dictionary<string, object> parametersOut = new Dictionary<string, object>();
            List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
            string table = owner + ".tticol118" + env;
            

            try
            {
                string strCondicion = string.Empty;
                double strCant = 0;
                string strRitem = string.Empty;

                foreach (Ent_tticol118 reg in parametros)
                {
                    strCant = reg.qtyr;
                    strRitem = reg.ritm;

                    strSQL = "UPDATE " + owner + ".tticol118" + env + " SET " +
                             "T$QTYR = T$QTYR + " + strCant + ", " +
                             "T$OBSE ='" + reg.obse.Trim() +"',"+
                             "T$RITM = '" + strRitem.TrimEnd() + "'";                              
                    strCondicion = " WHERE TRIM(T$ITEM) = '" + reg.item.Trim() + "'" +
                                   " AND TRIM(T$CWAR) = '" + reg.cwar.Trim() + "'" +
                                   " AND TRIM(T$CLOT) = '" + reg.clot.Trim() + "'";
                    //parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSQL + strCondicion, ref parametersOut, parametrosIn, false);
                    if (!string.IsNullOrEmpty(strError))
                        break;
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                strError += "Error updating data [col118]. Try again or contact your administrator";
                log.escribirError(strError + " - " + ex.Message, stackTrace.GetFrame(2).GetMethod().Name, metodo, method.ReflectedType.Name);
            }
            return Convert.ToInt32(retorno);
        }

        public DataTable listaRegistros_Param(ref Ent_tticol118 ParametrosIn, ref string strError)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ParametrosIn.item.Trim());
            paramList.Add(":T$CLOT", ParametrosIn.clot.ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, "selectticol118", ref owner, ref env, tabla, paramList);

            //strSentencia = "SELECT * FROM (SELECT T$ITEM, T$CWAR, T$CLOT, T$QTYR, T$CDIS, T$OBSE, T$LOGR, T$DATR, T$DISP, T$STOC, T$RITM, T$PROC, T$MESS, T$SUNO,T$PAID " +
            //               "FROM " + owner + ".tticol118" + env + " col118 " +
            //               "WHERE trim(col118.t$item) ='" + ParametrosIn.item.Trim() + "' " +
            //               "AND upper(col118.t$clot) = '" + ParametrosIn.clot.ToUpper() + "' order by T$DATR DESC) WHERE ROWNUM = 1";

            //SELECT * FROM (SELECT T$ITEM, T$CWAR, T$CLOT, T$QTYR, T$CDIS, T$OBSE, T$LOGR, T$DATR, T$DISP, T$STOC, T$RITM, T$PROC, T$MESS, T$SUNO,T$PAID
            //FROM BAAN.tticol118140 col118
            //WHERE trim(col118.t$item) ='[:T$ITEM]'
            //AND upper(col118.t$clot) = '[:T$CLOT]' order by T$DATR DESC) WHERE ROWNUM = 1


            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Data " + ParametrosIn.item.Trim() + " " + ParametrosIn.clot.Trim() + " not found"; }


            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tticol118]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaProveedores_Param(ref Ent_tticol118 ParametrosIn, ref string strError)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ParametrosIn.item.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, "listaProveedores_Param", ref owner, ref env, tabla, paramList);

            //strSentencia = "select ipu010.t$otbp proveedor, com100.t$nama nombre from " + owner + ".ttcibd001" + env + " ibd001 " +
            //               "left join " + owner + ".ttdipu010" + env + " ipu010 on ipu010.t$item = ibd001.t$item " +
            //               "left join " + owner + ".ttccom100" + env + " com100 on com100.t$bpid = ipu010.t$otbp " +
            //               "where trim(ibd001.t$item)='" + ParametrosIn.item.Trim() + "'";

            //select ipu010.t$otbp proveedor, com100.t$nama nombre from baan.ttcibd001140 ibd001
            //left join baan.ttdipu010140 ipu010 on ipu010.t$item = ibd001.t$item
            //left join baan.ttccom100140 com100 on com100.t$bpid = ipu010.t$otbp
            //where trim(ibd001.t$item)='[:T$ITEM]'

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Data " + ParametrosIn.item.Trim() + " " + ParametrosIn.clot.Trim() + " not found"; }


            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tdipu001]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
        public DataTable listaProveedores_ParamMRB(ref Ent_tticol118 ParametrosIn, ref string strError)
        {
            //strSentencia = "select ipu010.t$otbp proveedor, com100.t$nama nombre from " + owner + ".ttdipu010" + env + " ipu010 " +
            //               "inner join " + owner + ".ttccom100" + env + " com100 on ipu010.t$otbp = com100.t$bpid " +
            //               "where trim(ipu010.t$item) like '%" + ParametrosIn.item.Trim() + "%'";


            //select ipu010.t$otbp proveedor, com100.t$nama nombre from baan.ttdipu010140 ipu010
            //inner join baan.ttccom100140 com100 on ipu010.t$otbp = com100.t$bpid
            //where trim(ipu010.t$item) like '%[:T$ITEM]%'

            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ParametrosIn.item.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, "listaProveedores_ParamMRB", ref owner, ref env, tabla, paramList);

            //ipu010.t$otbp, com100.t$nama

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Data " + ParametrosIn.item.Trim() + " " + ParametrosIn.clot.Trim() + " not found"; }


            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tdipu001]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }	 

        public DataTable ListaProveedoresProducto(ref Ent_tticol118 ParametrosIn, ref string strError)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ParametrosIn.item.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, "ListaProveedoresProducto", ref owner, ref env, tabla, paramList);
            
            //strSentencia = "select com100.T$BPID proveedor, com100.T$NAMA nombre from " + owner + ".ttccom100" + env + " com100 INNER JOIN " + owner + ".ttdipu010" + env + "  dipu0101 ON  dipu0101.T$OTBP = com100.T$BPID WHERE Trim(dipu0101.T$ITEM) = Trim('" + ParametrosIn.item.Trim() + "')";
            //select com100.T$BPID proveedor, com100.T$NAMA nombre from baan.ttccom100140 com100 
            //INNER JOIN baan.ttdipu010140  dipu0101 ON  dipu0101.T$OTBP = com100.T$BPID 
            //WHERE Trim(dipu0101.T$ITEM) = Trim('[:T$ITEM]')

            
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Data " + ParametrosIn.item.Trim() + " " + ParametrosIn.clot.Trim() + " not found"; }


            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tdipu001]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaReason_Param(ref string strError)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", "");

            strSentencia = recursos.readStatement(method.ReflectedType.Name, "listaReason_Param", ref owner, ref env, tabla, paramList);
            
            //strSentencia =  "select t$cdis reason, t$dsca descr from " + owner + ".ttcmcs005" + env + 
            //                " where t$rstp = 10 " +
            //                " order by t$dsca";

            //select t$cdis reason, t$dsca descr from baan.ttcmcs005140
            //where t$rstp = 10
            //order by t$dsca
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Data not found"; }


            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tcmcs005]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaStockw_Param(ref string strError)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", "");

            strSentencia = recursos.readStatement(method.ReflectedType.Name, "listaStockw_Param", ref owner, ref env, tabla, paramList);

            //strSentencia = "select t$cwar warehouse, t$dsca descrw, CONCAT(CONCAT(t$cwar,'  -   '),t$dsca) warehouseFullName  from baan.ttcmcs003140 order by warehouse";
            
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Data not found"; }


            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tcmcs003]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaRegrind_Param(ref string strError)
        {
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", "");

            strSentencia = recursos.readStatement(method.ReflectedType.Name, "listaRegrind_Param", ref owner, ref env, tabla, paramList);

            //strSentencia = "select t$item item, t$dsca descri, trim(t$item)||' - '||trim(t$dsca) descrc from " + owner + ".ttcibd001" + env +
            //               " where t$cpcl = 'RET' " +
            //               " order by t$dsca";

            //select t$item item, t$dsca descri, trim(t$item)||' - '||trim(t$dsca) descrc from baan.ttcibd001140
            //where t$cpcl = 'RET'
            //order by t$dsca
            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Data not found"; }


            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tcmcs003]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable listaRegrind_ParamV2(ref string item, ref string strError)
        {

            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", item.Trim());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, "listaRegrind_ParamV2", ref owner, ref env, tabla, paramList);

            //strSentencia = "select distinct ticol001.t$item item, ticol001.t$dsca descri, trim(ticol001.t$item)||' - '||trim(ticol001.t$dsca) descrc" 
            //            + " from "+ owner + ".ttcibd001" + env + " ticol001 "
            //            + " INNER JOIN " + owner + ".tticol135" + env + " ticol135 ON ticol135.T$RGRN = ticol001.T$ITEM"
            //            + " WHERE TRIM(ticol135.T$ITEM) = '" + item.Trim() + "'";

            //select distinct ticol001.t$item item, ticol001.t$dsca descri, trim(ticol001.t$item)||' - '||trim(ticol001.t$dsca) descrc
            //from baan.ttcibd0011140 ticol001
            //INNER JOIN baan.tticol135140 ticol135 ON ticol135.T$RGRN = ticol001.T$ITEM"
            //WHERE TRIM(ticol135.T$ITEM) = '[:T$ITEM] '

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "Data not found"; }


            }
            catch (Exception ex)
            {
                strError = "Error when querying data [tcmcs003]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        public DataTable invLabel_registroImprimir_Param(ref Ent_tticol118 Parametros, ref string strError)
        {
            string strSentenciaS = string.Empty;
            string strLote = Parametros.clot.Trim().ToUpperInvariant();
            string strBodega = Parametros.cwar.Trim().ToUpperInvariant();
            string strItem = Parametros.item.Trim();

            Dictionary<string, object> parametersOut = new Dictionary<string, object>();

            //strSentenciaS = "select * from (select case trim(col118.t$ritm) when 'NA' then trim(col118.t$item) else trim(col118.t$ritm) end item, trim(ibd001.t$dsca) desci, trim(ibd001.t$cuni) unidad, col118.t$qtyr cantidad, " +
            //               " col118.t$clot lote, mcs005.t$dsca descr, substr(col118.t$obse,1,255) comentario, col020.t$pent factorkg, col118.t$datr-5/24 fecha, trim(col118.T$PAID) paid, col008.t$wreg wreg " +
            //               " from " + owner + ".tticol118" + env + " col118 " +
            //               " inner join " + owner + ".ttcibd001" + env + " ibd001 on ibd001.t$item = col118.t$item " +
            //               " left join " + owner + ".ttcibd001" + env + " dbi001 on dbi001.t$item = col118.t$ritm " +
            //               " inner join " + owner + ".ttccol020" + env + " col020 on col020.t$item = col118.t$item " +
            //               " inner join " + owner + ".ttcmcs005" + env + " mcs005 on mcs005.t$cdis = col118.t$cdis " +
            //               " inner join " + owner + ".tticol008" + env + " col008 on col008.t$wmrb = col118.t$cwar " +
            //               " where trim(col118.t$item)=trim('" + strItem + "')" +
            //               " and upper(col118.t$clot)='" + (strLote == "" ? " " : strLote) + "'" +
            //               " and col118.t$cwar='" + strBodega + "' order by col118.T$DATR DESC)where rownum = 1";


            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", strItem);
            paramList.Add(":T$CLOT", strLote == "" ? " " : strLote);
            paramList.Add(":T$CWAR", strBodega);

            strSentenciaS = recursos.readStatement(method.ReflectedType.Name, "invLabel_registroImprimir_Param", ref owner, ref env, tabla, paramList);

            //select * from (select case trim(col118.t$ritm) when 'NA' then trim(col118.t$item) else trim(col118.t$ritm) end item, trim(ibd001.t$dsca) desci, trim(ibd001.t$cuni) unidad, col118.t$qtyr cantidad, " +
            //col118.t$clot lote, mcs005.t$dsca descr, substr(col118.t$obse,1,255) comentario, col020.t$pent factorkg, col118.t$datr-5/24 fecha, trim(col118.T$PAID) paid, col008.t$wreg wreg " +
            //from baan.tticol118140 col118
            //inner join baan.ttcibd001140 ibd001 on ibd001.t$item = col118.t$item 
            //left join baan.ttcibd001140 dbi001 on dbi001.t$item = col118.t$ritm
            //inner join baan.ttccol020140 col020 on col020.t$item = col118.t$item
            //inner join baan.ttcmcs005140 mcs005 on mcs005.t$cdis = col118.t$cdis 
            //inner join baan.tticol008140 col008 on col008.t$wmrb = col118.t$cwar
            //where trim(col118.t$item)=trim('[:T$ITEM]')
            //and upper(col118.t$clot)='[:T$CLOT"]'
            //and col118.t$cwar='[::T$CWAR]' order by col118.T$DATR DESC)where rownum = 1

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentenciaS, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error al buscar informacion para imprimir [tticol118]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
        public DataTable inv_datospesos(ref Ent_tticol118 Parametros, ref string strError)
        {
            string strSentenciaS = string.Empty;
            string strLote = Parametros.clot.Trim().ToUpperInvariant();
            string strBodega = Parametros.cwar.Trim().ToUpperInvariant();
            string strItem = Parametros.item.Trim();

            Dictionary<string, object> parametersOut = new Dictionary<string, object>();
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", strItem);
            paramList.Add(":T$CLOT", strLote == "" ? " " : strLote);
            paramList.Add(":T$CWAR", strBodega);

            strSentenciaS = recursos.readStatement(method.ReflectedType.Name, "invLabel_registroImprimir_Param2", ref owner, ref env, tabla, paramList);


            //strSentenciaS =  "select trim(ibd001.t$item), col020.t$pent factorkg, col008.t$wreg wreg " +
            //                "from " + owner + ".ttcibd001" + env + "  ibd001 " +
            //                "inner join " + owner + ".ttccol020" + env + "  col020 on col020.t$item = ibd001.t$item " +
            //                "inner join " + owner + ".tticol008" + env + "  col008 on col008.t$wmrb = '" + strBodega + "' " +
            //                "where trim(ibd001.t$item)=trim('" + strItem + "')";

            //select trim(ibd001.t$item), col020.t$pent factorkg, col008.t$wreg wreg
            //from baan.ttcibd001140  ibd001
            //inner join baan.ttccol020140  col020 on col020.t$item = ibd001.t$item
            //inner join baan.tticol008140  col008 on col008.t$wmrb = '[:T$CWAR]'
            //where trim(ibd001.t$item)=trim('[::T$ITEM]')

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentenciaS, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error al buscar informacion para imprimir [ttcibd001]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }
        public DataTable invRegrid_Indentifier(ref Ent_tticol118 Parametros, ref string strError)
        {
            string strSentenciaS = string.Empty;
            string strPaid = Parametros.paid.Trim().ToUpperInvariant();
            string strDisp = Parametros.disp.ToString();
          
            Dictionary<string, object> parametersOut = new Dictionary<string, object>();
            Dictionary<string, object> paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", strPaid);
            paramList.Add(":T$DISP", strDisp);

            strSentenciaS = recursos.readStatement(method.ReflectedType.Name, "invRegrid_Indentifier", ref owner, ref env, tabla, paramList);

            //strSentenciaS = "select count(*) cnt from " + owner + ".tticol118" + env + " WHERE T$PAID= '"+ strPaid +"' and T$DISP ="+strDisp +" ";
            //select count(*) cnt from BAAN.tticol118140 WHERE T$PAID= '[:T$PAID]' and T$DISP = '[:T$DISP]';

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentenciaS, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                strError = "Error al buscar informacion para imprimir [tticol118]. Try again or contact your administrator \n " + strSentencia;
                log.escribirError(strError + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol118 parametros, bool blnUsarPRetorno = false)
        {
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ITEM", DbType.String, parametros.item);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CLOT", DbType.String, parametros.clot);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$QTYR", DbType.Double, parametros.qtyr);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DQTYR", DbType.Decimal, parametros.dqtyr);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CDIS", DbType.String, parametros.cdis);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$OBSE", DbType.String, parametros.obse);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$LOGR", DbType.String, parametros.logr);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DATR", DbType.String, parametros.datr);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$DISP", DbType.Int32, parametros.disp);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$STOC", DbType.String, parametros.stoc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$RITM", DbType.String, parametros.ritm);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PROC", DbType.Int32, parametros.proc);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$MESS", DbType.String, parametros.mess);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SUNO", DbType.String, parametros.suno);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu); 
				Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PAID", DbType.String, parametros.paid);																									  

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
                log.escribirError(ex.Message, stackTrace.GetFrame(2).GetMethod().Name, metodo, method.ReflectedType.Name); 
                throw (ex);
            }
            return parameterCollection;
        }
    }
}









