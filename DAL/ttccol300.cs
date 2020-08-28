using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection;
using System.Security.Principal;
using SendMailService.Models;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusa.DAL
{
    public class ttccol300
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
        private static string tabla = owner + ".ttccol300" + env;

        static ttccol300() 
        { 
            //Constructor
        }
        
        public int insertarRegistro(ref List<Ent_ttccol300> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);


                foreach (Ent_ttccol300 reg in parametros)
                {
                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                //Comentado CChaverra 11052017
                //strError = "Error when inserting data [ttccol300]. Try again or contact your administrator \n";
                //log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                
            }

            return Convert.ToInt32(retorno);
        }

        public int actualizarRegistro(ref List<Ent_ttccol300> parametros, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla);
                string strCondicion = string.Empty;

                foreach (Ent_ttccol300 reg in parametros)
                {
                    strSentencia += " WHERE UPPER(TRIM(t$user))='" + reg.user + "'";

                    parametrosIn = AdicionaParametrosComunes(reg);
                    retorno = DAL.BaseDAL.BaseDal.EjecutarCrud("text", strSentencia, ref parametersOut, parametrosIn, false);
                }
                return Convert.ToInt32(retorno);
            }

            catch (Exception ex)
            {
                //Comentado CChaverra 11052017
                //strError += "Error updating data[ttccol300]. Try again or contact your administrator";
                //log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
            }

            return 0;
        }

        public DataTable listaRegistro_Param(ref Ent_ttccol300 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.user);
            
            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }
                return consulta;
            }
            catch (Exception ex)
            {
                //Comentado CChaverra 11052017
                strError = "Error when querying data [ttccol300]. Try again or contact your administrator";
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                throw ex;
            }
        }

        private List<Ent_ParametrosDAL> AdicionaParametrosComunes(Ent_ttccol300 parametros, bool blnUsarPRetorno = false)
        {
            method = MethodBase.GetCurrentMethod();
            string strError = string.Empty;
            List<Ent_ParametrosDAL> parameterCollection = new List<Ent_ParametrosDAL>();
            try
            {

                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$USER", DbType.String, parametros.user);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$NAMA", DbType.String, parametros.nama);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$PASS", DbType.String, parametros.pass);
                //Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$UFIN", DbType.String, parametros.ufin);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTD", DbType.Int32, parametros.refcntd);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$REFCNTU", DbType.Int32, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$ROLE", DbType.Int32, parametros.refcntu);
                Ent_ParametrosDAL.AgregaParametro(ref parameterCollection, ":T$SHIF", DbType.Int32, parametros.refcntu);

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
                strError = "Error when creating parameters [301]. Try again or contact your administrator \n";
                log.escribirError(strError + Console.Out.NewLine + ex.Message, stackTrace.GetFrame(1).GetMethod().Name, method.Name, method.ReflectedType.Name);
            }
            return parameterCollection;
        }

        public bool updateUFIN(ref string user, ref string pss, ref string strError) 
        {
            method = MethodBase.GetCurrentMethod();
            bool retorno = false;

            try
            {
                paramList = new Dictionary<string, object>();
                paramList.Add(":T$USER", user.Trim());
                paramList.Add(":T$PASS", pss.Trim());

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


        public void RecordarContraseña(Ent_ttccol300 ParametrosIn, ref string strError)
        {
            method = MethodBase.GetCurrentMethod();

            paramList = new Dictionary<string, object>();
            paramList.Add("p1", ParametrosIn.user);

            strSentencia = recursos.readStatement(method.ReflectedType.Name, method.Name, ref owner, ref env, tabla, paramList);

            try
            {
                consulta = DAL.BaseDAL.BaseDal.EjecutarCons("Text", strSentencia, ref parametersOut, null, true);
                if (consulta.Rows.Count < 1) { strError = "-1"; }

                Ent_ttccol300 obj = new Ent_ttccol300();
                SendMailService.SendMailService SendMail = IntanciaServicionSendMail();

                foreach (DataRow item in consulta.Rows)
                {
                    obj.pass = item["pswd"].ToString();
                    obj.nama = item["Nombre"].ToString();
                    obj.user = item["userN"].ToString();
                    obj.ufin = item["UFIN"].ToString();
                    obj.role = Convert.ToInt32(item["role"]);
                    obj.shif = item["shif"].ToString();
                }

                SendMailService.Models.Email Email= new SendMailService.Models.Email();
                Email.Body = "Name: "+ obj.nama +"User: "+ obj.user +"Password: " + obj.pass;
                List<SendMailService.Models.EmailAccount> LstTo = new List<SendMailService.Models.EmailAccount>();
                LstTo.Add(new SendMailService.Models.EmailAccount() { Account = "ritesh.gawande@grupophoenix.com" });
                Email.To = LstTo;
                Email.Subject = "Password recovery : "+obj.user;

                SendMail.SendEmails(Email);
                
                //SendMail.SendEmails();
            }
            catch (Exception ex)
            {
                //Comentado CChaverra 11052017
                strError = "Error when querying data [ttccol300]. Try again or contact your administrator";
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ")" :
                    ex.Message;
                throw ex;
            }
        }

        public void configuracionEmail()
        {

        }

        public SendMailService.SendMailService IntanciaServicionSendMail()
        {
            return new SendMailService.SendMailService();
        }

    }
}
