using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Utilidades;
using System.Diagnostics;
using System.Reflection;
using whusa.Entidades;
using System.Data;
using System.Configuration;
using whusa;

namespace whusa.Interfases
{
    public class transfer
    {
        private static Seguimiento log = new Seguimiento();
        private static StackTrace stackTrace = new StackTrace();
        private static MethodBase method;
        private static Recursos recursos = new Recursos();

        List<Ent_ParametrosDAL> parametrosIn = new List<Ent_ParametrosDAL>();
        Dictionary<string, object> paramList = new Dictionary<string, object>();
        Dictionary<string, object> parametersOut = new Dictionary<string, object>();
        String strSentencia = string.Empty;
        String strSentenciaAux = string.Empty;
        DataTable consulta = new DataTable();

        private static String env = ConfigurationManager.AppSettings["env"].ToString();
        private static String owner = ConfigurationManager.AppSettings["owner"].ToString();
        private static string tabla = "transfer";

        public transfer()
        {
            //Constructor
        }

        public DataTable ConsultarRegistroTransferir(string PAID)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$PAID", PAID.Trim().ToUpper());
            DataTable retorno = new DataTable();

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retorno;
        }

        public bool InsertarTransferencia(Ent_twhcol020 objWhcol020)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            //paramList.Add(":T$PAID", PAID.Trim().ToUpper());

            paramList.Add(":T$CLOT", objWhcol020.clot.Trim().ToUpper());//objWhcol020.clot.Trim().ToUpper());
            paramList.Add(":T$SQNB", objWhcol020.sqnb.Trim().ToUpper());
            paramList.Add(":T$MITM", objWhcol020.mitm.ToUpper());
            paramList.Add(":T$DSCA", objWhcol020.dsca.Trim().ToUpper());//objWhcol020.dsca.Trim().ToUpper());
            paramList.Add(":T$CWOR", objWhcol020.cwor.Trim().ToUpper());
            paramList.Add(":T$LOOR", objWhcol020.loor.Trim().ToUpper() == "" ? " " : objWhcol020.loor.Trim().ToUpper());
            paramList.Add(":T$CWDE", objWhcol020.cwde.Trim().ToUpper() == "" ? " " : objWhcol020.cwde.Trim().ToUpper());
            paramList.Add(":T$LODE", objWhcol020.lode.Trim().ToUpper() == "" ? " " : objWhcol020.lode.Trim().ToUpper());
            paramList.Add(":T$QTDL", objWhcol020.qtdl);
            paramList.Add(":T$CUNI", objWhcol020.cuni.Trim());
            paramList.Add(":T$RCNO", " ");
            paramList.Add(":T$DATE", "");// objWhcol020.date.Trim().ToUpper());
            paramList.Add(":T$MESS", " ");// objWhcol020.mess.Trim().ToUpper());
            paramList.Add(":T$USER", objWhcol020.user.ToUpper());
            paramList.Add(":T$REFCNTD", 0);
            paramList.Add(":T$REFCNTU", 0);


            bool retorno = false;

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retorno;
        }

        public bool ActualizarTransferencia(Ent_twhcol020 objWhcol020)
        {

            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();

            paramList.Add(":T$CWAR", objWhcol020.cwde.Trim().ToUpper() == "" ? " " : objWhcol020.cwde.Trim().ToUpper());
            paramList.Add(":T$LOCA", objWhcol020.lode.Trim().ToUpper() == "" ? " " : objWhcol020.lode.Trim().ToUpper());
            paramList.Add(":T$PAID", objWhcol020.sqnb.Trim().ToUpper());


            switch (objWhcol020.tbl.Trim().ToLower())
            {
                //case "ticol022":
                //    strSentenciaAux = recursos.readStatement(method.ReflectedType.Name, "Actualizatticol022", ref owner, ref env, tabla, paramList);                   
                //    ActualizarTticol222(objWhcol020);
                //    break;
                //case "whcol130":
                //    strSentenciaAux = recursos.readStatement(method.ReflectedType.Name, "Actualizatwhcol130", ref owner, ref env, tabla, paramList);
                //    break;
                //case "whcol131":
                //    strSentenciaAux = recursos.readStatement(method.ReflectedType.Name, "Actualizatwhcol131", ref owner, ref env, tabla, paramList);
                //    break;
                //case "ticol042":
                //    strSentenciaAux = recursos.readStatement(method.ReflectedType.Name, "Actualizatticol042", ref owner, ref env, tabla, paramList);                    
                //    ActualizarTticol242(objWhcol020);
                //    break;
                //case "whcol016":
                //    strSentenciaAux = recursos.readStatement(method.ReflectedType.Name, "Actualizatwhcol016", ref owner, ref env, tabla, paramList);
                //    break;
            }


            return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentenciaAux, ref parametersOut, null, false);
        }

        public bool ActualizarTticol222(Ent_twhcol020 objWhcol020)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAT", objWhcol020.cwde.Trim().ToUpper() == "" ? " " : objWhcol020.cwde.Trim().ToUpper());
            paramList.Add(":T$ACLO", objWhcol020.lode.Trim().ToUpper() == "" ? " " : objWhcol020.lode.Trim().ToUpper());
            paramList.Add(":T$SQNB", objWhcol020.sqnb.Trim().ToUpper());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
        }

        public bool ActualizarTticol242(Ent_twhcol020 objWhcol020)
        {
            method = MethodBase.GetCurrentMethod();
            paramList = new Dictionary<string, object>();
            paramList.Add(":T$CWAT", objWhcol020.cwde.Trim().ToUpper() == "" ? " " : objWhcol020.cwde.Trim().ToUpper());
            paramList.Add(":T$ACLO", objWhcol020.lode.Trim().ToUpper() == "" ? " " : objWhcol020.lode.Trim().ToUpper());
            paramList.Add(":T$SQNB", objWhcol020.sqnb.Trim().ToUpper());
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
            return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
        }

        public DataTable ListWarehouses()
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            DataTable retorno = new DataTable();

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            return DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);

        }

        public DataTable ConsultarLocation(string CWAR, string LOCA)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();

            paramList = new Dictionary<string, object>();

            paramList.Add(":T$CWAR", CWAR.Trim().ToUpper());
            paramList.Add(":T$LOCA", LOCA.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retorno;
        }

        public string DescripcionItem(string ITEM)
        {
            method = MethodBase.GetCurrentMethod();
            string retorno = string.Empty;

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$ITEM", ITEM.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                DataTable DescripcionItems = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
                if (DescripcionItems.Rows.Count > 0)
                {
                    retorno = DescripcionItems.Rows[0]["DSCA"].ToString().Trim().ToUpper();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retorno;
        }

        public DataTable ConsultaTransferencia(string PAID)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();

            paramList = new Dictionary<string, object>();

            paramList.Add(":T$SQNB", PAID.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retorno;
        }

        public bool ActualizarTransferencia(string PAID, string CurrentWarehouse, string CurrentLocation, string TargetWarehouse, string TargetLocation,string USER)
        {

            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();


            paramList.Add(":T$SQNB", PAID.Trim().ToUpper());

            paramList.Add(":T$CWOR", CurrentWarehouse.Trim().ToUpper() == "" ? " " : CurrentWarehouse.Trim().ToUpper());
            paramList.Add(":T$LOOR", CurrentLocation.Trim().ToUpper() == "" ? " " : CurrentLocation.Trim().ToUpper());
            paramList.Add(":T$CWDE", TargetWarehouse.Trim().ToUpper() == "" ? " " : TargetWarehouse.Trim().ToUpper());
            paramList.Add(":T$LODE", TargetLocation.Trim().ToUpper() == "" ? " " : TargetLocation.Trim().ToUpper());


            paramList.Add(":T$USER", USER);

            bool retorno = false;

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retorno;
        }

        public DataTable ConsultarWarehouse(string LOCA)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();

            paramList = new Dictionary<string, object>();
            paramList.Add(":T$LOCA", LOCA.Trim().ToUpper());

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return retorno;
        }
    }
}
