using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using System.Data;
using System.Reflection;
using whusa.Utilidades;
using System.Diagnostics;
using System.Configuration;
using whusa.Interfases;

namespace whusa.DAL
{
    public class tticol082
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
        private static string tabla = owner + ".tticol082" + env;

        public tticol082()
        {
            //Constructor
        }

        public bool InsertarregistroItticol082(Entidades.Ent_tticol082 Objtticol082)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$OORG", Objtticol082.OORG);
                paramList.Add(":T$ORNO", Objtticol082.ORNO);
                paramList.Add(":T$OSET", Objtticol082.OSET);
                paramList.Add(":T$PONO", Objtticol082.PONO);
                paramList.Add(":T$SQNB", Objtticol082.SQNB);
                paramList.Add(":T$ADVS", Objtticol082.ADVS);

                paramList.Add(":T$ITEM", Objtticol082.ITEM);
                paramList.Add(":T$STAT", Objtticol082.STAT);
                paramList.Add(":T$QTYT", Objtticol082.QTYT);
                paramList.Add(":T$CWAR", Objtticol082.CWAR);
                paramList.Add(":T$UNIT", Objtticol082.UNIT);
                paramList.Add(":T$PRIO", Objtticol082.PRIO);
                paramList.Add(":T$PAID", Objtticol082.PAID);
                paramList.Add(":T$LOGN", Objtticol082.LOGN);
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }



        public DataTable ConsultarTticol082()
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            DataTable retrotno = new DataTable();
            try
            {
                paramList = new Dictionary<string, object>();
                //paramList.Add(":T$OORG", Objtticol082.OORG);
                //paramList.Add(":T$ORNO", Objtticol082.ORNO);
                //paramList.Add(":T$OSET", Objtticol082.OSET);
                //paramList.Add(":T$PONO", Objtticol082.PONO);
                //paramList.Add(":T$SQNB", Objtticol082.SQNB);
                //paramList.Add(":T$ADVS", Objtticol082.ADVS);

                //paramList.Add(":T$ITEM", Objtticol082.ITEM);
                //paramList.Add(":T$STAT", Objtticol082.STAT);
                //paramList.Add(":T$QTYT", Objtticol082.QTYT);
                //paramList.Add(":T$CWAR", Objtticol082.CWAR);
                //paramList.Add(":T$UNIT", Objtticol082.UNIT);
                //paramList.Add(":T$PRIO", Objtticol082.PRIO);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retrotno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retrotno;
        }

        public int PrioridadMaxima()
        {
            method = MethodBase.GetCurrentMethod();
            int retorno = 0;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                DataTable DataResult = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
                if (DataResult.Rows.Count > 0)
                {
                    retorno = Convert.ToInt32(DataResult.Rows[0]["PRIO"].ToString());
                }
                else
                {
                    retorno = 0;
                }
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool ActualizarPrioridadTticol082(Ent_tticol082 Objtticol082)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$OORG", Objtticol082.OORG);
                paramList.Add(":T$ORNO", Objtticol082.ORNO);
                paramList.Add(":T$OSET", Objtticol082.OSET);
                paramList.Add(":T$PONO", Objtticol082.PONO);
                paramList.Add(":T$SQNB", Objtticol082.SQNB);
                paramList.Add(":T$ADVS", Objtticol082.ADVS);

                paramList.Add(":T$ITEM", Objtticol082.ITEM);
                paramList.Add(":T$STAT", Objtticol082.STAT);
                paramList.Add(":T$QTYT", Objtticol082.QTYT);
                paramList.Add(":T$CWAR", Objtticol082.CWAR);
                paramList.Add(":T$UNIT", Objtticol082.UNIT);
                paramList.Add(":T$PRIO", Objtticol082.PRIT);
                paramList.Add(":T$TIME", Objtticol082.TIME);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }



        public bool InsertarregistroItticol093(Ent_tticol082 Objtticol082)
        {

            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {


                paramList = new Dictionary<string, object>();
                paramList.Add(":T$OORG", Objtticol082.OORG);
                paramList.Add(":T$ORNO", Objtticol082.ORNO);
                paramList.Add(":T$OSET", Objtticol082.OSET);
                paramList.Add(":T$PONO", Objtticol082.PONO);
                paramList.Add(":T$SEQN", Objtticol082.SQNB);
                paramList.Add(":T$ADVS", Objtticol082.ADVS);
                paramList.Add(":T$LOGN", Objtticol082.LOGN);
                paramList.Add(":T$PRIO", Objtticol082.PRIO);
                paramList.Add(":T$PRIT", Objtticol082.PRIT);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public DataTable ConsultaPlantTticol082()
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public DataTable ConsultaWarehouseTticol082( string plant )
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();
            string strError = string.Empty;

            try
            {


                paramList = new Dictionary<string, object>();
                paramList.Add(":T$TITE", plant);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public object ConsultaWarehouseTticol082(string plant, string warehouse)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public DataTable ConsultarTticol082(string plant)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            DataTable retrotno = new DataTable();
            try
            {
                paramList = new Dictionary<string, object>();


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retrotno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retrotno;
        }

        public DataTable ConsultarTticol082PorPlant(string plant,string warehouse,string machine)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            DataTable retrotno = new DataTable();
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$TITE", plant);
                paramList.Add(":T$CWAR", warehouse);
                paramList.Add(":T$MCNO", machine);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retrotno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retrotno;
        }

        public DataTable ConsultarTticol082PorPlantPono(string plant,int prio,string advs)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            DataTable retrotno = new DataTable();
            try
            {
                paramList = new Dictionary<string, object>();
                //paramList.Add(":T$TITE", plant);
                paramList.Add(":T$PRIO", prio);
                //paramList.Add(":T$ADVS", advs);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retrotno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retrotno;
        }

        public DataTable ConsultarPalletIDTticol082(string PalletID)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            DataTable retrotno = new DataTable();
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", PalletID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retrotno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retrotno;
        }

        public DataTable ConsultarPalletIDTticol083(string PalletID)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            DataTable retrotno = new DataTable();
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", PalletID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retrotno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retrotno;
        }

        public bool Actualizartticol222(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }
                  
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
        public bool Actualizartticol022(Ent_tticol082 MyObj)
        {

            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
        public bool Actualizartticol082(Ent_tticol082 MyObj)
        {

            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
        public bool Actualizartwhcol131(Ent_tticol082 MyObj)
        {

            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
        public bool Actualizartwhcol130(Ent_tticol082 MyObj)
        {

            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
        public bool Actualizartticol042(Ent_tticol082 MyObj)
        {

            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }


        public DataTable ConsultarPalletIDTticol082MFG(string PalletID)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            DataTable retrotno = new DataTable();
            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", PalletID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retrotno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retrotno;
        }

        public bool Actualizartticol022MFG(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartticol042MFG(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartticol082MFG(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartticol242MFG(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartticol222MFG(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartwhcol131MFG(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
        public bool Actualizartticol083MFG(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
        public bool Actualizartwhcol130MFG(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartticol242(Ent_tticol082 myObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", myObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public DataTable ConsultarRegistrosBloquedos()
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            DataTable retrotno = new DataTable();
            try
            {
                paramList = new Dictionary<string, object>();


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retrotno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retrotno;
        }

        public bool Actualizartticol022STAT(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartticol042STAT(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartwhcol131STAT(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno =  DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartwhcol130STAT(Ent_tticol082 MyObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", MyObj.PAID);


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public DataTable ConsultarOtrosRegistros()
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            DataTable retrotno = new DataTable();
            try
            {
                paramList = new Dictionary<string, object>();


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retrotno = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retrotno;
        }

        public bool Actualizartticol222Cant(Ent_tticol082 myObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", myObj.PAID);
                paramList.Add(":QTYA", myObj.QTYC);
                


                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartticol242Cant(Ent_tticol082 myObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", myObj.PAID);
                paramList.Add(":QTYA", myObj.QTYC);



                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartwhcol130Cant(Ent_tticol082 myObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", myObj.PAID);
                paramList.Add(":QTYA", myObj.QTYC);



                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }

        public bool Actualizartwhcol131Cant(Ent_tticol082 myObj)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;
            string strError = string.Empty;

            try
            {

                paramList = new Dictionary<string, object>();
                paramList.Add(":PAID", myObj.PAID);
                paramList.Add(":QTYA", myObj.QTYC);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }



        public object ConsultaMachineTticol082(string plant, string warehouse)
        {
            method = MethodBase.GetCurrentMethod();
            DataTable retorno = new DataTable();
            string strError = string.Empty;

            try
            {


                paramList = new Dictionary<string, object>();
                paramList.Add(":T$TITE", plant);
                paramList.Add(":T$CWAR", warehouse);

                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);
                return DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, false);
            }

            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;

            }
            return retorno;
        }
    }
}
