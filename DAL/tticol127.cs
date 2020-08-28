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
    public class tticol127
    {
        private static Utilidades.Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method = MethodBase.GetCurrentMethod();
        private static string metodo = method.Name;
        private static Recursos recursos = new Recursos();

        Dictionary<string, object> paramList = new Dictionary<string, object>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSQL = string.Empty;
        String strwareh = string.Empty;

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = owner + ".tticol127" + env;


        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        public static string _idioma;


        DataTable consulta = new DataTable();

        public DataTable listaRegistro_ObtieneAlmacen(ref Ent_tticol127 Parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$USER", Parametros.user.Trim().ToUpperInvariant());

            strSQL = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "User has not defined the warehouse, please contact the administrator."; }
            }
            catch (Exception ex)
            {
                strError = "Error to the search sequence [tticol127]. Try again or contact your administrator \n ";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return consulta;

        }



        public DataTable Lista_Items()
        {
            //Verificacion del item digitado
            //strSQL =
            //    "select distinct ibd001.t$item AS item, ibd001.t$kltc as kltc" +
            //    " from " + owner + ".ttcibd001" + env + " ibd001 " +
            //    " left join " + owner + ".ttdipu001" + env + " ipu001 on ipu001.t$item = ibd001.t$item " +
            //    " left join " + owner + ".ttccom100" + env + " com100 on com100.t$bpid = ipu001.t$otbp ";
            strSQL = recursos.readStatement(method.ReflectedType.Name,"Lista_Items", ref owner, ref env, tabla,null);
            consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
            return consulta;
        }

        public  DataTable Lista_Lote(string clot)
        {
            //Verificacion del lote digitado
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CLOT", clot.Trim().ToUpper());

            //strSQL = "select distinct ltc100.t$clot lot from " + owner + ".twhltc100" + env + " ltc100 where trim(upper(ltc100.t$clot)) ='" + clot.Trim().ToUpper() + "'";
            //select distinct ltc100.t$clot lot from baan.twhltc100140 ltc100 where trim(upper(ltc100.t$clot)) ='[:T$CLOT]'
            strSQL = recursos.readStatement(method.ReflectedType.Name, "Lista_Lote", ref owner, ref env, tabla, paramList);
            consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
            return consulta;
        }
        public DataTable listaRegistrosOrden_ParamMRB(ref Ent_tticol127 ParametrosIn, ref string strError, ref string strOrden, bool print = false)
        {

            //strSQL = "SELECT RTRIM(col127.t$cwar) waremrb FROM " + owner + ".tticol127" + env + " col127 " +
            //                "WHERE upper(col127.t$user) = '" + ParametrosIn.user + "'";

            //Dictionary<string, object> parametersOut = new Dictionary<string, object>();

            try
            {
                //consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                //if (consulta.Rows.Count < 1)
                //{
                //    strError = "User doesn't have warehouse MRB Setting.";
                //}
                //else
                //{
                strwareh = ParametrosIn.cwar.ToUpper();
                //Valido el item digitado

                paramList = new Dictionary<string, object>();
                paramList.Add(":T$ITEM", ParametrosIn.item.ToUpper());

                //strSQL =
                //    "select ibd001.t$item AS item, ibd001.t$dsca AS descitem, ibd001.t$cuni AS unit, ibd001.t$cwar  AS ware, ibd001.t$kltc as mlot, ipu001.t$otbp as proveedor, com100.t$nama as nombre,col020.t$pent AS factorkg " +
                //    " from " + owner + ".ttcibd001" + env + " ibd001 " +
                //    " left join " + owner + ".ttdipu001" + env + " ipu001 on ipu001.t$item = ibd001.t$item " +
                //    " left join " + owner + ".ttccom100" + env + " com100 on com100.t$bpid = ipu001.t$otbp " +
                //    " left join " + owner + ".ttccol020" + env + " col020 on col020.t$item = ibd001.t$item " +
                //    "where trim(ibd001.t$item)=TRIM('" + ParametrosIn.item.ToUpper() + "')";
               
                //"select ibd001.t$item AS item, ibd001.t$dsca AS descitem, ibd001.t$cuni AS unit, ibd001.t$cwar  AS ware, ibd001.t$kltc as mlot, ipu001.t$otbp as proveedor, com100.t$nama as nombre,col020.t$pent AS factorkg " +
                //" from " + owner + ".ttcibd001" + env + " ibd001 " +
                //" left join BAAN.ttdipu001140 ipu001 on ipu001.t$item = ibd001.t$item " +
                //" left join BAAN.ttccom100140 com100 on com100.t$bpid = ipu001.t$otbp " +
                //" left join BAAN.ttccol020140 col020 on col020.t$item = ibd001.t$item " +
                //"where trim(ibd001.t$item)=TRIM('[:T$ITEM]')"

                strSQL = recursos.readStatement(method.ReflectedType.Name, "listaRegistrosOrden_ParamMRB", ref owner, ref env, tabla, paramList);
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);



                if (string.IsNullOrEmpty(consulta.Rows[0]["factorkg"].ToString()))
                {
                    strError = _textoLabels.readStatement(formName, _idioma, "lblFactorNull");
                }


                if (consulta.Rows.Count < 1)
                {

                    strError = "Item doesn't Exist in Baan.";
                }
                else
                {

                    if (consulta.Rows[0]["MLOT"].ToString() == "1")
                    {
                        paramList = new Dictionary<string, object>();
                        paramList.Add(":T$ITEM", ParametrosIn.item.ToUpper());
                        paramList.Add(":T$CLOT", ParametrosIn.lote.ToUpper());
                        //Validar el lote digitado
                        //strSQL = "select ltc100.t$clot lot, ltc100.t$orno orden from " + owner + ".twhltc100" + env + " ltc100 " +
                        //         "where trim(ltc100.t$item)='" + ParametrosIn.item.ToUpper() + "' " +
                        //         "and trim(ltc100.t$clot)='" + ParametrosIn.lote.ToUpper() + "'";
                        
                        //"select ltc100.t$clot lot, ltc100.t$orno orden from BAAN.twhltc100140 ltc100 " +
                        //"where trim(ltc100.t$item)='[:T$ITEM]' " +
                        //"and trim(ltc100.t$clot)='[:T$CLOT]'"

                        strSQL = recursos.readStatement(method.ReflectedType.Name, "selecttwhltc100", ref owner, ref env, tabla, paramList);
                        consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                        if (consulta.Rows.Count < 1)
                        {
                            strError = "Lot doesn't Exist.";
                        }
                        else
                        {
                            strOrden = consulta.Rows[0]["orden"].ToString();
                            //Validar Cantidad Item, Warehouse, Lot y bodega MRB
                            //strSQL = "select  inr140.t$stks stock, inr140.t$cwar warehouse, inr140.t$item item, inr140.t$clot lot, ibd001.t$dsca desci, ibd001.t$cuni unidad, " +
                            //         "ipu001.t$otbp proveedor, com100.t$nama nombre " +
                            //         "from " + owner + ".twhinr140" + env + " inr140 " +
                            //         "inner join " + owner + ".ttcibd001" + env + " ibd001 on ibd001.t$item = inr140.t$item " +
                            //         "left join " + owner + ".ttdipu001" + env + " ipu001 on ipu001.t$item = ibd001.t$item " +
                            //         "left join " + owner + ".ttccom100" + env + " com100 on com100.t$bpid = ipu001.t$otbp " +
                            //         "where trim(inr140.t$item)=trim('" + ParametrosIn.item.ToUpper() + "') " +
                            //         "and trim(inr140.t$clot)=trim('" + ParametrosIn.lote.ToUpper() + "') " +
                            //         "and trim(inr140.t$cwar) = trim('" + strwareh.ToUpper() + "')";

                            paramList = new Dictionary<string, object>();
                            paramList.Add(":T$ITEM", ParametrosIn.item.ToUpper());
                            paramList.Add(":T$CLOT", ParametrosIn.lote.ToUpper());
                            paramList.Add(":T$CWAR", strwareh.ToUpper());

                            //"select  inr140.t$stks stock, inr140.t$cwar warehouse, inr140.t$item item, inr140.t$clot lot, ibd001.t$dsca desci, ibd001.t$cuni unidad, " +
                            //"ipu001.t$otbp proveedor, com100.t$nama nombre " +
                            //"from baan.twhinr140140 inr140 " +
                            //"inner join bann.ttcibd001140 ibd001 on ibd001.t$item = inr140.t$item " +
                            //"left join bann.ttdipu001140 ipu001 on ipu001.t$item = ibd001.t$item " +
                            //"left join bann.ttccom100140 com100 on com100.t$bpid = ipu001.t$otbp " +
                            //"where trim(inr140.t$item)=trim('[:T$ITEM]') " +
                            //"and trim(inr140.t$clot)=trim('[:T$CLOT]') " +
                            //"and trim(inr140.t$cwar) = trim('[:T$CWAR]')";

                            strSQL = recursos.readStatement(method.ReflectedType.Name, "selecttwhinr140", ref owner, ref env, tabla, paramList);
                            consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                            if (consulta.Rows.Count < 1)
                            {
                                strError = "Item doesn't have stock on MRB Warehouse.";
                            }
                        }
                        return consulta;
                    }
                    else
                    {
                        //Validar Cantidad Item, Warehouse y bodega MRB
                        paramList = new Dictionary<string, object>();
                        paramList.Add(":T$ITEM", ParametrosIn.item.ToUpper());
                        paramList.Add(":T$CWAR", strwareh.ToUpper());

                        //strSQL = "select  inr140.t$stks stock, inr140.t$cwar warehouse, inr140.t$item item, inr140.t$clot lot, ibd001.t$dsca desci, ibd001.t$cuni unidad, " +
                        //         "ipu001.t$otbp proveedor, com100.t$nama nombre " +
                        //         "from " + owner + ".twhinr140" + env + " inr140 " +
                        //         "inner join " + owner + ".ttcibd001" + env + " ibd001 on ibd001.t$item = inr140.t$item " +
                        //         "left join " + owner + ".ttdipu001" + env + " ipu001 on ipu001.t$item = ibd001.t$item " +
                        //         "left join " + owner + ".ttccom100" + env + " com100 on com100.t$bpid = ipu001.t$otbp " +
                        //         "where trim(inr140.t$item)=trim('" + ParametrosIn.item.ToUpper() + "') " +
                        //         "and trim(inr140.t$cwar) = trim('" + strwareh.ToUpper() + "')";

                        //select  inr140.t$stks stock, inr140.t$cwar warehouse, inr140.t$item item, inr140.t$clot lot, ibd001.t$dsca desci, ibd001.t$cuni unidad,
                        //ipu001.t$otbp proveedor, com100.t$nama nombre
                        //from baan.twhinr140140 inr140
                        //inner join baan.ttcibd001140 ibd001 on ibd001.t$item = inr140.t$item
                        //left join baan.ttdipu001140 ipu001 on ipu001.t$item = ibd001.t$item
                        //left join baan.ttccom100140 com100 on com100.t$bpid = ipu001.t$otbp
                        //where trim(inr140.t$item)=trim('[:T$ITEM]')
                        //and trim(inr140.t$cwar) = trim('[:T$CWAR]')

                        strSQL = recursos.readStatement(method.ReflectedType.Name, "selecttwhinr140el", ref owner, ref env, tabla, paramList);
                        consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                        if (consulta.Rows.Count < 1)
                        {
                            strError = "Item doesn't have stock on MRB Warehouse.";
                        }
                    }
                    return consulta;
                }
                //}
            }
            catch (Exception ex)
            { log.escribirError(strError + " - " + ex.Message, stackTrace.GetFrame(2).GetMethod().Name, metodo, method.ReflectedType.Name); }// throw ex; 
            return consulta;
        }

        public DataTable listaRegistrosOrden_Param(ref Ent_tticol127 ParametrosIn, ref string strError, ref string strOrden, bool print = false)
        {
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$USER", ParametrosIn.user);

            //strSQL = "SELECT RTRIM(col127.t$cwar) waremrb FROM " + owner + ".tticol127" + env + " col127 " +
            //                "WHERE upper(col127.t$user) = '" + ParametrosIn.user + "'";

            //SELECT RTRIM(col127.t$cwar) waremrb FROM BAAN.tticol127140 col127 
            //WHERE upper(col127.t$user) = '[:T$USER]'
            strSQL = recursos.readStatement(method.ReflectedType.Name, "listaRegistrosOrden_Param", ref owner, ref env, tabla, paramList);
            Dictionary<string, object> parametersOut = new Dictionary<string, object>();

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1)
                {
                    strError = "User doesn't have warehouse MRB Setting.";
                }
                else
                {
                    strwareh = consulta.Rows[0]["WAREMRB"].ToString().ToUpper();
                    //Valido el item digitado
                    paramList = new Dictionary<string, object>();
                    paramList.Add(":T$ITEM", ParametrosIn.item.ToUpper());

                    //strSQL =
                    //    "select ibd001.t$item AS item, ibd001.t$dsca AS descitem, ibd001.t$cuni AS unit, ibd001.t$cwar  AS ware, ibd001.t$kltc as mlot, ipu001.t$otbp as proveedor, com100.t$nama as nombre,col020.t$pent AS factorkg " +
                    //    " from " + owner + ".ttcibd001" + env + " ibd001 " +
                    //    " left join " + owner + ".ttdipu001" + env + " ipu001 on ipu001.t$item = ibd001.t$item " +
                    //    " left join " + owner + ".ttccom100" + env + " com100 on com100.t$bpid = ipu001.t$otbp " +
                    //    " left join " + owner + ".ttccol020" + env + " col020 on col020.t$item = ibd001.t$item " + 
                    //    "where trim(ibd001.t$item)=TRIM('" + ParametrosIn.item.ToUpper() + "')";

                    //select ibd001.t$item AS item, ibd001.t$dsca AS descitem, ibd001.t$cuni AS unit, ibd001.t$cwar  AS ware, ibd001.t$kltc as mlot, ipu001.t$otbp as proveedor, com100.t$nama as nombre,col020.t$pent AS factorkg
                    //from BAAN.ttcibd001140 ibd001
                    //left join BAAN.ttdipu001140 ipu001 on ipu001.t$item = ibd001.t$item
                    //left join BAAN.ttccom100140 com100 on com100.t$bpid = ipu001.t$otbp
                    //left join BAAN.ttccol020140 col020 on col020.t$item = ibd001.t$item 
                    //where trim(ibd001.t$item)=TRIM('[:T$ITEM]')

                    strSQL = recursos.readStatement(method.ReflectedType.Name, "selectttcibd001", ref owner, ref env, tabla, paramList);
                    consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);



                    if (string.IsNullOrEmpty(consulta.Rows[0]["factorkg"].ToString()))
                    {
                        strError = _textoLabels.readStatement(formName, _idioma, "lblFactorNull");
                    }
                    

                    if (consulta.Rows.Count < 1)
                    {

                        strError = "Item doesn't Exist in Baan.";
                    }
                    else
                    {

                        if (consulta.Rows[0]["MLOT"].ToString() == "1")
                        {
                            paramList = new Dictionary<string, object>();
                            paramList.Add(":T$ITEM", ParametrosIn.item.ToUpper());
                            paramList.Add(":T$CLOT", ParametrosIn.lote.ToUpper());
                            //Validar el lote digitado
                            //strSQL = "select ltc100.t$clot lot, ltc100.t$orno orden from " + owner + ".twhltc100" + env + " ltc100 " +
                            //         "where trim(ltc100.t$item)='" + ParametrosIn.item.ToUpper() + "' " +
                            //         "and trim(ltc100.t$clot)='" + ParametrosIn.lote.ToUpper() + "'";

                            //select ltc100.t$clot lot, ltc100.t$orno orden from BAAN.twhltc100140 ltc100
                            //where trim(ltc100.t$item)='[:T$ITEM]'
                            //and trim(ltc100.t$clot)='[:T$CLOT]'

                            strSQL = recursos.readStatement(method.ReflectedType.Name, "selecttwhltc100", ref owner, ref env, tabla, paramList);
                            consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                            if (consulta.Rows.Count < 1)
                            {
                                strError = "Lot doesn't Exist.";
                            }
                            else
                            {
                                strOrden = consulta.Rows[0]["orden"].ToString();
                                //Validar Cantidad Item, Warehouse, Lot y bodega MRB
                                //strSQL = "select  inr140.t$stks stock, inr140.t$cwar warehouse, inr140.t$item item, inr140.t$clot lot, ibd001.t$dsca desci, ibd001.t$cuni unidad, " +
                                //         "ipu001.t$otbp proveedor, com100.t$nama nombre " +
                                //         "from " + owner + ".twhinr140" + env + " inr140 " + 
                                //         "inner join " + owner + ".ttcibd001" + env + " ibd001 on ibd001.t$item = inr140.t$item " +
                                //         "left join " + owner + ".ttdipu001" + env + " ipu001 on ipu001.t$item = ibd001.t$item " +
                                //         "left join " + owner + ".ttccom100" + env + " com100 on com100.t$bpid = ipu001.t$otbp " +
                                //         "where trim(inr140.t$item)=trim('" + ParametrosIn.item.ToUpper() + "') " +
                                //         "and trim(inr140.t$clot)=trim('" + ParametrosIn.lote.ToUpper() + "') " +
                                //         "and trim(inr140.t$cwar) = trim('" + strwareh.ToUpper() + "')";

                                paramList = new Dictionary<string, object>();
                                paramList.Add(":T$ITEM", ParametrosIn.item.ToUpper());
                                paramList.Add(":T$CLOT", ParametrosIn.lote.ToUpper());
                                paramList.Add(":T$CWAR", ParametrosIn.lote.ToUpper());

                                //select  inr140.t$stks stock, inr140.t$cwar warehouse, inr140.t$item item, inr140.t$clot lot, ibd001.t$dsca desci, ibd001.t$cuni unidad,
                                //ipu001.t$otbp proveedor, com100.t$nama nombre
                                //from baan.twhinr140140 inr140
                                //inner join baan.ttcibd001140 ibd001 on ibd001.t$item = inr140.t$item 
                                //left join baan.ttdipu001140 ipu001 on ipu001.t$item = ibd001.t$item
                                //left join baan.ttccom100140 com100 on com100.t$bpid = ipu001.t$otbp
                                //where trim(inr140.t$item)=trim('[:T$ITEM]')
                                //and trim(inr140.t$clot)=trim('[:T$CLOT]')
                                //and trim(inr140.t$cwar) = trim('[:T$CWAR]')

                                strSQL = recursos.readStatement(method.ReflectedType.Name, "selecttwhinr140ttcibd001", ref owner, ref env, tabla, paramList);
                                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                                if (consulta.Rows.Count < 1)
                                {
                                    strError = "Item doesn't have stock on MRB Warehouse.";
                                }
                            }
                            return consulta;
                        }
                        else
                        {
                            paramList = new Dictionary<string, object>();
                            paramList.Add(":T$ITEM", ParametrosIn.item.ToUpper());
                            paramList.Add(":T$CWAR", strwareh.ToUpper());

                            //Validar Cantidad Item, Warehouse y bodega MRB
                            //strSQL = "select  inr140.t$stks stock, inr140.t$cwar warehouse, inr140.t$item item, inr140.t$clot lot, ibd001.t$dsca desci, ibd001.t$cuni unidad, " +
                            //         "ipu001.t$otbp proveedor, com100.t$nama nombre " +
                            //         "from " + owner + ".twhinr140" + env + " inr140 " +
                            //         "inner join " + owner + ".ttcibd001" + env + " ibd001 on ibd001.t$item = inr140.t$item " +
                            //         "left join " + owner + ".ttdipu001" + env + " ipu001 on ipu001.t$item = ibd001.t$item " +
                            //         "left join " + owner + ".ttccom100" + env + " com100 on com100.t$bpid = ipu001.t$otbp " +
                            //         "where trim(inr140.t$item)=trim('" + ParametrosIn.item.ToUpper() + "') " +
                            //         "and trim(inr140.t$cwar) = trim('" + strwareh.ToUpper() + "')";

                            //select  inr140.t$stks stock, inr140.t$cwar warehouse, inr140.t$item item, inr140.t$clot lot, ibd001.t$dsca desci, ibd001.t$cuni unidad,
                            //ipu001.t$otbp proveedor, com100.t$nama nombre
                            //from BAAN.twhinr140140 inr140
                            //inner join BAAN.ttcibd001140 ibd001 on ibd001.t$item = inr140.t$item
                            //left join  BAAN.ttdipu001140 ipu001 on ipu001.t$item = ibd001.t$item 
                            //left join  BAAN.ttccom100140 com100 on com100.t$bpid = ipu001.t$otbp
                            //where trim(inr140.t$item)=trim('[:T$ITEM]')
                            //and trim(inr140.t$cwar) = trim('[:T$CWAR]')

                            strSQL = recursos.readStatement(method.ReflectedType.Name, "selecttwhinr140com100", ref owner, ref env, tabla, paramList);
                            consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSQL, ref parametersOut, null, true);
                            if (consulta.Rows.Count < 1)
                            {
                                strError = "Item doesn't have stock on MRB Warehouse.";
                            }
                        }
                        return consulta;
                    }
                }
            }
            catch (Exception ex)
            { log.escribirError(strError + " - " + ex.Message, stackTrace.GetFrame(2).GetMethod().Name, metodo, method.ReflectedType.Name); }// throw ex; 
            return consulta;
        }
        
        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_tticol127 parametros, bool blnUsarPRetorno = false)
        {
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$USER", DbType.String, parametros.user);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);
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
                log.escribirError(ex.Message, stackTrace.GetFrame(2).GetMethod().Name, metodo, method.ReflectedType.Name);
            }
            return parameterCollection;
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunesUpdate(Ent_tticol127 parametros, bool blnUsarPRetorno = false)
        {
            string param = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$USER", DbType.String, parametros.user);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$CWAR", DbType.String, parametros.cwar);

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

                // throw (ex);
            }
            return parameterCollection;
        }    
    }
}
